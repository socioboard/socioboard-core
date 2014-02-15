<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="OnlineUsers.aspx.cs" Inherits="SocialSuitePro.Admin.OnlineUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="content">
<div class="innerLR">
    <div class="widget">
		
		<!-- Widget heading -->
		<div class="widget-head">
			<h4 class="heading">Online Users</h4>
		</div>
		<!-- // Widget heading END -->
		
		<div class="widget-body">
		
			<!-- Table -->
			<table class="dynamicTable table table-striped table-bordered table-condensed">
			
				<!-- Table heading -->
				<thead>
					<tr>
                        <th></th>
						<th>User Name</th>
						<th>Payment Status</th>
						<th>Time Zone</th>
						<th>Account Type</th>
					</tr>
				</thead>
				<!-- // Table heading END -->
				
				<!-- Table body -->
				<tbody id="divNews" runat="server">

                </tbody>
				<!-- // Table body END -->
				
			</table>
			<!-- // Table END -->
			
		</div>
	</div>
</div>
</div> 
</asp:Content>