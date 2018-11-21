import cx_Oracle as oracle
 
# connect oracle database
db = oracle.connect('')
 
# create cursor
cursor = db.cursor()
 
# execute sql
cursor.execute('select sysdate from dual')
 
# fetch data
data = cursor.fetchone()
 
print('Database time:%s' % data)
 
# close cursor and oracle
cursor.close()
db.close()