<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="EditCoupons.aspx.cs" Inherits="SocioBoard.Admin.EditCoupons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     $(function () {
         $("#<%= datepicker.ClientID %>").datepicker();
     });
     function getDateValue() {
         debugger;
         $("#<%= datepicker.ClientID %>").val($("#datepicker").val());
     }
     $(function () {
         $("#<%= datepicker1.ClientID %>").datepicker();
     });
     function getDateValue() {
         debugger;
         $("#<%= datepicker1.ClientID %>").val($("#datepicker1").val());
     }
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
				    <li class="active"><a href="#account-details">Edit Coupons</a></li>
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
									<label class="control-label">Copons</label>
									<div class="controls">
                                        <asp:TextBox ID="txtNews" runat="server" class="span10"></asp:TextBox>                                       
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" data-toggle="tooltip" data-placement="top" data-original-title="First name is mandatory"><i></i></span>
									</div>
								</div>
								<!-- // Group END -->
                                	<!-- Group -->
								<div class="control-group">
									<label class="control-label">Entry Date</label>
									<div class="controls">
										<div class="input-append">
                                      <%--  <input type="text" value="13/06/1988" id="datepicker" >--%>
                                            <asp:TextBox ID="datepicker" runat="server" Visible="true"></asp:TextBox>
											   
											<span class="add-on glyphicons calendar"><i></i></span>
										</div>
									</div>
								</div>
                                <div class="control-group">
									<label class="control-label">Expiry Date</label>
									<div class="controls">
										<div class="input-append">
                                      <%--  <input type="text" value="13/06/1988" id="datepicker" >--%>
                                            <asp:TextBox ID="datepicker1" runat="server" Visible="true"></asp:TextBox>
											   
											<span class="add-on glyphicons calendar"><i></i></span>
										</div>
									</div>
								</div>
								<!-- // Group END -->
                                <!-- Group -->
								<div class="control-group">
									<label class="control-label">Status</label>
									<div class="controls">
										<span style="margin: 0;" class="btn-action single glyphicons circle_question_mark" ><%--<i></i>--%>
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Enable</asp:ListItem>
                                            <asp:ListItem Value="0">Disable</asp:ListItem>
                                        </asp:DropDownList>
                                        </span>
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
