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
       	  4.4 그랑코인 사용
        <small> GranCoin Use</small>
      </h1>
      <h5>A. <a href="#sdk_billing_exhaust">그랑코인 사용</a></h5>
      <h5>B. <a href="#sdk_billing_exhaust_sourcecode">그랑코인 사용 소스코드</a></h5>
      <h5>C. <a href="#sdk_billing_exhaust_notice">제약 사항</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
		<div id="sdk_billing_exhaust">
		    <h1>A. 그랑코인 사용</h1>
		    <br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_billing_exhaust.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 그랑코인 사용</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			    <h5> <b>1)</b> 그랑코인으로 게임 내 상품이나 재화를 구매하고 싶다면 granmonster.GrancoinShopManager.RequestPurchaseByGranCoin() 함수를 사용합니다.</h5>
			    <h5>이 함수를 호출할 때 필요한 파라미터는 게임 키와 그랑코인 금액입니다.</h5>
			    <br/>
				<h5> <b>2)</b> 그랑코인을 통한 구매가 정상적으로 진행되었는지 검증하고 싶다면 2번 항목처럼 서버로부터 받은 State가 SUCCESS인지 아닌지 확인하면 됩니다.</h5>
				<h5>granmonster.ResponseProtocol.ResponseExhaust.StatusCode 안에는 에러코드가 포함되어 있습니다.</h5>
				<h5>SUCCESS 뿐만 아니라 여러가지 실패 사유를 나타내는 코드들이 있으므로 적절하게 활용하여 로직을 처리하면 됩니다.</h5>
				<br/>			   
			    <h5>자세한 코드는 ErrorCode를 참조하시길 바랍니다.</h5>
			    </div>
			     <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_billing_exhaust_sourcecode">
		 <h1>B. 그랑코인 사용 예제</h1>
			<pre><code>
public void exhaustTest()
{
	  int coin = 500;
	  
	  // 그랑 코인으로 결제를 하는 새로운 코드입니다.
	  // 그랑몬스터의 요청으로 이벤트가 변경되었습니다.
	  ResponseProtocol.ResponseExhaust responseExhaust = GrancoinShopManager.RequestPurchaseByGranCoin(
	     "$2a$10$rZVi36zyD3wPocTRQ58pMuT0sXQE1FTTlWCx2U3IrqnbiklI41YsW", coin);
	  if (responseExhaust.State == ResponseProtocol.ResponseExhaust.StatusCode.SUCCESS)
	  {
	     // 그랑코인으로 아이템 구매에 성공하였습니다.
	     LocalUser.Instance.OnUnlockUnit(pData);
	     LocalUser.gHeroCtrl.RunFxRankUpgrade(pData.Index);
	  }
	  else if (responseExhaust.State == ResponseProtocol.ResponseExhaust.StatusCode.NOT_ENOUGH_COIN)
	  {
	     Debug.Log("그랑코인이 부족합니다!");
	  }
}
			</code></pre>
	</div>
	<br/>
	<div id="sdk_billing_exhaust_notice">
		 <h1>C. 제약 사항</h1>
		<div class="callout callout-warning">
	        <h4>Notice!</h4>
	        <p><b>※</b> GranCoin API는 실시간 소켓통신이 아닌 동기 통신입니다. </p>
	        <p><b>※</b> 그러므로 Unity에서 실시간으로 Scene을 사용하려면 API를 비동기 통신(쓰레드)로 처리해야 합니다. </p>
	        <p><b>※</b> 비동기 통신에 CallBack함수를 등록하여 통신이 완료되면 CallBack함수를 통해 로직을 처리해야 합니다. </p>
	     </div>
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
       <h5>A. <a href="#sdk_billing_exhaust">그랑코인 사용</a></h5>
       <h5>B. <a href="#sdk_billing_exhaust_sourcecode">그랑코인 사용 소스코드</a></h5>
       <h5>C. <a href="#sdk_billing_exhaust_notice">제약 사항</a></h5>
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
		 $('#managementsdk_billing').attr('class',"treeview menu-open");
	};
</script>
</body>
</html>
