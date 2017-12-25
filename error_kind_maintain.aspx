<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error_kind_maintain.aspx.cs" Inherits="ecsfc.error_kind_maintain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="layer-v2.0/layer/layer.js"></script>
    <script src="Scripts/gridviewRowEffect.js"></script>
    <link href="Content/grdRowEffect.css" rel="stylesheet" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function nulltip2(message,IconNum) {
            //eg1
            layer.msg(message, { icon: IconNum });//
        }
        function ExceptionMsg(ExMsg) {
            layer.open({
                type: 1,
                title: false,
                closeBtn: 0,
                shadeClose: true,
                skin: 'layui-layer-nobg',
                content: ExMsg
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <asp:Label ID="Label1" runat="server" Text="產線工時收集系統" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="X-Large"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Text="維護異常主檔資料" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Large" BorderStyle="Dashed" BorderWidth="3px"></asp:Label>
    <br />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            
                    <br />
            <asp:Label ID="Label3" runat="server" Font-Names="微軟正黑體" Text="您好!"></asp:Label>
            &nbsp;
                    <%--<asp:Label ID="lbempl" runat="server" Text="Label"></asp:Label>--%>
                    &nbsp;
            <asp:Label ID="lbname" runat="server" Font-Names="微軟正黑體" Font-Size="Medium" Text="Label"></asp:Label>
<%--            <asp:Button ID="logout" runat="server" Font-Names="微軟正黑體" OnClick="logout_Click" Text="登出" />
            <asp:Button ID="backtomainpage" runat="server" Text="回上頁" Font-Names="微軟正黑體" OnClick="backtomainpage_Click"/>--%>
            </asp:Panel>
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="Medium" ForeColor="#9900CC" Text="欲離開此頁，請點擊周圍黑幕"></asp:Label>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="errno" GridLines="None" DataSourceID="SqlDataSource1" EmptyDataText="沒有資料錄可顯示。" ForeColor="#333333" OnRowEditing="GridView2_RowEditing" OnRowCommand="GridView2_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                            </EditItemTemplate>
                            <HeaderTemplate>
                                <asp:Button ID="InsertBtn" runat="server" OnClick="InsertBtn_Click1" Text="新增" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" />
                                &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="errno" HeaderText="errno" ReadOnly="True" SortExpression="errno" Visible="False">
                        </asp:BoundField>
                        <asp:BoundField DataField="errdsca" HeaderText="異常" SortExpression="errdsca">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="異常大類" SortExpression="type">
                            <EditItemTemplate>
<%--                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("type") %>'></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="type_data" DataTextField="typedesc" DataValueField="type" selectedvalue='<%# Bind("type") %>'></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="type_data" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="SELECT [typedesc], [type] FROM [ecsfc931_ud]"></asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("typedesc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="typedesc" HeaderText="typedesc" SortExpression="typedesc" Visible="False" />
                        <asp:BoundField DataField="esdate" HeaderText="停用日期" ReadOnly="true" SortExpression="esdate" />
                        <asp:BoundField DataField="ekindstatus" HeaderText="ekindstatus" SortExpression="ekindstatus" Visible="False" />

                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="kind_data" DefaultMode="Insert" Height="50px" Width="232px" AutoGenerateRows="False" DataKeyNames="errno" OnDataBound="DetailsView1_DataBound">
                            <Fields>
                                <asp:TemplateField HeaderText="異常編號" SortExpression="errno">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label_errorshow" runat="server" Text='<%# Eval("errno") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:Label ID="Label_errno" runat="server" Text="自動生成(請選異常大類)"></asp:Label>
<%--                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("errno") %>'></asp:TextBox>--%>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label_erroritem" runat="server" Text='<%# Bind("errno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
<%--                                <asp:BoundField DataField="esdate" HeaderText="esdate" SortExpression="esdate" />
                                <asp:BoundField DataField="ekindstatus" HeaderText="ekindstatus" SortExpression="ekindstatus" />--%>
                                <asp:TemplateField HeaderText="異常名稱" SortExpression="errdsca">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("errdsca") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("errdsca") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("errdsca") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="異常大類">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("type") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" SelectedValue='<%# Bind("type") %>' DataSourceID="type_data2" DataTextField="typedesc" DataValueField="type" AppendDataBoundItems="True" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                                            <asp:ListItem Text='<%$AppSettings:emptyitem %>'></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="type_data2" ConnectionString='<%$ ConnectionStrings:c008ConnectionString %>' SelectCommand="SELECT [type], [typedesc] FROM [ecsfc931_ud]"></asp:SqlDataSource>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="kind_data" runat="server" ConnectionString="<%$ ConnectionStrings:c008ConnectionString %>" InsertCommand="INSERT INTO [ecsfc932_ud] ([errno], [errdsca], [type]) VALUES (@errno, @errdsca, @type)" SelectCommand="SELECT * FROM [ecsfc932_ud]">
                            <InsertParameters>
                                <asp:Parameter Name="errno" type="String"/>
                                <asp:Parameter Name="errdsca" type="String"/>
                                <asp:Parameter Name="type" type="String"/>
<%--                                <asp:Parameter Name="esdate" />
                                <asp:Parameter Name="ekindstatus" />--%>
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:Button ID="InsertBtn" runat="server" Text="新增" onclick="InsertBtn_Click"/>
                        <asp:Button ID="BtnBack" runat="server" Text="結束新增" OnClick="BtnBack_Click" />
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:c008ConnectionString %>" DeleteCommand="DELETE FROM [ecsfc932_ud] WHERE [errno] = @errno" InsertCommand="INSERT INTO [ecsfc932_ud] ([errno], [errdsca], [type], [esdate], [ekindstatus]) VALUES (@errno, @errdsca, @type, @esdate, @ekindstatus)" SelectCommand="SELECT [errno], [errdsca], [t1].[type], [typedesc],[esdate], [ekindstatus] FROM [ecsfc932_ud] as t1 inner join [ecsfc931_ud] as t2 on t1.type = t2.type" UpdateCommand="UPDATE [ecsfc932_ud] SET [errdsca] = @errdsca, [type] = @type, [esdate] = @esdate, [ekindstatus] = @ekindstatus WHERE [errno] = @errno">
                    <DeleteParameters>
                        <asp:Parameter Name="errno" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="errno" Type="String" />
                        <asp:Parameter Name="errdsca" Type="String" />
                        <asp:Parameter Name="type" Type="String" />
                        <asp:Parameter DbType="Date" Name="esdate" />
                        <asp:Parameter Name="ekindstatus" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="errdsca" Type="String" />
                        <asp:Parameter Name="type" Type="String" />
                        <asp:Parameter DbType="Date" Name="esdate" />
                        <asp:Parameter Name="ekindstatus" Type="String" />
                        <asp:Parameter Name="errno" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    
    </div>
    </form>
</body>
</html>
