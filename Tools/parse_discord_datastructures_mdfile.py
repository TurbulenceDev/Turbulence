from lxml import etree
from typing import List
from argparse import ArgumentParser
import mistune


def snake2camel(text: str) -> str:
    return ''.join([x.capitalize() for x in text.split('_')])

def name2camel(text: str) -> str:
    return ''.join([x.capitalize() for x in text.split(' ')])

def find_tables(tree):
    tables = tree.findall(".//table")
    # return the table + the table name (which we just get from the header before, no guarantee that it is indeed a header)
    return [(table,table.getprevious().text) for table in tables]

class DataStructureClass:
    def __init__(self, name: str):
        self.name = name
        self.fields = []

    def addField(self, field):
        self.fields.append(field)

    def toString(self) -> str:
        newline = "\n"
        return f"""public class {self.name}
{{
{newline.join([x.toString() for x in self.fields])}
}}

"""

class DataStructureField:
    def __init__(self, name: str, fieldType: str, nullable: bool, optional: bool, description: str):
        self.name = name
        self.fieldType = fieldType
        self.nullable = nullable
        self.optional = optional
        self.description = description

    def toString(self) -> str:
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
        return f"""    /// <summary>
    /// {self.description.capitalize()}
    /// </summary>
    [JsonProperty("{self.name}"{required_str})]
    public {self.fieldType}{'?' if self.nullable else ''} {snake2camel(self.name)} {{ get; internal set; }}{' = null!;' if not self.nullable and self.fieldType == "string" else ''}
"""

class DataStructureEnum:
    def __init__(self, name: str):
        self.name = name
        self.values = []

    def addEnum(self, enum):
        self.values.append(enum)

    def toString(self) -> str: #TODO: add type?
        newline = "\n"
        return f"""enum {self.name}
{{
{newline.join([x.toString() for x in self.values])}
}}

"""

class DataStructureEnumValue:
    def __init__(self, name: str, value: str, desc: str):
        self.name = name
        self.value = value
        self.desc = desc

    def toString(self) -> str:
        return f"    {self.name} = {self.value}, // {self.desc}"


def recursive_find_text(obj) -> str:
    txt = ''.join([recursive_find_text(x) for x in obj])
    if obj.text is not None:
        txt = obj.text+txt
    if obj.tail is not None:
        txt = txt+obj.tail
    return txt


def parse_type(text: str) -> str:
    if text == "snowflake":
        return "ulong"

    if text == "integer":
        return "int"

    if text == "boolean":
        return "bool"

    if "array of " == text[:9]:
        return f"{text[9:]}[]"

    return text

def parse_structure(table_node, name) -> DataStructureClass:
    if table_node.tag != "table" or len(table_node) != 2 or table_node[1].tag != "tbody":
        print(f"search resulted in element that isnt a table in the expected format: {table_node}")
        return None
    
    # TODO: handle enums, fields and such by looking at the header
    head = table_node[0]
    headers = [node.text for node in head[0].getchildren()]
    if all(item in headers for item in ['Field', 'Type', 'Description']): # class # TODO: figure out when this is a request
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

            field_name = field_name.rstrip("*")
            if field_type.startswith("?"):
                nullable = True
                field_type = field_type[1:]
            
            if field_name.endswith("?"):
                optional = True
                field_name = field_name[:-1]
            
            clss.addField(DataStructureField(field_name.strip(), parse_type(field_type.strip()), nullable, optional, desc.strip()))
        return clss
    elif all(item in headers for item in ['Type', 'Value']): # enum
        enum = DataStructureEnum(name2camel(name))
        for row in table_node[1]: 
            field_name = recursive_find_text(row[0]).strip()
            field_val = recursive_find_text(row[1]).strip()
            
            desc = ""
            if "Description" in headers: # not every enum has descriptions
                desc = recursive_find_text(row[2])
            enum.addEnum(DataStructureEnumValue(field_name.strip(), field_val.strip(), desc.strip()))
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
            fields.append(field.toString())
        else:
            print(f"failed parsing {table}")
            print(etree.tostring(table))
    
    if args.file is not None:
        with open(args.file, "w") as f:
            f.writelines(fields)
    else:
        for field in fields:
            print(field)