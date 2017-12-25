<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MCS_homepage.aspx.cs" Inherits="ecsfc.MCS_homepage" %>

<!DOCTYPE html>

<html >
<head>
  <meta charset="UTF-8">
  <title>machChkSys</title>
  
  
  <link rel='stylesheet prefetch' href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0-beta/css/bootstrap.min.css'>
      <link href="Content/machChkSysStyles/machChkSysStyle.css" rel="stylesheet" />
      <link href="Content/machChkSysStyles/mcsHomepage.css" rel="stylesheet" />

  
</head>

<body>
  
<div class="header-top">
  <div class="navbar-header">OR machChkSys</div>
  <ul id="headerUL">
    <li> <a href="MCS_homepage.aspx"><span>首頁</span></a></li>
    <li><a href="machChkMaint.aspx"><span>點檢管理</span></a></li>
    <li><a href="#"><span>點檢歷程</span></a></li>
    <li><a href="#"><span>人員管理</span></a></li>
  </ul>
</div>
<div class="header-bottom"></div>
<div class="footer"></div>
<div class="funcBar">
  <div class="dropdown show">
    <button class="btn btn-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">請選擇</button>
    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton"><a class="dropdown-item" href="#">a</a><a class="dropdown-item" href="#">b</a></div>
    <button class="btn btn-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">請選擇</button>
    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton"><a class="dropdown-item" href="#">a</a><a class="dropdown-item" href="#">b</a></div>
  </div>
</div>
<div class="funcPageBox">
  <div id="boxRow" class="boxRow"> 
<%--    <div class="machBlocks" id="m1">
        <div class="machBox">
            <div class="machBoxIn-p1">
                <div class="upBlock">
                    <h2>m01</h2>
                </div>
                <div class="downBlock">
                    <div class="dbSubBlock">
                        <h3>日點檢：</h3>
                        <span>3</span>
                    </div>
                    <div class="dbSubBlock">
                        <h3>月點檢：</h3>
                        <span>1</span>
                    </div>
                    <div class="dbSubBlock">
                        <h3>週點檢：</h3>
                        <span>2</span>
                    </div>
                    <div class="dbSubBlock">
                        <h3>年點檢：</h3>
                        <span>0</span>
                    </div>
                </div>
            </div>
            <div class="machBoxIn-p2">
                <div class="part">
                    異常：<span>0</span>
                </div>
                <div class="part">異常歷程</div>
                <div class="part">維護主檔</div>
            </div>
        </div>
    </div>--%>
  </div>
</div>
<div class="circleObj"></div>
<script src="Scripts/jquery-3.2.1.min.js"></script>
<script src="Scripts/bootstrap.js"></script>
<script src="Scripts/mcsJS/mcsHomeCtrl.js"></script>

</body>
</html>
