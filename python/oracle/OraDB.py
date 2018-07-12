import os
from subprocess import Popen, PIPE
import sys
os.environ['NLS_LANG'] = 'SIMPLIFIED CHINESE_CHINA.AL32UTF8'#select userenv('language') from dual
gStrConnection = ''

def QueryOra(sqlCom):
  proc = Popen("sqlplus "+gStrConnection, stdout=PIPE, stdin=PIPE, stderr=PIPE,shell=True)
  proc.stdin.write(bytes(sqlCom,"UTF-8"))
  (out, err) = proc.communicate()
 
  if proc.returncode != 0:
    print(err)
    return ""
    #sys.exit(proc.returncode)
  else:
    str=out.decode('utf-8').split('SQL>')[1]
    #print(str)
    return str

class BaseDB(object):
  def __init__(self,username,conStr):
    self.username=username
    self.conStr=conStr
  def Query(self,sql):
    proc = Popen("sqlplus "+gStrConnection, stdout=PIPE,
                 stdin=PIPE, stderr=PIPE, shell=True)
    proc.stdin.write(bytes(sql, "UTF-8"))
    (out, err) = proc.communicate()
    os.kill(proc.pid,0)
    if proc.returncode != 0:
      print(err)
      return ""
    else:
      str = out.decode('utf-8').split('SQL>')[1]
      return str
  def Exec(self,sql):
      proc = Popen("sqlplus "+gStrConnection, stdout=PIPE,
                 stdin=PIPE, stderr=PIPE, shell=True)
      proc.stdin.write(bytes(sql, "UTF-8"))
       (out, err) = proc.communicate()
      os.kill(proc.pid,0)
      if proc.returncode != 0:
        print(err)
      else:
        print('ok')







