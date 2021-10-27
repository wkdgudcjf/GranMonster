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
       	 4.1  연동 방법
        <small> Billing Connect</small>
      </h1>
      <h5>A. <a href="#sdk_billing_payment_setting">결제 모듈 셋팅</a></h5>
      <h5>B. <a href="#sdk_billing_payment_setting_sourcecode">결제 모듈 셋팅 소스코드</a></h5>
      <h5>C. <a href="#sdk_billing_payment_logic">구매 진행 로직</a></h5>
      <h5>D. <a href="#sdk_billing_payment_logic_sourcecode">구매  진행 로직 소스코드</a></h5>
      <h5>E. <a href="#sdk_billing_payment_grancoin">그랑코인 연동</a></h5>
      <h5>F. <a href="#sdk_billing_payment_grancoin_sourcecode">그랑코인 연동 소스코드</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
		<div id="sdk_billing_payment_setting">
		    <h1>A. 결제 모듈 셋팅</h1>
		    <br/>
			<h5> <b> 그랑몬스터 SDK는 Unity IAP 모듈에 기초하여 그 기능이 구현되었습니다.</b></h5>
			<h5> <b>구매 프로세스에 대해서 설명하자면, 먼저 기존의 Unity IAP 구매 프로세스와 같이 유저가 선택한 상품에 대한 구매 요청을 Google Store 또는 Apple Store 서버에 보냅니다.</b></h5>
			<h5> <b>결제 서버가 영수증이 포함된 결제 데이터를 보내줬을 경우 이 데이터를 기초로 다시 그랑몬스터 서버에 결제 요청을 합니다. </b></h5>
			<h5> <b>그랑몬스터 서버는 다시 이 데이터를 가지고 그랑몬스터 서버만의 개별 구매 프로세스를 진행 후 최종 완료 통보를 클라이언트에게 하고 결제 프로세스는 완전히 종료됩니다.</b></h5>
			<h5> <b>결론적으로 말하자면 그랑몬스터 SDK를 사용하는 개발자의 경우에는 다음과 같은 작업을 진행하면 됩니다.</b></h5>
			<br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_billing_payment.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 결제 모듈 연동</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			    <h5> 위 그림은 그랑몬스터 서버가 내려주는 상품 키를 등록하는 작업입니다. </h5>
			    <h5>그랑코인 샵 다이얼로그가 처음 로딩되는 순간 그랑몬스터 서버로부터 판매 중인 아이템의 ProductName들을 받아옵니다.</h5>
			    <h5>해당 정보는 List&lt;GrancoinShopManager.Exchange&gt; 타입으로 제공되며 이를 받아 Unity IAP 초기화 규칙에 따르는 로직을 작성하면 됩니다. </h5>
			    <h5>위 함수는 4-3에 1번 항목에 들어가는 initializer Callback 함수 입니다. </h5>
			     </div>
			     <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_billing_payment_setting_sourcecode">
		 <h1>B. 결제 모듈 셋팅 소스코드</h1>
			<pre><code>
public void InitializePurchasing(List &lt;GrancoinShopManager.Exchange&gt; exchanges)
{
   if (IsInitialized())
   {
      return;
   }

   ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
   foreach (GrancoinShopManager.Exchange exchange in exchanges)
   {
      builder.AddProduct(exchange.ExchangeKey, ProductType.Consumable);
   }

   UnityPurchasing.Initialize(this, builder);
}
			</code></pre>
	</div>
	<br/>
	<div id="sdk_billing_payment_logic">
		    <h1>C. 구매 진행 로직</h1>
		    <br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_billing_logic.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 구매 진행 로직</h3>
			</div>
			<br/>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			    <h5> 위 그림은 그랑코인 샵 다이얼로그에서 유저가 구매  버튼을 눌렀을 때 Unity IAP 구매 진행 로직을 수행할 함수를 등록하는 작업입니다 </h5>
			 	<h5>Unity IAP 연동 모듈을 연동하는 과정에서 ConfigurationBuilder 클래스에 상품 키를 등록하는 부분이 있습니다. </h5>
			 	<h5>그랑몬스터 SDK를 사용하는 경우, 그랑몬스터 서버가 내려주는 상품 키를 Unity IAP에 등록해야 하기 때문에 서버로부터 정상적인 응답을 받은 경우
			  	해당 데이터를 가지고 Unity IAP 모듈에 상품을 등록하는 함수를 등록해야 합니다.
				</h5>
			     </div>
			     <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_billing_payment_logic_sourcecode">
		 <h1>D. 구매 진행 로직 소스코드</h1>
			<pre><code>
public void ProcessPurchase(GrancoinShopManager.Exchange exchange, string payload)
{
	   if (IsInitialized() == false)
	   {
	      Utility.DebugLog(ToString() + "/ProcessPurchase/Not initialized.");
	      return;
	   }
	
	   Product product = m_storeController.products.WithID(exchange.ExchangeKey);
	   if (product == null ||
	      product.availableToPurchase == false)
	   {
	      Utility.DebugLog(ToString() + "/ProcessPurchase/Not purchasing product, either is not found or is not available for purchase.");
	      return;
	   }
	
	   if (payload == null)
	   {
	      Utility.DebugLog(ToString() + "/ProcessPurchase/Invalid payload.");
	      return;
	   }
	
	   m_purchsingProductData = exchange;
	   m_payload = payload;
	
	   m_storeController.InitiatePurchase(product);
}
			</code></pre>
	</div>
	<br/>
	<div id="sdk_billing_payment_grancoin">
		    <h1>E. 그랑코인 연동</h1>
		    <br/>
			<div style="text-align:center;">
				<img src="/sdk_image/sdk_billing_grancoin.png" style="max-width: 800px; max-height: 800px;">
				<h3>그림 - 그랑코인 연동</h3>
			</div>
			<br/>
			<div class="box box-primary">
			    <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			    </div>
			     <div class="box-body">
			    <h5> 위 그림은 구글 또는 애플 결제 서버로부터 정상 응답을 받았을 때 결제 정보를 그랑몬스터 서버로 보내주는 작업입니다. </h5>
			    <h5>Unity IAP 모듈을 사용하여 결제에 성공했을 때 영수증 정보를 내려받아 처리하는 부분이 있습니다.</h5>
			    <h5> 이 시점에서 그랑몬스터 서버로 결제 정보를 보내고 그랑몬스터 서버가 결제 완료 응답을 보내면 최종적으로 결제가 완료되게 됩니다.</h5>
			    </div>
			    <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_billing_payment_grancoin_sourcecode">
		 <h1>F. 그랑코인 연동 소스코드</h1>
			<pre><code>
public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs arguments)
{
      // 구글 or 애플 스토어 영수증 검증
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
      bool validPurchase = true;
      CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
         AppleTangle.Data(), Application.identifier);

      try
      {
         IPurchaseReceipt[] receipts = validator.Validate(arguments.purchasedProduct.receipt);
         foreach (IPurchaseReceipt receipt in receipts)
         {
#if UNITY_ANDROID
            GooglePlayReceipt googlePlayReceipt = receipt as GooglePlayReceipt;

            RequestProtocol.Purchase requestPurchase = new RequestProtocol.Purchase
            {
               userKey = User.GetUserKey(),
               appKey = GlobalConstants.GranmonsterGameKey,
               coin = m_purchsingProductData.ExchangeCoin,
               price = m_purchsingProductData.ExchangeMoney,
               payload = m_payload,
               productId = arguments.purchasedProduct.definition.id,
               purchaseToken = googlePlayReceipt.purchaseToken
            };

            ServerResponse serverResponse = ServerConnection.Purchase(JsonUtility.ToJson(requestPurchase));
            if (RestApiCallStatusMethods.Error(serverResponse.Status))
            {
               Utility.DebugLog(ToString() + "/ProcessPurchase/Purchase request failed.");
            }
#elif UNITY_IOS
#endif
         }
      }
      catch (IAPSecurityException)
      {
         Utility.DebugLog(ToString() + "/ProcessPurchase/Invalid receipt, not unlocking content");
         validPurchase = false;
      }

      if (validPurchase)
      {
         // Unlock the appropriate content here.
      }

      m_purchsingProductData = null;
      m_payload = string.Empty;

      Utility.DebugLog("유저 코인 정보가 갱신되었습니다 : (" + User.GetCoin() + ")");

      return PurchaseProcessingResult.Complete;
#endif
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
       <h5>A. <a href="#sdk_billing_payment_setting">결제 모듈 셋팅</a></h5>
       <h5>B. <a href="#sdk_billing_payment_setting_sourcecode">결제 모듈 셋팅 소스코드</a></h5>
       <h5>C. <a href="#sdk_billing_payment_logic">구매 진행 로직</a></h5>
       <h5>D. <a href="#sdk_billing_payment_logic_sourcecode">구매  진행 로직 소스코드</a></h5>
       <h5>E. <a href="#sdk_billing_payment_grancoin">그랑코인 연동</a></h5>
       <h5>F. <a href="#sdk_billing_payment_grancoin_sourcecode">그랑코인 연동 소스코드</a></h5>
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
