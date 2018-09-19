import re
a=" where a='12323' and b=34 "
print(re.findall(r"'\d+",a))#["'12323"]
print(re.findall(r"'(\d+)",a))#['12323']
print(re.findall(r"(?<=\').*?(?=\')",a)) #['12323']
print(re.findall(r"=\d+\s",a))
print(re.findall(r"'(?P<name>\w+)'",a))#['12323']
print(re.search(r"'(?P<name>\w+)'",a).group(0))#'12323'
print(re.search(r"'(?P<name>\w+)'",a).group('name'))#12323
print(re.search(r"'(?P<name>\w+)'",a).group(1))#12323
print(re.sub(r"(?<=\')(?P<name>\w+)(?=\')",r"78\g<name>12",a))
