
import OraDB
sql='select * from dual;'
listResult = OraDB.QueryOra(sql)
print(listResult)

db=OraDB.BaseDB('halyhuang',OraDB.gStrConnection)
listResult =db.Query(sql)
print(listResult)
