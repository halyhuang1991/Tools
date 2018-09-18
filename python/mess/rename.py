import os
import datetime
from glob import glob
#保留多个同样大小的一个
def removeRepeat(path,extends):
    fs=glob(path+"\\*."+extends)
    dic={}
    for f in fs:
        list1=[]
        size=os.path.getsize(f)
        if dic.__contains__(size):
            dic[size].append(f)
        else:
            list1.append(f)
            dic[size]=list1
    for d in dic:
        arr=list(dic[d])
        for i in range(1,len(arr)):
            os.remove(dic[d][i])
path="E:\\download\\media\\Video"
os.chdir(path)
removeRepeat(path,"flv")
removeRepeat(path,"mp4")
files = os.listdir(path)
i=0
for filename in files:
    portion = os.path.splitext(filename)
    if portion[0].find('-')>0:
        continue
    if portion[1] ==".mp4":
        newname = datetime.datetime.now().strftime('%Y%m%d')+"-"+portion[0]+".mp4"
        os.rename(filename,newname)
        i+=1
        print(newname)
    elif portion[1] ==".flv":
        newname = datetime.datetime.now().strftime('%Y%m%d')+"-"+portion[0]+".flv"
        os.rename(filename,newname)
        print(newname)
        i+=1
    else:
        newname = portion[0]
print(i)

    



