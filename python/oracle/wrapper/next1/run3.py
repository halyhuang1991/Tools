import os,sys
parentdir =os.path.abspath(os.path.join(os.path.abspath(os.path.dirname(__file__)),".."))
sys.path.insert(0,parentdir)
from next import run1
run1.log()