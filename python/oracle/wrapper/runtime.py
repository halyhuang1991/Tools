import time
import sys,os
# parentdir = os.path.dirname(os.path.dirname(os.path.abspath(__file__))) 
# sys.path.insert(0,parentdir)  
def runtime(func):
    def wrapper(*arg,**kw):
        start_time = time.time()
        func(*arg,**kw)
        end_time = time.time()
        print(end_time - start_time)
    return wrapper
""" @runtime
def log():
    print('ok')
log() """

oraTest.log()



