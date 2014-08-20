<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true" CodeBehind="LoginDetails.aspx.cs" Inherits="SocioBoard.Admin.LoginDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Contents/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            scheduledmsg();
            window.setInterval(scheduledmsg, 30*60*1000);
        })

        function scheduledmsg() {
            $.ajax
        ({
            type: "POST",
            url: "../Admin/AjaxLoginDetails.aspx",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#ldtl").html(msg);
            }
        });
        };
    </script>
    <div id="content">
        <div class="innerLR">
            <div class="widget">
                <!-- Widget heading -->
                <div class="widget-head">
                    <h4 class="heading">
                        Manage Profiles</h4>
                </div>
                <!-- // Widget heading END -->
                <div class="widget-body">
                    <!-- Table -->
                    <table class="dynamicTable table table-striped table-bordered table-condensed">
                        <!-- Table heading -->
                        <thead>
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                   Total Login No
                                </th>
                                <%-- <th>User Status</th>--%>
                            </tr>
                        </thead>
                        <!-- // Table heading END -->
                        <!-- Table body -->
                        <tbody id="ldtl">
                        </tbody>
                        <!-- // Table body END -->
                    </table>
                    <!-- // Table END -->
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
</asp:Content>
