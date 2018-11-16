import re
import os
def is_contains(n):
    return n.find("=")>0
path=r"D:\C\github\Tools\python\regex\produre.txt"
f = open(path, 'r',encoding='UTF-8')
text=f.read()
arr=re.findall(r'[^()]+',text)
arr=list(set(arr))
arr=list(filter(is_contains,arr))
arr=arr[0].split(',')
pms=''
args=''
modelargs=''
i=0
for str1 in arr:
    key=str1.split('=>')[0].strip()
    if str1.find(':')>0:
        args+=('' if args=='' else ',')+'string '+key
        modelargs+=('' if modelargs=='' else ',')+'model.'+str(key).upper().replace('V_','')
        len1=""
        if key in ["outfname","err_msg"]:
            len1="Varchar2,500"
        elif key.find("dte")>0:
            len1="Date"
        else:
            len1="Varchar2"
           
        pms+='cmdParas[{0}] = new OracleParameter("{1}", OracleDbType.{2});\n'.format(i,key,len1)
       
        if len1.find('500')>0:
            pms+='cmdParas[{0}].Direction = ParameterDirection.Output;\n'.format(i)
        else:
            pms+='cmdParas[{0}].Value = {1};\n'.format(i,key)
    else:
        args+=('' if args=='' else ',')+'string[] '+key
        modelargs+=('' if args=='' else ',')+'ls'
        pms+='cmdParas[{0}] = new OracleParameter("{1}", OracleDbType.Varchar2);\n'.format(i,key)
        pms+='cmdParas[{0}].CollectionType = OracleCollectionType.PLSQLAssociativeArray;\n'.format(i)
        pms+='cmdParas[{0}].Value = {1};\n'.format(i,key)
    i=i+1
pms='OracleParameter[] cmdParas = new OracleParameter[{0}];\n'.format(i)+pms
print(pms)
print(args)
print(modelargs)
        
        
