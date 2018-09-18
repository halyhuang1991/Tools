class CommentList():
    def __init__(self):
        pass
    comments = None
    commentLen = None
    page = None
com=CommentList()
com.page =1
com.comments="ok"
#class to dict
dic=com.__dict__
print(dic)
print(com.commentLen==None)
if 'comments' in dic:
    print("ok")
# two
arr=[name for name in dir(com) if not name.startswith('__')]
print(arr)
d=dict((name, getattr(com, name)) for name in dir(com) if not name.startswith('__')) 
print(d)
for x in d:
    print(x,d[x])
print(dir(com),getattr(com, 'page'))
#dict to class
def obj_dic(d):
    top = type('new', (object,), d)
    seqs = tuple, list, set, frozenset
    for i, j in d.items():
    	if isinstance(j, dict):
    	    setattr(top, i, obj_dic(j))
    	elif isinstance(j, seqs):
    	    setattr(top, i, 
    		    type(j)(obj_dic(sj) if isinstance(sj, dict) else sj for sj in j))
    	else:
    	    setattr(top, i, j)
    return top
comm=obj_dic(dic)
print(comm.page)