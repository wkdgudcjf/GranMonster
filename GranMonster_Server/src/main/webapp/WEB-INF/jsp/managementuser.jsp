<%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8" %>
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt"%>
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
       	 유저 관리
        <small>Management User</small>
      </h1>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">User List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
              	<thead>
                  <tr>
                    <th>활성여부</th>
                    <th>플랫폼</th>
                    <th>키</th>
                    <th>이메일</th>
                    <th>비밀번호</th>
                    <th>금액(￦)</th>
                    <th>그랑코인</th>
                    <th>최초가입일</th>
                  </tr>
                </thead>
                <tfoot>
                  <tr>
                    <th>Enable</th>
                    <th>Platform</th>
                    <th>Key</th>
                    <th>email</th>
                    <th>password</th>
                    <th>money(￦)</th>
                    <th>Gran Coin</th>
                    <th>First Login</th>
                  </tr>
                </tfoot>
                <tbody>
                <c:forEach var="item" items="${userlist}">
                  <tr>
				        <td>
				        <c:choose>
						<c:when test="${item.userEnable==true}"> 
						    <img src="/image/enable.png" style="max-width: 30px; max-height: 30px;"> 
						</c:when> 
						<c:otherwise> 
						    <img src="/image/disable.png" style="max-width: 30px; max-height: 30px;"> 
						</c:otherwise> 
						</c:choose>
						</td>
						<td>${item.userTypeString}</td>
				        <td><a href="#" onclick="goToUserInfo(${item.userID});">${item.userKey}</a></td>
				        <td>${item.userEmail}</td>
				        <td>${item.userPassword}</td>
				        <td><fmt:formatNumber value="${item.userMoney}" pattern="#,###"/></td>
				        <td>${item.userCoin}</td>
				        <td>${item.userDateTime}</td>
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
		 $('#navi_user').attr('class',"active");
		});
	 function goToUserInfo(userID){
		 var form = document.createElement('form');
		 var objs;
		 objs = document.createElement('input');
		 objs.setAttribute('type', 'hidden');
		 objs.setAttribute('name' , 'userID');
		 objs.setAttribute('value', userID);
		 form.appendChild(objs);
		 form.setAttribute('method', 'post');
		 form.setAttribute('action', "/managementuserinfo");
		 document.body.appendChild(form);
		 form.submit();
	}
    </script>
</body>
</html>
