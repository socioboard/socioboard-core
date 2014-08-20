<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="Managecoupons.aspx.cs" Inherits="SocioBoard.Admin.Managecoupons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="content">
<div class="innerLR">
    <div class="widget">
		
		<!-- Widget heading -->
		<div class="widget-head">
			<h4 class="heading">Manage News</h4>
		</div>
		<!-- // Widget heading END -->
		
		<div class="widget-body">
		
			<!-- Table -->
			<table class="dynamicTable table table-striped table-bordered table-condensed">
			
				<!-- Table heading -->
				<thead>
					<tr>
                        <th></th>
						<th>Coupons</th>
						<th>Entry Date</th>
						<th>Expiry Date</th>
						<th>Status</th>
					</tr>
				</thead>
				<!-- // Table heading END -->
				
				<!-- Table body -->

                <tbody id="divNews" runat="server">
                <!-- Table row -->
				<%--	<tr class="gradeX">
						<td>Trident</td>
						<td>Internet Explorer 4.0</td>
						<td>Win 95+</td>
						<td class="center">4</td>
						<td class="center">X</td>
					</tr>--%>
					<!-- // Table row END -->


                    </tbody>
				
				<!-- // Table body END -->
				
			</table>
			<!-- // Table END -->
			
		</div>
	</div>
</div>
</div> 
</asp:Content>
