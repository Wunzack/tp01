<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machChkMaint.aspx.cs" Inherits="ecsfc.machChkMaint" %>

<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="UTF-8"/>
  <title>machChkSys</title>
  

<link href="Content/bootstrap.min.css" rel="stylesheet" />      
<%--<link href="Content/style.css" rel="stylesheet" />--%>
  <link href="Content/machChkSysStyles/machChkSysStyle.css" rel="stylesheet" />
  <link href="Content/machChkSysStyles/machMaintStyle.css" rel="stylesheet" />

</head>

<body>
  <form id="form1" runat="server">
<div class="header-top">
  <div class="navbar-header">OR machChkSys</div>
  <ul id="headerUL">
    <li><a href="MCS_homepage.aspx"><span>首頁</span></a></li>
    <li><a href="machChkMaint.aspx"><span>點檢管理</span></a></li>
    <li><a href="#"><span>點檢歷程</span></a></li>
    <li><a href="#"><span>人員管理</span></a></li>
  </ul>
</div>
<div class="header-bottom"><span></span><span id="mousecoords"></span>  
</div>
  <div class="nextData"><svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 455 455" style="enable-background:new 0 0 455 455;" xml:space="preserve">
<path d="M227.5,0C101.855,0,0,101.855,0,227.5S101.855,455,227.5,455S455,353.145,455,227.5S353.145,0,227.5,0z M199.476,355.589
	l-21.248-21.178L284.791,227.5L178.228,120.589l21.248-21.178L327.148,227.5L199.476,355.589z"/>
</svg></div>
  <div class="lastData"><svg version="1.1" id="Layer_2" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 455 455" style="enable-background:new 0 0 455 455;" xml:space="preserve">
<path d="M227.5,0C101.855,0,0,101.855,0,227.5S101.855,455,227.5,455S455,353.145,455,227.5S353.145,0,227.5,0z M276.772,334.411
	l-21.248,21.178L127.852,227.5L255.524,99.411l21.248,21.178L170.209,227.5L276.772,334.411z"/>
</svg></div>
            <div id="slide">
                <div id="slide-in">
                    <div id="open-btn">
                        <img class="chkmapImg" src="http://i.imgur.com/rn09TwK.gif" width="20" height="20" />
                    </div>
                    <h3></h3>
                    <div id="slide-contents" class="chkpotDataset">
                        <input id='subImgInput' type="file" onchange="subImgAccess()"/>
                        <img id="subImgPrev" src="#" alt="請上傳部件位置照片.." />
                    </div>
                </div>
            </div>
            <div id="slide2">
                <div id="slide-in2">
                    <div id="open-btn2">
                        <img class="chkmapImg" src="http://i.imgur.com/rn09TwK.gif" width="20" height="20" />
                    </div>
                    <h3></h3>
                    <div id="slide-contents2" class="chkpotDataset">
                        <p>contents</p>
                    </div>
                </div>
            </div>
<div id="imgInput"><input id='InputImg' type="file" onchange="previewFile()"/><div class='form-group'><span>機號：</span><input id="machID" list="machList" name="browser" placeholder="請輸入機號"/>
  <datalist id="machList"/></div><br />
</div>
<div id="chkmap" class="chkmap chkpotDataset" oncontextmenu="return false;"><img src='#' id="chkmapImg" style="height:100%;" alt="請上傳點檢地圖圖檔.."/>
<!--   <div id="cc" class='circle' coords='385,59' subimg='油壓箱01.png' parts='18,27,36,44,46' style='left:172px; top:72px;'><div class='in'>cc</div></div> -->
</div>
<div class="sp"></div>
<div class="chklist">
  <div class="outlayer">
        <div class="pageMarksCollect">
            <div class="pageMarks pMarksAct">
                <div id="clean" class="pMInLay" style="background-color:green">清掃</div>
                </div>
            <div class="pageMarks">
                <div id="oil" class="pMInLay" style="background-color:yellow">給油</div>
            </div>
            <div class="pageMarks">
                <div id="chk" class="pMInLay" style="background-color:orange">點檢</div>
            </div>
        </div>
    <div id="chklistInner" class="chklistInner">
      <div id="chkContent" class="chkContent">
        <div id="colHRow" class="ColumnHeaderRow">
        <div class="Cell-editable"><div class="headerCell">詳細</div></div>
        <div class="Cell-editable"><div class="headerCell">手順編號</div></div>
          <div class="Cell-editable"><div class="headerCell">部位</div></div>
          <div class="Cell-editable"><div class="headerCell">基準</div></div>
          <div class="Cell-editable"><div class="headerCell">方法</div></div>
          <div class="Cell-editable"><div class="headerCell">工具</div></div>
          <div class="Cell-editable"><div class="headerCell">時間</div></div>
          <div class="Cell-editable"><div class="headerCell">點檢週期</div></div>            
          <div class="Cell-editable"><div class="headerCell">下次點檢日</div></div>
          </div>
        <div id="dataSection" class="dataSection">
          
        </div>
      </div>
  </div>
</div>
</div>
<div class="addRow">
<svg xmlns="http://www.w3.org/2000/svg" xmlns:se="http://svg-edit.googlecode.com" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:cc="http://creativecommons.org/ns#" xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape" width="30" height="30" style="">
                                    <title>my vector image</title>
                                    <!-- Created with Vector Paint - http://www.vectorpaint.yaks.com/ https://chrome.google.com/webstore/detail/hnbpdiengicdefcjecjbnjnoifekhgdo -->
                                    <rect id="backgroundrect" width="100%" height="100%" x="0" y="0" fill="#FFFFFF" stroke="none" style="" class="" opacity="0"/>
                                <g style="" class="currentLayer"><title>Layer 1</title><path fill="#ffffff" stroke="#ffffff" stroke-width="2" stroke-linejoin="round" stroke-dashoffset="" fill-rule="nonzero" marker-start="" marker-mid="" marker-end="" id="svg_2" d="M1.0000000078977496,10.4139587517216 L10.413958109044836,10.4139587517216 L10.413958109044836,1.0000000019302488 L20.07089212800307,1.0000000019302488 L20.07089212800307,10.4139587517216 L29.484851182801474,10.4139587517216 L29.484851182801474,20.0708934114497 L20.07089212800307,20.0708934114497 L20.07089212800307,29.484851207590758 L10.413958109044836,29.484851207590758 L10.413958109044836,20.0708934114497 L1.0000000078977496,20.0708934114497 L1.0000000078977496,10.4139587517216 z" style="color: rgb(0, 0, 0);" class="selected" stroke-opacity="1" fill-opacity="1"/></g></svg></div>
  <div class="saveBtn">
<?xml version='1.0' encoding='iso-8859-1'?><svg version='1.1' id='Capa_1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' viewBox='0 0 58.064 58.064' style='enable-background:new 0 0 58.064 58.064;' xml:space='preserve'><polygon style='fill:#7383BF;' points='17.064,31.032 58.064,10.032 24.064,35.032 44.064,48.032 58.064,10.032 0,22.032 '/><polygon style='fill:#556080;' points='24.064,35.032 20.127,48.032 17.064,31.032 58.064,10.032 '/><polygon style='fill:#464F66;' points='24.064,35.032 20.064,48.032 31.912,40.133 '/><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g><g></g></svg>
</div>
      </form>
<%--<script src="Scripts/jquery-1.9.1.js"></script>--%>
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/index.js"></script>
    <script src="Scripts/mcsJS/mcsHomeCtrl.js"></script>
    <script src="layer-v2.0/layer/layer.js"></script>
</body>
</html>
