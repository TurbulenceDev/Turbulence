from lxml import etree
from argparse import ArgumentParser
import mistune
import re
from typing import Match


def snake2camel(text: str) -> str:
    return ''.join([x.capitalize() for x in text.split('_')]).replace("?", "")


def name2camel(text: str) -> str:
    return ''.join([x.capitalize() for x in text.split(' ')])


# Overwrite some names to make them more human readable
def pretty(name: str) -> str:
    match name:
        case "Op":
            return "Opcode"
        case "D":
            return "Data"
        case "S":
            return "Sequence"
        case "T":
            return "Name"
        case _:
            return name

def find_tables(tree):
    tables = tree.findall(".//table")
    # return the table + the table name (which we just get from the header before, no guarantee that it is indeed a header)
    return [(table, table.getprevious().text) for table in tables]


class DataStructureClass:
    def __init__(self, name: str):
        self.name = name
        self.fields = []

    def add_field(self, field):
        self.fields.append(field)

    def to_string(self) -> str:
        newline = "\n\n"
        return f"""\
public class {self.name[:-9] if self.name.endswith("Structure") else self.name}  # Remove "Structure" suffix
{{
{newline.join([x.to_string() for x in self.fields])}
}}

"""


class DataStructureField:
    def __init__(self, name: str, field_type: str, nullable: bool, optional: bool, description: str):
        self.name = name
        self.field_type = field_type
        self.nullable = nullable
        self.optional = optional
        self.description = description

    def to_string(self) -> str:
        required_str = ""
        if self.optional:
            if not self.nullable:
                required_str = ", Required = Required.DisallowNull"
        else:
            if self.nullable:
                required_str = ", Required = Required.AllowNull"
            else:
                required_str = ", Required = Required.Always"

        # TODO: adjust "JsonProperty" to the used json parsing lib
        return f"""\
    /// <summary>
    /// {self.description.capitalize()}.
    /// </summary>
    [JsonProperty("{self.name.replace('?', '')}"{required_str})]
    public {self.field_type}{'?' if self.nullable else ''} {pretty(snake2camel(self.name))} {{ get; set; }}{' = null!;' if not self.nullable and self.field_type == "string" else ''}\
"""


class DataStructureEnum:
    def __init__(self, name: str):
        self.name = name
        self.values = []

    def add_enum(self, enum):
        self.values.append(enum)

    def to_string(self) -> str:  # TODO: add type?
        newline = "\n"
        return f"""\
enum {self.name}
{{
{newline.join([x.to_string() for x in self.values])}
}}
"""


class DataStructureEnumValue:
    def __init__(self, name: str, value: str, desc: str):
        self.name = name
        self.value = value
        self.desc = desc

    def to_string(self) -> str:
        return f"    {self.name} = {self.value}, // {self.desc}"


def recursive_find_text(obj) -> str:
    txt = ''.join([recursive_find_text(x) for x in obj])
    if obj.text is not None:
        txt = obj.text+txt
    if obj.tail is not None:
        txt = txt+obj.tail
    return txt


def parse_type(text: str) -> str:
    if text == "string":
        return text

    if text == "object" or text == "mixed (any JSON value)":
        return "object"

    if text == "snowflake":
        return "ulong"

    if text == "integer":
        return "int"

    if text == "boolean":
        return "bool"

    if text == "boolean":
        return "bool"

    if text == "ISO8601 timestamp":  # TODO: Fix?
        return "string"

    if text == "array":  # TODO: Fix?
        return "object"

    # These upper rules should be applied below too
    # Currently "array of snowflakes" turns into "Snowflake[]"
    # but it should turn into "ulong[]"

    # array of two integers (shard_id, num_shards)
    array_of_two_integers_x: Match[str] = re.match("array of two integers .*", text)
    if array_of_two_integers_x:
        return f"object"  # TODO: Fix

    # array of user objects
    array_of_x_objects: Match[str] = re.match("array of (.*) objects", text)
    if array_of_x_objects and array_of_x_objects.group(1):
        return f"{array_of_x_objects.group(1).title().replace(' ', '')}[]"

    # array of users
    array_of_xs: Match[str] = re.match("array of (.*)s", text)
    if array_of_xs and array_of_xs.group(1):
        return f"{array_of_xs.group(1).title().replace(' ', '')}[]"

    # a user object
    a_x_object: Match[str] = re.match("a (.*) object", text)
    if a_x_object and a_x_object.group(1):
        return f"{a_x_object.group(1).title().replace(' ', '')}"

    # user object
    x_object: Match[str] = re.match("(.*) object", text)
    if x_object and x_object.group(1):
        return f"{x_object.group(1).replace('_', ' ').title().replace(' ', '')}"

    print(f'Could not match: {text}')

    return text


def parse_structure(table_node, name) -> DataStructureClass | DataStructureEnum | None:
    if table_node.tag != "table" or len(table_node) != 2 or table_node[1].tag != "tbody":
        print(f"Search resulted in element that isn't a table in the expected format: {table_node}")
        return None

    # TODO: handle enums, fields and such by looking at the header
    head = table_node[0]
    headers = [node.text for node in head[0].getchildren()]
    if all(item in headers for item in ['Field', 'Type', 'Description']):  # class # TODO: figure out when this is a request
        clss = DataStructureClass(name2camel(name))
        for row in table_node[1]:
            field_type = recursive_find_text(row[1])
            nullable = False
            optional = False
            field_name = recursive_find_text(row[0])
            desc = recursive_find_text(row[2])
            # filtering shit
            if field_name == "video_quality_mode?":
                print("")

            field_name = field_name.rstrip(" *\n")
            field_type = field_type.rstrip(" *\n")

            if "*" in field_type:
                pass

            if field_type.startswith("?"):
                nullable = True
                field_type = field_type[1:]

            if field_name.endswith("?"):
                optional = True
                field_name = field_name[:-1]

            clss.add_field(DataStructureField(field_name.strip(), parse_type(field_type.strip()), nullable, optional, desc.strip()))
        return clss
    elif all(item in headers for item in ['Type', 'Value']):  # enum
        enum = DataStructureEnum(name2camel(name))
        for row in table_node[1]:
            field_name = recursive_find_text(row[0]).strip()
            field_val = recursive_find_text(row[1]).strip()

            desc = ""
            if "Description" in headers:  # not every enum has descriptions
                desc = recursive_find_text(row[2])
            enum.add_enum(DataStructureEnumValue(field_name.strip(), field_val.strip(), desc.strip()))
        return enum
    else:
        print(f"Unrecognized headers: {headers}")
        return None


if __name__ == "__main__":
    argsparser = ArgumentParser()
    argsparser.add_argument("sourcefile", type=str)#, default="Sticker.md", nargs="?")
    argsparser.add_argument("file", type=str, nargs='?', default=None)#"text.cs")

    args = argsparser.parse_args()
    with open(args.sourcefile, "r") as fp:
        content = fp.read()
    # from md to html
    html = mistune.html(content)
    # then to soup
    tree = etree.HTML(html)

    fields = []
    for (table, name) in find_tables(tree):
        field = parse_structure(table, name)
        if field:
            fields.append(field.to_string())
        else:
            print(f"failed parsing {table}")
            print(etree.tostring(table))

    if args.file is not None:
        with open(args.file, "w") as f:
            f.writelines(fields)
    else:
        for field in fields:
            print(field)
