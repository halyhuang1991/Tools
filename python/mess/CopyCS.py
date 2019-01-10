import os
def GetFileExtension(path):
    return os.path.splitext(path)[-1]
def custombasename(path):
    return os.path.basename(os.path.splitext(path)[0])
def Getdir(path):
    return os.path.dirname(path)
def CopyRewrite(path,newname,dic,modelname):
    filename=os.path.basename(path)
    print(filename)
    clsname=custombasename(path).replace(modelname,newname)
    print(clsname)
    newpath=Getdir(path)+'\\'+clsname+GetFileExtension(path)
    print(newpath)
    if newname.strip()=='':
        return
    if os.path.exists(newpath):
        return
    f_new = open(newpath,'w',encoding='utf-8')
    f= open(path,'r',-1,'utf-8')
    print(len(f.readlines()))
    for line in f.readlines():
        line=line.replace(custombasename(path).split('.')[0],clsname)
        print(line)
        for key in dic:
            line=line.replace(key,dic[key])
        f_new.write(line)
    f.close()
    f_new.close()
    #os.rename(newpath,newpath.replace('3','34'))
def WriteNew(name):
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\Handlers\Basdat\VATDataHandler.ashx"
    CopyRewrite(path,name,{},'VAT')
    dic={}
    dic['ORFVATA']='ORFVATA'
    dic['VAC,VACD,VATTP']=''
    dic['VAT Code,VAT Description,VAT Type']=''
    dic['VAC']='VAC'
    dic['VACD']='VACD'
    dic['VATTP']='VATTP'
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\App_Code\Handlers\Basdat\VATDataHandler.ashx.cs"
    CopyRewrite(path,name,dic,'VAT')
    print('ok')
def WriteNew1(name):
    if name=='':
        return
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\Handlers\Salord\SpecialOrderDataHandler.ashx"
    CopyRewrite(path,name,{},'SpecialOrder')
    dic={}
    dic['IMFCLTC']='IMFCLTC'
    dic['CLSPTP,CLSPTD,FLAG2']=''
    dic['Special Order,Description,Printing Flag for Bogart Report']=''
    dic['CLSPTP']='CLSPTP'
    dic['CLSPTD']='CLSPTD'
    dic['VATTP']='FLAG2'
    path=r"D:\NewWork\BEWeb\BENet\BE.Web\App_Code\Handlers\Salord\SpecialOrderDataHandler.ashx.cs"
    CopyRewrite(path,name,dic,'SpecialOrder')
    print('ok')
#WriteNew1('LotFurtherProcess')



