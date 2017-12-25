<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error_maintain.aspx.cs" Inherits="ecsfc.error_maintain" %>

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
        var insertID = 0;
        var ee = { "etypes": [] };
        var etArray = { "etarray": [] };
        var originDatas = [];
        var updatas = { "upds": [] };
        var nRow
        $(function(){
            getErrorMainData();
            getEtype();
            //新增輸入列
            $('#createBtn').click(function () {
                if ($('#etype_input').val() != "") {
                    $('#trh').after("<tr class='ro' id='newRow"+insertID+"'>"
                        + "<td><input id='i"+insertID+"' class='canel' value='取消' type='button' /></td>"
                        + "<td><input class='lostno' type='text' /></td>"
                        + "<td><input class='etypeName' type='text' /></td>"
                        + "<td><select id='Select"+insertID+"' class='kindval'></select></td>"
                        //+ "<td><span class='or'>"+$('#etype_input').val()+"</span></td>"
                        + "</tr>");                    
                    $.each(etArray["etarray"], function (key, value) {
                        if ($('#etype_input').val() == value.objtype)
                            $('#Select' + insertID).append($("<option selected></option>").attr("value", value.objtype).text(value.objtypename));
                        else
                            $('#Select' + insertID).append($("<option></option>").attr("value", value.objtype).text(value.objtypename));
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
            $('#etype_input').change(function () {
                refm().done(getErrorMainData());
            });
        });
        $(document).on('mouseover', '.norRow', function () {
            nRow = $(this);
            nRow.css('background', '#66FFFF');
        })
        $(document).on('mouseout', '.norRow', function () {            
            nRow.css('background', '#FFFFFF');
        })
        $(document).on('click', '#cofBtn', function (e) {
            var deleteTarget = { "dt": [] };
            $('.deleteBtn:checkbox:checked').each(function () {
                console.log($(this).attr("errno"));
                var delTar = {
                    d1: $(this).attr("errno")
                };
                deleteTarget["dt"].push(delTar);
                $(this).closest('tr').remove();
            });
            $(this).closest('tr').remove();
            $.ajax({
                type: "delete",
                url: "api/etype/" + line,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(deleteTarget),
                success: function (data) {
                    //insert成功後要initialize EE陣列
                    //否則會造成資料堆疊                        
                    console.log("insert success..." + data);
                },
                error: function (ex) {
                    //alert(ex.responseText);
                    alert('主檔已被使用，無法直接刪除!!');
                    refm().done(getErrorMainData());
                }
            })
        })
        $(document).on('click', '.deleteBtn', function (e) {
            var delRow = $(this).closest('tr');
            var delCheckbox = delRow.find('deleteBtn').checked;
            //首列增加確認刪除選取項
            if (!$('.cofro').length) {
                $('#trh').after("<tr class='cofro'>"
                    + "<td><input id='cofBtn' type='button' value='確認刪除' /></td>"
                    + "</tr>");
            }
            if (this.checked) {
                delRow.css('background', '#FFCCCC');
                delRow.attr('class', 'dRow');
            }
            else {
                delRow.css('background', '#FFFFFF');
                delRow.attr('class', 'norRow');
            }
            
        })
        $(document).on('click', '#updCofBtn', function (e) {
            //var updDatas = [];            
            //$('.updRows').each(function () {
            //    var updObj = {
            //        errdsca: $(this).find('.errdsca').val()
            //        , etype: $(this).find(':selected').val()
            //        , errno: $(this).find('.updCancel').attr('errno')
            //    };
            //    updDatas.push(updObj);
            //});            
            updErrorMasData();
            $('.updRows').removeClass();
        })
        $(document).on('click', '.updCancel', function (e) {
            //alert('功能尚未完成');
            var edTar = $(this).closest('tr');
            var od = getByValue(originDatas, $(this).attr('errno'));
            edTar.attr('class', 'norRow');
            edTar.html("<td class='eid' errno='" + od.errno + "'>"
                    + "<input class='editBtn' type='button' value='編輯' errno='" + od.errno + "'/>"
                    + "<label class='checkbox-inline'><input type='checkbox' class='deleteBtn' errno='" + od.errno + "' name='optradio'>刪除</label>"
                    + "</td>"
                    + "<td class='lostno'><span>" + od.lostno + "</span></td>"
                    + "<td class='errdsca'><span>" + od.errdsca + "</span></td>"
                    + "<td class='typedesc' etype='"+od.etype+"'><span>" + od.etypeName + "</span></td>"
                    );
            edTar.css('background', '#FFFFFF');
        })
        $(document).on('click', '.editBtn', function (e) {
            //首列增加確認刪除選取項
            var tar = $(this).closest('tr');
            var conEtype = tar.find('.typedesc').attr('etype');
            //備份舊資料，供取消更新時使用
            var bkData = {                
                errno:tar.find('.eid').attr('errno')
                , errdsca: tar.find('.errdsca').text()
                , etype: conEtype
                , etypeName: tar.find('.typedesc').text()
                , lostno: tar.find('.lostno').text()
            }
            originDatas.push(bkData);
            if (!$('.upCofro').length) {
                $('#trh').after("<tr class='upCofro'>"
                    + "<td><input id='updCofBtn' type='button' value='確認更新' /></td>"
                    + "</tr>");
            }
            tar.removeClass();
            tar.addClass('updRows');
            tar.html("<td><input class='updCancel' type='button' value='取消' errno='" + tar.find('.eid').attr('errno') + "'/></td>"
                + "<td><input class='lostno' type='text' value='" + tar.find('.lostno').text() + "'/></td>"
                + "<td><input class='errdsca' type='text' value='" + tar.find('.errdsca').text() + "'/></td>"
                + "<td><select class='kindval'></select></td>"
                );
            tar.css('background', '	#33CCFF');
            //etArray內容透過迴圈pass資料至空容器select中
            //其中若etype與標的資料相符則selected
            $.each(etArray["etarray"], function (key, value) {
                if (conEtype == value.objtype)
                    tar.find('.kindval').append("<option selected value='" + value.objtype + "'>"+value.objtypename+"</option>");
                else
                    tar.find('.kindval').append("<option value='" + value.objtype + "'>" + value.objtypename + "</option>");
            })
            $(this).attr('class', 'tempRo');
        })
        $(document).on('click', '.canel', function (e) {
            $(this).closest("tr").remove();    // Find the row                        
            return false;
        });
        //從array 透過鍵值value查找資料
        function getByValue(arr, value) {
            var result = arr.filter(function (o) { return o.errno == value; });
            return result ? result[0] : null;
        }
        //確認新增function
        //透過webapi insert 資料
        function insertEtype() {
            //透過ro class loop搜尋每列輸入之異常名稱、異常大項
            //且將其加入至陣列ee中
            $('.ro').each(function () {
                var obj = {
                    lostno: $(this).find('.lostno').val(),
                    etypeName: $(this).find('.etypeName').val(),
                    kindval:$(this).find('.kindval').val()
                }
                ee["etypes"].push(obj);
                //ee儲存後取代原row為readonly(準備完成更新狀態)
                $(this).html('<td></td>'
                    + '<td><span>' + $(this).find('.etypeName').val() + '</span></td>'
                    + '<td><span>' + $(this).find(':selected').text() + '</span></td>'
                    );
                $(this).attr('class', 'tempRo');
            })
            //將陣列ee內容透過etype controller更新至資料庫
            $.ajax({
                type: "put",
                url: "api/etype/" + line,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(ee),
                success: function (data) {
                    //insert成功後要initialize EE陣列
                    //否則會造成資料堆疊
                    ee = { "etypes": [] };
                    console.log("insert success..." + data);
                    refm().done(getErrorMainData());
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            })            
            //getErrorMainData();
        }
        function updErrorMasData() {
            $('.updRows').each(function () {
                var obj = {
                    errno: $(this).find('.updCancel').attr('errno'),
                    lostno: $(this).find('.lostno').val(),
                    errdsca: $(this).find('.errdsca').val(),
                    kindval: $(this).find('.kindval').val()
                }
                updatas["upds"].push(obj);
                //ee儲存後取代原row為readonly(準備完成更新狀態)
                $(this).html('<td></td>'
                    + '<td><span>' + $(this).find('.lostno').val() + '</span></td>'
                    + '<td><span>' + $(this).find('.errdsca').val() + '</span></td>'
                    + '<td><span>' + $(this).find(':selected').text() + '</span></td>'
                    );
                $(this).attr('class', 'tempRo');
            })
            $.ajax({
                type: "post",
                url: "api/etype/" + line,
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(updatas),
                success: function (data) {
                    //update成功後要initialize updatas陣列
                    //否則會造成資料堆疊
                    updatas = { "upds": [] };
                    console.log("update success..." + data);
                    refm().done(getErrorMainData());
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            })
        }
        //刪除第一行以外資料行
        //使用deferred實現執行完才執行下任務的目標
        var refm = function rem() {            
            var r = $.Deferred();
            $('tr:not(:first)').remove();
            return r;
        }
        function getEtype() {
            $.ajax({
                type: "GET",
                url: "api/etype/" + line + ',' + '1',
                dataType: "json",
                success: function (data) {
                    $.each(data, function (key, value) {
                        var jsonData = JSON.stringify(value);
                        var objData = $.parseJSON(jsonData);
                        var optionstr = $("<option>" + objData.typedesc + "</option>").attr("value", objData.type);
                        $('#etype_input').append(optionstr);
                        var etObj = {
                            objtype: objData.type,
                            objtypename:objData.typedesc
                        }
                        etArray["etarray"].push(etObj);
                    })
                }
            })
        }
        function getErrorMainData() {
            $.ajax({
                type: "GET",
                url: "api/error/" + line,
                dataType: "json",
                success: function (data) {
                    $.each(data, function (key, value) {
                        var jsonData = JSON.stringify(value);
                        var objData = $.parseJSON(jsonData);
                        var df = $('#etype_input').val();
                        if (objData.type == $('#etype_input').val() | $('#etype_input').val() == '0') {
                            var td = objData.lostno == null ? '' : objData.lostno;
                            $('#cprj_main').append("<tr class='norRow' id='" + objData.errno + "'>"
                                + "<td class='eid' errno='" + objData.errno + "'>"
                                + "<input class='editBtn' type='button' value='編輯' errno='" + objData.errno + "'/>"
                                + "<label class='checkbox-inline'><input type='checkbox' class='deleteBtn' errno='" + objData.errno + "' name='optradio'>刪除</label>"
                                + "</td>"
                                + "<td class='lostno'>" + td + "</td>"
                                + "<td class='errdsca'>" + objData.errdsca + "</td>"
                                + "<td class='typedesc' etype='" + objData.type + "'>" + objData.typedesc + "</td>"
                                + "</tr>");
                        }
                    })
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });
        }
        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="createBtn" type="button" value="新增" />
            <input id="confirmBtn" type="button" value="確認新增" />
            <select id="etype_input">
                <option value="0">請選擇</option>
            </select><span style="color:#FF77FF">下拉點選可進行篩選!!</span>
        </div>
        <div>
            <table id="cprj_main" class="table-bordered" style="width: 60%; text-align: center; font-family: 'Microsoft JhengHei'">
                <tr id="trh">
                    <th style="text-align: center">&nbsp;&nbsp;</th>
                    <th style="text-align: center">異常編號</th>
                    <th style="text-align: center">異常名稱</th>
                    <th style="text-align: center">異常大項</th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
