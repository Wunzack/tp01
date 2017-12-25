<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abnormal_maintain.aspx.cs" Inherits="ecsfc.abnormal_maintain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--    <script runat="server" type="text/c#">
        protected void timer_tick(object sender, EventArgs e)
        {
            Label1.Text = "panel refresh at :　" + DateTime.Now.ToLongTimeString();
        }
    </script>--%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/jquery.signalR-2.2.1.js"></script>
    <script src="~/signalr/hubs"></script>
    <script src="layer-v2.0/layer/layer.js"></script>
    <script src="Scripts/gridviewRowEffect.js"></script>
    <link href="Content/grdRowEffect.css" rel="stylesheet" />

    <script type="text/javascript">
        var ssStatus = "";
        var position = "";
        var eClose = "";
        $(function () {
            $.ajax({
                type: "GET",
                url: "Handler3.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#cprj");
                    $.each(result, function (i, item) {
                        var optionstr = $("<option>" + item.cprj + "</option>").attr("value", item.cprj)
                        dataList.append(optionstr);
                    });
                }
            });
            //弹出一个tips层
            $('#Button1').on('click', function test99(){
                layer.tips('Hello tips!', '#Button2');
            });

            $('#Button2').on('click', function () {
                //eg1
                layer.msg('系統測試', { icon: 6 });
            });
            var shub = $.connection.statusTransHub;

            $.connection.hub.start().done(function () {
                shub.server.addGroup("GroupB");

                $('#msgBtn').click(function () {
                    var sbtn = ssStatus;
                    var posi = position;                    
                    shub.server.cancelError(sbtn,posi,eClose);
                });                
            });
        });
        function BindEvents() {
            $.ajax({
                type: "GET",
                url: "Handler3.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#cprj");
                    $.each(result, function (i, item) {
                        var optionstr = $("<option>" + item.cprj + "</option>").attr("value", item.cprj)
                        dataList.append(optionstr);
                    });
                }
            });
        }
        function load() { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
        function EndRequestHandler() { BindEvents(); }
        function populateLabel() {
            alert('hi');
        }
        function nulltip2() {
            //eg1
            layer.msg('處置說明不可空白!!', { icon: 5 });//
        }
        //弹出一个tips层
        function nulltip() {
            layer.tips('Hello tips!', '#Button1');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
<%--                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="timer_tick">
                </asp:Timer>--%>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                    <br /><asp:Label ID="Label3" runat="server" Font-Names="微軟正黑體" Text="您好!"></asp:Label>
                    &nbsp; <%--<asp:Label ID="lbempl" runat="server" Text="Label"></asp:Label>--%>&nbsp;
                    <asp:Label ID="lbname" runat="server" Text="Label" Font-Names="微軟正黑體"></asp:Label>
<%--                    <asp:Button ID="logout" runat="server" Text="登出" OnClick="logout_Click" Font-Names="微軟正黑體" />
                    <asp:Button ID="backtomainpage" runat="server" Text="回上頁" Font-Names="微軟正黑體" OnClick="backtomainpage_Click"/>--%>
                    <br />
                    <asp:Table ID="Table2" runat="server" Font-Names="微軟正黑體" >
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
                                <asp:SqlDataSource runat="server" ID="pud_nopr" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="select distinct rtrim(t2.nloc) as nloc from ecsfc930_pud as t1 inner join ecsfc929_memb as t2 on t1.cprj=t2.cprj and t1.line=t2.line and t1.nopr=t2.nopr
"></asp:SqlDataSource>
                            </asp:TableCell>
                            <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="狀態"></asp:Label>
                        </asp:TableCell>
                            <asp:TableCell>
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="False">
                                <asp:ListItem Value="否">未結束</asp:ListItem>
                                <asp:ListItem Value="是">已結束</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                            <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="開工日"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>~
                                <asp:TextBox ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            <asp:Button ID="filter_btn" runat="server" Text="查詢" onclick="filter_btn_Click"/>
                        </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
<br />
                </asp:Panel>
            <asp:Label ID="Label8" runat="server" Text="欲離開此頁，請點擊周圍黑幕" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Medium" ForeColor="#9900CC"></asp:Label>
<%--                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>--%>
                <br />
<%--                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
<%--                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>--%>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Names="微軟正黑體">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="cprj" HeaderText="專案" SortExpression="cprj" />
                        <asp:BoundField DataField="line" HeaderText="產線" SortExpression="line" />
                        <asp:BoundField DataField="nopr" HeaderText="nopr" ReadOnly="True" SortExpression="nopr" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nloc" HeaderText="工程" SortExpression="nloc" />
                        <asp:BoundField DataField="t_name" HeaderText="組裝人員" SortExpression="t_name" />
                        <asp:BoundField DataField="usdtim" HeaderText="異常開始時間" SortExpression="usdtim" />
                        <asp:BoundField DataField="typedesc" HeaderText="異常類型" SortExpression="typedesc" />
                        <asp:BoundField DataField="errdsca" HeaderText="異常說明" SortExpression="errdsca" />
                        <asp:BoundField DataField="workstop" HeaderText="停工狀態" SortExpression="workstop" />
                        <asp:TemplateField HeaderText="處置說明">
                            <ItemTemplate><%--required title="ASP.NET TextBox也可以使用required"--%>
                                <asp:TextBox ID="tbpdsca" runat="server"  Text='<%# Bind("pdsca") %>' Font-Names="微軟正黑體" Visible='<%#Convert.ToBoolean(tbpdsca_readonly(Eval("uclose").ToString())) %>'></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Text='<%#pdsca_label(Eval("pdsca").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="異常狀態">
                            <ItemTemplate>
                                <asp:Button ID="End_exception" CommandName="End_exception" runat="server" Text="結束異常" Visible='<%#Convert.ToBoolean(visible_status(Eval("uclose"),1))%>' CommandArgument="<%#((GridViewRow) Container).RowIndex %>" Font-Names="微軟正黑體"/>
                                <asp:Label ID="Ends_abnormally" runat="server" Text="已結束" Visible='<%#Convert.ToBoolean(visible_status(Eval("uclose"),2))%>' ForeColor="#00AE00" Font-Italic="False" Font-Bold="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="errno" HeaderText="errno" SortExpression="errno" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="position" HeaderText="position" SortExpression="position" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="puid" HeaderText="puid" SortExpression="puid" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
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
                <asp:SqlDataSource ID="ecsfc930ds" runat="server" ConnectionString="<%$ ConnectionStrings:c008ConnectionString %>" SelectCommand="SELECT t1.cprj, t1.line,t1.nopr, t1.nloc, t1.emno, t2.usdtim, t4.typedesc, t3.errdsca, t2.workstop FROM ecsfc929_memb AS t1 INNER JOIN ecsfc930_pud AS t2 INNER JOIN ecsfc932_ud AS t3 INNER JOIN ecsfc931_ud AS t4 ON t3.type = t4.type ON t2.errno = t3.errno ON t1.cprj = t2.cprj AND t1.line = t2.line AND t1.nopr = t2.nopr"></asp:SqlDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
<%--        <asp:Button ID="Button1" runat="server" Text="Button1" OnClick="Button1_Click" /><br />
        <asp:Button ID="Button2" runat="server" Text="Button2" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <input id="msgBtn" type="button" style="display:none;" value="傳送資料至receiveMsg" />           
    </div>
    </form>
</body>
</html>
