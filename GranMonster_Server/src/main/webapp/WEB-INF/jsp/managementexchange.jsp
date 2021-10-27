<%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
<!DOCTYPE html>
<html>

<jsp:include page="include/head.jsp" flush="false"/>

 <style type="text/css">
    /* imaged preview */ 
 	.filebox input[type="file"] {
	    position: absolute;
	    width: 1px;
	    height: 1px;
	    padding: 0;
	    margin: -1px;
	    overflow: hidden;
	    clip:rect(0,0,0,0);
	    border: 0;
	}
	
	.filebox label {
	    display: inline-block;
	    padding: .5em .75em;
	    color: #999;
	    font-size: inherit;
	    line-height: normal;
	    vertical-align: middle;
	    background-color: #fdfdfd;
	    cursor: pointer;
	    border: 1px solid #ebebeb;
	    border-bottom-color: #e2e2e2;
	    border-radius: .25em;
	}
	
	/* named upload */
	.filebox .upload-name {
	    display: inline-block;
	    padding: .5em .75em;
	    font-size: inherit;
	    font-family: inherit;
	    line-height: normal;
	    vertical-align: middle;
	    background-color: #f5f5f5;
	 	border: 1px solid #ebebeb;
	 	border-bottom-color: #e2e2e2;
	 	border-radius: .25em;
	 	-webkit-appearance: none; /* 네이티브 외형 감추기 */
	  	-moz-appearance: none;
	  	appearance: none;
	}
	
	/* imaged preview */
	.filebox .upload-display {
	    margin-bottom: 5px;
	}
	
	@media(min-width: 768px) {
	    .filebox .upload-display {
	        display: inline-block;
	        margin-right: 5px;
	        margin-bottom: 0;
	    }
	}
	
	.filebox .upload-thumb-wrap {
	    display: inline-block;
	    width: 54px;
	    padding: 2px;
	    vertical-align: middle;
	    border: 1px solid #ddd;
	    border-radius: 5px;
	    background-color: #fff;
	}
	
	.filebox .upload-display img {
	    display: block;
	    max-width: 100%;
	    width: 100% \9;
	    height: auto;
	}
	
	.filebox.bs3-primary label {
	  	color: #fff;
	  	background-color: #337ab7;
	    border-color: #2e6da4;
	    margin-top: 8px;
	}
 </style>
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
       	 그랑코인 관리
        <small>Management Grancoin</small>
      </h1>
      <ol class="breadcrumb">
        <li>
        <Button type="button" class="btn btn-success btn-flat" data-toggle="modal" data-target="#registExchangeModal">그랑코인 등록(Regist Grancoin)</Button>
        </li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Grancoin List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
              	<thead>
                  <tr>
                    <th>활성여부</th>
                    <th>상품명</th>
                    <th>상품키</th>
                    <th>금액(￦)</th>
                    <th>그랑코인</th>
                    <th>가로 이미지</th>
                    <th>세로 이미지</th>
                  </tr>
                </thead>
                <tfoot>
                  <tr>
                    <th>Enable</th>
                    <th>Name</th>
                    <th>Key</th>
                    <th>Money(￦)</th>
                    <th>Gran Coin</th>
                    <th>Horizontal Image</th>
                    <th>Vertical Image</th>
                  </tr>
                </tfoot>
                <tbody>
                <c:forEach var="item" items="${exchangelist}">
                <tr>
			        <td>
			        <c:choose>
						<c:when test="${item.exchangeEnable==true}">
						    <img src="/image/enable.png" style="max-width: 30px; max-height: 30px;">
						</c:when>
						<c:otherwise>
						    <img src="/image/disable.png" style="max-width: 30px; max-height: 30px;">
						</c:otherwise>
					</c:choose>
					</td>
			        <td><a href="#" onclick="modifyExchangeModal(${item.exchangeID});">${item.exchangeName}</a></td>
			        <td>${item.exchangeKey}</td>
			        <td><fmt:formatNumber value="${item.exchangeMoney}" pattern="#,###"/></td>
			        <td>${item.exchangeCoin}</td>
			        <td><img src="image/HExchange/${item.exchangeHImagePath}"  style="max-width: 150px; max-height: 100px;"></td>
			        <td><img src="image/VExchange/${item.exchangeVImagePath}"  style="max-width: 100px; max-height: 150px;"></td>
			    </tr>
			    </c:forEach>
                </tbody>
              </table>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->
  
  <jsp:include page="include/main_footer.jsp" flush="false"/>
  
   <!-- ModifyExchange Modal -->
    <div class="modal fade" id="modifyExchangeModal" tabindex="-1" role="dialog" aria-labelledby="modifyExchangeModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header" style="text-align: center;">
            <h1 class="modal-title" id="modifyExchangeModalLabel">Modify Exchange</h1>
          </div>
          <div class="modal-body">
          	  <form id="modifyexchangeform">
          	  <input type="hidden" id="modifyExchangeID" value="temp" name="exchangeID">
          	 	<div class="form-group">
	            	<label for="modifyExchangeName">상품명</label>
		            <input type="text" name="exchangeName" class="form-control" id="modifyExchangeName" aria-describedby="nameHelp" placeholder="상품명">
	           </div>
	           	<div class="form-group">
	            	<label for="modifyExchangeKey">상품키(고유)</label>
		            <input type="text" name="exchangeKey" class="form-control" id="modifyExchangeKey" aria-describedby="nameHelp" placeholder="상품키">
	           </div>
	            <div class="form-group">
	              <label for="modifyExchangeMoney">금액(고유)</label>
	              <input type="number" name="exchangeMoney" class="form-control" id="modifyExchangeMoney" aria-describedby="nameHelp" placeholder="금액">
	            </div>
	             <div class="form-group">
	              <label for="modifyExchangeCoin">코인(고유)</label>
	              <input type="number" name="exchangeCoin" class="form-control" id="modifyExchangeCoin" aria-describedby="nameHelp" placeholder="코인">
	            </div>
	            <div class="form-group">
		            <label for="modifyHExchangeImage">가로형 이미지 업로드(124x253)(부재시 기존이미지 사용)</label>
		             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="modifyHExchangeImage">이미지찾기</label> 
	                    <input type="file" id="modifyHExchangeImage" name="exchangeHImage" class="upload-hidden" accept="image/*"> 
	                  </div>
		            </div>
	             <div class="form-group">
		            <label for="modifyVExchangeImage">세로형 이미지 업로드(145x210)(부재시 기존이미지 사용)</label>
		             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="modifyVExchangeImage">이미지찾기</label> 
	                    <input type="file" id="modifyVExchangeImage" name="exchangeVImage" class="upload-hidden" accept="image/*"> 
	                  </div>
		            </div>
	            <div class="form-group">
	              <label for="modifyEnable">활성여부</label><br>
	              <label class="radio-inline">
			     	 <input type="radio" id="modifyExchangeEnable" value="true" name="exchangeEnable">활성
				  </label>
				  <label class="radio-inline">
				     <input type="radio" id="modifyExchangeDisable" value="false" name="exchangeEnable">비활성
				  </label>
	            </div>
	          </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <a class="btn btn-primary" href="#" onclick="modifyExchange();">수정하기</a>
          </div>
        </div>
      </div>
    </div>
    
	 <!-- RegistCompany Modal -->
    <div class="modal fade" id="registExchangeModal" tabindex="-1" role="dialog" aria-labelledby="registExchangeModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header" style="text-align: center;">
            <h1 class="modal-title" id="registExchangeModalLabel">Regist Exchange</h1>
          </div>
          <div class="modal-body">
          	  <form id="registexchangeform">
          	     <div class="form-group">
	            	<label for="inputExchangeName">상품명</label>
		            <input type="text" name="exchangeName" class="form-control" id="inputExchangeName" aria-describedby="nameHelp" placeholder="상품명">
		          </div>
		          <div class="form-group">
	            	<label for="inputExchangeKey">상품키(고유)</label>
		            <input type="text" name="exchangeKey" class="form-control" id="inputExchangeKey" aria-describedby="nameHelp" placeholder="상품키">
		          </div>
	          	  <div class="form-group">
	            	<label for="inputExchangeMoney">금액(고유)</label>
		            <input type="number" name="exchangeMoney" class="form-control" id="inputExchangeMoney" aria-describedby="nameHelp" placeholder="금액">
		          </div>
		           <div class="form-group">
		            <label for="inputExchangeCoin">코인(고유)</label>
		            <input type="number" name="exchangeCoin" class="form-control" id="inputExchangeCoin" aria-describedby="nameHelp" placeholder="코인">
		          </div>
		          <div class="form-group">
		            <label for="inputHExchangeImage">가로형 이미지 업로드(124x253)</label>
		             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="inputHExchangeImage">이미지찾기</label> 
	                    <input type="file" id="inputHExchangeImage" name="exchangeHImage" class="upload-hidden" accept="image/*"> 
	                  </div>
		            </div>
	             <div class="form-group">
		            <label for="inputVExchangeImage">세로형 이미지 업로드(145x210)</label>
		             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="inputVExchangeImage">이미지찾기</label> 
	                    <input type="file" id="inputVExchangeImage" name="exchangeVImage" class="upload-hidden" accept="image/*"> 
	                  </div>
		            </div>
          	 </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <a class="btn btn-primary" href="#" onclick="registExchange();">등록하기</a>
          </div>
        </div>
      </div>
    </div>
</div>
<!-- ./wrapper -->
<jsp:include page="include/plugin_js.jsp" flush="false"/>
 <!-- Custom scripts for this template -->
	<script type="text/javascript">
	 $(document).ready(function()
	 {
		 $('#mytable').DataTable({
		      'paging'      : true,
		      'lengthChange': true,
		      'searching'   : true,
		      'ordering'    : true,
		      'info'        : true,
		      "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
		      "pageLength"  : 20,
		      'autoWidth'   : true
		    })
		 $('#navi_exchange').attr('class',"active");
		   var fileTarget = $('.filebox .upload-hidden');

		    fileTarget.on('change', function(){
		        if(window.FileReader){
		            // 파일명 추출
		            var filename = $(this)[0].files[0].name;
		        } 

		        else {
		            // Old IE 파일명 추출
		            var filename = $(this).val().split('/').pop().split('\\').pop();
		        };

		        $(this).siblings('.upload-name').val(filename);
		    });

		    //preview image 
		    var imgTarget = $('.preview-image .upload-hidden');

		    imgTarget.on('change', function(){
		        var parent = $(this).parent();
		        parent.children('.upload-display').remove();

		        if(window.FileReader){
		            //image 파일만
		            if (!$(this)[0].files[0].type.match(/image\//)) return;
		            
		            var reader = new FileReader();
		            reader.onload = function(e){
		                var src = e.target.result;
		                parent.prepend('<div class="upload-display"><div class="upload-thumb-wrap"><img src="'+src+'" class="upload-thumb"></div></div>');
		            }
		            reader.readAsDataURL($(this)[0].files[0]);
		        }

		        else {
		            $(this)[0].select();
		            $(this)[0].blur();
		            var imgSrc = document.selection.createRange().text;
		            parent.prepend('<div class="upload-display"><div class="upload-thumb-wrap"><img class="upload-thumb"></div></div>');

		            var img = $(this).siblings('.upload-display').find('img');
		            img[0].style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(enable='true',sizingMethod='scale',src=\""+imgSrc+"\")";        
		        }
		    });
		});
	 function registExchange(){
		 var inputExchangeMoney = $('#inputExchangeMoney'),
		  inputExchangeCoin = $('#inputExchangeCoin'),
		  inputExchangeName = $('#inputExchangeName'),
		  inputExchangeKey = $('#inputExchangeKey'),
		  inputHExchangeImage = $('#inputHExchangeImage');
		  inputVExchangeImage = $('#inputVExchangeImage');
		 if (inputExchangeMoney.val().length == 0) {
			 alert('금액을 입력하세요.');
			 return;
		 }
		 if (inputExchangeCoin.val().length == 0) {
			 alert('코인을 입력하세요.');
			 return;
		 }
		 if (inputExchangeName.val().length == 0) {
			 alert('상품명을 입력하세요.');
			 return;
		 }
		 if (inputHExchangeImage.val().length == 0) {
			 alert('가로형 이미지를 입력하세요.');
			 return;
		 }
		 if (inputVExchangeImage.val().length == 0) {
			 alert('세로형 이미지를 입력하세요.');
			 return;
		 }
		 if (inputExchangeKey.val().length == 0) {
			 alert('상품키를 입력하세요.');
			 return;
		 }
		$.ajax({
			url:"/registexchange",
			type: "POST",
			data: new FormData($("#registexchangeform")[0]),
			enctype: 'multipart/form-data',
			dataType : "text",
	        processData: false,
	        contentType: false,
	        cache: false,
	        success: function (data) {
	        	location.reload();
	        },
	        error:function(request,status,error){
	        	alert(request.responseText);
	        }
		})
	 }
	 function modifyExchange(){
		 var modifyExchangeMoney = $('#modifyExchangeMoney');
		  modifyExchangeCoin = $('#modifyExchangeCoin'),
		  modifyExchangeName = $('#modifyExchangeName');
		  modifyExchangeKey = $('#modifyExchangeKey');
		 if (modifyExchangeMoney.val().length == 0) {
			 alert('금액을 입력하세요.');
			 return;
		 }
		 if (modifyExchangeCoin.val().length == 0) {
			 alert('코인을 입력하세요.');
			 return;
		 }
		 if (modifyExchangeName.val().length == 0) {
			 alert('상품명을 입력하세요.');
			 return;
		 }
		 if (modifyExchangeKey.val().length == 0) {
			 alert('상품키을 입력하세요.');
			 return;
		 }
			$.ajax({
				url:"/modifyexchange",
				type: "POST",
				data: new FormData($("#modifyexchangeform")[0]),
				enctype: 'multipart/form-data',
		        processData: false,
		        contentType: false,
		        dataType : "text",
		        cache: false,
		        success: function (data) {
		        	location.reload();
		        },
		        error:function(request,status,error){
		        	alert(request.responseText);
		        }
			})
		 }
	 function modifyExchangeModal(id){
			$.ajax({
				url:"/getexchange",
				type: "POST",
				data : {'exchangeID':id},
				dataType  : "json",
		        success: function (data) {
		        	 $("#modifyExchangeMoney").val(data.exchangeMoney);
		        	 $('#modifyExchangeCoin').val(data.exchangeCoin);
		        	 $('#modifyExchangeName').val(data.exchangeName);
		        	 $('#modifyExchangeKey').val(data.exchangeKey);
		        	 if(data.exchangeEnable)
		        	{
		        		 $("#modifyExchangeEnable").prop("checked", true)
		        	}
		        	 else
		        	{
		        		 $("#modifyExchangeDisable").prop("checked", true)
		        	}
		        	 $("#modifyExchangeID").val(data.exchangeID);
		        	 $("#modifyExchangeModal").modal('show');
		        },
		        error:function(request,status,error){
		        	alert(request.responseText);
		        }
			})
		}
    </script>
</body>
</html>
