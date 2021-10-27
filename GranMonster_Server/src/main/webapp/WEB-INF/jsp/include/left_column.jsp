 <%@ page language="java" contentType="text/html; charset=UTF-8"
         pageEncoding="UTF-8" %>
 <!-- Left side column. contains the logo and sidebar -->
  <aside class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">
      <!-- sidebar menu: : style can be found in sidebar.less -->
      <ul class="sidebar-menu" data-widget="tree">
        <li class="header">MAIN MENU</li>
        <li id="navi_dash">
          <a href="/admin">
            <i class="fa fa-dashboard"></i><span>Dashboard</span>
          </a>
        </li>
		<li id="navi_app">
          <a href="/managementapp">
            <i class="fa fa-cubes"></i><span>앱 관리</span>
          </a>
        </li>
       	<li id="navi_company">
          <a href="/managementcompany">
            <i class="fa fa-building"></i><span>회사 관리</span>
          </a>
        </li>
        <li id="navi_user">
          <a href="/managementuser">
            <i class="fa fa-user-circle-o"></i><span>회원 관리</span>
          </a>
        </li>
		<li id="navi_billing">
          <a href="/managementbilling">
            <i class="fa fa-won"></i><span>영수증 관리</span>
          </a>
        </li>
		<li id="navi_exchange">
          <a href="/managementexchange">
            <i class="fa fa-credit-card"></i><span>그랑코인 관리</span>
          </a>
        </li>
        <li id="navi_sdk" class="treeview">
          <a href="">
            <i class="fa fa-support"></i><span>SDK Guide</span>
             <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
            <li>
            	<a href="/managementsdk"><i class="fa fa-circle-o"></i> 1. SDK 다운로드</a>
            </li>
             <li>
            	<a href="/managementsdk_login"><i class="fa fa-circle-o"></i> 2. 로그인 연동</a>
            </li>
            <li class="treeview">
              <a href="#"><i class="fa fa-circle-o"></i> 3. 위젯 연동
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu" id="managementsdk_widget">
                <li><a href="/managementsdk_widget_dialog"><i class="fa fa-circle-o"></i> 3-1. 위젯 다이얼로그</a></li>
                <li><a href="/managementsdk_widget_button"><i class="fa fa-circle-o"></i> 3-2. 위젯 플로팅 버튼</a></li>
              </ul>
            </li>
            <li class="treeview">
              <a href="#"><i class="fa fa-circle-o"></i> 4. 결제 연동
                <span class="pull-right-container">
                  <i class="fa fa-angle-left pull-right"></i>
                </span>
              </a>
              <ul class="treeview-menu" id="managementsdk_billing">
                <li><a href="/managementsdk_billing"><i class="fa fa-circle-o"></i> 4-1. 연동 방법 </a></li>
                <li><a href="/managementsdk_billing_dialog"><i class="fa fa-circle-o"></i> 4-2. Gran샵 다이얼로그</a></li>
                <li><a href="/managementsdk_billing_button"><i class="fa fa-circle-o"></i> 4-3. Gran샵 플로팅 버튼</a></li>
                <li><a href="/managementsdk_billing_exhaust"><i class="fa fa-circle-o"></i> 4-3. 그랑코인 사용</a></li>
              </ul>
            </li>
             <li>
            	<a href="/managementsdk_event"><i class="fa fa-circle-o"></i> 5. 이벤트 연동</a>
            </li>
            <li>
            	<a href="/managementsdk_errorcode"><i class="fa fa-circle-o"></i> 6. 에러 코드</a>
            </li>
          </ul>
        </li>
        
      </ul>
    </section>
    <!-- /.sidebar -->
  </aside>