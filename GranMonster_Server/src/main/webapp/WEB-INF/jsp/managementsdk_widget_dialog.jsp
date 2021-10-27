<%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html>

<jsp:include page="include/head.jsp" flush="false"/>

<body class="hold-transition skin-blue sidebar-mini">
<div class="wrapper">

  <jsp:include page="include/main_header.jsp" flush="false"/>
  
  <jsp:include page="include/left_column.jsp" flush="false"/>

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
     <section class="content-header" 
   		style="padding:20px; 
    	background-color:#fff;">
      <h1>
       	 3.1 위젯 다이얼로그
        <small> Widget Dialog</small>
      </h1>
      <h5>A. <a href="#sdk_widget_dialog">widget 생성</a></h5>
      <h5>B. <a href="#sdk_widget_sourcecode">소스코드</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
		<div id="sdk_widget_dialog">
		    <h1>A. widget 생성</h1>
		    <br/>
			<h5><b> 그랑몬스터 위젯에는 디자인을 위한 여러가지 그래픽 리소스가 있으며, 내용물을 채우기 위해서 서버와의 블록킹 통신이 필요합니다.</b></h5>
			<h5><b>따라서 꽤 많은 로딩 시간이 필요하기 때문에 게임을 처음 시작할 때나 씬 전환 시에 미리 로딩하는 작업을 하는 것을 권장 드립니다.</b></h5>
			<br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_widget_dialog.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 위젯 생성</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			       <h5> 위 그림에 보이는 1번 항목처럼 Granmonster SDK 안에 있는 함수를 호출하여 위젯을 미리 로딩할 수 있습니다.</h5>
			       <h5>만약 그랑코인 보유량이 변할 때마다 게임 내 UI에서 실시간으로 반영을 하고 싶다면 위 스크린샷에서 표시한 부분과 같이 이벤트 핸들러를 등록하면 됩니다.</h5>
			       <h5>서버가 다이얼로그 노출을 의도적으로 막은 경우 다이얼로그는 열리지 않을 수 있습니다.</h5>
			     </div>
			     <!-- /.box-body -->
		   </div>
	   </div>
	   <br/>
		<div id="sdk_widget_sourcecode">
		 <h1>B. 소스코드</h1>
		<pre><code>
// 반드시 그랑몬스터 위젯 버튼을 먼저 초기화 해야합니다.
GranmonsterWidgetButton.Initialize("$2a$10$Kr/mC59ODWIAFM0CFn1R6eQp/Yj9QM03VmINS85geyilXYNhhBKHy",
   new Orientation(Orientation.Type.Horizontal), WidgetParent.transform, this, OnWidgetOpen, OnWidgetClose);

GranmonsterWidgetButton.Show(WidgetParent.transform, new Vector3(screenPosition.x - 70, 247, 0));

if (User.IsInitialized())
{
   GranCoin.text = granmonster.User.GetCoin().ToString("N0");
   granmonster.User.AddGranCoinValueChangedHandler((value) =>
   {
      GranCoin.text = value.ToString("N0");
   });
}
else
{
   GranCoin.text = "0";
}
		</code></pre>
     </div>
    </section>
    <!-- /.content -->
  </div>
   <!-- /.content-wrapper -->
  <div id="layer_QuickMenu">
        <!-- Form Element sizes -->
   <div class="box box-success" style="margin:0px;">
     <div class="box-header with-border">
       <h3 class="box-title">Quick Menu</h3>
     </div>
     <div class="box-body">
       <h5>A. <a href="#sdk_widget_dialog">widget 생성</a></h5>
       <h5>B. <a href="#sdk_widget_sourcecode">소스코드</a></h5>
     </div>
     <!-- /.box-body -->
   </div>
   <!-- /.box -->
  </div>
  <jsp:include page="include/main_footer.jsp" flush="false"/>
</div>
<!-- ./wrapper -->
<jsp:include page="include/plugin_js.jsp" flush="false"/>
<script type="text/javascript">
	window.onload = function(){
		 $('#navi_sdk').attr('class',"treeview menu-open active");
		 $('#managementsdk_widget').attr('class',"treeview menu-open");
	};
</script>
</body>
</html>
