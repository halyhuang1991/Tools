import redis
r=redis.Redis(host="localhost",port=6379,decode_responses=True)

#发布使用publish(self, channel, message):Publish ``message`` on ``channel``.
Flag=True
while Flag:
    msg='发布的信息'
    if len(msg)==0:
        continue
    elif msg=='quit':
        break
    else:
        r.publish('cctv0',msg)