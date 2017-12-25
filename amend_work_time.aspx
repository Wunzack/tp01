
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amend_work_time.aspx.cs" Inherits="ecsfc.amend_work_time" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        var cprj = "";
        var line = "";
        var nloc = "";
        var posi = "";
        var t_name = "";
        var mkind = "";
        $(function () {
            var shub = $.connection.statusTransHub;

            $.connection.hub.start().done(function () {
                shub.server.addGroup("amendPage");
                $('#StartBtn').click(function () {
                    shub.server.sendMsg("GroupA", cprj, line, nloc, "1", posi, t_name, mkind);
                });
                $('#PauseBtn').click(function () {
                    shub.server.sendMsg("GroupA", cprj, line, nloc, "0", posi, t_name, mkind);
                });
                $('#SNlocBtn').click(function () {
                    shub.server.sendMsg("GroupA", cprj, line, nloc, "1", posi, t_name, mkind);
                });
                $('#FinishBtn').click(function () {
                    shub.server.sendMsg("GroupA", cprj, line, nloc, "2", posi, t_name, mkind);
                });
                $('#testbtn').click(function () {
                    shub.server.sendTest();
                });

            });
            //function sdata(c,l,n,p){
            //    cprj = c;
            //    line = l;
            //    nloc = n;
            //    posi = p;
            //$('#testbtn').click()
            //}

            $.ajax({
                type: "GET",
                url: "Handler4.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#cprj");
                    $.each(result, function (i, item) {
                        var optionstr = $("<option>" + item.cprj + "</option>").attr("value", item.cprj)
                        dataList.append(optionstr);
                    });
                }
            });
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
            });            //弹出一个tips层
            //$('#Button1').on('click', function test99(){
            //    layer.tips('Hello tips!', '#Button2');
            //});
            $('#Button1').click(function () {
                location.href = window.location.href == "http://localhost:61434/amend_work_time.aspx" ? "http://localhost:61434/position_maintain.aspx" : "http://192.168.101.33/WebTest/5682/ecsfc/position_maintain.aspx";
            });

            $('#Button2').on('click', function () {
                //eg1
                layer.msg('系統測試', { icon: 6 });
            });
        });
        function BindEvents() {
            $.ajax({
                type: "GET",
                url: "Handler4.ashx",
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
        function load() { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
        function EndRequestHandler() { BindEvents(); }
        function populateLabel() {
            alert('hi');
        }
        function nulltip2() {
            //eg1
            layer.msg('日期欄不可為空!!', { icon: 5 });//
        }
        //弹出一个tips层
        function nulltip() {
            layer.tips('Hello tips!', '#Button1');
        }
        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toGMTString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        }

        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        function checkCookie() {
            var user = getCookie("username");
            if (user != "") {
                alert("Welcome again " + user);
            } else {
                user = prompt("Please enter your name:", "");
                if (user != "" && user != null) {
                    setCookie("username", user, 30);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                <asp:Label ID="Label1" runat="server" Text="產線工時收集系統" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="X-Large"></asp:Label>
                <br />
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
                    <asp:Table ID="Table2" runat="server" Font-Names="微軟正黑體">
                        <asp:TableRow>
                            <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="產線"></asp:Label>
                        </asp:TableCell>
                            <asp:TableCell>
<%--                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True"  AppendDataBoundItems="True" Font-Names="微軟正黑體" OnDataBinding="DropDownList1_DataBinding" OnDataBound="DropDownList1_DataBound">
                                    <asp:ListItem Text='<%$AppSettings:emptyitem %>'></asp:ListItem>
                                </asp:DropDownList>--%>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="cprj_data" DataTextField="line" DataValueField="line" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Text='<%$AppSettings:all_line %>'></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="cprj_data" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="select distinct line from  ecsfc929_memb"></asp:SqlDataSource>
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
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>~
                                <asp:TextBox ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            <asp:Button ID="filter_btn" runat="server" Text="查詢" OnClick="filter_btn_Click"/>
                        </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table><br />
            <asp:Label ID="Label8" runat="server" Text="欲離開此頁，請點擊周圍黑幕" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Medium" ForeColor="#9900CC"></asp:Label>
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Button ID="change_mode" runat="server" Text="轉換補開工模式" OnClick="change_mode_Click" />
                <asp:Button ID="ChangeToSSmode" runat="server" Text="轉換補開始模式" OnClick="ChangeToSSmode_Click" />
                <input id="Button1" type="button" value="變更產區" runat="server"/>
                <asp:Label ID="Label4" runat="server" Text="" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Large" BorderStyle="Dashed" BorderWidth="3px"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
<%--                    <asp:Button ID="logout" runat="server" Text="登出" OnClick="logout_Click" Font-Names="微軟正黑體" />
                    <asp:Button ID="backtomainpage" runat="server" Text="回上頁" Font-Names="微軟正黑體" OnClick="backtomainpage_Click"/>--%>
                    <br />
                    <br />
                </asp:Panel>         
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="cprj,line,nopr"  ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" Font-Names="微軟正黑體" Width="908px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="cprj" HeaderText="專案" ReadOnly="True" SortExpression="cprj" />
                        <asp:BoundField DataField="nloc" HeaderText="工程" SortExpression="nloc" />
                        <asp:BoundField DataField="line" HeaderText="產線" SortExpression="line" ReadOnly="True" />
                        <asp:BoundField DataField="nopr" HeaderText="nopr" ReadOnly="True" SortExpression="nopr" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="工號" SortExpression="emno">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("emno") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <input id="emno" name="emno" type="text" list="emplist" value='<%#emnostr(Eval("cprj").ToString(),Eval("line").ToString(),Eval("nopr").ToString()) %>'/>
                                <datalist id="emplist"/>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="t_name" HeaderText="人員" SortExpression="t_name" />
                        <asp:BoundField DataField="tb" HeaderText="開工日期" />
                        <asp:TemplateField HeaderText="完工日期">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tb2") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate><%--<%#datetime_value(Eval("cprj"),Eval("line"),Eval("nopr")) %>--%>
                                <input id="Text1" type="datetime-local" name="datel" value='<%#datetime_value(Eval("cprj"),Eval("line"),Eval("nopr"),Session["nowtrigger"])%>' style="font-family: 微軟正黑體" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="補完工">
                            <ItemTemplate>
                                <asp:Button ID="amend_finish_status" runat="server" Text='<%#amwork_start_time_gd(Session["change_mode"].ToString()) %>' commandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="afs" Font-Names="微軟正黑體" Visible='<%#Convert.ToBoolean(amend_btn_visible(Eval("cprj"),Eval("line"),Eval("nopr"))) %>' Width="100px" OnClientClick=<%# "return confirm('確定開工完工此專案嗎?\\n\\r'+'"+Eval("cprj")+"'+'"+Eval("line")+"'+'"+Eval("nloc")+"'); "%>/>
                                <asp:Label ID="Ends_abnormally" runat="server" Text='<%#manage_end_time_status(Eval("cprj").ToString(),Eval("line").ToString(),Eval("nopr").ToString()) %>'  ForeColor="#00AE00" Font-Italic="False" Font-Bold="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="now">
                            <ItemTemplate>
                                <asp:Button ID="getnowtime" runat="server" Text="現在時間" commandArgument="<%#((GridViewRow) Container).RowIndex %>" CommandName="getnowtime" Font-Names="微軟正黑體" Width="100px"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="position">
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mkind" >
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <input id="StartBtn" style="display:none;" type="button" value="button" />
        <input id="PauseBtn" style="display:none;" type="button" value="button" />
        <input id="SNlocBtn" style="display:none;" type="button" value="button" />
        <input id="FinishBtn" style="display:none;" type="button" value="button" />
        <input id="testbtn" type="button" value="sendTest" />
    </div>
    </form>
</body>
</html>
