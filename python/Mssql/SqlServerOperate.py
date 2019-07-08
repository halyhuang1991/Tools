import pymssql
 
db_host = 'XPHP0004\HALY'
db_port = '1433'
db_user = 'haly'
db_pwd = 'admin'
db_name = 'test'
tb_name = 'test'
 
 
class SqlServerOperate(object):
 
    def __init__(self, server, port, user, password, db_name, as_dict=True):
        self.server = server
        self.port = port
        self.user = user
        self.password = password
        self.db_name = db_name
        self.conn = self.get_connect(as_dict=as_dict)
        pass
 
    def __del__(self):
        self.conn.close()
 
    def get_connect(self, as_dict=True):
        conn = pymssql.connect(
            server=self.server,
            port=self.port,
            user=self.user,
            password=self.password,
            database=self.db_name,
            as_dict=as_dict,
            charset="utf8"
        )
        return conn
 
    def exec_query(self, sql):
        cur = self.conn.cursor()
        cur.execute(sql)
        result_list = list(cur.fetchall())
        cur.close()
 
        # 使用with语句（上下文管理器）来省去显式的调用close方法关闭连接和游标
        # print('****************使用 with 语句******************')
        # with self.get_connect() as cur:
        #     cur.execute(sql)
        #     result_list = list(cur.fetchall())   # 把游标执行后的结果转换成 list
        #     # print(result_list)
 
        return result_list
 
    def exec_non_query(self, sql, params=None):
        cur = self.conn.cursor()
        # cur.execute(sql, params=params)
        cur.execute(sql, params=params)
        self.conn.commit()
        cur.close()
 
    def exec_mutil_sql(self, sql, data_list):
        """
           执行一次 sql, 批量插入多条数据
        :param sql: 参数用 %s 代替 : insert into table_name(col1, col2, col3) values(%s, %s, %s)
        :param data_list:  list类型, list中每个元素都是元组
        :return:
        """
        cur = self.conn.cursor()
        cur.executemany(sql, data_list)
        self.conn.commit()
        cur.close()
 
 
def test():
    ms = SqlServerOperate(db_host, db_port, db_user, db_pwd, db_name)
    sql_string = "select * from score"
    temp_result_list = ms.exec_query(sql_string)
    for i in temp_result_list:
        print(i)
    pass
 
 
if __name__ == "__main__":
    test()
    pass