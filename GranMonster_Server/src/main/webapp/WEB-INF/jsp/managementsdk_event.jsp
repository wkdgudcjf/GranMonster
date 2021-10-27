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
       	 5.1 이벤트 연동
        <small> Event Connect</small>
      </h1>
       <h5>A. <a href="#sdk_event_success">이벤트 완료</a></h5>
       <h5>B. <a href="#sdk_event_success_sourcecode">이벤트 완료 소스코드</a></h5>
       <h5>C. <a href="#sdk_event_reward">이벤트 보상</a></h5>
       <h5>D. <a href="#sdk_event_reward_sourcecode">이벤트 보상 소스코드</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
		<div id="sdk_event_success">
		    <h1>A. 이벤트 완료</h1>
		    <br/>
			<h5> <b> 그랑몬스터 이벤트는 이벤트를 진행 중인 상태, 이벤트를 완료하여 보상을 받을 수 있는 상태, 이벤트를 완료하고 보상까지 받은 상태, 이렇게 총 세 가지 상태가 있습니다.</b></h5>
			<h5> <b>이벤트 완료 요청을 서버에 보내기 위해서는 관리자 페이지에서 특정 게임에 등록한 이벤트의 키를 알아야 합니다.</b></h5>
			<br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_event_success.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 이벤트 완료 연동</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			    <h5> 위 그림은 특정 시점에 이벤트 완료 요청을 보내는 소스 코드입니다.</h5>
				<h5>요청이 정상적으로 받아들여지는 경우 해당 유저의 해당 이벤트 상태는 보상을 받을 수 있는 상태가 되게 됩니다.</h5>
			     </div>
			     <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_event_success_sourcecode">
		 <h1>B. 이벤트 완료 소스코드</h1>
			<pre><code>
public void RequestEventComplete(string eventKey)
{
	string userKey = User.GetUserKey();
	ResponseProtocol.ResponseEvent responseEvent =
		granmonster.EventManager.EventComplete(GlobalConstants.GranmonsterGameKey, eventKey);
	console.GetComponentInChildren&lt;UnityEngine.UI.Text&gt;().text = responseEvent != null ? "" + responseEvent.State : "실패하였습니다.";
}
			</code></pre>
	</div>
	<br/>
	<div id="sdk_event_reward">
		    <h1>C. 이벤트 보상</h1>
		    <br/>
			<h5> <b> 그랑몬스터 이벤트는 이벤트를 진행 중인 상태, 이벤트를 완료하여 보상을 받을 수 있는 상태, 이벤트를 완료하고 보상까지 받은 상태, 이렇게 총 세 가지 상태가 있습니다.</b></h5>
			<h5> <b>이벤트 보상 받기 요청을 서버에 보내기 위해서는 관리자 페이지에서 특정 게임에 등록한 이벤트의 키를 알아야 합니다.</b></h5>
			<br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_event_reward.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 이벤트 보상 연동</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			    <h5>위 그림은 위젯 다이얼로그에서 유저가 이벤트 보상 받기 버튼을 클릭했을 때 서버로 이벤트 보상 받기 요청을 보내도록 하는 소스 코드입니다.</h5>
				<h5>요청이 정상적으로 받아들여지는 경우 해당 유저의 해당 이벤트 상태는 이벤트를 완료하고 보상을 이미 받은 상태가 되게 됩니다.</h5>
			    </div>
			    <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_event_reward_sourcecode">
		 <h1>D. 이벤트 보상 소스코드</h1>
			<pre><code>
public void RequestEventReward(string eventKey)
{
	string userKey = User.GetUserKey();
	ResponseProtocol.ResponseEventReward responseEventReward =
		granmonster.EventManager.EventReward(GlobalConstants.GranmonsterGameKey, eventKey);
	console.GetComponentInChildren&lt;UnityEngine.UI.Text&gt;().text = responseEventReward != null ? "" + responseEventReward.State : "실패하였습니다.";
}
			</code></pre>
	</div>
	<br/>
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
       <h5>A. <a href="#sdk_event_success">이벤트 완료</a></h5>
       <h5>B. <a href="#sdk_event_success_sourcecode">이벤트 완료 소스코드</a></h5>
       <h5>C. <a href="#sdk_event_reward">이벤트 보상</a></h5>
       <h5>D. <a href="#sdk_event_reward_sourcecode">이벤트 보상 소스코드</a></h5>
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
	   //$('#managementsdk_event').attr('class',"treeview menu-open");
	};
</script>
</body>
</html>
