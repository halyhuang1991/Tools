import re
import os
import sys
path=sys.path[0]+"\\produre.txt" #os.getcwd()
try:
    exceptArr=["operation_del_dts","FROM"]
    f = open(path, 'r')
    text=f.read().upper().replace("("," ")
    if text.strip()=="":
        raise Exception("file is empty! ")
        #exit(1)
    arr=[]
    arr1=re.findall(r"insert into (.*?) ".upper(),text)
    arr=arr+arr1
    arr1=re.findall(r"update (.*?) ".upper(),text)
    arr=arr+arr1
    arr1=re.findall(r"delete from (.*?) ".upper(),text)
    arr=arr+arr1
    arr1=re.findall(r"delete (.*?) ".upper(),text)
    arr=arr+arr1
    arr=list(set(arr))
    for table in exceptArr:
        if table.upper() in arr:
            arr.remove(table.upper())
    for table in arr:
        if table.strip().startswith("TMP_"):
            arr.remove(table)
    print(','.join(arr))
    print('ok')
except Exception as e:
    print(e)
finally:
    if f:
        f.close()
    
