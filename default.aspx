<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ecsfc._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>管理頁面</title>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.js"></script>
<%--    <script src="Scripts/jquery-1.9.1.min.js"></script>--%>
    
<%--    <script src="Scripts/bootstrap.min.js"></script>--%>
    <%--<link href="Content/bootstrap.css" rel="stylesheet" />--%>
    <link href="Content/bootstrap3/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/moment.min.js"></script>
    <link href="Content/ecsfcKanbanStyles/ecsfcBaseStyle.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script type="text/javascript" src="scripts/bootstrap-datetimepicker.js"></script>
    <link rel="stylesheet" href="Content/bootstrap-datetimepicker.css" />

    <script src="Scripts/jquery.signalR-2.2.1.js"></script>
    <script src="~/signalr/hubs"></script>
<%--    <link href="Content/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="layer-v2.0/layer/layer.js"></script>
    <script src="Scripts/popup.js"></script>
    <link href="Content/popup.css" rel="stylesheet" />
    <link href="Content/tooltipster.bundle.min.css" rel="stylesheet" />
    <script src="Scripts/tooltipster.bundle.min.js"></script>
    <script src="Scripts/freqfunc.js"></script>
            <script type="text/javascript">
                //var saclose = $('.layui-layer-shade');
                //saclose[0].click(function () {
                //    console.log('shadow click!!');
                //    $('#gvbindBtn').click();
                //});

                //string.padLeft 為 javascript 擴充方法
                //目的為為字串補零
                String.prototype.padLeft = function padLeft(length, leadingChar) {
                    if (leadingChar === undefined) leadingChar = "0";
                    return this.length < length ? (leadingChar + this).padLeft(length, leadingChar) : this;
                };
                var tline = "<%=Session["gline"].ToString()%>";
                var gline = tline.trim();
                var t_nopr;
                var tri_cprj;
                var tri_nloc;
                var t_status;
                var w_name;
                var tri_dsca = '';
                var tri_mkind = '';
                var tRow;
                var tTd;
                var edRow;
                var edTd;
                var upds = 0;
                $(function () {
                    prefixes();
                    var shub = $.connection.statusTransHub;
                    //宣告hub對應client function.sn
                    shub.client.sendMsg = function (cprj, line, nloc, sbtn, posi, t_name, mkind) {
                        //$('#gvbindBtn').click();
                        if (sbtn == "0") {
                            //green
                            $('#slide-contents').append('<p style=\"margin:0px\">'+cprj+' '+nloc+'休停</p>')
                            //要考慮狀態優先度
                            //如 不停工異常
                            $('#slide-contents').css('background', '#BA55D3');
                        }
                        else if (sbtn == "1") {
                            $('#slide-contents').append('<p style=\"margin:0px\">' + cprj + ' ' + nloc + '進行中</p>')
                            //要考量狀態優先度
                            //如 不停工異常
                            $('#slide-contents').css('background', '#5BFF24');
                        }
                        else {
                            $('#slide-contents').append('<p style=\"margin:0px\">' + cprj + ' ' + nloc + '工程完工</p>')
                            //要考量狀態優先度
                            //如 不停工異常
                            $('#slide-contents').css('background', '#00FFFF');
                        }
                        t_nopr = nlocTrans(nloc.trim());
                        switch (sbtn) {
                            case "0":
                            case "1"://轉為進行
                                $('#' + cprj.trim() + '_' + t_nopr).css('background', '#5BFF24');
                                $('#' + cprj.trim() + '_' + t_nopr).text(w_name);
                                if (sbtn == "0") {
                                    $('#' + cprj.trim() + '_' + t_nopr).attr('onclick', "tipOpen('" + cprj.trim() + '_' + t_nopr + "','1','" + t_name + "')");
                                    getEmplName(t_name, $('#' + cprj.trim() + '_' + t_nopr));                                                                        
                                }
                                break;
                            case "2"://轉為休停
                                $('#' + cprj.trim() + '_' + t_nopr).css('background', '#BA55D3');
                                $('#' + cprj.trim() + '_' + t_nopr).text(w_name);
                                break;
                            case "6"://轉為完工
                                $('#' + cprj.trim() + '_' + t_nopr).css('background', '#00FFFF');
                                $('#' + cprj.trim() + '_' + t_nopr).text(w_name);
                                break;
                            default:
                                $('#' + cprj.trim() + '_' + t_nopr).css('background', '#00FFFF');
                                $('#' + cprj.trim() + '_' + t_nopr).text("default");
                                break;
                        }                        
                    }
                    shub.client.errorMsg = function (cprj, line, nloc, eDesc, eType, posi) {
                        //$('#gvbindBtn').click();
                        t_nopr = nlocTrans(nloc.trim());
                        var ndate = new Date();
                        $('#sct').prepend("<tr id='" + eDesc + ndate.getYear() + '/' + ndate.getMonth() + '/' + ndate.getDate() + ' ' + ndate.getHours() + ':' + ndate.getMinutes() + ':' + ndate.getSeconds() + "'>"
                            + "<td>" + cprj + "</td>"
                            + "<td>" + nloc + "</td>"
                            + "<td>" + ndate.getYear()+'/'+ndate.getMonth()+'/'+ndate.getDate()+' '+ ndate.getHours() +':'+ ndate.getMinutes()+':'+ndate.getSeconds()+"</td>"
                            + "<td>" + eDesc + "</td>"
                            + "</tr>");
                        //$('#' + eDesc + ndate.getYear() + '/' + ndate.getMonth() + '/' + ndate.getDate() + ' ' + ndate.getHours() + ':' + ndate.getMinutes() + ':' + ndate.getSeconds()).css('background', eType == '1' ? '#FF0000' : '#FFFF00');
                        $('#' + cprj.trim() + '_' + t_nopr).css('background', eType);
                        var layUrl = './abnormal_maintain.aspx?cprj=' + cprj + '&line=' + gline + ' &nopr=' + t_nopr;
                        $('#' + cprj.trim() + '_' + t_nopr).attr('onclick', 'LayerOpen("'+layUrl+'");');
                    }
                    shub.client.cancelError = function (sbtn, posi) {
                        $('#gvbindBtn').click();
                    }
                    getErrorNotice(gline);
                    getMainCprjdata_ver2(gline);
                    fieldName_creator();
                    //宣告hub對應client function.en
                    //handshake後監視
                    $.connection.hub.start().done(function () {
                        shub.server.addGroup("sectionKb");
                        $('#CprjBtnTrigger').click(function () {
                            //傳輸狀態
                            shub.server.sendMsg(gline, tri_cprj, gline, tri_nloc, t_status, tri_dsca, w_name, tri_mkind);

                        });
                    });
                    $.connection.hub.disconnected(function () {
                        setTimeout(function () {
                            $.connection.hub.start();
                        }, 5000); // Re-start connection after 5 seconds
                    });
                    //layui-layer-shade on click
                    //暫時抓不出index num因此用實數'1'代替
                    $('#layui-layer-shade' + 1).click(function () {
                        //location.reload();
                    });

                    //第一種作法：
                    $('#test1').on('click', function () {
                        layer.msg('Hello layer', 10, -1);   //2秒后自动关闭，-1代表不显示图标
                    });

                    //第三種作法：
                    $('#test3').on('click', function () {
                        //eg1
                        layer.msg('系統測試', { icon: 6 });
                    });
                    //弹出一个页面层
                    $('#test2').on('click', function () {
                        layer.open({
                            type: 1,
                            area: ['600px', '360px'],
                            shadeClose: true, //点击遮罩关闭
                            content: '\<\div style="padding:20px;">工時收集系統測試\<\/div>'
                        });
                    });
                    //弹出一个tips层
                    $('#test5').on('click', function () {
                        layer.tips('Hello tips!', '#test5');
                    });
                    //iframe层-多媒体
                    $('#test9').on('click', function test99() {
                        layer.open({
                            type: 2,
                            title: false,
                            area: ['880px', '560px'],
                            shade: 0.8,
                            closeBtn: false,
                            shadeClose: true,
                            content: 'http://192.168.101.33/WebTest/5682/ecsfc/abnormal_maintain.aspx'
                        });
                    });
                    $('#test12').on('click', function () {
                        layer.alert('内容', function () {
                            location.reload();
                        })
                    });
                    $.ajax({
                        type: "GET",
                        url: "Handler2.ashx",
                        dataType: "json",
                        success: function (result) {
                            var dataList = $("#cprj");
                            $.each(result, function (i, item) {
                                var optionstr = $("<option>" + item.cprj + "</option>").attr("value", item.cprj)
                                dataList.append(optionstr);
                            });
                        }
                    });
                    //$('.tooltips').tooltipster({
                    //    theme: 'tooltipster-light',
                    //    interactive: true,
                    //    trigger: 'custom',
                    //    triggerOpen: {
                    //        click: false
                    //        ,touchstart: true
                    //    },
                    //    triggerClose: { click: false }
                    //});
                    $('#yolobtn').click(function () {
                        $('.tooltips').tooltipster('close');
                    });
                    $('#nowtimeBtn').click(function () {
                        var nt = new Date();
                        $('#datetimepicker4').val(nt.getFullYear() + '-' + (nt.getMonth() + 1).toString().padLeft(2) + '-' + nt.getDate().toString().padLeft(2) + ' ' + nt.getHours().toString().padLeft(2) + ':' + nt.getMinutes().toString().padLeft(2) + ':' + nt.getSeconds().toString().padLeft(2));
                    });
                //function EndRequestHandler() { test99();}
                });
                $(document).on('click', '.layui-layer-shade', function () {
                    console.log('layui-layer-shade has been clicked!!');
                    upds = 0;
                    edRow.css('border', '0px');
                    edTd.css('border', '0px');
                })
                //$(document).on('mouseover', '.dRow td', function () {
                //    tRow = $(this).closest('tr');
                //    tTd = $(this);
                //    if (tTd.attr('classTri') == '0' && upds == 0) {
                //        tRow.css('border', '2pt dashed black');
                //        tTd.css('border', '2pt solid black');
                //    }
                //})
                //$(document).on('mouseout', '.dRow', function () {                    
                //    if (tTd.attr('classTri') == '0' && upds == 0) {
                //        tRow.css('border', '0px');
                //        tTd.css('border', '0px');
                //    }
                //})
                //$(document).on('click', '.dRow td', function () {
                //    var ttr = $(this).closest('tr');
                //    if (typeof edRow == 'undefined') {
                //        edRow = ttr;
                //        edTd = $(this);
                //        upds = 1;
                //    }
                //    else if (edRow.context == ttr.context) {
                //        upds = upds == 1 ? 0 : 1;
                //    }
                //    else {
                //        edRow.css('border', '0px');
                //        edTd.css('border', '0px');
                //        edRow = ttr;
                //        edTd = $(this);
                //        ttr.css('border', '2pt dashed black');
                //        edTd.css('border', '2pt solid black');
                //        upds = 1;
                //    }
                //    var drTd = $(this);                    
                //    //drTd.attr('classTri', '1');
                //})
                function BindEvents() {
                    $.ajax({
                        type: "GET",
                        url: "Handler2.ashx",
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
                function populateLabel() {
                    alert('hi');
                }
                function specific_abnormal_maintain(cprj,line,nopr,url) {
                    layer.open({//0923 修改layer.js 欲搜尋請查關鍵字"//5682 0923"
                        type: 2,
                        title: false,
                        area: ['880px', '560px'],
                        shade: 0.8,
                        closeBtn: false,
                        shadeClose: true,
                        content: 'http://192.168.101.33/WebTest/5682/ecsfc/abnormal_maintain.aspx'+'?cprj='+cprj+'&line='+line+'&nopr='+nopr
                    });
                }
                function fieldName_creator() {
                    if (gline == "P722") {
                        $('#trh').html(
                        +"<div class='mtbRowCol'>專案</div>"
                        +"<div class='mtbRowCol'>專案</div>"
                        +"<div class='mtbRowCol'>產線</div>"
                        + "<div class='mtbRowCol'>預計進度</div>"
                        + "<div class='mtbRowCol' id='w1'>電氣副站</div>"
                        + "<div class='mtbRowCol' id='w2'>油壓副站</div>"
                        +"<div class='mtbRowCol' id='w3'>一工程</div>"
                        +"<div class='mtbRowCol' id='w4'>二工程</div>"
                        +"<div class='mtbRowCol' id='w5'>三工程</div>"
                        +"<div class='mtbRowCol' id='w6'>四工程</div>"
                        +"<div class='mtbRowCol' id='w7'>五工程</div>"
                        +"<div class='mtbRowCol' id='w8'>六工程</div>"
                        + "<div class='mtbRowCol'>總工時</div>"
                            );
                    }
                }
                function LayerOpen(url) {
                    layer.open({//0923 修改layer.js 欲搜尋請查關鍵字"//5682 0923"
                        type: 2,
                        title: false,
                        area: ['880px', '560px'],
                        shade: 0.8,
                        closeBtn: false,
                        shadeClose: true,
                        //content: 'http://192.168.101.33/WebTest/5682/ecsfc/abnormal_maintain.aspx' + '?cprj=' + cprj + '&line=' + line + '&nopr=' + nopr
                        //content: 'http://192.168.101.33/WebTest/5682/ecsfc/pop_windows.aspx' + '?cprj=' + cprj + '&line=' + line + '&nopr=' + nopr + '&nloc=' + nloc +'&sdate=' + sdate
                        //content: 'http://localhost:61434/pop_windows.aspx' + '?cprj=' + cprj + '&line=' + line + '&nopr=' + nopr + '&nloc=' + nloc +'&sdate=' + sdate
                        content: url
                    });
                }
                function getErrorNotice(line){
                    $.ajax({
                        type: "GET",
                        url: "api/newest/" + line,
                        contentType: "json",
                        dataType: "json",
                        success: function (data) {
                            $.each(data, function (key, value) {
                                var jsonData = JSON.stringify(value);
                                var objData = $.parseJSON(jsonData);
                                $('#sct').append("<tr id='" + objData.puid + "'>"
                                    + "<td>" + objData.cprj + "</td>"
                                    + "<td>" + objData.nloc + "</td>"
                                    + "<td>" + objData.usdtim + "</td>"
                                    + "<td>" + objData.errdsca + "</td>"
                                    +"</tr>");
                                $('#' + objData.puid).css('background', objData.WorkStop == '1' ? '#FF0000' : '#FFFF00');
                            })
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                        }
                    });
                }
                function updateCprj(cprj, nopr, status, emno) {
                    var data123 = {
                        "cprj": "" + cprj + "",
                        "nopr": "" + nopr + "",
                        "status": "" + status + "",
                        "time": "" + $('#datetimepicker4').val() + "",
                        "line": "" + gline + "",
                        "empl": "" + emno.trim() + "",
                        "mode": "0"
                    }
                    tri_cprj = cprj;
                    tri_nloc = noprTrans(nopr.trim(),gline);                    
                    if (status == "7" || status == "8") {
                        t_status = "6";
                    }
                    else {
                        t_status = status == "0" ? "1" : status;
                    }
                    w_name = $('#' + cprj + '_' + nopr.trim()).attr('tw_name');
                    if (gline == "N112") {
                        tri_dsca = $('#' + cprj + '_' + nopr.trim()).attr('t_dsca').trim();
                        tri_mkind = $('#' + cprj + '_' + nopr.trim()).attr('mkind').trim();
                    }
                    $('#CprjBtnTrigger').click();

                    $.ajax({
                        type: "POST",
                        url: "api/lineID/" + $('#linetext').val(),
                        dataType: "text",//dataType 為回傳格式指定
                        contentType: "application/json; charset=utf-8",//contentType為輸入資料格式宣告
                        data: JSON.stringify(data123),
                        success: function (data) {
                            if (data.trim() != '"1"') {
                                console.log("success..." + data);
                                $('.tooltips').tooltipster('close');
                            }
                            else {
                                alert("完成時間不可小於開始時間!!或先補開始接著再補完工!!");
                                location.reload();
                            }
                            $('#' + cprj + '_' + nopr.trim()).attr('onclick', "tipOpen2('" + cprj + '_' + nopr.trim() + "','" + t_status + "')");
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            
                        }
                    });
                    upds = 0;
                    edRow.css('border', '0px');
                    edTd.css('border', '0px');
                    $('#btnSpan').html('');
                    $('.timeCtrlEle').css('opacity', '0');
                }
                function getMainCprjdata(line) {
                    $.ajax({
                        type: "GET",
                        url: "api/lineID/" + line,
                        dataType: "json",
                        success: function (data) {
                            var apData = "";
                            $.each(data, function (key, value) {
                                var jsonData = JSON.stringify(value);//convert to a string 
                                var objData = $.parseJSON(jsonData);//convert to a jsondata
                                if (gline != "N112") {
                                    apData += "<tr class='dRow'>"
                                        + "<td>" + objData.cprj + "</td>"
                                        + "<td>" + objData.line + "</td>"
                                        + "<td>00</td>"
                                        + setSpan(parseInt((objData.w1 == null ? '0' : objData.w1).substr(0, 1)), objData.cprj + '_1', (objData.w1 == null ? '0' : objData.w1).substr(9, 3), '', '', objData.w1.substr(2, 4))
                                        + setSpan(parseInt((objData.w2 == null ? '0' : objData.w2).substr(0, 1)), objData.cprj + '_2', (objData.w2 == null ? '0' : objData.w2).substr(9, 3), '', '', objData.w2.substr(2, 4))
                                        + setSpan(parseInt((objData.w3 == null ? '0' : objData.w3).substr(0, 1)), objData.cprj + '_3', (objData.w3 == null ? '0' : objData.w3).substr(9, 3), '', '', objData.w3.substr(2, 4))
                                        + setSpan(parseInt((objData.w4 == null ? '0' : objData.w4).substr(0, 1)), objData.cprj + '_4', (objData.w4 == null ? '0' : objData.w4).substr(9, 3), '', '', objData.w4.substr(2, 4))
                                        + setSpan(parseInt((objData.w5 == null ? '0' : objData.w5).substr(0, 1)), objData.cprj + '_5', (objData.w5 == null ? '0' : objData.w5).substr(9, 3), '', '', objData.w5.substr(2, 4))
                                        + setSpan(parseInt((objData.w6 == null ? '0' : objData.w6).substr(0, 1)), objData.cprj + '_6', (objData.w6 == null ? '0' : objData.w6).substr(9, 3), '', '', objData.w6.substr(2, 4))
                                        + setSpan(parseInt((objData.w7 == null ? '9' : objData.w7).substr(0, 1)), objData.cprj + '_7', (objData.w7 == null ? '0' : objData.w7).substr(9, 3), '', '', (objData.w7 == null ? '0' : objData.w7).substr(2, 4))
                                        + setSpan(parseInt((objData.w8 == null ? '9' : objData.w8).substr(0, 1)), objData.cprj + '_8', (objData.w8 == null ? '0' : objData.w8).substr(9, 3), '', '', (objData.w8 == null ? '0' : objData.w8).substr(2, 4))
                                        + "<td>00</td>"
                                        + "</tr>";
                                }
                                else {
                                    apData += "<tr class='dRow'>"
                                        + "<td>" + objData.cprj + "</td>"
                                        + "<td>" + objData.line + "</td>"
                                        + "<td>00</td>"
                                        + setSpan(parseInt((objData.w1 == null ? '0' : objData.w1).substr(0, 1)), objData.cprj + '_1', (objData.w1 == null ? '0' : objData.w1).substr(9, 3), objData.t_dsca, objData.mkind, objData.w1.substr(2, 4))
                                        + setSpan(parseInt((objData.w2 == null ? '0' : objData.w2).substr(0, 1)), objData.cprj + '_2', (objData.w2 == null ? '0' : objData.w2).substr(9, 3), objData.t_dsca, objData.mkind, objData.w2.substr(2, 4))
                                        + setSpan(parseInt((objData.w3 == null ? '0' : objData.w3).substr(0, 1)), objData.cprj + '_3', (objData.w3 == null ? '0' : objData.w3).substr(9, 3), objData.t_dsca, objData.mkind, objData.w3.substr(2, 4))
                                        + setSpan(parseInt((objData.w4 == null ? '0' : objData.w4).substr(0, 1)), objData.cprj + '_4', (objData.w4 == null ? '0' : objData.w4).substr(9, 3), objData.t_dsca, objData.mkind, objData.w4.substr(2, 4))
                                        + setSpan(parseInt((objData.w5 == null ? '0' : objData.w5).substr(0, 1)), objData.cprj + '_5', (objData.w5 == null ? '0' : objData.w5).substr(9, 3), objData.t_dsca, objData.mkind, objData.w5.substr(2, 4))
                                        + setSpan(parseInt((objData.w6 == null ? '0' : objData.w6).substr(0, 1)), objData.cprj + '_6', (objData.w6 == null ? '0' : objData.w6).substr(9, 3), objData.t_dsca, objData.mkind, objData.w6.substr(2, 4))
                                        + setSpan(parseInt((objData.w7 == null ? '9' : objData.w7).substr(0, 1)), objData.cprj + '_7', (objData.w7 == null ? '0' : objData.w7).substr(9, 3), objData.t_dsca, objData.mkind, (objData.w7 == null ? '0' : objData.w7).substr(2, 4))
                                        + setSpan(parseInt((objData.w8 == null ? '9' : objData.w8).substr(0, 1)), objData.cprj + '_8', (objData.w8 == null ? '0' : objData.w8).substr(9, 3), objData.t_dsca, objData.mkind, (objData.w8 == null ? '0' : objData.w8).substr(2, 4))
                                        + "<td>00</td>"
                                        + "</tr>";
                                }
                            })
                            $('#cprj_main').append(apData);
                            $('.tooltips').tooltipster({
                                theme: 'tooltipster-light',
                                interactive: true,
                                trigger: 'custom',
                                triggerOpen: {
                                    click: false
                                    ,touchstart: true
                                },
                                triggerClose: { click: true },
                                size: {
                                    height: 300,
                                    width: 500
                                }
                            });
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);

                        }
                    });
                }
                function getMainCprjdata_ver2(line) {
                    $.ajax({
                        type: "GET",
                        url: "api/lineID/" + line,
                        dataType: "json",
                        success: function (data) {
                            var apData = "";
                            $.each(data, function (key, value) {
                                var jsonData = JSON.stringify(value);//convert to a string 
                                var objData = $.parseJSON(jsonData);//convert to a jsondata
                                if (gline != "N112") {
                                    apData += "<div class='mtbRow'>"
                                        + "<div class='mtbRowCol'>" + objData.cprj + "</div>"
                                        + "<div class='mtbRowCol'>" + objData.line + "</div>"
                                        + "<div class='mtbRowCol'>00</div>"
                                        + setSpan2(parseInt((objData.w1 == null ? '0' : objData.w1).substr(0, 1)), objData.cprj + '_1', (objData.w1 == null ? '0' : objData.w1).substr(9, 3), '', '', objData.w1.substr(2, 4))
                                        + setSpan2(parseInt((objData.w2 == null ? '0' : objData.w2).substr(0, 1)), objData.cprj + '_2', (objData.w2 == null ? '0' : objData.w2).substr(9, 3), '', '', objData.w2.substr(2, 4))
                                        + setSpan2(parseInt((objData.w3 == null ? '0' : objData.w3).substr(0, 1)), objData.cprj + '_3', (objData.w3 == null ? '0' : objData.w3).substr(9, 3), '', '', objData.w3.substr(2, 4))
                                        + setSpan2(parseInt((objData.w4 == null ? '0' : objData.w4).substr(0, 1)), objData.cprj + '_4', (objData.w4 == null ? '0' : objData.w4).substr(9, 3), '', '', objData.w4.substr(2, 4))
                                        + setSpan2(parseInt((objData.w5 == null ? '0' : objData.w5).substr(0, 1)), objData.cprj + '_5', (objData.w5 == null ? '0' : objData.w5).substr(9, 3), '', '', objData.w5.substr(2, 4))
                                        + setSpan2(parseInt((objData.w6 == null ? '0' : objData.w6).substr(0, 1)), objData.cprj + '_6', (objData.w6 == null ? '0' : objData.w6).substr(9, 3), '', '', objData.w6.substr(2, 4))
                                        + setSpan2(parseInt((objData.w7 == null ? '9' : objData.w7).substr(0, 1)), objData.cprj + '_7', (objData.w7 == null ? '0' : objData.w7).substr(9, 3), '', '', (objData.w7 == null ? '0' : objData.w7).substr(2, 4))
                                        + setSpan2(parseInt((objData.w8 == null ? '9' : objData.w8).substr(0, 1)), objData.cprj + '_8', (objData.w8 == null ? '0' : objData.w8).substr(9, 3), '', '', (objData.w8 == null ? '0' : objData.w8).substr(2, 4))
                                        + "<div class='mtbRowCol'>00</div>"
                                        + "</div>";
                                }
                                else {
                                    apData += "<div class='mtbRow'>"
                                        + "<div class='mtbRowCol'>" + objData.cprj + "</div>"
                                        + "<div class='mtbRowCol'>" + objData.line + "</div>"
                                        + "<div class='mtbRowCol'>00</div>"
                                        + setSpan2(parseInt((objData.w1 == null ? '0' : objData.w1).substr(0, 1)), objData.cprj + '_1', (objData.w1 == null ? '0' : objData.w1).substr(9, 3), objData.t_dsca, objData.mkind, objData.w1.substr(2, 4))
                                        + setSpan2(parseInt((objData.w2 == null ? '0' : objData.w2).substr(0, 1)), objData.cprj + '_2', (objData.w2 == null ? '0' : objData.w2).substr(9, 3), objData.t_dsca, objData.mkind, objData.w2.substr(2, 4))
                                        + setSpan2(parseInt((objData.w3 == null ? '0' : objData.w3).substr(0, 1)), objData.cprj + '_3', (objData.w3 == null ? '0' : objData.w3).substr(9, 3), objData.t_dsca, objData.mkind, objData.w3.substr(2, 4))
                                        + setSpan2(parseInt((objData.w4 == null ? '0' : objData.w4).substr(0, 1)), objData.cprj + '_4', (objData.w4 == null ? '0' : objData.w4).substr(9, 3), objData.t_dsca, objData.mkind, objData.w4.substr(2, 4))
                                        + setSpan2(parseInt((objData.w5 == null ? '0' : objData.w5).substr(0, 1)), objData.cprj + '_5', (objData.w5 == null ? '0' : objData.w5).substr(9, 3), objData.t_dsca, objData.mkind, objData.w5.substr(2, 4))
                                        + setSpan2(parseInt((objData.w6 == null ? '0' : objData.w6).substr(0, 1)), objData.cprj + '_6', (objData.w6 == null ? '0' : objData.w6).substr(9, 3), objData.t_dsca, objData.mkind, objData.w6.substr(2, 4))
                                        + setSpan2(parseInt((objData.w7 == null ? '9' : objData.w7).substr(0, 1)), objData.cprj + '_7', (objData.w7 == null ? '0' : objData.w7).substr(9, 3), objData.t_dsca, objData.mkind, (objData.w7 == null ? '0' : objData.w7).substr(2, 4))
                                        + setSpan2(parseInt((objData.w8 == null ? '9' : objData.w8).substr(0, 1)), objData.cprj + '_8', (objData.w8 == null ? '0' : objData.w8).substr(9, 3), objData.t_dsca, objData.mkind, (objData.w8 == null ? '0' : objData.w8).substr(2, 4))
                                        + "<div class='mtbRowCol'>00</div>"
                                        + "</div>";
                                }
                            })
                            $('#mtb').append(apData);
                            //$('.tooltips').tooltipster({
                            //    theme: 'tooltipster-light',
                            //    interactive: true,
                            //    trigger: 'custom',
                            //    triggerOpen: {
                            //        click: false
                            //        , touchstart: true
                            //    },
                            //    triggerClose: { click: true },
                            //    size: {
                            //        height: 300,
                            //        width: 500
                            //    }
                            //});
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);

                        }
                    });
                }
                function setSpan(status, id, name, t_dsca, mkind, emno) {
                    var cprj = id.substr(0, id.search('_'));
                    var ss = id.substr(-1, 1);
                    switch (status) {
                        case 0://未開
                            return "<td id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" class='tooltips' data-tooltip-content='#tooltip_content' >未開工</td>";
                            break;
                        case 1://進行
                            return "<td id='" + id + "' classTri='0' stext='補休停' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" class='tooltips' data-tooltip-content='#tooltip_content'  style='background-color:#5BFF24;'>" + name + "</td>";
                            break;
                        case 2://休停
                            return "<td id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" class='tooltips' data-tooltip-content='#tooltip_content'  style='background-color:#BA55D3;'>" + name + "</td>";
                            break;
                        case 3://紅異
                            return "<td id='" + id + "' classTri='0' stext='紅異' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"LayerOpen('./abnormal_maintain.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" class='tooltips' data-tooltip-content='#tooltip_content'  style='background-color:#FF0000;'>" + name + "</td>";
                            break;
                        case 4://黃異
                            return "<td id='" + id + "' classTri='0' stext='黃異' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"LayerOpen('./abnormal_maintain.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" class='tooltips' data-tooltip-content='#tooltip_content'  style='background-color:#FFFF00;'>" + name + "</td>";
                            break;
                        case 5://待復工
                            return "<td id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" class='tooltips' data-tooltip-content='#tooltip_content'  style='background-color:#FFBB66;'>待復工</td>";
                            break;
                        case 6://已完工
                            return "<td id='" + id + "' classTri='0' t_dsca='" + t_dsca + "' mkind='" + mkind + "' style='background-color:#00FFFF;'>已完工</td>";
                            break;
                        default:
                            return "<td id='" + id + "' classTri='0' t_dsca='" + t_dsca + "' mkind='" + mkind + "'>-----</td>";
                            break;
                    }
                }
                function setSpan2(status, id, name, t_dsca, mkind, emno) {
                    var cprj = id.substr(0, id.search('_'));
                    var ss = id.substr(-1, 1);
                    switch (status) {
                        case 0://未開
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" data-tooltip-content='#tooltip_content' >未開工</div>";
                            break;
                        case 1://進行
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='補休停' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\"  data-tooltip-content='#tooltip_content'  style='background-color:#5BFF24;'>" + name + "</div>";
                            break;
                        case 2://休停
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\"  data-tooltip-content='#tooltip_content'  style='background-color:#BA55D3;'>" + name + "</div>";
                            break;
                        case 3://紅異
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='紅異' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"LayerOpen('./abnormal_maintain.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" data-tooltip-content='#tooltip_content'  style='background-color:#FF0000;'>" + name + "</div>";
                            break;
                        case 4://黃異
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='黃異' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"LayerOpen('./abnormal_maintain.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" data-tooltip-content='#tooltip_content'  style='background-color:#FFFF00;'>" + name + "</div>";
                            break;
                        case 5://待復工
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' stext='補開工' t_dsca='" + t_dsca + "' mkind='" + mkind + "' tw_name='" + name + "' onclick=\"tipOpen2('" + id + "','" + status + "','" + emno + "')\" data-tooltip-content='#tooltip_content'  style='background-color:#FFBB66;'>待復工</div>";
                            break;
                        case 6://已完工
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' t_dsca='" + t_dsca + "' mkind='" + mkind + "' style='background-color:#00FFFF;'>已完工</div>";
                            break;
                        default:
                            return "<div class='mtbRowCol tooltips' id='" + id + "' classTri='0' t_dsca='" + t_dsca + "' mkind='" + mkind + "'>-----</div>";
                            break;
                    }
                }
                function tipOpen(tid, status, emno) {
                    console.log(tid + '+' + status);
                    var ss = tid.substr(-1, 1);//取工程站     
                    var cprj = tid.substr(0, tid.search('_'));                    
                    $('#' + tid).tooltipster('open');
                    var nt = new Date();
                    $('#datetimepicker4').val(nt.getFullYear() + '-' + (nt.getMonth() + 1).toString().padLeft(2) + '-' + nt.getDate().toString().padLeft(2) + ' ' + nt.getHours().toString().padLeft(2) + ':' + nt.getMinutes().toString().padLeft(2) + ':' + nt.getSeconds().toString().padLeft(2));
                    switch (parseInt(status)) {
                        case 0:
                            $('#btnSpan').html("<input id=\"s1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','1','" + emno + "')\" value=\"補開工\" class=\"btn btn-primary\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作' class=\"btn btn-warning\" style=\"background-color: #00AAAA;border-color:#00AAAA\" >"
                                );
                            break;
                        case 1:
                            $('#btnSpan').html("<input id=\"p2\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','2','" + emno + "')\" style=\"background-color: #B94FFF;border-color:#B94FFF\" value=\"補休停\" class=\"btn btn-success\"/>"
                                + "<input id=\"p1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','7','" + emno + "')\" style=\"color: white; \" value=\"補完工\" class=\"btn btn-warning\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作'  style=\"background-color: #00AAAA;border-color:#00AAAA\" class=\"btn btn-warning\">"
                                );
                            break;
                        case 2:
                        case 5:
                            $('#btnSpan').html("<input id=\"s2\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','0','" + emno + "')\" style=\"color: white; \" value=\"補開始\" class=\"btn btn-success\"/>"
                                + "<input id=\"p1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','8','" + emno + "')\" style=\"color: white; \" value=\"補完工\" class=\"btn btn-warning\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作'  style=\"background-color: #00AAAA;border-color:#00AAAA\" class=\"btn btn-warning\">"
                                );
                            break;                                                    
                        default:
                            break;
                    }
                }
                function tipOpen2(tid, status, emno) {
                    console.log(tid + '+' + status);
                    var ss = tid.substr(-1, 1);//取工程站     
                    var cprj = tid.substr(0, tid.search('_'));                    
                    var nt = new Date();
                    $('#datetimepicker4').val(nt.getFullYear() + '-' + (nt.getMonth() + 1).toString().padLeft(2) + '-' + nt.getDate().toString().padLeft(2) + ' ' + nt.getHours().toString().padLeft(2) + ':' + nt.getMinutes().toString().padLeft(2) + ':' + nt.getSeconds().toString().padLeft(2));
                    switch (parseInt(status)) {
                        case 0:
                            $('.timeCtrlEle').css('opacity', '1');
                            $('#btnSpan').html("<input id=\"s1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','1','" + emno + "')\" value=\"補開工\" class=\"btn btn-primary\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作' class=\"btn btn-warning\" style=\"background-color: #00AAAA;border-color:#00AAAA\" >"
                                );
                            break;
                        case 1:
                            $('.timeCtrlEle').css('opacity', '1');
                            $('#btnSpan').html("<input id=\"p2\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','2','" + emno + "')\" style=\"background-color: #B94FFF;border-color:#B94FFF\" value=\"補休停\" class=\"btn btn-success\"/>"
                                + "<input id=\"p1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','7','" + emno + "')\" style=\"color: white; \" value=\"補完工\" class=\"btn btn-warning\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作'  style=\"background-color: #00AAAA;border-color:#00AAAA\" class=\"btn btn-warning\">"
                                );
                            break;
                        case 2:
                        case 5:
                            $('.timeCtrlEle').css('opacity', '1');
                            $('#btnSpan').html("<input id=\"s2\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','0','" + emno + "')\" style=\"color: white; \" value=\"補開始\" class=\"btn btn-success\"/>"
                                + "<input id=\"p1\" type=\"button\" onclick=\"updateCprj('" + cprj + "','" + ss + "','8','" + emno + "')\" style=\"color: white; \" value=\"補完工\" class=\"btn btn-warning\"/>"
                                + "<input type='button'  onclick=\"LayerOpen('./cprj_worker_specific.aspx?cprj=" + cprj + "&line=" + gline + " &nopr=" + ss + "')\" value='指派協作'  style=\"background-color: #00AAAA;border-color:#00AAAA\" class=\"btn btn-warning\">"
                                );
                            break;
                        default:
                            break;
                    }
                }

                function statusUpdate(statusType, cprj, nopr, targetdata) {

                }
                function load() { Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); }
                function EndRequestHandler() { BindEvents(); }
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
                        getEmplName(empl);
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
                function getEmplName(emno,tarElement) {
                    var transaction = db.transaction(["employee"]);
                    var objectStore = transaction.objectStore("employee");
                    var request = objectStore.get(emno);
                    request.onsuccess = function (event) {
                        console.log(tarElement.text());
                        tarElement.text(request.result.objt_name);
                        console.log('get empl success! ' + request.result.objt_name);                        
                    }
                    request.onerror = function (event) {
                        console.log('get failed!!');                        
                    }
                }



</script>
    <script>
        // Window load event used just in case window height is dependant upon images
        $(window).bind("load", function () {

            var footerHeight = 0,
            footerTop = 0,
            $footer = $("#footer");

            positionFooter();

            function positionFooter() {

                footerHeight = $footer.height();
                footerTop = ($(window).scrollTop() + $(window).height() - footerHeight) + "px";

                if (($(document.body).height() + footerHeight) < $(window).height()) {
                    $footer.css({
                        position: "absolute"
                    }).animate({
                        top: footerTop
                    });
                } else {
                    $footer.css({
                        position: "static"
                    });
                }

            }

            $(window)
            .scroll(positionFooter)
            .resize(positionFooter)

        });    </script>

</head>
<body data-spy="scroll">    
        <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <div class="bgimage temphi">                
                <img id="icon" src="./images/OR_Logo.jpg" alt="" />
                <div id="SysNameTag"><asp:Label ID="Label7" runat="server" Text="產線工時收集系統" Font-Bold="True" Font-Names="微軟正黑體" Font-Size="55px"></asp:Label></div>                    
                <div id="userInfBloc">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label18" runat="server" ></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="logout" CssClass="btn btn-default" runat="server" Text="登出" OnClick="logout_Click"  />
                </div>
            </div>            
            <div class="tooltip_templates" style="display: none;">
            </div>
<%--            <div class="row">
                <div class="col-md-12">
                            <asp:Table ID="Table1" runat="server" Style="text-align: left" Font-Names="微軟正黑體" Visible='<%#Convert.ToBoolean(Session["manager_visible"].ToString()) %>' Width="687px" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Button ID="Button2" runat="server" Visible='True' Height="57px" Font-Names="微軟正黑體" Text="補輸入報工日期時間" Width="190px" Font-Size="Larger" OnClick="Button2_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="Button3" runat="server" Visible='True' Height="57px" Font-Names="微軟正黑體" Text="異常狀態處理" Width="130px" Font-Size="Larger" OnClick="Button3_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="Button4" runat="server" Visible='True' Height="57px" Font-Names="微軟正黑體" Text="異常主檔維護" Width="130px" Font-Size="Larger" OnClick="Button4_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="Button1" runat="server" Visible='True' Height="57px" Font-Names="微軟正黑體" Text="指定工程作業人員" Width="180px" Font-Size="Larger" OnClick="Button1_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="Button6" runat="server" Text="進度管理看板" OnClick="Button6_Click" Height="57px" Font-Names="微軟正黑體" Width="130px" Font-Size="Larger" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                </div>
            </div>--%>
            <div id="ctrlBox">
                <table id="tooltip_content">
                    <tr class="timeCtrlEle">
                        <td>
                            <%--<input id="dtinput" type="datetime-local" style="color: black" />--%>
                            <input class="form-control" id="datetimepicker4" style="color: black" />
                            
                        </td>
                        <%--<strong>This is the content of my tooltip!</strong><br />--%>
                    </tr>
                    <tr>
                        <td class="timeCtrlEle">
                            <input id="nowtimeBtn" type="button" value="現在時間" class="btn btn-default" style="color: black;" />
                            <span id="btnSpan" style="font-family: 'Microsoft JhengHei'"></span>
                        </td>
                    </tr>
                </table>
            </div>
                                <div id="trh" class="mtbRow head">
                                    <div class="mtbRowCol">專案</div>
                                    <div class="mtbRowCol">產線</div>
                                    <div class="mtbRowCol">預計進度</div>
                                    <div class="mtbRowCol" id="w1">工程一</div>
                                    <div class="mtbRowCol" id="w2">工程二</div>
                                    <div class="mtbRowCol" id="w3">工程三</div>
                                    <div class="mtbRowCol" id="w4">工程四</div>
                                    <div class="mtbRowCol" id="w5">工程五</div>
                                    <div class="mtbRowCol" id="w6">工程六</div>
                                    <div class="mtbRowCol" id="w7">工程七</div>
                                    <div class="mtbRowCol" id="w8">工程八</div>
                                    <div class="mtbRowCol">總工時</div>
                                </div>
                            <div id="mainDataTB">
                                <input id="CprjBtnTrigger" style="display:none" type="button" value="button" />
<%--                                <table id="cprj_main" style="width: 100%; text-align: center;font-family:'Microsoft JhengHei'">
                                    <tr id="trh">
                                        <th style="text-align: center">專案</th>
                                        <th style="text-align: center">產線</th>
                                        <th style="text-align: center">預計進度</th>
                                        <th style="text-align: center" id="w1">工程一</th>
                                        <th style="text-align: center" id="w2">工程二</th>
                                        <th style="text-align: center" id="w3">工程三</th>
                                        <th style="text-align: center" id="w4">工程四</th>
                                        <th style="text-align: center" id="w5">工程五</th>
                                        <th style="text-align: center" id="w6">工程六</th>
                                        <th style="text-align: center" id="w7">工程七</th>
                                        <th style="text-align: center" id="w8">工程八</th>
                                        <th style="text-align: center">總工時</th>
                                    </tr>
                                </table>--%>
                                <div id="mtb"></div>
                            </div>
            <div id="slide">
                <div id="slide-in">
                    <div id="open-btn">
                        <img src="images/open-btn.gif" width="20" height="20" />
                    </div>
                    <h3></h3>
                    <div id="slide-contents" style="overflow: scroll">
                        <table id="sct" style="color: black;" border="1">
                        </table>
                    </div>
                </div>
            </div>
        </form>
        <script type="text/javascript">            
            //function updateGV() {
            //    console.log('shadow click!!');
            //    $('#gvbindBtn').click();
            //}
            //刪除第一行以外資料行
            //使用deferred實現執行完才執行下任務的目標
            var refm = function rem() {
                var r = $.Deferred();
                $('#cprj_main tr:not(:first)').remove();
                return r;
            }
            function nlocTrans(nloc) {
                switch (nloc) {
                    case "工程一":
                    case "電氣副站":
                        return "1";
                        break;
                    case "工程二":
                    case "油壓副站":
                        return "2";
                        break;
                    case "工程三":
                    case "一工程":
                        return "3";
                        break;
                    case "工程四":
                    case "二工程":
                        return "4";
                        break;
                    case "工程五":
                    case "三工程":
                        return "5";
                        break;
                    case "工程六":
                    case "四工程":
                        return "6";
                        break;
                    case "工程七":
                    case "五工程":
                        return "7";
                        break;
                    default:
                        return "8";
                        break;
                }
            }
            function timePickLayer() {
                layer.open({
                    type: 2,
                    title: false,
                    area: ['780px', '360px'],
                    closeBtn: 0,
                    shadeClose: false,
                    skin: 'Layerclass1',
                    content: './dateTimeSelectPage.html'
                });
            }
            $(function () {
                $('#datetimepicker4').click(function () {
                    timePickLayer();
                });
                $('#datetimepicker1').datetimepicker({
                    showTodayButton: true,
                    format: "DD-MM-YYYY HH:mm",
                    inline: true,
                    sideBySide: true
                });
                //$('#datetimepicker5').datetimepicker({
                //    showTodayButton: true,
                //    format: "DD-MM-YYYY HH:mm",
                //    sideBySide: true,
                //    inline: true,
                //    widgetPositioning: {
                //        horizontal: 'left',
                //        vertical: 'bottom'
                //    }
                //});              
            })
        </script>
    <div id="colorExplnBlock">
        <div class="subBlock">
            <div class="blockName">
                <h3>預計進度顏色說明</h3>
            </div>
            <div class="colorchip">
                <div class="color_tag">如期進行</div>
                <div class="color_block color_block_green"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">尚未開始</div>
                <div class="color_block"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">落後進度</div>
                <div class="color_block color_block_orange"></div>
            </div>
        </div>
        <div class="subBlock">
            <div class="blockName">
                <h3>工程狀態顏色說明</h3>
            </div>
            <div class="colorchip">
                <div class="color_tag">進行中</div>
                <div class="color_block color_block_green"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">休停中</div>
                <div class="color_block color_block_purple"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">已完工</div>
                <div class="color_block color_block_lightBlue"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">異常完工</div>
                <div class="color_block color_block_darkBlue"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">普通異常</div>
                <div class="color_block color_block_yellow"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">停工異常</div>
                <div class="color_block color_block_red"></div>
            </div>
            <div class="colorchip">
                <div class="color_tag">已復工</div>
                <div class="color_block color_block_orange"></div>
            </div>
        </div>
    </div>
    <div id="btnColl">
        <div class="lbtn">
            <div id="slideBtn">
                <div class="s">></div>
            </div>
            <div id="locaCBtn" class="t">工位調整</div>
        </div>
        <div class="lbtn">
            <div id="errStatMaint"  class="t">異常狀態處理</div>
        </div>
        <div class="lbtn">
            <div id="errMainDtMaint"  class="t">異常主檔維護</div>
        </div>
        <div id="workerSpec"  class="lbtn">
            <div class="t">指定工程人員</div>
        </div>
        <div id="skanban"  class="lbtn">
            <div class="t">進度管理看板</div>
        </div>
    </div>
    <script src="Scripts/ecsfcMain/mangePage.js"></script>
</body>
</html>
