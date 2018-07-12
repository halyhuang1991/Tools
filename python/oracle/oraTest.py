
import OraDB
import sys
sql='select * from dual;'

if OraDB.gStrConnection=='':
    print('connectionString is empty!')
    sys.exit(0)


listResult = OraDB.QueryOra(sql)
print(listResult)

db=OraDB.BaseDB('halyhuang',OraDB.gStrConnection)
listResult =db.Query(sql)
print(listResult)
