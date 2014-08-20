<%@ Page Title="Social Suite Pro" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="EditUserDetail.aspx.cs" Inherits="SocialSuitePro.Admin.EditUserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
.row-fluid .span12 {
    width: 94%;
}
.row-fluid .span10 {
    width: 94%;
}
</style><div "="" id="content">
	
    <div class="innerLR" style="margin-top: 10px; margin-left:-15px;">
	    <!-- Widget -->
	    <div class="widget widget-tabs border-bottom-none">	
		    <!-- Widget heading -->
		    <div class="widget-head">
			    <ul>
				    <li class="active"><a href="#account-details">User Detail</a></li>
				    <%--<li class=""><a data-toggle="tab" href="#account-settings" class="glyphicons settings"><i></i>Account settings</a></li>--%>
			    </ul>
		    </div>
		    <!-- // Widget heading END -->
		
		    <div class="widget-body">
			    <form style="margin: 0;" class="form-horizontal">
				    <div style="padding: 0;" class="tab-content">
                    <div class="tab-pane active" id="account-details">
					
						<!-- Row -->
						<div class="row-fluid">
						
							<!-- Column -->
							<div class="span6">
							
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">Name</label>
									<div class="controls">
                                        <asp:TextBox ID="txtName" runat="server" class="span10"></asp:TextBox>                                       
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" data-toggle="tooltip" data-placement="top" data-original-title="First name is mandatory"><i></i></span>
									</div>
								</div>
								<!-- // Group END -->
								
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">Email</label>
									<div class="controls">
                                      <asp:TextBox ID="txtEmail" runat="server" class="span10"></asp:TextBox>
										<%--<input type="text" value="Doe" class="span10" />--%>
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" data-toggle="tooltip" data-placement="top" data-original-title="Last name is mandatory"><i></i></span>
									</div>
								</div>
								<!-- // Group END -->
								
								<!-- Group -->
								<div class="control-group" style="display:none">
									<label class="control-label">Expiry Date</label>
									<div class="controls">
										<div class="input-append">
                                      <%--  <input type="text" value="13/06/1988" id="datepicker" >--%>
                                            <asp:TextBox ID="datepicker1" runat="server" Visible="true" ReadOnly="True"></asp:TextBox>
											   
											<%--<span class="add-on glyphicons calendar"><i></i></span>--%>
										</div>
									</div>
								</div>
								<!-- // Group END -->
							
							
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">Gender</label>
									<div class="controls">
										<select class="span12">
											<option>Male</option>
											<option>Female</option>
										</select>
									</div>
								</div>
								<!-- // Group END -->
                                	<!-- Group -->
								<div class="control-group">
									<label class="control-label">Package </label>
									<div class="controls">
                                        <asp:DropDownList ID="ddlPackage" runat="server">
                                        </asp:DropDownList>
									</div>
								</div>
								<!-- // Group END -->
                                 	<!-- Group -->
								<div class="control-group">
									<label class="control-label">Status </label>
									<div class="controls">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem Value="2">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Enable</asp:ListItem>
                                            <asp:ListItem Value="0">Disable</asp:ListItem>
                                        </asp:DropDownList>
									</div>
								</div>
								<!-- // Group END -->
						</div>
						<!-- // Row END -->
						
						<div class="separator line bottom"></div>
						
						<!-- Group -->
						<div class="control-group row-fluid">
							<label class="control-label"></label>
							<div class="controls">
								
							</div>
						</div>
						<!-- // Group END -->
						
						<!-- Form actions -->
						<div class="form-actions" style="margin: 0;">
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" 
                                class="btn btn-icon btn-primary circle_ok" onclick="btnSave_Click"/>
                            <%--	<button type="submit" class="btn btn-icon btn-primary glyphicons circle_ok"><i></i>Save changes</button>
							<button type="button" class="btn btn-icon btn-default glyphicons circle_remove"><i></i>Cancel</button>--%>
						</div>
						<!-- // Form actions END -->
						
					</div>
                    </div>
			    </form>
		    </div>
	    </div>
	    <!-- // Widget END -->
	
    </div>		
</div>

</asp:Content>
