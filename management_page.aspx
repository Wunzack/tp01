<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="management_page.aspx.cs" Inherits="ecsfc.management_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title></title>
    <style>
.vertical-container {
  height: 100px;
  display: -webkit-flex;
  display:         flex;
  -webkit-align-items: center;
          align-items: center;
  -webkit-justify-content: center;
          justify-content: center;
}    

    </style>
    <script type="text/javascript">
        var employeeData = [];
        var db;
        var userComfirm = 0;
        //indexedDB test.sn
        function prefixes() {
            //prefixes of implementation that we want to test
            window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;

            //prefixes of window.IDB objects
            window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction || window.msIDBTransaction;
            window.IDBKeyRange = window.IDBKeyRange || window.webkitIDBKeyRange || window.msIDBKeyRange

            if (!window.indexedDB) {
                window.alert("Your browser doesn't support a stable version of IndexedDB.")
            }            
            //const employeeData2 = [
            //   { id: "0001", name: "gopal", age: 35, email: "gopal@tutorialspoint.com" },
            //   { id: "0002", name: "prasad", age: 32, email: "prasad@tutorialspoint.com" }
            //];
            //開啟DB(若不存在則建立)
            var request = window.indexedDB.open("c008");

            request.onerror = function (event) {
                console.log("error: ");
            };

            request.onsuccess = function (event) {
                db = request.result;
                console.log("success: " + db);
                checkDB();
            };
            //新建資料庫或版本號更新時觸發onupgradeneeded
            request.onupgradeneeded = function (event) {
                console.log('hohohoho');
                var dbs = event.target.result;
                //若不存在則新增
                var objectStore = dbs.createObjectStore("employee", { keyPath: "objempl" });
                objectStore.add({ objempl: "0000", objt_name: "first", objrole: "" });
                //for (var i in employeeData) {
                //    objectStore.add(employeeData[i]);
                //}
            }
        }
        //indexedDB test.en


        $(function () {
            prefixes();            
            $('#TextBox1').on('input', function () {
                var alt = $('#alert');
                console.log('hey!! look at me ' + $('#TextBox1').val());
                //初始化#alert為隱藏
                //$('#alert').toggle($('#alert').is(':visible'));                
                var tbv = $(this);
                if (tbv.val().toString().length != 4) {
                    $('strong').text(tbv.val());
                    //console.log('c' + $('#alert').is(':hidden'));
                    if (alt.is(':hidden'))
                        alt.toggle("swing");
                    !$('#alertWelcome').is(':hidden') ? $('#alertWelcome').toggle() : console.log('');
                    !$('#alertAuth').is(':hidden') ? $('#alertAuth').toggle() : console.log('');
                    $('#login').attr('class', 'btn btn-default disabled');
                    userComfirm = userComfirm == 1 ? 0 : userComfirm;
                    $('#login').attr('disabled', 'disabled');
                }//工號格式符合
                else {
                    !alt.is(':hidden') ? alt.toggle("swing") : console.log('');
                    console.log($('#TextBox1').val());
                    read();
                }
            });
            //$('#TextBox1').on('change', function () {
            //    console.log('hey!! look at me ' + $('#TextBox1').val());
            //})
        })
        function checkDB() {
            var transaction = db.transaction(["employee"]);
            var objectstore = transaction.objectStore("employee");
            var ct = objectstore.count();
            ct.onsuccess = function () {
                console.log('ct is ' + ct.result);
                //if (ct.result <= 1) {
                    getEmployees();
                //}
            }
            ct.onerror = function () {
                console.log('table employee does not exist!!');
            }
            
        }
        function getEmployees() {
            $.ajax({
                type: "GET",
                url: "api/employee/" + '11',
                dataType: "json",
                success: function (data) {
                    var transaction = db.transaction(["employee"],"readwrite");
                    var objectstore = transaction.objectStore("employee");                   
                    var objectStoreRequest = objectstore.clear();
                    objectStoreRequest.onsuccess = function () {
                        console.log('successful!!');
                    }
                    $.each(data, function (key, value) {
                        var jsonData = JSON.stringify(value);
                        var objData = $.parseJSON(jsonData);
                        //var emplObj = {
                        //    objempl: objData.empl.trim(),
                        //    objt_name: objData.t_name.trim(),
                        //    objrole: objData.role.trim()
                        //}
                        //employeeData.push(emplObj);
                        var result = objectstore.add({ objempl: objData.empl.trim(), objt_name: objData.t_name.trim(), objrole: objData.role.trim() });
                        result.onsuccess = function (event) {
                            console.log(objData.empl.trim() + " add successful!!");
                        }
                        result.onerror = function (event) {
                            console.log(objData.empl.trim() + " has been added to your database!!");
                        }
                    });
                },
                error: function (ehx) {
                    alert(ehx);
                }
                //complete: function () {
                //    add();
                //}
            });
        }
    </script>
</head>
<body class="temppp wrapper">
    <div class="center">
        <form id="form1" runat="server">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="login">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:TextBox ID="lineTempStor" runat="server" CssClass="hidden"></asp:TextBox>
                <div class="vertical-container">                    
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="工號："></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="login" runat="server" Text="登入" OnClick="login_Click" CssClass="btn btn-default disabled" Enabled="False" /></td>
                                    <td>
                                        <asp:Button ID="lineComfirmTri" runat="server" Text="Button" CssClass="hidden" OnClick="Button1_Click" /></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="login" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <table>
                        <tr>
                            <td colspan="2">
                                <div id="alert" class="alert alert-danger" style="display: none">
                                    <strong></strong>格式錯誤!!
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="alertAuth" class="alert alert-danger" style="display: none">
                                    <strong></strong>此帳號不存在或無權限，請聯絡180!!
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="alertWelcome" class="alert alert-success" style="display: none">
                                    Hi! <strong></strong>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <%--                        <asp:Timer ID="Timer1" runat="server" Interval="250" OnTick="Timer1_Tick"></asp:Timer>--%>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="Label2" runat="server" Text="<%# emno_status(TextBox1.Text) %>"></asp:Label>
                                    </ContentTemplate>
                                    <%--                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </Triggers>--%>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </form>
    </div>
    <script src="layer-v2.0/layer/layer.js"></script>
    <script type="text/javascript">
        //事件function.sn
        function read() {
            var tb1 = $('#TextBox1');
            var transaction = db.transaction(["employee"]);
            var objectStore = transaction.objectStore("employee");
            var request = objectStore.get(tb1.val());

            request.onerror = function (event) {
                console.log('request failed');   
            };

            request.onsuccess = function (event) {
                var alertAuth = $('#alertAuth');
                var alertWelcome = $('#alertWelcome');
                var login = $('#login');
                try{
                    console.log('empl:' + request.result.objempl + 'name:' + request.result.objt_name);
                    // Do something with the request.result!
                    if (alertWelcome.is(':hidden')) {                        
                        alertWelcome.find('strong').text(request.result.objt_name);
                        alertWelcome.toggle();
                        login.attr('class', 'btn btn-default').removeAttr('disabled');
                        userComfirm = 1;
                    }
                }
                catch (ehx) {
                    console.log('AuthError');                    
                    if (alertAuth.is(':hidden')) {
                        !alertWelcome.is(':hidden') ? alertWelcome.toggle() : console.log('');
                        alertAuth.find('strong').text(tb1.val());
                        alertAuth.toggle("swing");
                        
                        userComfirm = userComfirm == 1 ? 0 : userComfirm;
                    }
                }
            };
        }

        function readAll() {
            var objectStore = db.transaction("employee").objectStore("employee");

            objectStore.openCursor().onsuccess = function (event) {
                var cursor = event.target.result;

                if (cursor) {
                    console.log("Name for id " + cursor.key + " is " + cursor.value.name + ", Age: " + cursor.value.age + ", Email: " + cursor.value.email);
                    cursor.continue();
                }

                else {
                    console.log("No more entries!");
                }
            };
        }

        function add() {
            var request = db.transaction(["employee"], "readwrite")
            .objectStore("employee")
            .add({ id: "00-03", name: "Kenny", age: 19, email: "kenny@planet.org" });

            request.onsuccess = function (event) {
                alert("Kenny has been added to your database.");
            };

            request.onerror = function (event) {
                alert("Unable to add data\r\nKenny is aready exist in your database! ");
            }
        }

        function remove() {
            var request = db.transaction(["employee"], "readwrite")
            .objectStore("employee")
            .delete("00-03");

            request.onsuccess = function (event) {
                alert("Kenny's entry has been removed from your database.");
            };
        }
        //事件function.en        
        function lineComfirmTri(lines) {
            var lineAry = lines.split(",");
            var dropdlist = "";
            $.each(lineAry, function (id,value) {
                dropdlist += "<option>" + value.toString().trim() + "</option>"
            });

            layer.open({
                type: 1,
                title: false,
                closeBtn: 0,
                shadeClose: true,
                skin: 'Layerclass1',
                content: "<html><head>"
                    + "</head>"
                    + "<body>"
                    + "<select id='Select1'>"
                    + "<option>請選產線</option>"
                    + dropdlist
                    +"</select>"
                    + "<input id=\"layBtn\" type=\"button\" style=\"color:#000000;\" onclick='javascript: loginTri();' value=\"依選擇產線登入\"/>"
                    //+ "<input id=\"sErrorBtn\" type=\"button\" style=\"color:#000000;\" onclick='javascript: stopErrorUpdate();' value=\"停機異常\"/><br/>"                    
                    + "<label id=\"layLabel2\"></label>"
                + "</body></html>"
            });

        }
        function loginTri() {
            console.log('已依產線 ' + $('#Select1 option:selected').text() + ' 登入');
            $('#lineTempStor').val($('#Select1 option:selected').text());
            $('#lineComfirmTri').click();
            //$('#lineComfirmTri').click();
        }
        function testReceive(responseData) {
            console.log('client has received ' + responseData);
        }
        $(function () {
            
        })
    </script>
</body>
</html>
