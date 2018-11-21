from enum import Enum
class Extn(Enum):
    number = 'N'
    string = 'S'
    datetime = 'D'
    
def GenCsClss(table,columns):
    table=str(table).upper()
    columns=str(columns).upper()
    arr=columns.split(',')
    arr1=columns.lower().split(',')
    ret=' [Serializable] \r\n'
    ret+=' public partial class '+table+'\r\n{'
    ret+='   public '+table+'()\r\n{}\r\n\t'
    for col in arr1:
        if len(col.split('.'))>1:
            extension=str(col.split('.')[1]).strip().upper()
            col=col.split('.')[0]
            if Extn(extension)==Extn.number:
                ret+='\t\t private decimal? _'+col
            elif Extn(extension)==Extn.string:
                ret+='\t\t private string _'+col
            elif Extn(extension)==Extn.datetime:
                ret+='\t\t private string _'+col
            else:
                ret+='\t\t private string _'+col
        else:
             ret+='\t\t private string _'+col
        ret+=';\r\n'
    for col in arr:
        if len(col.split('.'))>1:
            extension=str(col.split('.')[1]).strip().upper()
            col=col.split('.')[0]
            if Extn(extension)==Extn.number:
                ret+='\t\t private decimal? '+col
            elif Extn(extension)==Extn.string:
                ret+='\t\t private string '+col
            elif Extn(extension)==Extn.datetime:
                ret+='\t\t private string '+col
            else:
                ret+='\t\t private string '+col
        else:
             ret+='\t\t private string '+col
        ret+='\r\n{\r\n\t\t'+'set{_'+col.lower()+'=value;}'
        ret+='\r\n\r\n\t\t'+'get{return _'+col.lower()+';}\r\n}\r\n'
    ret+='\t\t}'
    return ret
#ret=GenCsClss('table','a.d,b,c.n')
