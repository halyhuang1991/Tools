import redis
r=redis.Redis(host="localhost",port=6379,decode_responses=True)

#发布使用publish(self, channel, message):Publish ``message`` on ``channel``.
Flag=True
chan=r.pubsub()#返回一个发布/订阅对象
msg_reciver=chan.subscribe('cctv0')#订阅

msg=chan.parse_response()#第一次会返回订阅确认信息
print(msg)
print("订阅成功，开始接收------")
while Flag:
    msg=chan.parse_response()#接收消息
    print(">>:",msg[2])#此处的信息格式['消息类型', '频道', '消息']，所以使用[2]来获取