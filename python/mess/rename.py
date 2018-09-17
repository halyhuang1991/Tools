import os
import datetime
path=r"E:\download\media\Video"
os.chdir(path)
files = os.listdir(path)
for filename in files:
    portion = os.path.splitext(filename)
    if portion[1] ==".mp4":
        newname = portion[0]+"-"+datetime.datetime.now().strftime('%Y%m%d')+".mp4"
        os.rename(filename,newname)
        print(newname)
    elif portion[1] ==".flv":
        newname = portion[0]+"-"+datetime.datetime.now().strftime('%Y%m%d')+".flv"
        os.rename(filename,newname)
        print(newname)
    else:
        newname = portion[0]
        
from glob import glob
fs=glob(path+"\*.flv")
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
        print(dic[d][i])
