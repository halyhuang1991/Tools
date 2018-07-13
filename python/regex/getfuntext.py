import re
import os
path=r"D:\NewWork\BEWeb\BENet\BE.Web\App_Code\Handlers\Salord\CallLotDataHandler.ashx.cs"
f = open(path, 'r',encoding='UTF-8')
text=f.read()
#print(len(text))
arr=re.findall(r'WorkContext.GetFunText[(]formKey, (.*?)[)]',text)
#print(arr)
arr=list(set(arr))
dic={}
for x in arr:
    arr1=x.split(',')
    if arr1[0].find('arr[0]')==0:
        continue
    if len(arr1)<=1:
        continue
    print(arr1[0].strip('"'),arr1[1].strip('"').replace('"',""))
    dic[arr1[0].strip('"')]=arr1[1].strip('"').replace('"',"")
print(dic)
