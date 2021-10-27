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
       	 6. 에러 코드
        <small> Error Code</small>
      </h1>
      <br/>
      <h5>A. <a href="#sdk_errorcode_api">API와 에러 코드 종류</a></h5>
      <h5>B. <a href="#sdk_errorcode_sourcecode">에러코드 처리 소스</a></h5>
    </section>

    <!-- Main content -->
    <section class="content">
   		<div id="sdk_errorcode_api">
		    <h1>API와 에러 코드 종류</h1>
		    <br/>
			<h5> <b> API는 RequestProtocol과 ResponseProtocol을 사용하여 호출합니다.</b></h5>
			<h5> <b> 밑 내용은 SDK 내부에서 사용하는 코드를 제외한 일부입니다.</b></h5>
			<br/>
			<pre><code>
public class ResponseProtocol
{
   public class ResponseLogin
   {
      public enum StatusCode
      {
         UNKNOWN,
         NOT_EXIST_APPKEY,
         USER_KEY_INVALID,
         USER_ALREADY_JOIN_APP,
         SUCCESS
      }

      [JsonProperty("state")] public StatusCode State { get; set; }
      [JsonProperty("userEmail")] public string Email { get; set; }
      [JsonProperty("userCoin")] public int Coin { get; set; }
      [JsonProperty("firstLogin")] public bool FirstLogin { get; set; }

      public ResponseLogin()
      {
         State = StatusCode.UNKNOWN;
         Email = string.Empty;
         Coin = 0;
         FirstLogin = false;
      }

      public ResponseLogin(StatusCode state, string email, int coin, bool firstLogin)
      {
         State = state;
         Email = email;
         Coin = coin;
         FirstLogin = firstLogin;
      }

      public static ResponseLogin CreateFromJson(string jsonString)
      {
         return JsonConvert.DeserializeObject&lt;ResponseLogin&gt;(jsonString);
      }
   }

   public class ResponseEvent
   {
      public enum StatusCode
      {
         UNKNOWN,
         NOT_EXIST_APPKEY,
         NOT_REGIST_EVENT,
         ALREADY_EVENT_END,
         NOT_ENABLE_EVENT,
         ALREADY_SUCCESS_EVENT,
         INVALID_EVENT,
         SUCCESS,
      }

      [JsonProperty("state")] public StatusCode State { get; set; }

      public ResponseEvent()
      {
         State = StatusCode.UNKNOWN;
      }

      public ResponseEvent(StatusCode state)
      {
         State = state;
      }

      public static ResponseEvent CreateFromJson(string jsonString)
      {
         return JsonConvert.DeserializeObject&lt;ResponseEvent&gt;(jsonString);
      }
   }

   public class ResponseEventReward
   {
      public enum StatusCode
      {
         UNKNOWN,
         NOT_EXIST_APPKEY,
         NOT_EXIST_EVENT,
         ALREADY_EVENT_END,
         ALREADY_EVENT_LIMIT_COUNT,
         NOT_ACHIEVE_EVENT,
         NOT_ENABLE_EVENT,
         ALREADY_REWARD_EVENT,
         INVALID_USER,
         INVALID_BILLING,
         INVALID_EVENT_COUNT,
         SUCCESS
      }

      [JsonProperty("state")] public StatusCode State { get; set; }
      [JsonProperty("coin")] public int AfterCoin { get; set; }

      public ResponseEventReward()
      {
         State = StatusCode.UNKNOWN;
         AfterCoin = 0;
      }

      public ResponseEventReward(StatusCode state, int coin)
      {
         State = state;
         AfterCoin = coin;
      }

      public static ResponseEventReward CreatFromJson(string jsonString)
      {
         return JsonConvert.DeserializeObject&lt;ResponseEventReward&gt;(jsonString);
      }
   }

   public class ResponseExhaust
   {
      public enum StatusCode
      {
         UNKNOWN,
         NOT_EXIST_APPKEY,
         NOT_EQUAL_PAYLOAD,
         NOT_ENOUGH_COIN,
         INVALID_BILLING,
         SUCCESS
      }

      [JsonProperty("state")] public StatusCode State { get; set; }
      [JsonProperty("coin")] public int AfterCoin { get; set; }

      public ResponseExhaust()
      {
         State = StatusCode.UNKNOWN;
         AfterCoin = 0;
      }

      public ResponseExhaust(StatusCode state, int coin)
      {
         State = state;
         AfterCoin = coin;
      }

      public static ResponseExhaust CreateFromJson(string jsonString)
      {
         return JsonConvert.DeserializeObject&lt;ResponseExhaust&gt;(jsonString);
      }
   }
}
			</code></pre>
			<div class="box box-primary">
			     <div class="box-header with-border">
			       <h5 class="box-title">Explane</h5>
			     </div>
			     <div class="box-body">
			       <h5>공개된 API와 에러코드들입니다. 예제처럼 에러코드로 분기를 하여 로직을 처리하시면 됩니다.  </h5>
			     </div>
			     <!-- /.box-body -->
		   </div>
		</div>
		<br/>
		<div id="sdk_errorcode_sourcecode">
		 <h1>B. 에러코드 처리 소스</h1>
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
        <h5>A. <a href="#sdk_errorcode_api">API와 에러 코드 종류</a></h5>
     	<h5>B. <a href="#sdk_errorcode_sourcecode">에러코드 처리 소스</a></h5>
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
		 //$('#managementsdk_login').attr('class',"treeview menu-open");
	};
</script>
</body>
</html>
