<%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ page import="java.text.SimpleDateFormat" %>
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
       	 <img src="image/appIcon/${app.appImageIconPath}"  style="max-width: 57px; max-height: 57px;">  ${app.appName}
        <small>App info</small>
      </h1>
      <ol class="breadcrumb">
        <li><Button type="button" class="btn btn-success btn-flat" onclick="modifyAppModal(${app.appID})">앱 수정(Modify App)</Button></li>
      </ol>
    </section>
	
   <div class="box box-primary">
     <div class="box-header with-border">
       <h5 class="box-title">노출 정보</h5>
     </div>
     <div class="box-body">
	<h5><b>위젯 노출</b> ( <c:choose>
							<c:when test="${app.appWidgetVisible==true}"> 
							    <img src="/image/enable.png" style="max-width:30px; max-height:30px;"> 
							</c:when> 
							<c:otherwise> 
							    <img src="/image/disable.png" style="max-width:30px; max-height:30px;"> 
							</c:otherwise>
						</c:choose> )
	</h5>
	<h5><b>그랑코인 샵 노출</b> - ( <c:choose>
								<c:when test="${app.appBillingVisible==true}"> 
								    <img src="/image/enable.png" style="max-width:30px; max-height:30px;"> 
								</c:when> 
								<c:otherwise> 
								    <img src="/image/disable.png" style="max-width:30px; max-height:30px;"> 
								</c:otherwise>
							</c:choose> )
	</h5>
    </div>
     <!-- /.box-body -->
  </div>
    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Route List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
              	<thead>
              	<tr>
              		<th>앱 이름</th>
                    <th>설치</th>
                    <th>실행</th>
                  </tr>
                </thead>
                <tfoot>
                 <tr>
                  	<th>합계(Total)</th>
                    <th id="install_total"></th>
                    <th id="play_total"></th>
                  </tr>
                  <tr>
                    <th>App Name</th>
                    <th>Install</th>
                    <th>Play</th>
                  </tr>
                </tfoot>
                <tbody>
               	<c:forEach var="item" items="${routeList}">
	                <tr>
				        <td>${item.appName}</td>
				        <td>${item.install}</td>
				        <td>${item.play}</td>
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
     <!-- ModifyApp Modal -->
    <div class="modal fade" id="modifyAppModal" tabindex="-1" role="dialog" aria-labelledby="modifyAppModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header" style="text-align: center;">
            <h1 class="modal-title" id="modifyAppModalLabel">Modify App</h1>
          </div>
          <div class="modal-body">
          	  <form id="modifyappform">
          	  <input type="hidden" id="modifyAppID" value="temp" name="appID">
	            <div class="form-group">
                  <label for="modifyAppName">앱 이름(고유)</label>
                  <input type="text" name="appName" class="form-control" id="modifyAppName" aria-describedby="nameHelp" placeholder="앱이름">
	            </div>
	            <div class="form-group">
                  <label for="modifyAppAndroidPackage">Android 패키지명</label>
                  <input type="text" name="appAndroidPackage" class="form-control" id="modifyAppAndroidPackage" aria-describedby="nameHelp" placeholder="Android 패키지명">
	            </div>
	            <div class="form-group">
	              <label for="modifyAppAndroidURL">Android URL</label>
	              <input type="text" name="appAndroidURL" class="form-control" id="modifyAppAndroidURL" aria-describedby="nameHelp" placeholder="Android URL">
	            </div>
	            <div class="form-group">
                  <label for="modifyAppIOSPackage">IOS 패키지명</label>
                  <input type="text" name="appIOSPackage" class="form-control" id="modifyAppIOSPackage" aria-describedby="nameHelp" placeholder="IOS 패키지명">
	            </div>
	            <div class="form-group">
	              <label for="modifyAppIOSURL">IOS URL</label>
	              <input type="text" name="appIOSURL" class="form-control" id="modifyAppIOSURL" aria-describedby="nameHelp" placeholder="IOS URL">
	            </div>
	            <div class="form-group">
	              <label for="appKey">고유키</label>
	              <p id="appKey"></p>
	            </div>
	             <div class="form-group">
	             	<label for="modifyCompany">회사 선택</label>
             		  <select name="companyID" class="input-large form-control">
	             		  <c:forEach var="item" items="${companylist}">
			                 <option value="${item.companyID}" id="modify_${item.companyID}">${item.companyName}</option>
						    </c:forEach>
                 	  </select>
		         </div>
	            <div class="form-group">
	            <label for="modifyAppIconImage">아이콘 이미지 업로드(57x57)(부재시 기존이미지 사용)</label>
	             <div class="filebox bs3-primary preview-image">
                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
                      <label for="modifyAppIconImage">이미지찾기</label> 
                    <input type="file" id="modifyAppIconImage" name="appIconImage" class="upload-hidden" accept="image/*"> 
                  </div>
	            </div>
	            
	            <!-- Form Element sizes -->
		          <div class="box box-success">
		            <div class="box-header with-border">
		              <h3 class="box-title">가로형 배너 이미지 업로드(635x395)(부재시 기존이미지 사용)</h3>
		            </div>
		            <div class="box-body">
		                <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
		                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
		                      <label for="modifyAppHBannerImage1">이미지찾기</label> 
		                    <input type="file" id="modifyAppHBannerImage1" name="appHBannerImage1" class="upload-hidden" accept="image/*"> 
		                  </div>
		          	   </div>
		              <br>
		              <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
		                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
		                      <label for="modifyAppHBannerImage2">이미지찾기</label> 
		                    <input type="file" id="modifyAppHBannerImage2" name="appHBannerImage2" class="upload-hidden" accept="image/*"> 
		                  </div>
		          	   </div>
		              <br>
		              <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
		                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
		                      <label for="modifyAppHBannerImage3">이미지찾기</label> 
		                    <input type="file" id="modifyAppHBannerImage3" name="appHBannerImage3" class="upload-hidden" accept="image/*"> 
		                  </div>
		          	   </div>
		            </div>
		            <!-- /.box-body -->
		          </div>
		          <!-- /.box -->
		          
          		<!-- Form Element sizes -->
		          <div class="box box-success">
		            <div class="box-header with-border">
		              <h3 class="box-title">세로형 배너 이미지 업로드(580x285)(부재시 기존이미지 사용)</h3>
		            </div>
		            <div class="box-body">
		                <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="modifyAppVBannerImage1">이미지찾기</label> 
	                    <input type="file" id="modifyAppVBannerImage1" name="appVBannerImage1" class="upload-hidden" accept="image/*"> 
	                 	 </div>
		          	   </div>
		              <br>
		              <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="modifyAppVBannerImage2">이미지찾기</label> 
	                    <input type="file" id="modifyAppVBannerImage2" name="appVBannerImage2" class="upload-hidden" accept="image/*"> 
	                  	</div>
		          	   </div>
		              <br>
		              <div class="form-group" style="margin-bottom: 0px;">
			             <div class="filebox bs3-primary preview-image">
	                      <input class="upload-name" value="파일선택" disabled="disabled" style="width: 200px;">
	                      <label for="modifyAppVBannerImage3">이미지찾기</label> 
	                    <input type="file" id="modifyAppVBannerImage3" name="appVBannerImage3" class="upload-hidden" accept="image/*"> 
	                  </div>
		            </div>
		            <!-- /.box-body -->
		           </div>
	              <!-- /.box -->
	          </div>
	           
	            <div class="form-group">
	              <label for="modifyEnable">활성여부</label><br>
	              <label class="radio-inline">
			     	 <input type="radio" id="modifyAppEnable" value="true" name="appEnable">활성
				  </label>
				  <label class="radio-inline">
				     <input type="radio" id="modifyAppDisable" value="false" name="appEnable">비활성
				  </label>
	            </div>
	            
	             <div class="form-group">
	              <label for="modifyBillingVisible">결제 노출 여부</label><br>
	              <label class="radio-inline">
			     	 <input type="radio" id="modifyBillingVisible" value="true" name="appBillingVisible">노출
				  </label>
				  <label class="radio-inline">
				     <input type="radio" id="modifyBillingInVisible" value="false" name="appBillingVisible">비노출
				  </label>
	            </div>
	            
	            <div class="form-group">
	              <label for="modifyWidgetVisible">위젯 노출 여부</label><br>
	              <label class="radio-inline">
			     	 <input type="radio" id="modifyWidgetVisible" value="true" name="appWidgetVisible">노출
				  </label>
				  <label class="radio-inline">
				     <input type="radio" id="modifyWidgetInVisible" value="false" name="appWidgetVisible">비노출
				  </label>
	            </div>
	          </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <a class="btn btn-primary" href="#" onclick="modifyApp();">수정하기</a>
          </div>
        </div>
      </div>
    </div>
</div>
<!-- ./wrapper -->
<jsp:include page="include/plugin_js.jsp" flush="false"/>
 <!-- Custom scripts for this template -->
	<script type="text/javascript">
	 $(document).ready(function(){
		 $('#mytable').DataTable({
		      'paging'      : true,
		      'lengthChange': true,
		      'searching'   : true,
		      'ordering'    : true,
		      'info'        : true,
		      'autoWidth'   : true,
		      "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
		      "pageLength"  : 20,
		      'drawCallback': tableSum
		    })
		 $('#navi_app').attr('class',"active");
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
		
		 function tableSum() {
			    var install = 0;
			    $('#mytable tbody tr td:nth-child(2)').each(function() {
			    	install += parseInt($(this).text());
			    });
			
			    var play = 0;
			    $('#mytable tbody tr td:nth-child(3)').each(function() {
			    	play += parseInt($(this).text());
			    });
			    $('#install_total').text(install);
			    $('#play_total').text(play);
			}
		 function modifyApp(){
			 var modifyAppName = $('#modifyAppName'),
			 modifyAppAndroidPackage = $('#modifyAppAndroidPackage'),
			 modifyAppAndroidURL = $('#modifyAppAndroidURL'),
			 modifyAppIOSPackage = $('#modifyAppIOSPackage'),
			 modifyAppIOSURL = $('#modifyAppIOSURL');
			 if (modifyAppName.val().length == 0) {
				 alert('앱 이름을 입력하세요.');
				 return;
			 }
			 if (modifyAppAndroidPackage.val().length == 0 && modifyAppAndroidURL.val().length != 0 )
		 	 {
				 alert('Android Package를 입력하세요.');
				 return;
		 	 }
			 if (modifyAppAndroidPackage.val().length != 0 && modifyAppAndroidURL.val().length == 0 )
		 	 {
				 alert('Android URL을 입력하세요.');
				 return;
		 	 }
			 if (modifyAppIOSPackage.val().length == 0 && modifyAppIOSURL.val().length != 0 )
		 	 {
				 alert('IOS Package를 입력하세요.');
				 return;
		 	 }
			 if (modifyAppIOSPackage.val().length != 0 && modifyAppIOSURL.val().length == 0 )
		 	 {
				 alert('IOS URL을 입력하세요.');
				 return;
		 	 }
			 if (modifyAppAndroidPackage.val().length == 0 && modifyAppIOSPackage.val().length == 0) {
				 alert('패키지명을 하나 이상 입력하세요.');
				 return;
			 }
			 if (modifyAppAndroidURL.val().length == 0  && modifyAppIOSURL.val().length == 0) {
				 alert('URL을 하나 이상 입력하세요.');
				 return;
			 }
				$.ajax({
					url:"/modifyapp",
					type: "POST",
					data: new FormData($("#modifyappform")[0]),
					enctype: 'multipart/form-data',
			        processData: false,
			        dataType : "text",
			        contentType: false,
			        cache: false,
			        success: function () {
			        	location.reload();
			        },
			        error:function(request,status,error){
			        	alert(request.responseText);
			        }
				})
			 }
		 function modifyAppModal(id){
				$.ajax({
					url:"/getapp",
					type: "POST",
					data : {'appID':id},
					dataType  : 'json',
			        success: function (data) {
			        	 $("#modifyAppName").val(data.appName);
			        	 $("#modifyAppAndroidPackage").val(data.appAndroidPackage);
			        	 $("#modifyAppAndroidURL").val(data.appAndroidURL);
			        	 $("#modifyAppIOSPackage").val(data.appIOSPackage);
			        	 $("#modifyAppIOSURL").val(data.appIOSURL);
			        	 $("#appKey").text(data.appKey);
			        	 $("#modify_"+data.companyID).prop("selected", true);
			           	 if(data.appEnable)
			        	{
			        		 $("#modifyAppEnable").prop("checked", true)
			        	}
			        	 else
			        	{
			        		 $("#modifyAppDisable").prop("checked", true)
			        	}
			           	if(data.appBillingVisible)
			        	{
			        		 $("#modifyBillingVisible").prop("checked", true)
			        	}
			        	 else
			        	{
			        		 $("#modifyBillingInVisible").prop("checked", true)
			        	}
			           	if(data.appWidgetVisible)
			        	{
			        		 $("#modifyWidgetVisible").prop("checked", true)
			        	}
			        	 else
			        	{
			        		 $("#modifyWidgetInVisible").prop("checked", true)
			        	}
			        	 $("#modifyAppID").val(data.appID);
			        	 $("#modifyAppModal").modal('show');
			        },
			        error:function(request,status,error){
			        	alert(request.responseText);
			        }
				})
			}
    </script>
</body>
</html>
