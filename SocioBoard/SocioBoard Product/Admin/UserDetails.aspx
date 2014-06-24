<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="SocialSuitePro.Admin.UserDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Contents/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../Contents/js/admin.js"></script>
    <div id="content">
        <div class="innerLR">
            <div class="widget">
                <!-- Widget heading -->
                <div class="widget-head">
                    <h4 class="heading">
                        Manage user</h4>
                </div>
                <!-- // Widget heading END -->
                <div class="widget-body">
                    <!-- Table -->
                    <table class="dynamicTable table table-striped table-bordered table-condensed">
                        <!-- Table heading -->
                        <thead>
                            <tr>
                                <th>
                                </th>
                                <th>
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Account Type
                                </th>
                                <th>
                                    Create Date
                                </th>
                                <th>
                                    Email Id
                                </th>
                             
                                <th>
                                    User Status
                                </th>
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
    <%--<script src="../Contents/js/jquery.sparkline.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.ba-resize.js"></script>

 <script type="text/javascript" src="../Contents/js/jquery.dataTables.min.js"></script>
 <script type="text/javascript" src="../Contents/js/DT_bootstrap.js"></script>
 <script type="text/javascript" src="../Contents/js/tables.js"></script>--%>
</asp:Content>
