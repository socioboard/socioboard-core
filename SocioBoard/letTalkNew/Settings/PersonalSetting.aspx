<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true" CodeBehind="PersonalSetting.aspx.cs" Inherits="letTalkNew.Settings.PersonalSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    #change_password > input {float: left;margin: 0 8px 0 0;}
     #content section {display: block;}
</style>
    <nav>
        <ul >
            <li><a class="current" href="PersonalSetting.aspx">Personal Setting</a></li>
            <li><a href="BusinessSetting.aspx">Business Setting</a></li>
            <li><a href="UserGroups.aspx">User & Groups</a></li>
            <li><a href="Billing.aspx">Billing</a></li>
        </ul>
        <span id="indicator"></span> 
   </nav>
        <div id="content">
              <section>
                    <div class="ws_tm_mid">
                            <div class="ws_tm_personal_setting">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div class="personal_setting_left">
                                <div class="invite_friends"> Personal Settings </div>
                                <div class="first_name_last_name_div">
                                     <span>
                                        <label>First Name</label>
                                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                    </span>
                                     <span>
                                        <label>Last Name</label>
                                       <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                    </span> 
                                    <span>
                                        <label>Email Id</label>
                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    </span>
                                    <h1>Change Password</h1>
                                    <span>
                                        <label>Current Password</label>
                                         <asp:TextBox class="pwd" ID="txtPassword" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                    </span> 
                                    <span>
                                        <label>New Password</label>
                                        <asp:TextBox class="cpwd" ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    </span> 
                                         <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" ErrorMessage="Password Missmatch" 
                            ValidationGroup="setting" ForeColor="Red" SetFocusOnError="True"></asp:CompareValidator>
                             </div>
                            <div id="change_password" class="first_name_last_name_div">
                                  <asp:Button class="cngpwd" ID="btnChangePassword" runat="server" Text="Change Password"
                            OnClick="changePassoword" />
                        <input id="btncancel" type="button" value="Cancel" />
                            </div>
                            <div class="ws_ps_user_content">
                                <div class="user_photo">  <asp:Image ID="custImg" runat="server" /></div>
                                <div class="personal_details" id="borwse"> Team members will see the avatar you have set for <strong> <em id="ContentPlaceHolder1_email_personal_for_setting"></em> </strong> on Browse. <br>
                                    <a class="file-input-wrapper btn">
                                        <em>Upload file</em>
                                         <asp:FileUpload ID="imgfileupload" runat="server" />
                            <asp:HiddenField ID="imghdn" runat="server" />
                                    </a>
                                </div>
                                <div style="width: 235px; height: 25px; margin-left: 57px;">
                                        <div style="width: 140px; height: 22px; float: left;">
                                            <div id="ListingImgMessage"> </div>
                                            <div class="FPlstImgUploading"> <img id="imgProgress" alt="" src=""> </div>
                                        </div>
                              </div>
                              <div class="img_crop"> <img id="listImg" style="width: 214px; height: 300px; display: none;" alt="" src=""> </div>
                           </div>
                            <div class="ws_ps_user_content">
                                <div class="time_zone"> Time Zone</div>
                                <div class="ps_select">
                                 <asp:DropDownList ID="ddlTimeZone" CssClass="select" runat="server" Height="21px"
                                Width="405px">
                            </asp:DropDownList>
                                       <%-- <select id="ContentPlaceHolder1_ddlTimeZone" class="select" style="height:21px;width:405px;" name="ctl00$ContentPlaceHolder1$ddlTimeZone">
                                        <option value="(UTC-12:00) International Date Line West">(UTC-12:00) International Date Line West</option>
                                        <option value="(UTC-11:00) Coordinated Universal Time-11">(UTC-11:00) Coordinated Universal Time-11</option>
                                        <option value="(UTC-10:00) Hawaii">(UTC-10:00) Hawaii</option>
                                        <option value="(UTC-09:00) Alaska">(UTC-09:00) Alaska</option>
                                        <option value="(UTC-08:00) Baja California">(UTC-08:00) Baja California</option>
                                        <option value="(UTC-08:00) Pacific Time (US & Canada)">(UTC-08:00) Pacific Time (US & Canada)</option>
                                        <option value="(UTC-07:00) Arizona">(UTC-07:00) Arizona</option>
                                        <option value="(UTC-07:00) Chihuahua, La Paz, Mazatlan">(UTC-07:00) Chihuahua, La Paz, Mazatlan</option>
                                        <option value="(UTC-07:00) Mountain Time (US & Canada)">(UTC-07:00) Mountain Time (US & Canada)</option>
                                        <option value="(UTC-06:00) Central America">(UTC-06:00) Central America</option>
                                        <option value="(UTC-06:00) Central Time (US & Canada)">(UTC-06:00) Central Time (US & Canada)</option>
                                        <option value="(UTC-06:00) Guadalajara, Mexico City, Monterrey">(UTC-06:00) Guadalajara, Mexico City, Monterrey</option>
                                        <option value="(UTC-06:00) Saskatchewan">(UTC-06:00) Saskatchewan</option>
                                        <option value="(UTC-05:00) Bogota, Lima, Quito">(UTC-05:00) Bogota, Lima, Quito</option>
                                        <option value="(UTC-05:00) Eastern Time (US & Canada)">(UTC-05:00) Eastern Time (US & Canada)</option>
                                        <option value="(UTC-05:00) Indiana (East)">(UTC-05:00) Indiana (East)</option>
                                        <option value="(UTC-04:30) Caracas">(UTC-04:30) Caracas</option>
                                        <option value="(UTC-04:00) Asuncion">(UTC-04:00) Asuncion</option>
                                        <option value="(UTC-04:00) Atlantic Time (Canada)">(UTC-04:00) Atlantic Time (Canada)</option>
                                        <option value="(UTC-04:00) Cuiaba">(UTC-04:00) Cuiaba</option>
                                        <option value="(UTC-04:00) Georgetown, La Paz, Manaus, San Juan">(UTC-04:00) Georgetown, La Paz, Manaus, San Juan</option>
                                        <option value="(UTC-04:00) Santiago">(UTC-04:00) Santiago</option>
                                        <option value="(UTC-03:30) Newfoundland">(UTC-03:30) Newfoundland</option>
                                        <option value="(UTC-03:00) Brasilia">(UTC-03:00) Brasilia</option>
                                        <option value="(UTC-03:00) Buenos Aires">(UTC-03:00) Buenos Aires</option>
                                        <option value="(UTC-03:00) Cayenne, Fortaleza">(UTC-03:00) Cayenne, Fortaleza</option>
                                        <option value="(UTC-03:00) Greenland">(UTC-03:00) Greenland</option>
                                        <option value="(UTC-03:00) Montevideo">(UTC-03:00) Montevideo</option>
                                        <option value="(UTC-03:00) Salvador">(UTC-03:00) Salvador</option>
                                        <option value="(UTC-02:00) Coordinated Universal Time-02">(UTC-02:00) Coordinated Universal Time-02</option>
                                        <option value="(UTC-02:00) Mid-Atlantic">(UTC-02:00) Mid-Atlantic</option>
                                        <option value="(UTC-01:00) Azores">(UTC-01:00) Azores</option>
                                        <option value="(UTC-01:00) Cape Verde Is.">(UTC-01:00) Cape Verde Is.</option>
                                        <option value="(UTC) Casablanca">(UTC) Casablanca</option>
                                        <option value="(UTC) Coordinated Universal Time">(UTC) Coordinated Universal Time</option>
                                        <option value="(UTC) Dublin, Edinburgh, Lisbon, London">(UTC) Dublin, Edinburgh, Lisbon, London</option>
                                        <option value="(UTC) Monrovia, Reykjavik">(UTC) Monrovia, Reykjavik</option>
                                        <option value="(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna">(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna</option>
                                        <option value="(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague">(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague</option>
                                        <option value="(UTC+01:00) Brussels, Copenhagen, Madrid, Paris">(UTC+01:00) Brussels, Copenhagen, Madrid, Paris</option>
                                        <option value="(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb">(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb</option>
                                        <option value="(UTC+01:00) West Central Africa">(UTC+01:00) West Central Africa</option>
                                        <option value="(UTC+01:00) Windhoek">(UTC+01:00) Windhoek</option>
                                        <option value="(UTC+02:00) Athens, Bucharest">(UTC+02:00) Athens, Bucharest</option>
                                        <option value="(UTC+02:00) Beirut">(UTC+02:00) Beirut</option>
                                        <option value="(UTC+02:00) Cairo">(UTC+02:00) Cairo</option>
                                        <option value="(UTC+02:00) Damascus">(UTC+02:00) Damascus</option>
                                        <option value="(UTC+02:00) E. Europe">(UTC+02:00) E. Europe</option>
                                        <option value="(UTC+02:00) Harare, Pretoria">(UTC+02:00) Harare, Pretoria</option>
                                        <option value="(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius">(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius</option>
                                        <option value="(UTC+02:00) Istanbul">(UTC+02:00) Istanbul</option>
                                        <option value="(UTC+02:00) Jerusalem">(UTC+02:00) Jerusalem</option>
                                        <option value="(UTC+03:00) Amman">(UTC+03:00) Amman</option>
                                        <option value="(UTC+03:00) Baghdad">(UTC+03:00) Baghdad</option>
                                        <option value="(UTC+03:00) Kaliningrad, Minsk">(UTC+03:00) Kaliningrad, Minsk</option>
                                        <option value="(UTC+03:00) Kuwait, Riyadh">(UTC+03:00) Kuwait, Riyadh</option>
                                        <option value="(UTC+03:00) Nairobi">(UTC+03:00) Nairobi</option>
                                        <option value="(UTC+03:30) Tehran">(UTC+03:30) Tehran</option>
                                        <option value="(UTC+04:00) Abu Dhabi, Muscat">(UTC+04:00) Abu Dhabi, Muscat</option>
                                        <option value="(UTC+04:00) Baku">(UTC+04:00) Baku</option>
                                        <option value="(UTC+04:00) Moscow, St. Petersburg, Volgograd">(UTC+04:00) Moscow, St. Petersburg, Volgograd</option>
                                        <option value="(UTC+04:00) Port Louis">(UTC+04:00) Port Louis</option>
                                        <option value="(UTC+04:00) Tbilisi">(UTC+04:00) Tbilisi</option>
                                        <option value="(UTC+04:00) Yerevan">(UTC+04:00) Yerevan</option>
                                        <option value="(UTC+04:30) Kabul">(UTC+04:30) Kabul</option>
                                        <option value="(UTC+05:00) Islamabad, Karachi">(UTC+05:00) Islamabad, Karachi</option>
                                        <option value="(UTC+05:00) Tashkent">(UTC+05:00) Tashkent</option>
                                        <option value="(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi">(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi</option>
                                        <option value="(UTC+05:30) Sri Jayawardenepura">(UTC+05:30) Sri Jayawardenepura</option>
                                        <option value="(UTC+05:45) Kathmandu">(UTC+05:45) Kathmandu</option>
                                        <option value="(UTC+06:00) Astana">(UTC+06:00) Astana</option>
                                        <option value="(UTC+06:00) Dhaka">(UTC+06:00) Dhaka</option>
                                        <option value="(UTC+06:00) Ekaterinburg">(UTC+06:00) Ekaterinburg</option>
                                        <option value="(UTC+06:30) Yangon (Rangoon)">(UTC+06:30) Yangon (Rangoon)</option>
                                        <option value="(UTC+07:00) Bangkok, Hanoi, Jakarta">(UTC+07:00) Bangkok, Hanoi, Jakarta</option>
                                        <option value="(UTC+07:00) Novosibirsk">(UTC+07:00) Novosibirsk</option>
                                        <option value="(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi">(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi</option>
                                        <option value="(UTC+08:00) Krasnoyarsk">(UTC+08:00) Krasnoyarsk</option>
                                        <option value="(UTC+08:00) Kuala Lumpur, Singapore">(UTC+08:00) Kuala Lumpur, Singapore</option>
                                        <option value="(UTC+08:00) Perth">(UTC+08:00) Perth</option>
                                        <option value="(UTC+08:00) Taipei">(UTC+08:00) Taipei</option>
                                        <option value="(UTC+08:00) Ulaanbaatar">(UTC+08:00) Ulaanbaatar</option>
                                        <option value="(UTC+09:00) Irkutsk">(UTC+09:00) Irkutsk</option>
                                        <option value="(UTC+09:00) Osaka, Sapporo, Tokyo">(UTC+09:00) Osaka, Sapporo, Tokyo</option>
                                        <option value="(UTC+09:00) Seoul">(UTC+09:00) Seoul</option>
                                        <option value="(UTC+09:30) Adelaide">(UTC+09:30) Adelaide</option>
                                        <option value="(UTC+09:30) Darwin">(UTC+09:30) Darwin</option>
                                        <option value="(UTC+10:00) Brisbane">(UTC+10:00) Brisbane</option>
                                        <option value="(UTC+10:00) Canberra, Melbourne, Sydney">(UTC+10:00) Canberra, Melbourne, Sydney</option>
                                        <option value="(UTC+10:00) Guam, Port Moresby">(UTC+10:00) Guam, Port Moresby</option>
                                        <option value="(UTC+10:00) Hobart">(UTC+10:00) Hobart</option>
                                        <option value="(UTC+10:00) Yakutsk">(UTC+10:00) Yakutsk</option>
                                        <option value="(UTC+11:00) Solomon Is., New Caledonia">(UTC+11:00) Solomon Is., New Caledonia</option>
                                        <option value="(UTC+11:00) Vladivostok">(UTC+11:00) Vladivostok</option>
                                        <option value="(UTC+12:00) Auckland, Wellington">(UTC+12:00) Auckland, Wellington</option>
                                        <option value="(UTC+12:00) Coordinated Universal Time+12">(UTC+12:00) Coordinated Universal Time+12</option>
                                        <option value="(UTC+12:00) Fiji">(UTC+12:00) Fiji</option>
                                        <option value="(UTC+12:00) Magadan">(UTC+12:00) Magadan</option>
                                        <option value="(UTC+12:00) Petropavlovsk-Kamchatsky - Old">(UTC+12:00) Petropavlovsk-Kamchatsky - Old</option>
                                        <option value="(UTC+13:00) Nuku'alofa">(UTC+13:00) Nuku'alofa</option>
                                        <option value="(UTC+13:00) Samoa">(UTC+13:00) Samoa</option>
                                    </select>--%>
                                </div>
                            </div>
                        </div>
                            </div>
                            <div class="ws_tm_button_div">
                            <%--<input id="ContentPlaceHolder1_btnSave" type="submit" onClick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("ctl00$ContentPlaceHolder1$btnSave", "", true, "setting", "", false, false))" value="Save" name="ctl00$ContentPlaceHolder1$btnSave">--%>
                               <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="setting" OnClick="btnSave_Click" />
                            </div>
                    </div>
              </section>
            </div>
</asp:Content>
