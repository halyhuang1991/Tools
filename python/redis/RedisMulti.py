import redis
from redis.sentinel import Sentinel
sentinel = Sentinel([('localhost', 6379),
                     ('localhost', 6378),
                     ('localhost', 6345)
		     ],socket_timeout=0.5)
master = sentinel.master_for('mymaster', socket_timeout=0.1)
slave = sentinel.slave_for('mymaster', socket_timeout=0.1)
master.set('foo', 'bar')
msg=slave.get('foo')
print(msg)
