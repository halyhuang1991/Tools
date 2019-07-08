import pymssql

# conn=pymssql.connect(host='127.0.0.1',user='haly',password='admin',database='test')

conn=pymssql.connect(host='.',database='test')

#cur=conn.cursor()
cur = conn.cursor(as_dict=True)
cur.execute('select top 5 * from [dbo].[score]')
#如果update/delete/insert记得要conn.commit()
#否则数据库事务无法提交
#print (cur.fetchall())
for row in cur:
    print("ID=%d, Name=%s,math=%s" % (row['id'], row['name'], row['math']))

cur.close()

conn.close()
