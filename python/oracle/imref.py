import os,sys
def relatref(path):
    os.chdir(path)
    parentdir =os.path.abspath(os.path.join(os.getcwd()))
    print(parentdir)
    sys.path.insert(0,parentdir)


#放入C:\Python34\Lib\site-packages