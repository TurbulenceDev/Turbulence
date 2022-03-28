from asyncio import subprocess
from argparse import ArgumentParser
import os
import subprocess

# usage: point to a folder with the markdown docs (like https://github.com/discord/discord-api-docs/tree/main/docs/resources)
if __name__ == "__main__":
    argsparser = ArgumentParser()
    argsparser.add_argument("infolder", type=str)
    argsparser.add_argument("outfolder", type=str, nargs='?', default=None)
    
    args = argsparser.parse_args()
    files = os.listdir(args.infolder)

    outFolder = args.infolder
    if args.outfolder is not None:
        outFolder = args.outfolder
    
    for file in files:
        path = os.path.join(args.infolder, file)
        if os.path.isfile(path):
            if file[-3:] == ".md": # ends with .md
                out = os.path.join(outFolder, file[:-3] + ".cs")
                subprocess.run(["python", "parse_discord_datastructures_mdfile.py", path, out])