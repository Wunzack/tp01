<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cprj_sechedule_kanban.aspx.cs" Inherits="ecsfc.cprj_sechedule_kanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
            <form id="form1" runat="server">
                <div class="row bgimage temphi">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="col-md-1"></div>
                    <div class="col-md-6 mleft">
                        <div class="col-md-12 mtop">
                            <asp:Label ID="Label7" runat="server" Text="進度管理看板" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="40px"></asp:Label>
                        </div>
                    </div>

                    <div class="col-md-5">
                    </div>
                </div>
                <div class="row">
                    <asp:Button ID="back_pre" runat="server" Text="回首頁" onclick="back_pre_Click"/></div>
                <div>
                    <div class="table-responsive">
                        <table class="table">
                            <tr>
                                <th style="text-align: center;">
<%--                                    <asp:Timer ID="Timer1" runat="server" Interval="2000" OnTick="Timer1_Tick"></asp:Timer>--%>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="微軟正黑體" Font-Size="15px" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound" Width="1000px" OnRowCreated="GridView1_RowCreated" OnDataBound="GridView1_DataBound">
                                                <HeaderStyle BackColor="#e78100" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="cprj" HeaderText="專案" SortExpression="cprj" />
                                                    <asp:BoundField DataField="line" HeaderText="產線" SortExpression="line" />
<%--                                                    <asp:TemplateField HeaderText="預計工程">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text='<%# scheduled_project(Eval("cprj").ToString(),Eval("line").ToString(),1) %>' BackColor='<%#System.Drawing.Color.FromName(scheduled_project(Eval("cprj").ToString(),Eval("line").ToString(),2)) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="1" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" ToolTip="1" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),1,DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" ToolTip="2" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),2,DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="3" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" ToolTip="3" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),3,DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="th22">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label38" runat="server" Text="預計進度"></asp:Label>
                                                            <asp:Label ID="Label39" runat="server" Text="實際進度"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label10" runat="server" Text='<%# scheduled_project(Eval("cprj").ToString(),Eval("line").ToString(),1) %>' BackColor='<%#System.Drawing.Color.FromName(scheduled_project(Eval("cprj").ToString(),Eval("line").ToString(),2)) %>'></asp:Label>
                                                            <asp:Label ID="Label4" ToolTip="4" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),4,DateTime.Now.ToString("yyyy-MM-dd")) %>' BackColor='<%#System.Drawing.Color.FromName(Session["wc"].ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="5">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" ToolTip="5" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),5,DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" ToolTip="6" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),6,DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" ToolTip="7" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),7,DateTime.Now.AddDays(3).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" ToolTip="8" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),8,DateTime.Now.AddDays(4).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="9">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label9" ToolTip="9" runat="server" Text='<%#sechedule_nopr(Eval("cprj").ToString(),Eval("line").ToString(),9,DateTime.Now.AddDays(5).ToString("yyyy-MM-dd")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#e78100" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </ContentTemplate>
<%--                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                    <asp:SqlDataSource ID="temp_SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:c008ConnectionString %>" SelectCommand="SELECT [cprj], [line] FROM [ecsfc929_memb]"></asp:SqlDataSource>
                                </th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-3">
                        <h4>
                            <asp:Label ID="Label31" runat="server" Font-Names="微軟正黑體" Style="font-weight: 700" Text="預計進度顏色說明" ></asp:Label>
                        </h4>
                        <asp:Table ID="Table4" runat="server" Font-Names="微軟正黑體" BorderStyle="None">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label32" runat="server" Text="如期進行　"></asp:Label>
                                    <asp:Label ID="Label33" runat="server" Text="　" BackColor="#5BFF24"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label36" runat="server" Text="　預計尚未開始(無色)　"></asp:Label>
                                    <asp:Label ID="Label37" runat="server" Text="　" BackColor="#FFFFFF"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label34" runat="server" Text="落後進度　"></asp:Label>
                                    <asp:Label ID="Label35" runat="server" Text="　" BackColor="#FFBB66"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label11" runat="server" Text="　超前進度　"></asp:Label>
                                    <asp:Label ID="Label12" runat="server" Text="　" BackColor="#00BBFF"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="col-md-4">
                        <h4>
                            <asp:Label ID="Label30" runat="server" Font-Names="微軟正黑體" Style="font-weight: 700" Text="工程狀態顏色說明" ></asp:Label>
                        </h4>
                        <asp:Table ID="Table3" runat="server"  Font-Names="微軟正黑體" BorderStyle="None">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label8" runat="server" Text="已復工　"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" Text="　" BackColor="#FF8800"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label20" runat="server" Text="　停工異常"></asp:Label>
                                    <asp:Label ID="Label21" runat="server" Text="　" BackColor="#FF0000"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label22" runat="server" Text="　普通異常"></asp:Label>
                                    <asp:Label ID="Label23" runat="server" Text="　" BackColor="#FFFF00"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label24" runat="server" Text="進行中　"></asp:Label>
                                    <asp:Label ID="Label25" runat="server" Text="　" BackColor="#5BFF24"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label26" runat="server" Text="　已完工　"></asp:Label>
                                    <asp:Label ID="Label27" runat="server" Text="　" BackColor="#00FFFF"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="Label28" runat="server" Text="　異常完工"></asp:Label>
                                    <asp:Label ID="Label29" runat="server" Text="　" BackColor="#00BBFF"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label ID="Label13" runat="server" Text="預備中　"></asp:Label>
                                    <asp:Label ID="Label14" runat="server" Text="　" BackColor="#E38EFF"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
<%--                <div class="row footer">
                    <div class="col-md-12 bgimage2">
                        <asp:Panel ID="Panel1" runat="server" Height="100"></asp:Panel>
                    </div>
                </div>--%>
            </form>
    </div>
<%--    <div class="navbar navbar-default navbar-fixed-bottom">
                <div class="row bgimage2">
                    <div class="col-md-12 footer">
                        <asp:Panel ID="Panel2" runat="server" Height="100"></asp:Panel>
                    </div>
                </div>
    </div>--%>
</body>
</html>
