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
       	 1. SDK 사용법
        <small>SDK Guide</small>
      </h1>
      <ol class="breadcrumb">
        <li>

         <Button type="button" class="btn btn-block btn-success btn-flat" onClick="downloadSDK()">SDK 다운로드</Button>
        </li>
      </ol>
      <br/>
      <h5>A. <a href="#sdk_import">Import 하기</a></h5>
    </section>
    <!-- Main content -->
    <section class="content">
    <div id="sdk_import">
    <h1>A. Import 하기</h1>
    <br/>
	<h5> <b> SDK를 다운받은 후 압축을 풀고 Package파일을 더블클릭합니다.</b></h5>
	<br/>
	<div style="text-align:center;">
		<img src="/sdk_image/sdk_import.png" style="max-width: 1000px; max-height: 1000px;">
		<h3>그림 - Unity Import 화면</h3>
	</div>
	<br/>
	<h5> <b>위와 같이 Import버튼을 클릭하면 SDK Import가 완료됩니다.</b></h5>
	
	<div class="box box-primary">
	     <div class="box-header with-border">
	       <h5 class="box-title"><b>릴리즈 노트 - 버전 1.6</b></h5>
	     </div>
	     <div class="box-body">
	    <h5> * 위젯 다이얼로그에서 미션 CLEAR 버튼을 누르고 보상을 받은 뒤, 다른 게임 정보를 보고 돌아오면 다시 CLEAR 버튼이 나오는 문제 수정</h5>
	    <h5> * Unity IAP 모듈에서 상품 키 등록하는 부분 그랑코인 샵 플로팅 버튼을 누르는 시점이 아니라 그랑코인 샵 플로팅 버튼을 초기화 하는 Initialize() 함수를 부르는 시점으로 변경</h5>
	    <h5> * 위젯 플로팅 버튼을 보여주기 위해 Show() 함수를 부른 경우, 위젯 플로팅 버튼을 클릭했지만 서버가 다이얼로그 오픈 요청을 거부한 경우 다이얼로그는 띄우지 않으면서 플로팅 버튼을 사라지게 함</h5>	   
	    <h5> * 그랑코인 샵 플로팅 버튼을 보여주기 위해 Show() 함수를 부른 경우, 그랑코인 샵 플로팅 버튼을 클릭했지만 서버가 다이얼로그 오픈 요청을 거부한 경우 다이얼로그는 띄우지 않으면서 플로팅 버튼을 사라지게 함</h5>
	    <h5> * 안드로이드 운영체제도 아니고 iOS 운영체제도 아닌 경우 경고 로그를 찍고 안드로이드로 간주하도록 플랫폼 의존 코드 정리(안드로이드와 iOS 이외에는 전혀 고려하지 않는다)</h5>
	    </div>
	     <!-- /.box-body -->
   </div>
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
       <h5>A. <a href="#sdk_import">Import 하기</a></h5>
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
	};
	function downloadSDK()
	{
		 var form = document.createElement('form');
		 var objs;
		 objs = document.createElement('input');
		 objs.setAttribute('type', 'hidden');
		 form.appendChild(objs);
		 form.setAttribute('method', 'post');
		 form.setAttribute('action', "/downloadSDK");
		 document.body.appendChild(form);
		 form.submit();
	}
</script>
</body>
</html>
