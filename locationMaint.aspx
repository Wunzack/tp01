<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="locationMaint.aspx.cs" Inherits="ecsfc.locationMaint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <span style="color:#FF77FF">變更工位!!</span>
        </div>
        <div>
            <table id="cprj_main" class="table-bordered" style="width: 60%; text-align: center; font-family: 'Microsoft JhengHei'">
                <tr id="trh">
                    <th style="text-align: center">&nbsp;&nbsp;</th>
                    <th style="text-align: center">機號</th>                                        
                    <th style="text-align: center">工位</th>
                </tr>
            </table>
        </div>
    </form>
</body>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>    
    <script src="Scripts/freqfunc.js"></script>
    <script src="Scripts/ecsfcMain/locationDataCtrl.js"></script>
</html>
