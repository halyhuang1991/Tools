import sys,os
import imref
imref.relatref(r'D:\C\github\Tools')
from python.oracle.wrapper import runtime
@runtime.runtime
def log():
    print("test")
log()