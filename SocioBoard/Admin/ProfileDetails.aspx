<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="ProfileDetails.aspx.cs" Inherits="SocialSuitePro.Admin.ProfileDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="content">
<div class="innerLR">
    <div class="widget">
		
		<!-- Widget heading -->
		<div class="widget-head">
			<h4 class="heading">Manage Profiles</h4>
		</div>
		<!-- // Widget heading END -->
		
		<div class="widget-body">
		
			<!-- Table -->
			<table class="dynamicTable table table-striped table-bordered table-condensed">
			
				<!-- Table heading -->
				<thead>
					<tr>
                    <th></th>
						<th>Name</th>
						<th>Profile Type</th>
						<th>Friends</th>
                       <%-- <th>User Status</th>--%>
					</tr>
				</thead>
				<!-- // Table heading END -->
				
				<!-- Table body -->
			<tbody id="Users" runat="server">
				
                </tbody>
				<!-- // Table body END -->
				
			</table>
			<!-- // Table END -->
			
		</div>
	</div>
</div>
</div>
</asp:Content>
