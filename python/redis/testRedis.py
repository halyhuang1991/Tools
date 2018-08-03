import redis

pool= redis.ConnectionPool(host='localhost',port=6379,decode_responses=True)

r=redis.Redis(connection_pool=pool)
r2=redis.Redis(connection_pool=pool)
r.set('apple','a')
print(r.get('apple'))
r2.set('banana','b')
print(r.get('banana'))

r.hset('info','name','lilei')
r.hset('info','age','18')
print(r.hgetall('info'))
r.sadd('course','math','english','chinese')
print(r.smembers('course'))

print(r.client_list())
print(r2.client_list())#可以看出两个连接的id是一致的，说明是一个客户端连接

import redis.exceptions
pipe=r.pipeline()
try:
    # pipe.watch('a')
    pipe.multi()
    pipe.set('here', 'there')
    pipe.set('here1', 'there1')
    pipe.set('here2', 'there2')
    pipe.execute()

except redis.exceptions.WatchError as e:
    print("Error")
