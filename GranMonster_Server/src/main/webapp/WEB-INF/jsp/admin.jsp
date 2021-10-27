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
       	Dashboard
        <small>Dashboard</small>
      </h1>
       <ol class="breadcrumb">
        <li><Button type="button" class="btn btn-success btn-flat" onClick="searchModal()">검색 설정</Button></li>
      </ol>
    </section>
	<!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Search List</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="mytable" class="table table-bordered table-striped">
                <thead>
                  <tr>
                    <th>게임 명</th>
                    <th>일일 설치수</th>
                    <th>누적 설치수</th>
                    <th>일일 획득량</th>
                    <th>누적 획득량</th>
                    <th>일일 사용량</th>
                    <th>누적 사용량</th>
                    <th>일일 매출(￦)</th>
                    <th>누적 매출(￦)</th>
                  </tr>
                </thead>
                <tfoot>
	              <tr>
	               <th>합계(Total)</th>
	               <th id="Daily-installation"></th>
	               <th id="Entire-installation"></th>
	               <th id="Daily-acquisition"></th>
	               <th id="Entire-acquisition"></th>
	               <th id="Daily-usage"></th>
	               <th id="Entire-usage"></th>
	               <th id="Daily-sales"></th>
	               <th id="Entire-sales"></th>
	              </tr>
                  <tr>
                    <th>Game Name</th>
                    <th>Daily installation</th>
                    <th>Entire installation</th>
                    <th>Daily acquisition</th>
                    <th>Entire acquisition</th>
                    <th>Daily usage</th>
                    <th>Entire usage</th>
                    <th>Daily sales(￦)</th>
                    <th>Entire sales(￦)</th>
                  </tr>
                </tfoot>
                <tbody>
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
</div>


<jsp:include page="include/main_footer.jsp" flush="false"/>

<!-- search Modal -->
<div class="modal fade" id="searchModal" tabindex="-1" role="dialog" aria-labelledby="searchModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header" style="text-align: center;">
        <h1 class="modal-title" id="searchModalLabel">Setting Period</h1>
      </div>
      <div class="modal-body">
      	  <form id="modifyappeventform">
         <!-- Date and time range -->
            <div class="form-group">
            <label>기간 설정</label>
              <div class="input-group">
                <div class="input-group-addon">
                  <i class="fa fa-clock-o"></i>
                </div>
                <input type="text" name="searchTime" class="form-control pull-right" id="searchTime"/>
              </div>
              <!-- /.input group -->
            </div>
          <!-- /.form group -->
       </form>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
        <a class="btn btn-primary" href="#" onClick="searchDash();">검색</a>
      </div>
    </div>
  </div>
</div>
<jsp:include page="include/plugin_js.jsp" flush="false"/>

<!-- page script -->
<script>
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
		 $('#navi_dashboard').attr('class',"active");
	     searchDash();
	 });
	function searchDash()
	{
		 $.ajax({
				url:"/dashboard",
				type: "POST",
				data : {'searchTime':$("#searchTime").val()},
				dataType  : 'json',
		        success: function (data) {
				    var datatable = $('#mytable').DataTable();
                    datatable.clear();
		        	$.each(data , function(idx, item) {
                        datatable.row.add([
                            item.appName,
                            item.dailyInstallation,
                            item.entireInstallation,
                            item.dailyAcquisition,
                            item.entireAcquisition,
                            item.dailyUsage,
                            item.entireUsage,
                            item.dailySales,
                            item.entireSales
                        ]).draw();
		        	});
		        	$("#searchModal").modal('hide');
		        },
		        error:function(request,status,error){
		        	alert(request.responseText);
		        }
			});
	 }

	function searchModal(){
		 var date = moment();
		    $("#searchTime").daterangepicker({
		        timePicker: true,
		        timePickerIncrement: 1,
		        startDate: date,
		        locale: {
		            format: 'YYYY-MM-DD HH:mm'
		        }
		    });
		 $("#searchModal").modal('show');
	 }
	
	function tableSum() {
	    var di = 0;
	    $('#mytable tbody tr td:nth-child(2)').each(function() {
	    	di += parseInt($(this).text());
	    });
	
	    var ei = 0;
	    $('#mytable tbody tr td:nth-child(3)').each(function() {
	    	ei += parseInt($(this).text());
	    });
	    
	    var da = 0;
	    $('#mytable tbody tr td:nth-child(4)').each(function() {
	        da += parseInt($(this).text());
	    });
	
	    var ea = 0;
	    $('#mytable tbody tr td:nth-child(5)').each(function() {
	        ea += parseInt($(this).text());
	    });
	    
	    var du = 0;
	    $('#mytable tbody tr td:nth-child(6)').each(function() {
	    	du += parseInt($(this).text());
	    });
	
	    var eu = 0;
	    $('#mytable tbody tr td:nth-child(7)').each(function() {
	        eu += parseInt($(this).text());
	    });
	    
	    var ds = 0;
	    $('#mytable tbody tr td:nth-child(8)').each(function() {
	        ds += parseInt($(this).text().replace(",",""));
	        $(this).text($(this).text().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
	    });
	
	    var es = 0;
	    $('#mytable tbody tr td:nth-child(9)').each(function() {
	        es += parseInt($(this).text().replace(",",""));
	        $(this).text($(this).text().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
	    });
	
	    $('#Daily-installation').text(di);
	    $('#Entire-installation').text(ei);
	    $('#Daily-acquisition').text(da);
	    $('#Entire-acquisition').text(ea);
	    $('#Daily-usage').text(du);
	    $('#Entire-usage').text(eu);
	    $('#Daily-sales').text(ds.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
	    $('#Entire-sales').text(es.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
	}
</script>
</body>
</html>
