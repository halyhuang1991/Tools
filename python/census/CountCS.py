import os
path = r"D:\NewWork\BEWeb\BENet\BE.Web\UI"


def GetLines(path):
    length = len(open(path, 'r',-1,'UTF-8').readlines())
    return length


count = 0
for root,dirs,files in os.walk(path):
	for each in files:
		if each.find(".cs")>0:
			count+=1
			#print(root+"\\"+each)
		else:
			#print(root+"\\"+each)
			filepath=root+"\\"+each
			print(GetLines(filepath),each)
print(count)
