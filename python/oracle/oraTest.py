
import OraDB
import sys
from wrapper import runtime
sql='select * from dual;'
@runtime.runtime
def log():
    print("test")
log()
if OraDB.gStrConnection=='':
    print('connectionString is empty!')
    sys.exit(0)


listResult = OraDB.QueryOra(sql)
print(listResult)

db=OraDB.BaseDB('halyhuang',OraDB.gStrConnection)
listResult =db.Query(sql)
print(listResult)

