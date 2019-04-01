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
                ret+='\t\t private DateTime? _'+col
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
                ret+='\t\t public decimal? '+col
            elif Extn(extension)==Extn.string:
                ret+='\t\t public string '+col
            elif Extn(extension)==Extn.datetime:
                ret+='\t\t public DateTime? '+col
            else:
                ret+='\t\t public string '+col
        else:
             ret+='\t\t public string '+col
        ret+='\r\n{\r\n\t\t'+'set{_'+col.lower()+'=value;}'
        ret+='\r\n\r\n\t\t'+'get{return _'+col.lower()+';}\r\n}\r\n'
    ret+='\t\t}'
    return ret
def GenJavaClass(table,columns):
    table=str(table).upper()
    columns=str(columns).upper()
    ret=' public  class '+table+'\r\n{'
    arr=columns.split(',')
    arr1=columns.lower().split(',')
    for col in arr1:
        if len(col.split('.'))>1:
            extension=str(col.split('.')[1]).strip().upper()
            col=col.split('.')[0]
            if Extn(extension)==Extn.number:
                ret+='\t\t private Integer '+col.lower()
            elif Extn(extension)==Extn.string:
                ret+='\t\t private String '+col.lower()
            elif Extn(extension)==Extn.datetime:
                ret+='\t\t private Date '+col.lower()
            else:
                ret+='\t\t private String '+col.lower()
        else:
             ret+='\t\t private String '+col.lower()
        ret+=';\r\n'
    for col in arr:
        datatype=""
        if len(col.split('.'))>1:
            extension=str(col.split('.')[1]).strip().upper()
            col=col.split('.')[0]
            if Extn(extension)==Extn.number:
                ret+='\t\t public Integer '+col
                datatype="Integer"
            elif Extn(extension)==Extn.string:
                ret+='\t\t public String '+col
                datatype="String"
            elif Extn(extension)==Extn.datetime:
                ret+='\t\t public Date '+col
                datatype="Date"
            else:
                ret+='\t\t public String '+col
        else:
             ret+='\t\t public String '+col
             datatype="String"
        ret+='\r\n{\r\n\t\t'+'return this.'+col.lower()+';}'
        ret+="\r\npublic void Set"+col+"("+datatype+" "+col.lower()+"){\r\n this."+col.lower()+"="+col.lower()+"};\r\n"
    ret+='\t\t}'
    return ret
ret=GenCsClss('TEMP_CRTN','PURCNO,PURCRZ,OVY,KCOL,CSRMCN,QTYORD.N,BBVALQ.N,DLVRZZ,SUG,UNM,UNITPR.N,CRNCD')
print(ret)