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
        
