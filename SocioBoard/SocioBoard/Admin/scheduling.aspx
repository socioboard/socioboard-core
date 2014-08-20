<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/adminSite.Master" AutoEventWireup="true"
    CodeBehind="scheduling.aspx.cs" Inherits="SocioBoard.Admin.scheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Contents/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            scheduledmsg();
            window.setInterval(scheduledmsg, 5000);
        })

        function scheduledmsg() {
            $.ajax
        ({
            type: "POST",
            url: "../Admin/Ajaxscheduled.aspx",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (msg) {
                $("#smsg").html(msg);
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
                                    Total Scheduled Message
                                </th>
                                <th>
                                    Already Scheduled
                                </th>
                                <th>
                                    Remaining
                                </th>
                                <%-- <th>User Status</th>--%>
                            </tr>
                        </thead>
                        <!-- // Table heading END -->
                        <!-- Table body -->
                        <tbody id="smsg">
                        </tbody>
                        <!-- // Table body END -->
                    </table>
                    <!-- // Table END -->
                </div>
            </div>
        </div>
    </div>
    <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>--%>
</asp:Content>
