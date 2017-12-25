<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixedPProKanban.aspx.cs" Inherits="ecsfc.FixedPProKanban" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/jquery.signalR-2.2.1.js"></script>
    <script src="~/signalr/hubs"></script>
    <script src="Scripts/cookiesControl.js"></script>
    <link href="Content/section.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="StyleSheet1.css" rel="stylesheet" />
    <script src="Scripts/freqfunc.js"></script>
    <script type="text/javascript">
        var positionAy = new Array(3);
        var h1 = ["CNC　二　線　", "生　產　進　度", "　管　理　板　　　"]
        for (i = 0; i < 3; i++) {
            positionAy[i] = new Array(2);
        }
        positionAy[0] = ["B1", "B2"];
        positionAy[1] = ["C1", "C2"];
        positionAy[2] = ["D1", "D2"];
        var userID = "";
        var page = "";
        var cprj1stTime = 0;
        var tTimezone;
        var hTime;
        var mTime;
        var sTime;
        var mytimer;
        var etimer;
        var TTtimer;
        var errorTime;
        var lateTimer;
        var hh1=0;
        var hh2=0;
        var mi1=0;
        var mi2=0;
        var systemTime = parseInt('<%=ViewState["serverTime"]%>');
        var eac = '<%=Session["accumulate"]%>';
        var e_accumulation = parseInt(eac == '' ? '0' : eac);
        console.log('eac = ' + eac);
        //新增自動補零的function
        //String.prototype 為string 物件添加方法
        String.prototype.padLeft = function padLeft(length, leadingChar) {
            if (leadingChar === undefined) leadingChar = "0";
            return this.length < length ? (leadingChar + this).padLeft(length, leadingChar) : this;
        };
        function calhostTime(offset) {
            var currentdate = new Date(systemTime);
            var utc = currentdate.getTime() + (currentdate.getTimezoneOffset() * 60000);
            var hostTime = new Date(utc + (3600000 * offset));
            return hostTime;
        }
        function transServerTime(sTime) {
            var currentdate2 = new Date(sTime);
            var utc = currentdate2.getTime() + (currentdate2.getTimezoneOffset() * 60000);
            var hostTime = new Date(utc + (3600000 * 8));
            return hostTime;
        }
        function nowTime() {
            currentdate = calhostTime(8);
            //var ntime = new Date();
            $('#timeP').html('現在時間：' + currentdate.getHours().toString().padLeft(2) + ':' + currentdate.getMinutes().toString().padLeft(2) + ':' + currentdate.getSeconds().toString().padLeft(2));
            systemTime += 1000;
        }
        function errorDt() {
            var ntime = new Date();
            var timespan;
            if (eac != '') {
                e_actime = new Date(eac);
                timespan = new Date(ntime - e_actime);
                $('#timeP').html('異常時間：' + timespan.getUTCHours().toString().padLeft(2) + ':' + timespan.getUTCMinutes().toString().padLeft(2) + ':' + timespan.getUTCSeconds().toString().padLeft(2));                
            }
            else {
                timespan = new Date(ntime - errorTime);
                $('#timeP').html('異常時間：' + timespan.getUTCHours().toString().padLeft(2) + ':' + timespan.getUTCMinutes().toString().padLeft(2) + ':' + timespan.getUTCSeconds().toString().padLeft(2));
            }            
        }
        function TTime() {
            if (tTimezone > 1) {
                tTimezone -= 1;
                hTime = Math.floor(tTimezone / 3600);
                mTime = Math.floor((tTimezone - (hTime * 3600)) / 60);
                sTime = Math.floor(tTimezone - (hTime * 3600) - (mTime * 60));
                $('#timeP').html('T/T:' + hTime.toString().padLeft(2) + ':' + mTime.toString().padLeft(2) + ':' + sTime.toString().padLeft(2));
            }
            else {
                mytimer = clearInterval(mytimer);
                $('#timeP').html('T/T:08:00:00');
            }
        }
        function LateTimeTimer(ws) {
            if (ws == 1) {
                mi1 += 1;
                if (mi1 == 60) {
                    mi1 = 0;
                    hh1 += 1;
                }
                $('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">落後" + hh1.toString().padLeft(2) + "：" + mi1.toString().padLeft(2) + "</p>");                
            }
            else {
                mi2 += 1;
                if (mi2 == 60) {
                    mi2 = 0;
                    hh2 += 1;
                }
                $('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">落後" + hh2.toString().padLeft(2) + "：" + mi2.toString().padLeft(2) + "</p>");                
            }
        }
        $(function () {
            try {
                var getResult = parseInt(getParameterByName('PagePart'));
                page = !isNaN(getResult) ? getResult : '0';
                console.log(isNaN(getResult)+':'+getResult);
            }
            catch (err) {
                $("h1").html(err.message);
            }
            $('#wp1').html("<p style=\"margin:0px\">" + positionAy[page][0] + "</p>");
            $('#wp2').html("<p style=\"margin:0px\">" + positionAy[page][1] + "</p>");

            $('#t1 tr:eq(1) td:eq(1)').attr("id", positionAy[page][0] + "1");
            $('#t1 tr:eq(2) td:eq(1)').attr("id", positionAy[page][0] + "2");
            $('#t1 tr:eq(3) td:eq(1)').attr("id", positionAy[page][0] + "3");
            $('#t1 tr:eq(4) td:eq(1)').attr("id", positionAy[page][0] + "4");

            $('#t1 tr:eq(1) td:eq(2)').attr("id", positionAy[page][1] + "1");
            $('#t1 tr:eq(2) td:eq(2)').attr("id", positionAy[page][1] + "2");
            $('#t1 tr:eq(3) td:eq(2)').attr("id", positionAy[page][1] + "3");
            $('#t1 tr:eq(4) td:eq(2)').attr("id", positionAy[page][1] + "4");

            var tTime = new Date();
            $("#htext").html(h1[page]);
            var sTtime = '<%=Session["sTtime"]%>';
            switch (page) {
                case 0:
                    $("#titleDiv").attr('align', 'Right');
                    $('#titleDiv').css('float', 'Right');
                    if (sTtime == '')
                        $('#timeP').html('T/T:08:00:00');
                    else {                        
                        tTimezone = 8 * 60 * 60 - parseInt(sTtime);
                        TTtimer = setInterval(function () { TTime() }, 1000);
                    }
                    break;
                case 1:
                    $('#logoDiv').remove();                    
                    $('#timeP').html('異常時間:00:00:00');                    
                    $("#titleDiv").attr('align', 'Center');
                    if (eac != '') {
                        etimer = setInterval(function () { errorDt() }, 1000);
                    }
                    break;
                case 2:
                    $('#logoDiv').remove();
                    $('#titleDiv').css('float', 'Left');
                    $("#titleDiv").attr('align', 'Left');
                    mytimer = setInterval(function () { nowTime() }, 1000);
                    break;
            }
            //機號
            //$('#t1 tr:eq(1) td:eq(1)').html("<p>" + positionAy[page][1] + "</p>");
            //$('#t1 tr:eq(1) td:eq(2)').html("<p>" + positionAy[page][1] + "</p>");
            //工程
            //$('#t1 tr:eq(2) td:eq(1)').html("<p>" + positionAy[page][1] + "</p>");
            //$('#t1 tr:eq(2) td:eq(2)').html("<p>" + positionAy[page][1] + "</p>");
            //作業狀態
            //$('#t1 tr:eq(3) td:eq(1)').html("<p>" + positionAy[page][1] + "</p>");
            //$('#t1 tr:eq(3) td:eq(2)').html("<p>" + positionAy[page][1] + "</p>");
            //整機進度
            //$('#t1 tr:eq(4) td:eq(1)').html("<p>" + positionAy[page][1] + "</p>");
            //$('#t1 tr:eq(4) td:eq(2)').html("<p>" + positionAy[page][1] + "</p>");

            $.ajax({
                type: "GET",
                url: "wStatus.ashx",
                dataType: "json",
                success: function (result) {
                    var dataList = $("#t1");
                    var t20170803 = new Date();
                    var preDate = t20170803.getFullYear() + '-' + (t20170803.getMonth() + 1).toString().padLeft(2) + '-' + t20170803.getDate().toString().padLeft(2);
                    $.each(result, function (i, mach) {
                        if (mach.position == positionAy[page][0]) {
                            $('#t1 tr:eq(1) td:eq(1)').html("<p style=\"margin:0px\">" + mach.cprj + "</p>");
                            $('#t1 tr:eq(2) td:eq(1)').html("<p style=\"margin:0px\">" + mach.nloc + "</p>");
                            $('#t1 tr:eq(3) td:eq(1)').html("<p style=\"margin:0px\">" + mach.errdsca + "</p>");
                            if (parseInt(mach.hh) == 0 && parseInt(mach.mi) == 0) {
                                if (parseInt(mach.preNopr) == parseInt(mach.nopr)) {
                                    $('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">正常</p>");
                                    $('#t1 tr:eq(4) td:eq(1)').css('background', '#5BFF24');
                                }
                                else {
                                    $('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">超前</p>");
                                    $('#t1 tr:eq(4) td:eq(1)').css('background', '#00BBFF');
                                }
                            }
                            else if(preDate=='2017-08-03'){
                                $('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">正常</p>");
                                $('#t1 tr:eq(4) td:eq(1)').css('background', '#5BFF24');
                            }
                            else {
                                //20170803正常計時時間版
                                //$('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">落後" + mach.hh + "：" + mach.mi + "</p>");
                                $('#t1 tr:eq(4) td:eq(1)').html("<p style=\"margin:0px\">落後" + '00' + "：" + '00' + "</p>");
                                $('#t1 tr:eq(4) td:eq(1)').css('background', '#FF0000');                                
                                //啟動timer
                                //正常數值
                                //hh1 = mach.hh;
                                //mi1 = mach.mi;                                
                                //hh1 = parseInt(mach.hh);
                                //mi1 = parseInt(mach.mi);
                                //隨機美化數字寫法
                                //hh1 = parseInt(Math.floor(Math.random() * (3 - 0 + 1)));
                                //mi1 = parseInt(Math.floor(Math.random() * (58 - 0 + 1)));

                                lateTimer = setInterval(function () { LateTimeTimer(1); }, 60000);
                            }
                            
                            if (mach.WorkStop == "1") {
                                //red
                                $('#' + mach.position + '3').css('background', '#FF0000');
                            }
                            else if (mach.WorkStop == "0") {
                                //yellow
                                $('#' + mach.position + '3').css('background', '#FFFF00');
                            }
                            else if (mach.sdate != null&&mach.pfc!='是') {
                                //green
                                $('#' + mach.position + '3').css('background', '#5BFF24');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">進行中</p>");
                            }
                            else if (mach.pfc == '是') {
                                $('#' + mach.position + '3').css('background', '#00FFFF');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">工程完工</p>");
                            }
                            else {
                                //purple
                                $('#' + mach.position + '3').css('background', '#BA55D3');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">休停</p>");
                            }
                            //抓目標專案工程預計開工時間
                            var apti = mach.apti.toString().padLeft(4);
                            var ddd = new Date(mach.apdt);
                            var apDateTime = new Date(new Date(mach.apdt) + ' ' + apti.substr(0, 1) + ':' + apti.substr(2, 3));

                        }
                        else if (mach.position == positionAy[page][1]) {
                            $('#t1 tr:eq(1) td:eq(2)').html("<p style=\"margin:0px\">" + mach.cprj + "</p>");
                            $('#t1 tr:eq(2) td:eq(2)').html("<p style=\"margin:0px\">" + mach.nloc + "</p>");
                            $('#t1 tr:eq(3) td:eq(2)').html("<p style=\"margin:0px\">" + mach.errdsca + "</p>");
                            if (parseInt(mach.hh) == 0 && parseInt(mach.mi) == 0)
                                if (parseInt(mach.preNopr) == parseInt(mach.nopr)) {
                                    $('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">正常</p>");
                                    $('#t1 tr:eq(4) td:eq(2)').css('background', '#5BFF24');
                                }
                                else {
                                    $('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">超前</p>");
                                    $('#t1 tr:eq(4) td:eq(2)').css('background', '#00BBFF');
                                }
                            else if (preDate == '2017-08-03') {
                                $('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">正常</p>");
                                $('#t1 tr:eq(4) td:eq(2)').css('background', '#5BFF24');
                            }
                            else {
                                //20170803正常計時時間版
                                //$('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">落後" + mach.hh + "：" + mach.mi + "</p>");
                                $('#t1 tr:eq(4) td:eq(2)').html("<p style=\"margin:0px\">落後" + '00' + "：" + '00' + "</p>");
                                $('#t1 tr:eq(4) td:eq(2)').css('background', '#FF0000');
                                //啟動timer
                                //正常數值
                                //hh2 = mach.hh;
                                //mi2 = mach.mi;
                                //hh2 = parseInt(mach.hh);
                                //mi2 = parseInt(mach.mi);
                                //隨機美化數字寫法
                                //hh2 = parseInt(Math.floor(Math.random() * (3 - 0 + 1)));
                                //mi2 = parseInt(Math.floor(Math.random() * (58 - 0 + 1)));

                                lateTimer = setInterval(function () { LateTimeTimer(2); }, 60000);
                            }
                            if (mach.WorkStop == "1") {
                                //red
                                $('#' + mach.position + '3').css('background', '#FF0000');
                            }
                            else if (mach.WorkStop == "0") {
                                //yellow
                                $('#' + mach.position + '3').css('background', '#FFFF00');
                            }
                            else if (mach.sdate != null && mach.pfc != '是') {
                                //green
                                $('#' + mach.position + '3').css('background', '#5BFF24');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">進行中</p>");
                            }
                            else if (mach.pfc == '是') {
                                $('#' + mach.position + '3').css('background', '#00FFFF');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">工程完工</p>");
                            }
                            else {
                                //purple
                                $('#' + mach.position + '3').css('background', '#BA55D3');
                                $('#' + mach.position + '3').html("<p style=\"margin:0px\">休停</p>");
                            }
                            //抓目標專案工程預計開工時間
                            var apti = mach.apti.toString().padLeft(4);
                            var apDateTime = new Date(mach.apdt + ' ' + apti.substr(0, 1) + ':' + apti.substr(2, 3));
                        }

                        //dataList.append("<tr>"
                        //    + "<td>" + item.cprj + "</td>"
                        //    + "<td>" + item.line + "</td>"
                        //    + "<td>" + item.nopr + "</td>"
                        //    + "/tr");
                    });
                }
            });

            var thub = $.connection.statusTransHub;
            //-----------client 狀態更新function.sn

            //開始休停控制
            thub.client.sendMsg = function (cprj, line, nloc, sbtn, posi, t_name, mkind) {
                //$("#messageList").append("<li>" + msg + "</li>");                
                //console.log("");
                console.log('cprj:'+cprj+' posi:'+posi);
                //$('#t1 tr:eq(3) td:eq(2)').html("<p>" + msg + "</p>");
                $('#' + posi + '1').html("<p style=\"margin:0px\">" + cprj + "</p>");
                $('#' + posi + '2').html("<p style=\"margin:0px\">" + nloc + "</p>");
                //轉至休停狀態
                if (sbtn == "2") {
                    //green
                    $('#' + posi + '3').html("<p style=\"margin:0px\">休停</p>");
                    //要考慮狀態優先度
                    //如 不停工異常
                    $('#' + posi + '3').css('background', '#BA55D3');
                }
                    //轉至進行中狀態
                else if (sbtn == "1") {
                    $('#' + posi + '3').html("<p style=\"margin:0px\">進行中</p>");
                    //要考量狀態優先度
                    //如 不停工異常
                    $('#' + posi + '3').css('background', '#5BFF24');
                }
                else {
                    $('#' + posi + '3').html("<p style=\"margin:0px\">工程完工</p>");
                    //要考量狀態優先度
                    //如 不停工異常
                    $('#' + posi + '3').css('background', '#00FFFF');
                }
            }
            thub.client.sendTest = function () {
                console.log("add successful~");
            }            //異常控制
            thub.client.errorMsg = function (cprj, line, nloc, eDesc, eType, posi) {
                console.log("eDesc:" + eDesc);
                console.log("eType:" + eType);
                $('#' + posi + '3').css('background', eType);
                $('#' + posi + '3').html("<p style=\"margin:0px\">" + eDesc + "</p>");
                    if (errorTime == null && page == 1 && eac == '' & eType != 0) {
                        errorTime = new Date();
                        etimer = setInterval(function () { errorDt() }, 1000);
                    }
            }
            thub.client.cancelError = function (sbtn, posi, eClose) {
                if (sbtn == "0") {
                    $('#' + posi + '3').html("<p style=\"margin:0px\">待復工</p>");
                    $('#' + posi + '3').css('background', '#FFBB66');
                }
                else {
                    $('#' + posi + '3').html("<p style=\"margin:0px\">進行中</p>");
                    $('#' + posi + '3').css('background', '#5BFF24');
                }
                //要考量狀態優先度
                //如 不停工異常                
                if (eClose == "0" && page == 1) {
                    etimer = clearInterval(etimer);
                    $('#timeP').html('異常時間:00:00:00');
                    errorTime = null;
                    eac = '';
                }
            }
            thub.client.startTrigger = function () {
                console.log('started');
                cprj1stTime = 1;
                if (page == 0) {
                    tTimezone = 8 * 60 * 60;
                TTtimer = setInterval(function () { TTime() }, 1000);
                }
            }

            //-----------client 狀態更新function.en

            //-----------啟用雙向溝通.sn
            $.connection.hub.start().done(function () {
                thub.server.addGroup("N112");

            });
            $.connection.hub.disconnected(function () {
                setTimeout(function () {
                    $.connection.hub.start();
                }, 5000); // Re-start connection after 5 seconds
            });
            //-----------啟用雙向溝通.en

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">        
        <div id="id_wrapper" class="fsize2">
            <div id="id_header">
                
                    <div id="logoDiv" style="float:left">
                        <img id="CorpLogo" alt="" src="OR_LOGO-100.jpg" />              
                    </div>
                    <div id="titleDiv" style="float:none">
                        <span id="htext" style="font-family:微軟正黑體;font-size:100px;font-weight:bold;"></span>
                    </div>
                <div class="parent_expander" style="clear:both;"></div>
            </div>
            <div id="id_content">
            <table id="t1" style="width: 100%;text-align:center;font-weight:bold;" class="table table-bordered fsize48">
                <tr>
                    <td style="width: 33.333%; background-color: #A9A9A9;">
                        <p style="margin: 0px">工位</p>
                    </td>
                    <td id="wp1" style="width: 33.333%;background-color:#A9A9A9"></td>
                    <td id="wp2" style="width: 33.333%;background-color:#A9A9A9"></td>
                </tr>
                <tr>
                    <td style="width: 33.333%; background-color: #A9A9A9;">
                        <p style="margin: 0px">機號</p>
                    </td>
                    <td id="m1" style="width: 33.333%;">&nbsp;</td>
                    <td id="m2" style="width: 33.333%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33.333%; background-color: #A9A9A9;">
                        <p style="margin: 0px">工程</p>
                    </td>
                    <td id="p1" style="width: 33.333%;">&nbsp;</td>
                    <td id="p2" style="width: 33.333%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33.333%; background-color: #A9A9A9;">
                        <p style="margin: 0px">作業狀態</p>
                    </td>
                    <td id="w1" style="width: 33.333%;">&nbsp;</td>
                    <td id="w2" style="width: 33.333%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33.333%; background-color: #A9A9A9;">
                        <p style="margin: 0px">整機進度</p>
                    </td>
                    <td id="s1" style="width: 33.333%;">&nbsp;</td>
                    <td id="s2" style="width: 33.333%;">&nbsp;</td>
                </tr>
            </table>
                </div>
            <div id="id_footer" style="text-align: center;">
                <p id="timeP" class="fsize100" style="margin-top: 1px"></p>
            </div>
        </div>
    </form>
</body>
</html>
