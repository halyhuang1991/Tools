Columns = "CSTREF,CLOT,CSTORD,EXTNAM,COLLD,DEG,PRTYPD,CSPRD,STMNT1,STMNT2,STMNT3,STMNT4,DLVM,DT_30Z,DT_05Z,DT_4Z,CSTS_1,CSTS_3,PLANUN,USRDTA,DT_5Z,QTYR,DEP02,DAYS,QTYP,QTY33"
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,PO Qty,Allowance Qty,Sample Qty,All Qty,Delivery Code,Original EX-Fty Date,EX-Fty Date,Closing Date,Lot Status,Lot Closed,Plant/Site,Issue By,Actual Ex-fty Date,Actual Ship Qty,Delivery Code,Metrics,Ontime Qty,Balance Qty"
dic = dict(zip(Columns.split(','), defaultNames.split(',')))
Columns = "CSTREF,CLOT,CSTORD,EXTNAM,COLLD,DEG,PRTYPD,CSPRD,CSCOM,CSCOMD,STMNT1,STMNT2,STMNT3,STMNT4,DLVM,DT_30Z,DT_05Z,DT_4Z,CSTS_1,CSTS_3,PLANUN,USRDTA,DT_5Z,QTYR,DEP02,DAYS,QTYP,QTY33"
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,Customer Color Code,Customer Color Name,PO Qty,Allowance Qty,Sample Qty,All Qty,Delevery Code,Original EX-Fty Date,EX-Fty Date,Closing Date,Lot Status,Lot Closed,Plant/Site,Issue By,Actual Ex-fty Date,Actual Ship Qty,Delivery Code,Metrics,Ontime Qty,Balance Qty"
dic1=dict(zip(Columns.split(','), defaultNames.split(',')))
dic.update(dic1)
Columns = "CSTREF,CLOT,CSTORD,EXTNAM,COLLD,DEG,PRTYPD,CSPRD,CSCOM,CSCOMD,SIZE,STMNT1,STMNT2,STMNT3,STMNT4,DLVM,DT_30Z,DT_05Z,DT_4Z,CSTS_1,CSTS_3,PLANUN,USRDTA,DT_5Z,QTYR,DEP02,DAYS,QTYP,QTY33"
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,Customer Color Code,Customer Color Name,Size Fit,PO Qty,Allowance Qty,Sample Qty,All Qty,Delevery Code,Original EX-Fty Date,EX-Fty Date,Closing Date,Lot Status,Lot Closed,Plant/Site,Issue By,Actual Ex-fty Date,Actual Ship Qty,Delivery Code,Metrics,Ontime Qty,Balance Qty"
dic1=dict(zip(Columns.split(','), defaultNames.split(',')))
dic.update(dic1)
Columns = "CSTREF,CLOT,CSTORD,EXTNAM,COLLD,DEG,PRTYPD,CSPRD,STMNT1,STMNT2,STMNT3,STMNT4,DLVM,DT_30Z,DT_05Z,DT_4Z,CSTS_1,CSTS_3,PLANUN,USRDTA,DT_5Z,QTYR,DEP02,DAYS,QTYP,QTY33,,LHINV"
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,PO Qty,Allowance Qty,Sample Qty,All Qty,Delivery Code,Original EX-Fty Date,EX-Fty Date,Closing Date,Lot Status,Lot Closed,Plant/Site,Issue By,Actual Ex-fty Date,Actual Ship Qty,Delivery Code,Metrics,Ontime Qty,balance Qty,Customer Invoice"
dic1=dict(zip(Columns.split(','), defaultNames.split(',')))
dic.update(dic1)
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,PO Qty,Allowance    Qty,Sample    Qty,All Qty,Delivery Code,Original          Ex-Fty Date,Ex-Fty   Date,Closing Date,Lot     Status,Lot     Closed,Plant      Site,Issue By,Actual        Ex-fty Date,Actual   Ship Qty,Delivery Code,Metrics,Ontime   Qty,Balance Qty                       (PO Qty - Actual Ship Qty),Invoice No."
col=""
for x in defaultNames.split(','):
    key="--"
    for k,v in dic.items():
        if(x.replace(' ','').startswith(v.replace(' ',''))):
            key=k
    #print(x,key)
    col=col+","+key+"_1"
#print(col)
Columns = "CSTREF_1,CLOT_1,CSTORD_1,EXTNAM_1,COLLD_1,DEG_1,PRTYPD_1,CSPRD_1,CSCOM_1,CSCOMD_1,SIZE_1,STMNT1_1,STMNT2_1,STMNT3_1,STMNT4_1,DLVM_1,DT_30Z_1,DT_05Z_1,DT_4Z_1,CSTS_1_1,CSTS_3_1,PLANUN_1,USRDTA_1,DT_5Z_1,QTYR_1,DEP02_1,DAYS_1,QTYP_1,QTY33_1"
defaultNames = "PO Nbr,Call Lot,Lot Number,Customer Name,Group Name,Product,Product Category,Customer Product,Customer Color Code,Customer Color Name,Size Fit,PO Qty,Allowance    Qty,Sample    Qty,All Qty,Delivery Code,Original          Ex-Fty Date,Ex-Fty   Date,Closing Date,Lot     Status,Lot     Closed,Plant      Site,Issue By,Actual        Ex-fty Date,Actual   Ship Qty,Metrics,Ontime   Qty,Balance Qty                       (PO Qty - Actual Ship Qty)"
i=0
for x in defaultNames.split(','):
    val=""
    if(Columns.split(',').__len__()>i):
        val=Columns.split(',')[i]
    print(x+"==========="+val)
    i+=1

