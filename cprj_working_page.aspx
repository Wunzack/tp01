<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cprj_working_page.aspx.cs" Inherits="ecsfc.cprj_working_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="table-responsive">
                <table class="table">
                    <tr>
                        <th style="text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="cprj_label1" runat="server" Text="專案："></asp:Label>

                        </td>
                        <td>

                            <asp:Label ID="cprj_label2" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="stander_label1" runat="server" Text="標準工時："></asp:Label>

                        </td>
                        <td>

                            <asp:Label ID="stander_label2" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="actual_label1" runat="server" Text="實際工時："></asp:Label>

                        </td>
                        <td>

                            <asp:Label ID="actual_label2" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="errortime_label1" runat="server" Text="異常工時："></asp:Label>

                        </td>
                        <td>

                            <asp:Label ID="errortime_label2" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="worker_label1" runat="server" Text="作業者："></asp:Label>

                        </td>
                        <td>

                            <asp:Label ID="worker_label2" runat="server" Text="Label"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">

                            <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="Button1" runat="server" Text="Button" />

                        </td>
                        <td>

                            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="Button2" runat="server" Text="Button" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
