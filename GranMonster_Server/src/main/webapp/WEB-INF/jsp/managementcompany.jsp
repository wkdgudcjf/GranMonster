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
       	 회사 관리
        <small>Management Company</small>
      </h1>
      <ol class="breadcrumb">
        <li>
         <Button type="button" class="btn btn-success btn-flat" data-toggle="modal" data-target="#registCompanyModal">회사 등록(Regist Company)</Button>
        </li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Company List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
              	<thead>
                  <tr>
                    <th>활성여부</th>
                    <th>회사명</th>
                    <th>등록시간</th>
                  </tr>
                </thead>
                <tfoot>
                  <tr>
                    <th>Enable</th>
                    <th>Company Name</th>
                    <th>Date</th>
                  </tr>
                </tfoot>
                <tbody>
                <c:forEach var="item" items="${companylist}">
                <tr>
			        <td>
			        <c:choose>
						<c:when test="${item.companyEnable==true}">
						    <img src="/image/enable.png" style="max-width: 30px; max-height: 30px;">
						</c:when>
						<c:otherwise>
						    <img src="/image/disable.png" style="max-width: 30px; max-height: 30px;">
						</c:otherwise>
					</c:choose>
					</td>
			        <td><a href="#" onclick="modifyCompanyModal(${item.companyID});">${item.companyName}</a></td>
			        <td>${item.companyDateTime}</td>
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
  
    <!-- ModifyCompany Modal -->
    <div class="modal fade" id="modifyCompanyModal" tabindex="-1" role="dialog" aria-labelledby="modifyCompanyModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header" style="text-align: center;">
            <h1 class="modal-title" id="modifyCompanyModalLabel">Modify Company</h1>
          </div>
          <div class="modal-body">
          	  <form id="modifycompanyform">
          	  <input type="hidden" id="modifyCompanyID" value="temp" name="companyID">
	            <div class="form-group">
	              <label for="modifyCompanyName">회사명</label>
	              <input type="text" name="companyName" class="form-control" id="modifyCompanyName" aria-describedby="nameHelp" placeholder="회사명">
	            </div>
	            <div class="form-group">
	              <label for="modifyEnable">활성여부</label><br>
	              <label class="radio-inline">
			     	 <input type="radio" id="modifyCompanyEnable" value="true" name="companyEnable">활성
				  </label>
				  <label class="radio-inline">
				     <input type="radio" id="modifyCompanyDisable" value="false" name="companyEnable">비활성
				  </label>
	            </div>
	          </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <a class="btn btn-primary" href="#" onclick="modifyCompany();">수정하기</a>
          </div>
        </div>
      </div>
    </div>
    
	 <!-- RegistCompany Modal -->
    <div class="modal fade" id="registCompanyModal" tabindex="-1" role="dialog" aria-labelledby="registCompanyModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header" style="text-align: center;">
            <h1 class="modal-title" id="registCompanyModalLabel">Regist Company</h1>
          </div>
          <div class="modal-body">
          	  <form id="registcompanyform">
	            <div class="form-group">
	              <label for="inputCompanyName">회사명</label>
	              <input type="text" name="companyName" class="form-control" id="inputCompanyName" aria-describedby="nameHelp" placeholder="회사명">
	            </div>
	          </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            <a class="btn btn-primary" href="#" onclick="registCompany();">등록하기</a>
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
		      "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
		      "pageLength"  : 20,
		      'autoWidth'   : true
		    })
		 $('#navi_company').attr('class',"active");
		});
	 function registCompany(){
		 var inputCompanyName = $('#inputCompanyName');
		 if (inputCompanyName.val().length == 0) {
			 alert('회사 이름을 입력하세요.');
			 return;
		 }
		$.ajax({
			url:"/registcompany",
			type: "POST",
			data: new FormData($("#registcompanyform")[0]),
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
	 function modifyCompany(){
		 var modifyCompanyName = $('#modifyCompanyName');
		 if (modifyCompanyName.val().length == 0) {
			 alert('회사 이름을 입력하세요.');
			 return;
		 }
			$.ajax({
				url:"/modifycompany",
				type: "POST",
				data: new FormData($("#modifycompanyform")[0]),
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
	 function modifyCompanyModal(id){
			$.ajax({
				url:"/getcompany",
				type: "POST",
				data : {'companyID':id},
				dataType  : "json",
		        success: function (data) {
		        	 $("#modifyCompanyName").val(data.companyName);
		        	 if(data.companyEnable)
		        	{
		        		 $("#modifyCompanyEnable").prop("checked", true)
		        	}
		        	 else
		        	{
		        		 $("#modifyCompanyDisable").prop("checked", true)
		        	}
		        	 $("#modifyCompanyID").val(data.companyID);
		        	 $("#modifyCompanyModal").modal('show');
		        },
		        error:function(request,status,error){
		        	alert(request.responseText);
		        }
			})
		}
    </script>
</body>
</html>
