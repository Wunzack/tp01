<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cprj_worker_specific.aspx.cs" Inherits="ecsfc.cprj_worker_specific" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/freqfunc.js"></script>
    <script type="text/javascript">
        var line = getParameterByName("line");
        var cprj = getParameterByName("cprj");
        var nopr = getParameterByName("nopr");
        //var line = "P722";
        //var cprj = "LH2773";
        //var nopr = "2";
        var insertID = 0;
        var ee = { "membs": [] };
        var membAry = { "membAry": [] };
        var originDatas = [];
        //取得主資料
        function getMainData() {
            var data123 = {
                "cprj": "" + cprj + "",
                "nopr": "" + nopr + "",
                "line": "" + line + ""
            }
            $.ajax({
                type: "GET",
                url: "api/member/" +cprj+","+ line+","+nopr,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data:JSON.stringify(data123),
                success: function (data) {
                    $.each(data, function (key, value) {
                        var jsonData = JSON.stringify(value);
                        var objData = $.parseJSON(jsonData);
                        $('#cprj_main').append("<tr id='" + objData.emno + "'>"
                            + "<td class='eid' errno='" + objData.emno + "'>"
                            + "<input class='editBtn' type='button' value='編輯' errno='" + objData.emno + "'/>"
                            + "<label class='checkbox-inline'><input type='checkbox' class='deleteBtn' errno='" + objData.emno + "' name='optradio'>刪除</label>"
                            + "</td>"
                            + "<td class='errdsca'>" + objData.emno + "</td>"
                            + "<td class='typedesc' etype='" + objData.t_name + "'>" + objData.t_name + "</td>"
                            + "</tr>");
                    })
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });
        }
        $(function () {
            getMainData();
            getMembs();
            //新增輸入列
            $('#createBtn').click(function () {
                if ($('#etype_input').val() != "") {
                    $('#trh').after("<tr class='ro' id='newRow" + insertID + "'>"
                        + "<td>"
                        + "<input id='i" + insertID + "' class='canel' value='取消' type='button' />"
                        + "<label class='checkbox-inline'><input type='checkbox' class='stBtn' name='optradio'>新增後開始紀錄</label>"
                        + "</td>"
                        + "<td>請選人員</td>"
                        + "<td><select id='Select" + insertID + "' class='name'></select></td>"
                        //+ "<td><span class='or'>"+$('#etype_input').val()+"</span></td>"
                        + "</tr>");
                    $.each(membAry["membAry"], function (key, value) {
                      $('#Select' + insertID).append($("<option></option>").attr("value", value.t_empl).text(value.t_name));
                    })

                    insertID += 1;
                }
                else
                    alert("etype不得為空");
            });
            //呼叫確認新增
            $('#confirmBtn').click(function () {
                insertEtype();
            });
        })
        $(document).on('click', '.updCancel', function (e) {
            //alert('功能尚未完成');
            var edTar = $(this).closest('tr');
            var od = getByValue(originDatas, $(this).attr('errno'));
            edTar.html("<td class='eid' errno='" + od.errno + "'>"
                    + "<input class='editBtn' type='button' value='編輯' errno='" + od.errno + "'/>"
                    + "<label class='checkbox-inline'><input type='checkbox' class='deleteBtn' errno='" + od.errno + "' name='optradio'>刪除</label>"
                    + "</td>"
                    + "<td class='errdsca'><span>" + od.errdsca + "</span></td>"
                    + "<td class='typedesc' etype='" + od.etype + "'><span>" + od.emno + "</span></td>"
                    );
        })
        $(document).on('click', '.canel', function (e) {
            $(this).closest("tr").remove();    // Find the row                        
            return false;
        });
        function getMembs() {
            $.ajax({
                type: "GET",
                url: "api/linemembs/" + line,
                dataType: "json",
                success: function (data) {
                    $.each(data, function (key, value) {
                        var jsonData = JSON.stringify(value);
                        var objData = $.parseJSON(jsonData);
                        var optionstr = $("<option>" + objData.t_name + "</option>").attr("value", objData.t_empl);
                        $('#etype_input').append(optionstr);
                        var membObj = {
                            t_empl: objData.t_empl,
                            t_name: objData.t_name
                        }
                        membAry["membAry"].push(membObj);
                    })
                }
                , error: function (xhr) {
                    alert(xhr.responseText);
                }
            })
        }
        function insertEtype() {
            //透過ro class loop搜尋每列輸入之異常名稱、異常大項
            //且將其加入至陣列ee中
            $('.ro').each(function () {
                var dfk = $(this).find('.stBtn').is(':checked');
                var obj = {
                    emno: $(this).find('.name').val().trim(),
                    name: $(this).find(':selected').text().trim(),
                    st: dfk ? '1' : '0',
                    cprj: cprj,
                    line: line,
                    nopr: nopr
                }
                ee["membs"].push(obj);
                //ee儲存後取代原row為readonly(準備完成更新狀態)
                $(this).html('<td></td>'
                    + '<td><span>' + $(this).find('.name').val() + '</span></td>'
                    + '<td><span>' + $(this).find(':selected').text() + '</span></td>'
                    );
                $(this).attr('class', 'tempRo');
            })
            //將陣列ee內容透過etype controller更新至資料庫
            $.ajax({
                type: "POST",
                url: "api/member/" + line,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(ee),
                success: function (data) {
                    //insert成功後要initialize EE陣列
                    //否則會造成資料堆疊
                    ee = { "membs": [] };
                    console.log("insert success..." + data);
                    refm().done(getErrorMainData());
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            })
            //getErrorMainData();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="createBtn" type="button" value="新增" />
            <input id="confirmBtn" type="button" value="確認指派" />
            <select id="etype_input">
                <option>請選擇</option>
            </select>
        </div>
        <div>
            <table id="cprj_main" style="width: 60%; text-align: center; font-family: 'Microsoft JhengHei'">
                <tr id="trh">
                    <th style="text-align: center">&nbsp;&nbsp;</th>
                    <th style="text-align: center">工號</th>
                    <th style="text-align: center">姓名</th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
