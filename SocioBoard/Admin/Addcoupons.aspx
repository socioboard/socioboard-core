<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="Addcoupons.aspx.cs" Inherits="SocioBoard.Admin.Addcoupons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div "="" id="content">
	
    <div class="innerLR" style="margin-top: 10px; margin-left:-15px;">
	    <!-- Widget -->
	    <div class="widget widget-tabs border-bottom-none">	
		    <!-- Widget heading -->
		    <div class="widget-head">
			    <ul>
				    <li class="active"><a href="#account-details">Add Coupons</a></li>
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
							<div class="span10">
							
								<!-- Group -->
								<div class="control-group">
									<label class="control-label">Entry coupons</label>
									<div class="controls">
                                     <asp:Label ID="Label2" runat="server" Text=""></asp:Label>  <asp:TextBox ID="txtcoupons" runat="server" class="span5"></asp:TextBox>     
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>                                 
									</div>
								</div>
								<!-- // Group END -->
                                	<!-- Group -->
								
								<!-- // Group END -->
                                <!-- Group -->
								
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
                            <asp:Button ID="btnSave" runat="server" Text="Add" 
                                class="btn btn-icon btn-primary circle_ok" onclick="btnSave_Click"/>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcoupons" ErrorMessage="Please must Enter 25 digits code."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtcoupons" ValidationExpression="^[a-zA-Z0-9]{25}$" runat="server" ErrorMessage="Please must Enter 25 digits code."></asp:RegularExpressionValidator>
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
