import os
def GetfileText():
    path=r"D:\C\github\Tools\python\regex\produre.txt"
    f = open(path, 'r',encoding='UTF-8')
    text=f.read()
    return text

