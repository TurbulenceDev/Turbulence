Communication to join calls:
https://discord.com/developers/docs/topics/voice-connections#connecting-to-voice
# private calls
## creating call
### action: user calls another user

TODO: client also asks for all voice regions here? but why? it doesnt seem like the client needs to send the preferred voice region

### c: VOICE_STATE_UPDATE => i joined a call with this user
channel id is the dm channel (sent in the ready data)
```
info: Client -> Server: op: 4
info: {'guild_id': None, 'channel_id': '963085128693862430', 'self_mute': False, 'self_deaf': False, 'self_video':
False}
```

### s: "okay, we pinged the other person for you"
```
info: Server -> Client: op: 0, t: MESSAGE_CREATE
info: {'type': 3, 'tts': False, 'timestamp': '2022-04-20T18:36:07.383000+00:00', 'pinned': False, 'mentions':
[{'username': 'Exp', 'public_flags': 0, 'id': '138397087229280257', 'discriminator': '7856', 'avatar_decoration': None,
'avatar': '7062212282927ef52c1bac8cb3e33e34'}], 'mention_roles': [], 'mention_everyone': False, 'id':
'966406931252150292', 'flags': 0, 'embeds': [], 'edited_timestamp': None, 'content': '', 'components': [], 'channel_id':'963085128693862430', 'call': {'participants': ['959467381011415120'], 'ended_timestamp': None}, 'author': {'username':
'turbulence', 'public_flags': 0, 'id': '959467381011415120', 'discriminator': '0485', 'avatar_decoration': None,
'avatar': None}, 'attachments': []}
```

### s: creates call
```
info: Server -> Client: op: 0, t: CALL_CREATE
info: {'voice_states': [], 'ringing': [], 'region': 'rotterdam', 'message_id': '966406931252150292', 'channel_id':
'963085128693862430'}
```

### s: "you are now in this channel"
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'user_id': '959467381011415120', 'suppress': False, 'session_id': '826966321946bf3dc0c12bca64bbdbdd',
'self_video': False, 'self_mute': False, 'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False,
'guild_id': None, 'deaf': False, 'channel_id': '963085128693862430'}
```


### s: "join this server"
```
info: Server -> Client: op: 0, t: VOICE_SERVER_UPDATE
info: {'token': '04cfd23059d5dfcb', 'guild_id': None, 'endpoint': 'rotterdam5347.discord.media:443', 'channel_id':
'963085128693862430'}
```

### connection: joins wss
```
info: 127.0.0.1:55897: server connect rotterdam5347.discord.media:443 (162.159.130.235:443)
```

## inside a call
see the voice gateway section

## leaving a call
### action: user leaves call
### connection: leaves wss
```
info: 127.0.0.1:55897: server disconnect rotterdam5347.discord.media:443 (162.159.130.235:443)
```

### voice_state_update: "hey i left the call"
```
info: Client -> Server: op: 4
info: {'guild_id': None, 'channel_id': None, 'self_mute': False, 'self_deaf': False, 'self_video': False}
```

### s: "yes you left the call"
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'user_id': '959467381011415120', 'suppress': False, 'session_id': '826966321946bf3dc0c12bca64bbdbdd',
'self_video': False, 'self_mute': False, 'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False,
'guild_id': None, 'deaf': False, 'channel_id': None}
```

### s: "call ended now"
```
info: Server -> Client: op: 0, t: MESSAGE_UPDATE
info: {'id': '966406931252150292', 'channel_id': '963085128693862430', 'call': {'participants': ['959467381011415120'],
'ended_timestamp': '2022-04-20T18:41:07.771848+00:00'}}
```

### s: call gone
```
info: Server -> Client: op: 0, t: CALL_DELETE
info: {'channel_id': '963085128693862430'}
```

### s: TODO: message_ack
```
info: Server -> Client: op: 0, t: MESSAGE_ACK
info: {'version': 37, 'message_id': '966408190558208000', 'channel_id': '963085128693862430'}
```

## joining a call
### s: someone pinged you with a call msg
```
info: Server -> Client: op: 0, t: MESSAGE_CREATE
info: {'type': 3, 'tts': False, 'timestamp': '2022-04-20T18:49:57.813000+00:00', 'pinned': False, 'mentions':
[{'username': 'turbulence', 'public_flags': 0, 'id': '959467381011415120', 'discriminator': '0485', 'avatar_decoration':None, 'avatar': None}], 'mention_roles': [], 'mention_everyone': False, 'id': '966410414328016986', 'flags': 0,
'embeds': [], 'edited_timestamp': None, 'content': '', 'components': [], 'channel_id': '963085128693862430', 'call':
{'participants': ['138397087229280257'], 'ended_timestamp': None}, 'author': {'username': 'Exp', 'public_flags': 0,
'id': '138397087229280257', 'discriminator': '7856', 'avatar_decoration': None, 'avatar':
'7062212282927ef52c1bac8cb3e33e34'}, 'attachments': []}
```

### s: someone created a call related to that msg
```
info: Server -> Client: op: 0, t: CALL_CREATE
info: {'voice_states': [], 'ringing': [], 'region': 'rotterdam', 'message_id': '966410414328016986', 'channel_id':
'963085128693862430'}
```

### s: that user is now in that call
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'user_id': '138397087229280257', 'suppress': False, 'session_id': '2a77d1ac77c7383332dde4c9c7384267',
'self_video': False, 'self_mute': False, 'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False,
'guild_id': None, 'deaf': False, 'channel_id': '963085128693862430'}
```

### action: user joins the call

### c: i joined that call
```
info: Client -> Server: op: 4
info: {'guild_id': None, 'channel_id': '963085128693862430', 'self_mute': False, 'self_deaf': False, 'self_video':
False}
```

### s: look there are more people in that call
```
info: Server -> Client: op: 0, t: MESSAGE_UPDATE
info: {'id': '966410414328016986', 'channel_id': '963085128693862430', 'call': {'participants': ['959467381011415120',
'138397087229280257'], 'ended_timestamp': None}}
```

### s: also you are in that call
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'user_id': '959467381011415120', 'suppress': False, 'session_id': '6c6e28b3ced70ebd1616620e2c69b086',
'self_video': False, 'self_mute': False, 'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False,
'guild_id': None, 'deaf': False, 'channel_id': '963085128693862430'}
```

### s: TODO: message ack
```
info: Server -> Client: op: 0, t: MESSAGE_ACK
info: {'version': 41, 'message_id': '966410414328016986', 'channel_id': '963085128693862430'}
```

### s: join this server
```
info: Server -> Client: op: 0, t: VOICE_SERVER_UPDATE
info: {'token': '9afbdd391f4a2de7', 'guild_id': None, 'endpoint': 'rotterdam7572.discord.media:443', 'channel_id':
'963085128693862430'}
```

### connection: client joins the wss
```
info: 127.0.0.1:55992: client connect
info: 127.0.0.1:55992: server connect rotterdam7572.discord.media:443 (162.159.128.235:443)
```

# voice channels
overall similiar to the private call thing (from the client side)
## joining channel
### action: user joins vc

### c: i joined that vc
```
info: Client -> Server: op: 4
info: {'guild_id': '934828556603764816', 'channel_id': '934828556603764821', 'self_mute': True, 'self_deaf': False,
'self_video': False}
```

### c: TODO: also give me updates (LAZY_REQUEST) about this channel?? its a vc my dude
```
info: Client -> Server: op: 14
info: {'guild_id': '934828556603764816', 'channels': {'934828556603764821': [[0, 99]], '934828556603764820': [[0, 99]]}}
```

### s: you joined that channel
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'member': {'user': {'username': 'turbulence', 'id': '959467381011415120', 'discriminator': '0485', 'avatar':
None}, 'roles': ['963747331558219786', '963753358118772786'], 'mute': False, 'joined_at':
'2022-04-01T15:03:07.723000+00:00', 'hoisted_role': None, 'flags': 0, 'deaf': False}, 'user_id': '959467381011415120',
'suppress': False, 'session_id': '8dc6698fb78a211003eed23498d35aaa', 'self_video': False, 'self_mute': True,
'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False, 'guild_id': '934828556603764816', 'deaf': False,
'channel_id': '934828556603764821'}
```

### s: join that servers
```
info: Server -> Client: op: 0, t: VOICE_SERVER_UPDATE
info: {'token': '273acb23b05ad3bc', 'guild_id': '934828556603764816', 'endpoint': 'rotterdam5129.discord.media:443'}
```

### connection: client joins wss
```
info: 127.0.0.1:56202: client connect
info: 127.0.0.1:56202: server connect rotterdam5129.discord.media:443 (162.159.130.235:443)
```

## leaving channel
### action: user leaves vc

### connection: client disconnects from wss
```
info: 127.0.0.1:56204: server disconnect rotterdam5129.discord.media:443 (162.159.130.235:443)
info: 127.0.0.1:56204: client disconnect
```

### c: i left that channel
```
info: Client -> Server: op: 4
info: {'guild_id': None, 'channel_id': None, 'self_mute': False, 'self_deaf': False, 'self_video': False}
```

### s: you left that channel
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'member': {'user': {'username': 'turbulence', 'id': '959467381011415120', 'discriminator': '0485', 'avatar':
None}, 'roles': ['963747331558219786', '963753358118772786'], 'mute': False, 'joined_at':
'2022-04-01T15:03:07.723000+00:00', 'hoisted_role': None, 'flags': 0, 'deaf': False}, 'user_id': '959467381011415120',
'suppress': False, 'session_id': '8dc6698fb78a211003eed23498d35aaa', 'self_video': False, 'self_mute': False,
'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False, 'guild_id': '934828556603764816', 'deaf': False,
'channel_id': None}
```

## changing channel
### action: user changes channel

### c: i changed channel to this one
```
info: Client -> Server: op: 4
info: {'guild_id': '934828556603764816', 'channel_id': '966416163309518878', 'self_mute': False, 'self_deaf': False,
'self_video': False}
```

### c: TODO: LAZY_REQUEST: update me also on this channel
```
info: Client -> Server: op: 14
info: {'guild_id': '934828556603764816', 'channels': {'966416163309518878': [[0, 99]], '934828556603764821': [[0, 99]],
'934828556603764820': [[0, 99]]}}
```

### s: you changed to this channel
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'member': {'user': {'username': 'turbulence', 'id': '959467381011415120', 'discriminator': '0485', 'avatar':
None}, 'roles': ['963747331558219786', '963753358118772786'], 'mute': False, 'joined_at':
'2022-04-01T15:03:07.723000+00:00', 'hoisted_role': None, 'flags': 0, 'deaf': False}, 'user_id': '959467381011415120',
'suppress': False, 'session_id': '8dc6698fb78a211003eed23498d35aaa', 'self_video': False, 'self_mute': False,
'self_deaf': False, 'request_to_speak_timestamp': None, 'mute': False, 'guild_id': '934828556603764816', 'deaf': False,
'channel_id': '966416163309518878'}
```

### connection: client disconnects from wss
```
info: 127.0.0.1:56218: client disconnect
info: 127.0.0.1:56218: server disconnect rotterdam3306.discord.media:443 (162.159.128.235:443)
```

### s: join this server
```
info: Server -> Client: op: 0, t: VOICE_SERVER_UPDATE
info: {'token': '8402e51425dc7f4f', 'guild_id': '934828556603764816', 'endpoint': 'rotterdam4266.discord.media:443'}
```

### connection: client connects to wss
```
info: 127.0.0.1:56223: client connect
info: 127.0.0.1:56223: server connect rotterdam4266.discord.media:443 (162.159.138.234:443)
```

## other user joins channel
### action: other user joins this vc

### s: this user joined your channel
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'member': {'user': {'username': 'Exp', 'id': '138397087229280257', 'discriminator': '7856', 'avatar':
'7062212282927ef52c1bac8cb3e33e34'}, 'roles': [], 'mute': False, 'joined_at': '2022-01-23T15:14:56.185000+00:00',
'hoisted_role': None, 'flags': 0, 'deaf': False}, 'user_id': '138397087229280257', 'suppress': False, 'session_id':
'517637282485f19cc030aa937fd7d098', 'self_video': False, 'self_mute': False, 'self_deaf': False,
'request_to_speak_timestamp': None, 'mute': False, 'guild_id': '934828556603764816', 'deaf': False, 'channel_id':
'966416163309518878'}
```

### c: TODO: LAZY_REQUEST: update me on that user pls
```
info: Client -> Server: op: 14
info: {'guild_id': '934828556603764816', 'members': ['138397087229280257']}
```

## other user leaves channel
### action: other user leaves vc

### s: user left this channel
```
info: Server -> Client: op: 0, t: VOICE_STATE_UPDATE
info: {'member': {'user': {'username': 'Exp', 'id': '138397087229280257', 'discriminator': '7856', 'avatar':
'7062212282927ef52c1bac8cb3e33e34'}, 'roles': [], 'mute': False, 'joined_at': '2022-01-23T15:14:56.185000+00:00',
'hoisted_role': None, 'flags': 0, 'deaf': False}, 'user_id': '138397087229280257', 'suppress': False, 'session_id':
'517637282485f19cc030aa937fd7d098', 'self_video': False, 'self_mute': False, 'self_deaf': False,
'request_to_speak_timestamp': None, 'mute': False, 'guild_id': '934828556603764816', 'deaf': False, 'channel_id': None}
```

# Voice Gateway:
req:
```
GET /?v=6 HTTP/1.1
Host: rotterdam3761.discord.media
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:99.0) Gecko/20100101 Firefox/99.0
Accept: */*
Accept-Language: de,en-US;q=0.7,en;q=0.3
Accept-Encoding: gzip, deflate
Sec-WebSocket-Version: 13
Origin: https://discord.com
Sec-WebSocket-Key: <key>
Connection: keep-alive, Upgrade
Sec-Fetch-Dest: websocket
Sec-Fetch-Mode: websocket
Sec-Fetch-Site: cross-site
Pragma: no-cache
Cache-Control: no-cache
Upgrade: websocket
```

not compressed

https://discord.com/developers/docs/topics/voice-connections (only v4 though, current seems to be v6)

## s: hello
```
{"op":8,"d":{"v":6,"heartbeat_interval":13750}}
```

## c: identify
token is a 16 char string you get from the VOICE_SERVER_UPDATE which also contains the wss link.
maybe we need that for auth?

session_id is gained from somewhere else. TODO: session_id from where? maybe VOICE_STATE_UPDATE?
```
{"op":0,"d":{"server_id":"934828556603764816","user_id":"959467381011415120","session_id":"bc2bdbdaebf0d131ae580b79e01197ab","token":"<token>","video":true,"streams":[{"type":"video","rid":"100","quality":100}]}}
```

## s: ready
contains ssrc (?), ip/port, supported modes

what is the type? i just joined the vc here, so no direct video call happened. maybe the highest supported type?

```
{"op":2,"d":{"streams":[{"type":"video","ssrc":713746,"rtx_ssrc":713747,"rid":"100","quality":100,"active":false}],"ssrc":713745,"port":50001,"modes":["aead_aes256_gcm_rtpsize","aead_aes256_gcm","xsalsa20_poly1305_lite_rtpsize","xsalsa20_poly1305_lite","xsalsa20_poly1305_suffix","xsalsa20_poly1305"],"ip":"213.163.95.84","experiments":["bwe_conservative_link_estimate","bwe_remote_locus_client","fixed_keyframe_interval"]}}
```

## c: TODO: op 16
```
{"op":16,"d":{}}
```

## s: TODO: op 16 resp
```
{"op":16,"d":{"voice":"0.8.34","rtc_worker":"0.3.19"}}
```

## c: TODO: 12 (info?)
```
{"op":12,"d":{"audio_ssrc":0,"video_ssrc":0,"rtx_ssrc":0,"streams":[{"type":"video","rid":"100","ssrc":0,"active":false,"quality":100,"rtx_ssrc":0,"max_bitrate":2500000,"max_framerate":20,"max_resolution":{"type":"fixed","width":1280,"height":720}}]}}
```

## connection: client establishes webrtc connection?

## c: SELECTED
https://discord.com/developers/docs/topics/voice-connections#establishing-a-voice-udp-connection

client tells server what to use?
```
{"op":1,"d":{"protocol":"webrtc","data":"a=ice-options:trickle\na=extmap:1 urn:ietf:params:rtp-hdrext:ssrc-audio-level\na=extmap:2/recvonly urn:ietf:params:rtp-hdrext:csrc-audio-level\na=extmap:3 urn:ietf:params:rtp-hdrext:sdes:mid\na=ice-pwd:9473594540563294f62301e5dc183472\na=ice-ufrag:925273d0\na=rtpmap:109 opus/48000/2\na=extmap:4 http://www.webrtc.org/experiments/rtp-hdrext/abs-send-time\na=extmap:5 urn:ietf:params:rtp-hdrext:toffset\na=extmap:6/recvonly http://www.webrtc.org/experiments/rtp-hdrext/playout-delay\na=extmap:7 http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01\na=rtpmap:120 VP8/90000\na=rtpmap:124 rtx/90000","sdp":"a=ice-options:trickle\na=extmap:1 urn:ietf:params:rtp-hdrext:ssrc-audio-level\na=extmap:2/recvonly urn:ietf:params:rtp-hdrext:csrc-audio-level\na=extmap:3 urn:ietf:params:rtp-hdrext:sdes:mid\na=ice-pwd:9473594540563294f62301e5dc183472\na=ice-ufrag:925273d0\na=rtpmap:109 opus/48000/2\na=extmap:4 http://www.webrtc.org/experiments/rtp-hdrext/abs-send-time\na=extmap:5 urn:ietf:params:rtp-hdrext:toffset\na=extmap:6/recvonly http://www.webrtc.org/experiments/rtp-hdrext/playout-delay\na=extmap:7 http://www.ietf.org/id/draft-holmer-rmcat-transport-wide-cc-extensions-01\na=rtpmap:120 VP8/90000\na=rtpmap:124 rtx/90000","codecs":[{"name":"opus","type":"audio","priority":1000,"payload_type":109,"rtx_payload_type":null},{"name":"H264","type":"video","priority":1000,"payload_type":126,"rtx_payload_type":127},{"name":"VP8","type":"video","priority":2000,"payload_type":120,"rtx_payload_type":124},{"name":"VP9","type":"video","priority":3000,"payload_type":121,"rtx_payload_type":125}],"rtc_connection_id":"e621fc80-9998-4ace-b100-34383822e063"}}
```

## s: TODO: 15, resp to 12?
```
{"op":15,"d":{"any":100}}
```

## s: Session Description
https://discord.com/developers/docs/topics/voice-connections#establishing-a-voice-udp-connection

TODO: that seems like a public key? read into h265 and opus to find out wtf this is
```
{"op":4,"d":{"video_codec":"H264","sdp":"m=audio 50001 ICE/SDP\na=fingerprint:sha-256 4A:79:94:16:44:3F:BD:05:41:5A:C7:20:F3:12:54:70:00:73:5D:33:00:2D:2C:80:9B:39:E1:9F:2D:A7:49:87\nc=IN IP4 213.163.95.84\na=rtcp:50001\na=ice-ufrag:tK/L\na=ice-pwd:g40S9IK0OQ8GOTCorkDZDR\na=fingerprint:sha-256 4A:79:94:16:44:3F:BD:05:41:5A:C7:20:F3:12:54:70:00:73:5D:33:00:2D:2C:80:9B:39:E1:9F:2D:A7:49:87\na=candidate:1 1 UDP 4261412862 213.163.95.84 50001 typ host\n","media_session_id":"8e60b8396805ee8d0985a0c57ef59461","audio_codec":"opus"}}
```

## c: heartbeat
https://discord.com/developers/docs/topics/voice-connections#heartbeating

data is a int nonce
```
{"op":3,"d":1650483735947}
```

## s: heartbeat ack
https://discord.com/developers/docs/topics/voice-connections#heartbeating
```
{"op":6,"d":1650483735947}
```

## c: speaking
https://discord.com/developers/docs/topics/voice-connections#speaking

need to send that before sending voice data?

i have no idea where the ssrc comes from. maybe this is related to the webrtc stuff
```
{"op":5,"d":{"speaking":1,"delay":5,"ssrc":1331704490}}
```

```
{"op":5,"d":{"speaking":0,"delay":5,"ssrc":1331704490}}
```