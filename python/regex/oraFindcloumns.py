import re
path=r"D:\C\github\Tools\python\regex\produre.txt"
f = open(path, 'r',encoding='UTF-8')
text=f.read().strip()
arr=text.split(',')
arr1=[]
for col in arr:
    col=col.strip()
    arr2=re.findall(r"(?<=[\s|.|\)|,])(?P<name>\S+)$",col)
    if len(arr2)>0:
        c=arr2[0]
        if c.find(')')==-1:
            arr1.append(c)
arr1=list(set(arr1))
print(','.join(arr1))