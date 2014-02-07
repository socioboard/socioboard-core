<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="AddAdvertisement.aspx.cs" Inherits="SocialSuitePro.Admin.AddAdvertisement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="content">
<script type="text/jscript" src="../Contents/js/jquery-1.9.1.min.js"></script>

    <script type="text/javascript">
        $(function () {
            // alert("ok");
            $("#<%= datepicker.ClientID %>").datepicker();

        });
    </script>
	
    <div class="innerLR" style="margin-top: 10px; margin-left:-15px;">
	    <!-- Widget -->
	    <div class="widget widget-tabs border-bottom-none">	
		    <!-- Widget heading -->
		    <div class="widget-head">
			    <ul>
				    <li class="active"><a href="#account-details">Add Advertisement</a></li>
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
							<!-- Group -->
								<div class="control-group">
									<label class="control-label">Advertisement</label>
									<div class="controls">
                                                                              
										
                                       <asp:TextBox ID="txtAdv" runat="server"></asp:TextBox>
                                       
									</div>
								</div>
								<!-- // Group END -->
							<!-- Column -->
							<div class="formdetails">
							
								<!-- Group -->
								<div class="control-group">
									<label class="control-label"> Image url</label>
									<div class="controls">
                                                                              
										
                                          <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
                                      
									</div>
                                    <div class="or">OR</div>
                                    <label class="control-label" style="width:auto; margin-right:10px; margin-left: 6px;">Image</label>
									<div class="controls" style="width:200px;">
                                            <asp:FileUpload ID="fuAdv" runat="server" />
									</div>
                                   <%-- <div class="or">OR</div>
                                    <label class="control-label" style="width:auto; margin-right:10px; margin-left: 6px;">Script</label>
									<div class="controls">
                                            <asp:TextBox ID="txtScript" runat="server"></asp:TextBox>
                                   
									</div>--%>

								</div>
								<!-- // Group END -->

                                	<!-- Group -->
								<%--<div class="control-group">
									
								</div>--%>
								<!-- // Group END -->

                                
                                	<!-- Group -->
								<%--<div class="control-group">
									
								</div>--%>
								<!-- // Group END -->

                                	<!-- Group -->
								<div class="control-group">
									<label class="control-label">Expiry Date</label>
									<div class="controls">
										<div class="input-append">
                                      <%--  <input type="text" value="13/06/1988" id="datepicker" >--%>
                                            <asp:TextBox ID="datepicker" runat="server" Visible="true"></asp:TextBox>
										</div>
									</div>
								</div>
								<!-- // Group END -->
                                <!-- Group -->
								<div class="control-group">
									<label class="control-label">Status</label>
									<div class="controls">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="True">Enable</asp:ListItem>
                                            <asp:ListItem Value="False">Disable</asp:ListItem>
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
