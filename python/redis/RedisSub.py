
import redis
import time
r=redis.Redis(host="localhost",port=6379,decode_responses=True)
p = r.pubsub(ignore_subscribe_messages=True)
p.subscribe('my-channel')
print(p.parse_response())
time.sleep(10)
r.publish('my-channel','ok')
print(p.parse_response())