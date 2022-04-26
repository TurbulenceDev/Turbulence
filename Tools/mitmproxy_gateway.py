# this script is used with mitmproxy to read out gateway websocket messages and decompress them
#  so we can check what the web client sends through the gateway
from mitmproxy import ctx
import zlib, json
# run with mitmproxy -s <filename>.py
# also need to trust the ca cert from mitm.it

class Gateway:
    def __init__(self):
        self.ZLIB_SUFFIX = b'\x00\x00\xff\xff'
        self.buffer = bytearray()
        self.inflator = zlib.decompressobj()        

    def websocket_message(self, flow):
        # only check discord gateway msgs 
        if not "gateway.discord.gg" in flow.request.host:
            return
        
        #TODO: as we listen to all requests there is a possibility of having two seperate gateway ws, so we should probably assign a inflator for each ws
        message = flow.websocket.messages[-1]
        msg = message.content

        # was the message sent from the client or server?
        if message.from_client:
            # parse as json
            msg = json.loads(msg)
            # print stuff about msgs
            if msg["op"] == 0: # t only != null, if op == 0
                ctx.log.info(f"Client -> Server: op: 0, t: {msg['t']}")
            else:
                ctx.log.info(f"Client -> Server: op: {msg['op']}")
            ctx.log.info(msg["d"])
        else:
            # zlib decompress (inflate) the msg
            self.buffer.extend(msg)
            if len(msg) < 4 or msg[-4:] != self.ZLIB_SUFFIX:
                ctx.log.info(f"invalid msg: {msg!r}")
                return

            msg = self.inflator.decompress(self.buffer)
            self.buffer = bytearray()
            # parse as json
            msg = json.loads(msg)
            # print info about msg
            ctx.log.info(f"Server -> Client: op: {msg['op']}, t: {msg['t']}")
            # ignore the long msgs we dont want anyway for now
            if msg["t"] != "READY" \
                and msg["t"] != "READY_SUPPLEMENTAL":
                ctx.log.info(msg["d"])
            #TODO: log this into a nice view. too lazy for that

addons = [
    Gateway()
]