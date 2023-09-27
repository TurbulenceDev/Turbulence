# this script is used with mitmproxy to read out gateway websocket messages and decompress them
#  so we can check what the web client sends through the gateway
from mitmproxy import ctx
import zlib, json
# run with mitmproxy -s <filename>.py
# also need to trust the ca cert from mitm.it

class Gateway:
    def __init__(self):
        self.ZLIB_SUFFIX = b'\x00\x00\xff\xff'
        self.buffers = {}
        self.inflators = {}

    def websocket_message(self, flow):
        # only check discord gateway msgs 
        if not "gateway.discord.gg" in flow.request.host:
            return
        
        message = flow.websocket.messages[-1]
        id = flow.id
        msg = message.content

        # was the message sent from the client or server?
        if message.from_client:
            # parse as json
            msg = json.loads(msg)
            # print stuff about msgs
            if msg["op"] == 0: # t only != null, if op == 0
                ctx.log.info(f"[{id}] Client -> Server: op: 0, t: {msg['t']}")
            else:
                ctx.log.info(f"[{id}] Client -> Server: op: {msg['op']}")
            ctx.log.info(msg["d"])
        else:
            # init buffer+inflator for this websocket
            if not id in self.buffers:
                self.buffers[id] = bytearray()
                self.inflators[id] = zlib.decompressobj()
            # zlib decompress (inflate) the msg
            self.buffers[id].extend(msg)
            if len(msg) < 4 or msg[-4:] != self.ZLIB_SUFFIX:
                ctx.log.info(f"[{id}] invalid msg: {msg!r}")
                return

            msg = self.inflators[id].decompress(self.buffers[id])
            self.buffers[id] = bytearray()
            # parse as json
            msg = json.loads(msg)
            # print info about msg
            ctx.log.info(f"[{id}] Server -> Client: op: {msg['op']}, t: {msg['t']}")
            # ignore the long msgs we dont want anyway for now
            if msg["t"] != "READY" \
                and msg["t"] != "READY_SUPPLEMENTAL":
                ctx.log.info(msg["d"])
            #TODO: log this into a nice view. too lazy for that

addons = [
    Gateway()
]