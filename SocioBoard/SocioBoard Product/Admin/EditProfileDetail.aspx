<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="EditProfileDetail.aspx.cs" Inherits="SocialSuitePro.Admin.EditProfileDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
.row-fluid .span12 {
    width: 94%;
}
.row-fluid .span10 {
    width: 94%;
}
</style>
<div "="" id="content">
	
    <div class="innerLR" style="margin-top: 10px; margin-left:-15px;">
	    <!-- Widget -->
	    <div class="widget widget-tabs border-bottom-none">	
		    <!-- Widget heading -->
		    <div class="widget-head">
			    <ul>
				    <li class="active"><a data-toggle="tab" href="#account-details" class="glyphicons edit">
                        Profile</a></li>
				    <%--<li class=""><a data-toggle="tab" href="#account-settings" class="glyphicons settings"><i></i>Account settings</a></li>--%>
			    </ul>
		    </div>
		    <!-- // Widget heading END -->
		
		    <div class="widget-body">
			    <form style="margin: 0;" class="form-horizontal">
				    <div style="padding: 0;" class="tab-content">
				
					    <!-- Tab content -->
					    <div id="Div1" class="tab-pane active">
					
						    <!-- Row -->
						    <div class="row-fluid">
							    <!-- Column -->
							    <div class="span6">
								    <!-- Group -->
								    <div class="control-group">
									    <label class="control-label">Name</label>
									    <div class="controls">
										    <asp:TextBox ID="txtName" runat="server" class="span10"></asp:TextBox> 
										    <span data-original-title="First name is mandatory" data-placement="top" data-toggle="tooltip" class="btn-action single glyphicons circle_question_mark" style="margin: 0;"><i></i></span>
									    </div>
								    </div>
								    <!-- // Group END -->

                                    <!-- Group -->
								   <%-- <div class="control-group">
									    <label class="control-label">Status</label>
									    <div class="controls">
										    <asp:DropDownList ID="ddltatus" runat="server" class="span12">
                                                <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Enable</asp:ListItem>
                                                <asp:ListItem Value="0">Disable</asp:ListItem>
                                            </asp:DropDownList>
									    </div>
								    </div>--%>
								    <!-- // Group END -->
							    </div>
							    <!-- // Column END -->
							   
						    </div>
						    <!-- // Row END -->
						
						    <div class="separator line bottom"></div>
						
						    <!-- Group -->
						    
						    <!-- // Group END -->
						
						    <!-- Form actions -->
						    <div style="margin: 0;" class="form-actions">
							    <%--<button class="btn btn-icon btn-primary glyphicons circle_ok" type="submit"><i></i>Save changes</button>--%>
							   <asp:Button ID="btnSave" runat="server" Text="Save Changes" class="btn btn-icon btn-primary circle_ok" onclick="btnSave_Click"/>
						    </div>
						    <!-- // Form actions END -->
						
					    </div>
					    <!-- // Tab content END -->
					    
				    </div>
			    </form>
		    </div>
	    </div>
	    <!-- // Widget END -->
	
    </div>		
</div>
</asp:Content>
