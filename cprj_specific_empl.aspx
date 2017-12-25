<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cprj_specific_empl.aspx.cs" Inherits="ecsfc.cprj_specific_empl" %>

<!DOCTYPE html5>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/gridviewRowEffect.js"></script>
    <link href="Content/grdRowEffect.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        #Text1 {
            width: 78px;
            height: 22px;
        }
    </style>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "GET",
                url: "Handler1.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#emplist");
                    $.each(result, function (i, item) {
                        var optionstr = $("<option>" + item.name + "</option>").attr("value", item.empno)
                        dataList.append(optionstr);
                    });
                }
            });
            $.ajax({
                type: "GET",
                url: "Handler5.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#cprj");
                    $.each(result, function (i, item) {
                        var optionstr = $("<option>" + item.cprj + "</option>").attr("value", item.cprj)
                        dataList.append(optionstr);
                    });
                }
            });
        })
        //$(document).ready(function () {
            function BindEvents() {
                $.ajax({
                    type: "GET",
                    url: "Handler1.ashx",
                    dataType: "json",
                    success: function (result) {
                        var dataList = $("#emplist");
                        $.each(result, function (i, item) {
                            var optionstr = $("<option>" + item.name + "</option>").attr("value", item.empno)
                            dataList.append(optionstr);
                        });
                    }
                });
            }

        //})
            function getSpecificEmpl() {
                $('#SpcEmpltb').css('display', 'normal');
                console.log("hihihi");
                $.ajax({
                    type: "GET",
                    url: "http://localhost:61434/api/cooperationList/N112",
                    contentType: "json",
                    dataType: "json",
                    success: function (data) {

                        $.each(data, function (key, value) {
                            //stringify
                            var jsonData = JSON.stringify(value);
                            //Parse JSON
                            var objData = $.parseJSON(jsonData);
                            $('#SpcEmpltb').append("<tr>"
                                + "<td>" + objData.empl + "</td>"
                                + "<td>" + objData.role + "</td>"
                                + "<td>" + objData.line + "</td>"
                                + "</tr>");
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            }
                function load() { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
        function EndRequestHandler() { BindEvents(); }
        //$(document).ready(function () {
        //    $.post('Handler1.ashx', function (data) {
        //        $('#browsers').html(data);
        //    });
        //});
    </script>
</head>
<body onload="load()">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                <asp:Label ID="Label1" runat="server" Text="產線工時收集系統" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="X-Large"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Text="變更未開工程作業人員" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Large" BorderStyle="Dashed" BorderWidth="3px"></asp:Label>
                <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <br /><asp:Label ID="Label3" runat="server" Text="您好!" Font-Names="微軟正黑體"></asp:Label>
                    &nbsp;
                    <%--<asp:Label ID="lbempl" runat="server" Text="Label"></asp:Label>--%>
                    &nbsp;
                    <asp:Label ID="lbname" runat="server" Text="Label" Font-Names="微軟正黑體" Font-Size="Medium"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                    <asp:Table ID="Table2" runat="server" Font-Names="微軟正黑體">
                        <asp:TableRow>
                            <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="產線"></asp:Label>
                        </asp:TableCell>
                            <asp:TableCell>
<%--                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  AppendDataBoundItems="True" Font-Names="微軟正黑體" OnDataBinding="DropDownList1_DataBinding" OnDataBound="DropDownList1_DataBound">
                                    <asp:ListItem Text='<%$AppSettings:emptyitem %>'></asp:ListItem>
                                </asp:DropDownList>--%>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="cprj_data" DataTextField="line" DataValueField="line" AutoPostBack="False" AppendDataBoundItems="True">
                                <asp:ListItem Text='<%$AppSettings:all_line %>'></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="cprj_data" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="select distinct line from ecsfc000_emplrole where line is not null"></asp:SqlDataSource>
                        </asp:TableCell>
                            <asp:TableCell>
                            <asp:Label ID="Label6" runat="server" Text="專案"></asp:Label>
                        </asp:TableCell>
                            <asp:TableCell>
                            <input id="cprj_input" name="cprj_input" type="text" list="cprj"/>
                            <datalist id="cprj"/>
                        </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label2" runat="server" Text="工程"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="pud_nopr" DataTextField="nloc" DataValueField="nloc" AutoPostBack="False" AppendDataBoundItems="True">
                                <asp:ListItem Text='<%$AppSettings:all_nopr %>'></asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="pud_nopr" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="select distinct rtrim(t2.nloc) as nloc from ecsfc933_mdt as t1 inner join ecsfc929_memb as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr
"></asp:SqlDataSource>
                            </asp:TableCell>
                            <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="開工日"></asp:Label>
                        </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" Width="180"></asp:TextBox>~
                                <asp:TextBox ID="TextBox3" runat="server" TextMode="Date" Width="180"></asp:TextBox>
                        </asp:TableCell>
                            <asp:TableCell>
                            <asp:Button ID="filter_btn" runat="server" Text="查詢" OnClick="filter_btn_Click"/>
                        </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="changeMod" runat="server" Text="指定已開工" OnClick="changeMod_Click"/>
                                <asp:Button ID="changeMod2" runat="server" Text="指定未開工" OnClick="changeMod2_Click"/>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
            <asp:Label ID="Label8" runat="server" Text="欲離開此頁，請點擊周圍黑幕" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Medium" ForeColor="#9900CC"></asp:Label>
    <div class="container" style="text-align:center;margin-left:1px">
    <div class="row" style="width:860px;margin:1px">
        <div class="col-sm-9 col-md-9" style="font-family:'Microsoft JhengHei';">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
<%--                    <asp:Button ID="logout" runat="server" Text="登出" OnClick="logout_Click" Font-Names="微軟正黑體" />
                    <asp:Button ID="backtomainpage" runat="server" Text="回上頁" Font-Names="微軟正黑體" OnClick="backtomainpage_Click"/>--%>
                    <br />
                </asp:Panel>   
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Names="微軟正黑體" Font-Size="Medium" style="margin-left: 5px" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="cprj" HeaderText="專案" SortExpression="cprj" />
                        <asp:BoundField DataField="line" HeaderText="產線" SortExpression="line" />
                        <asp:BoundField DataField="nopr" HeaderText="nopr" SortExpression="nopr" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nloc" HeaderText="工程站" SortExpression="nloc" />
                        <asp:BoundField DataField="apdt" HeaderText="預計開工日" SortExpression="apdt" dataformatstring="{0:d}"/>
                        <asp:TemplateField HeaderText="工號" SortExpression="emno">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("emno") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <input id="emno" name="emno" type="text" list="emplist" value='<%#emnostr(Eval("cprj").ToString(),Eval("line").ToString(),Eval("nopr").ToString()) %>'/>
                                <datalist id="emplist"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="姓名" SortExpression="t_name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("t_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("t_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="更新">
                            <ItemTemplate>
                                <asp:Button ID="updbtn" name="updbtn" CommandName="updcomd" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" runat="server" Text="更新" Font-Names="微軟正黑體"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="指派協作">
                            <ItemTemplate>
                                <asp:Button ID="addemplBtn" name="addemplBtn" CommandName="addempl" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" runat="server" Text="指派協作" Font-Names="微軟正黑體"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
            </div>
        <div class="col-sm-3 col-md-3" style="font-family:'Microsoft JhengHei';">
        <table id="SpcEmpltb" style="width: 100%;display:normal;">
            <tr>
                <td>empl</td>
                <td>role</td>
                <td>line</td>
        </table>
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
