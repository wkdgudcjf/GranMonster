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
       	 ${user.userKey}
        <small>User Info</small>
      </h1>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">User App List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
              	 <thead>
                  <tr>
                    <th>가입된 게임</th>
                    <th class="no-sort">이벤트 내용</th>
                    <th class="no-sort">보상</th>
                    <th class="no-sort">달성 여부</th>
                    <th class="no-sort">보상 여부</th>
                  </tr>
                </thead>
                <tfoot>
                  <tr>
                    <th>Game Name</th>
                    <th>Event Content</th>
                    <th>Coin</th>
                    <th>Event Success Enable</th>
                    <th>Event Reward Enable</th>
                  </tr>
                </tfoot>
                <tbody>
                <c:forEach var="item" items="${userinapplist}">
               		<c:forEach var="event" items="${item.appEventList}" varStatus="status">  
              	     <tr>
	              	    <c:choose>
							<c:when test="${status.first==true}"> 
							   	 <td rowspan="${item.appEventList.size()}">${item.appName}</td>
							</c:when> 
							<c:otherwise> 
						     	<td style="display: none;">${item.appName}</td>
							</c:otherwise>
						</c:choose>
				        <td>${event.appEventContent}</td>
				        <td>${event.appEventCoin}</td>
				        <td>${event.appEventSuccessEnable}</td>
				        <td>${event.appEventRewardEnable}</td>
			         </tr>
				    </c:forEach>
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
	      'autoWidth'   : true,
	      columnDefs: [ { orderable: false, targets: 'no-sort' }],
	    })
		 $('#navi_user').attr('class',"active");
		});
    </script>
</body>
</html>
