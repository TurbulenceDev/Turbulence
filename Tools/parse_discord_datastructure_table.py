from lxml import etree
from typing import List
from argparse import ArgumentParser


def snake2camel(text: str) -> str:
    return ''.join([x.capitalize() for x in text.split('_')])


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

        return f"""    /// <summary>
    /// {self.description.capitalize()}
    /// </summary>
    [JsonProperty("{self.name}"{required_str})]
    public {self.fieldType}{'?' if self.nullable else ''} {snake2camel(self.name)} {{ get; internal set; }}{' = null!;' if not self.nullable and self.fieldType == "string" else ''}

"""


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

    return text


def parse_structure(sourcefile: str, xpath: str) -> List[DataStructureField]:
    with open(sourcefile, "r") as fp:
        page_content = fp.read()
        tree = etree.HTML(page_content)
        table_node = tree.xpath(xpath)
        if len(table_node) != 1:
            print(f"XPATH search resulted in {len(table_node)} results")
            exit(-1)

        table_node = table_node[0]

        if table_node.tag != "table" or len(table_node) != 2 or table_node[1].tag != "tbody":
            print(f"XPATH search resulted in element that isnt a table in the expected format: {table_node}")
            exit(-1)

        field_list = list()
        for row in table_node[1]:
            field_type = recursive_find_text(row[1])
            nullable = False
            optional = False
            field_name = recursive_find_text(row[0])
            if field_name == "video_quality_mode?":
                print("")
            if field_name.endswith("*"):
                while field_name.endswith("*"):
                    field_name = field_name[:-1]
                field_name = field_name[:-1]
            if field_type.startswith("?"):
                nullable = True
                field_type = field_type[1:]

            if field_name.endswith("?"):
                optional = True
                field_name = field_name[:-1]

            field_list.append(DataStructureField(field_name, parse_type(field_type), nullable, optional, recursive_find_text(row[2])))

        return field_list


if __name__ == "__main__":
    argsparser = ArgumentParser()
    argsparser.add_argument("sourcefile", type=str)
    argsparser.add_argument("xpath", type=str)
    argsparser.add_argument("file", type=str, default=None, nargs='?')
    
    args = argsparser.parse_args()
    fields = [x.toString() for x in parse_structure(args.sourcefile, args.xpath)]
    
    if args.file is not None:
        with open(args.file, "w") as f:
            f.writelines(fields)
    else:
        for field in fields:
            print(field)