import os
def GetFileExtension(path):
    return os.path.splitext(path)[-1]
def custombasename(path):
    return os.path.basename(os.path.splitext(path)[0])
def Getdir(path):
    return os.path.dirname(path)
def CopyRewrite(path,newname,dic):
    filename=os.path.basename(path)
    print(filename)
    clsname=custombasename(path).replace('VAT',newname)
    print(clsname)
    newpath=Getdir(path)+'\\'+clsname+GetFileExtension(path)
    print(newpath)
    if newname.strip()=='':
        return
    if os.path.exists(newpath):
        return
    f_new = open(newpath,'w',encoding='utf-8')
    f= open(path,'r',-1,'utf-8')
    for line in f:
        line=line.replace(custombasename(path).split('.')[0],clsname)
        for key in dic:
            line=line.replace(key,dic[key])
        f_new.write(line)
    f.close()
    f_new.close()
    #os.rename(newpath,newpath.replace('3','34'))
def WriteNew():
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\Handlers\Basdat\VATDataHandler.ashx"
    CopyRewrite(path,'Price',{})
    dic={}
    dic['ORFVATA']='ORFVATA'
    dic['VAC,VACD,VATTP']=''
    dic['VAT Code,VAT Description,VAT Type']=''
    dic['VAC']='VAC'
    dic['VACD']='VACD'
    dic['VATTP']='VATTP'
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\App_Code\Handlers\Basdat\VATDataHandler.ashx.cs"
    CopyRewrite(path,'Price',dic)
    print('ok')
WriteNew()



