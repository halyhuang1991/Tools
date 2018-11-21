import json
import os
import re
path=r"E:\web\users.json"
with open(path,'r',-1,'utf-8') as f:
    #print(f.readlines())
    text = json.load(f)
    print(text)


