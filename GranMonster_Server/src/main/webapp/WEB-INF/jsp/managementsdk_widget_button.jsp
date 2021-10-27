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
       	 3.2 위젯 플로팅 버튼
        <small> Widget Floating Button</small>
      </h1>
      <h5>A. <a href="#sdk_widget_button_floating">widget 호출</a></h5>
      <h5>B. <a href="#sdk_widget_button_sourcecode">소스코드</a></h5>
      <h5>C. <a href="#sdk_widget_button_notice">notice</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
		<div id="sdk_widget_button_floating">
	    <h1>A. widget 호출.</h1>
	    <br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_widget_button.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 위젯 호출</h3>
			</div>
		<br/>
		<div class="box box-primary">
		     <div class="box-header with-border">
		       <h5 class="box-title">Explane</h5>
		     </div>
		     <div class="box-body">
		       <h5>위젯 로딩이 끝나고 나면 별도의 로딩 시간을 들이지 않고 바로 다이얼로그를 열 수 있는 상태가 됩니다.</h5>
		       <h5>위젯 다이얼로그를 열기 위해서는 위젯 플로팅 버튼이 필요합니다.</h5>
		       <h5>위젯 플로팅 버튼을 보여주기 전에 초기화를 진행하여야 하는데, 위젯 플로팅 버튼을 초기화 하는 방법으로는 위 그림의 1번 항목처럼 granmonster.GranmonsterWidgetButton.Initialize() 함수를 호출하는 것이 있습니다.</h5>
		       <h5>위 함수를 호출할 때에는 다음 매개변수들을 사용하여야 하며 위젯 다이얼로그를 로딩할 때 필요한 매개변수의 값들과 유사합니다.</h5>
		       <br/>
		       <h5><b>appKey</b> : 관리자 페이지에서 앱 등록 시 발급된 게임 키를 넣습니다.</h5>
		       <h5><b>orientation</b> : 위젯은 가로형 디자인과 세로형 디자인이 약간의 차이가 있습니다. 따라서 현재 게임이 어떤 형태의 게임이냐에 따른 값을 넣어줍니다.</h5>
		       <h5><b>widgetParent</b> : 위젯을 객체로 새로 생성하였을 때 상위 부모 컴포넌트를 지정하여 줍니다. 이를 지정하여 줄 때에는 depth를 고려하여 적절한 위치에 지정하여야만 합니다. 그렇지 않을 경우 위젯이 다른 UI에 가려 보이지 않는다던가, UI 관련 이벤트 핸들링이 의도했던 대로 이루어지지 않을 수도 있습니다. </h5>
		       <h5>그렇지 않을 경우 위젯이 다른 UI에 가려 보이지 않는다던가, UI 관련 이벤트 핸들링이 의도했던 대로 이루어지지 않을 수도 있습니다.</h5>
		       <h5><b>monoBehavior</b> : granmonster.GranmonsterWidgetButton.Initialize() 함수 내부에서는 코루틴을 통한 로직을 처리합니다.</h5>
		       <h5>따라서 이 코루틴을 돌릴 주체가 되는 MonoBehaviour 타입이 필요합니다. 대개의 경우에는 이 함수를 호출하는 MonoBehaviour를 상속받는 클래스의 this가 되는 것이 좋습니다. </h5>
		       <h5><b>onDialogOpenCallBack</b> : 위젯이 열렸을 때 어떤 작업이 추가로 실행되도록 하고 싶다면 이 곳에 콜백함수를 넣어줍니다. </h5>
		       <h5>NGUI와 같이 3rd Party 라이브러리를 사용하여 UI를 디스플레이 할때 문제가 있다면 이 곳에서 처리하는 함수를 등록하여 해결할 수 있습니다.</h5>
		       <h5>이는 옵션이기 때문에 필요하지 않다면 공란으로 비워두셔도 좋습니다.</h5>
		       <h5><b>onDialogCloseCallBack</b> : 만약 유저가 위젯을 의도적으로 닫았을 때 어떤 작업이 추가로 실행되도록 하고 싶다면 이 곳에 콜백함수를 넣어줍니다.</h5>
		       <h5>이는 옵션이기 때문에 필요하지 않다면 공란으로 비워두셔도 좋습니다. </h5>
			   <br/>		      
		       <h5>위젯 플로팅 버튼은 초기화를 한 뒤, 원하는 위치와 부모 컴포넌트를 지정하여 위젯 플로팅 버튼을 화면에 띄울 수 있습니다.</h5>
		       <h5>위 그림의 2번 항목처럼 granmonster.GranmonsterWidgetButton.Show() 함수를 이용하여 화면에 띄웁니다.</h5>
		       <h5>매개변수는 각각 버튼의 부모 컴포넌트가 될 GameObject와 버튼의 시작 위치를 결정하는 Vector3입니다.</h5>
		       <h5>추가적으로 위젯 플로팅 버튼을 다시 감추고 싶다면 granmonster.GranmonsterWidgetButton.Hide() 함수를 이용하면 됩니다.</h5>
		     </div>
		     <!-- /.box-body -->
	   </div>
		</div>
		<div id="sdk_widget_button_sourcecode">
		 <h1>B. 소스코드</h1>
		<pre><code>
using UnityEngine;

using granmonster;

public class WidgetTestManager : MonoBehaviour {
	   public Transform buttonParent;
	   public Transform widgetParent;
	
	   private void Start()
	   {
	      // 반드시 그랑몬스터 위젯 버튼을 먼저 초기화 해야합니다.
	      bool success = GranmonsterWidgetButton.Initialize(GlobalConstants.GranmonsterGameKey,
	         new Orientation(Orientation.Type.Horizontal), widgetParent, this, OnWidgetOpen, OnWidgetClose);
	      Debug.Log("Initialize : " + success);
	
	      // 100, 100 위치에 플로팅 버튼을 띄웁니다.
	      // 위치는 상황에 맞게 어느 곳에서나 띄울 수 있습니다.
	      GranmonsterWidgetButton.Show(buttonParent, new Vector3(100, 100, 0));
	   }
	
	   public void OnWidgetOpen()
	   {
	      // NOTE: 위젯 다이얼로그를 열고나서 해야할 작업이 있다면 이곳에 코드를 작성합니다.
	      Utility.DebugLog("WidgetTestManager/OnWidgetOpen");
	   }
	
	   public void OnWidgetClose()
	   {
	      // NOTE: 위젯 다이얼로그를 닫은 후에 해야할 작업이 있다면 이곳에 코드를 작성합니다.
	      Utility.DebugLog("WidgetTestManager/OnWidgetClose");
	   }
}
		</code></pre>
		<br/>
		<div id="sdk_widget_button_notice">
			 <h1>C. notice</h1>
			<div class="callout callout-warning">
		        <h4>Notice!</h4>
		         <p><b>※</b> 위젯 샵 플로팅 버튼의 경우 Canvas에서 동작하게 구현되었습니다.
				<p>따라서 그랑코인 샵 플로팅 버튼은 반드시 Canvas 컴포넌트를 갖고 있는 오브젝트의 자식으로 들어가야 합니다.</p>
				<p>또한 Canvas의 Render Mode가 Screen Space - Camera로 되어 있는지 확인해야하고 되어 있다면 Render Camera에 오브젝트가 들어가 있는지 확인해야 합니다.</p>
		     	<p>플로팅 버튼을 보여주기 위해 Show() 함수를 호출했을 때와 플로팅 버튼을 클릭했을 때 본 로직 수행에 앞서 그랑코인 서버로부터 다이얼로그를 열 수 있는지 허가를 받는 작업을 합니다.</p>
				<p>만약 관리자 페이지에서 의도적으로 다이얼로그 노출을 막은 상태일 경우 모듈은 그랑코인 서버로부터 요청 거절 응답을 받게 되고, 이 후 버튼을 보이지 않게 합니다.</p>
		     </div>
    	 </div>
    	 <br/>
     </div>
    </section>
    <!-- /.content -->
  </div>
   <!-- /.content-wrapper -->
  <div id="layer_QuickMenu">
        <!-- Form Element sizes -->
   <div class="box box-primary" style="margin:0px;">
     <div class="box-header with-border">
       <h3 class="box-title">Quick Menu</h3>
     </div>
     <div class="box-body">
       <h5>A. <a href="#sdk_widget_button_floating">widget 호출</a></h5>
       <h5>B. <a href="#sdk_widget_button_sourcecode">소스코드</a></h5>
       <h5>C. <a href="#sdk_widget_button_notice">notice</a></h5>
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
