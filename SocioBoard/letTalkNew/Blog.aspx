<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="letTalkNew.Blog" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Blog</title>
    <script>
        (function () { if (!/*@cc_on!@*/0) return; var e = "abbr,article,aside,audio,bb,canvas,datagrid,datalist,details,dialog,eventsource,figure,footer,header,hgroup,mark,menu,meter,nav,output,progress,section,time,video".split(','), i = e.length; while (i--) { document.createElement(e[i]) } })()
    </script>
    <!--[if lt Ie 9]>
        <script src="js/html5.js" type="text/javascript"></script>
    <![endif]-->
    <link href="../Contents/css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/admin.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/download.css" rel="stylesheet" type="text/css" />
    <link href="../Contents/css/calender.css" rel="stylesheet" type="text/css" />
    <!--[if Ie 7]>
        <style type="text/css">
            nav > .menu > ul { width:610px; margin:0 auto;}
            nav > .menu > ul li {float:left;}
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
            li{float:left;}
            section > .main > .section_top > .container_right > .graph > .social_graph ul > li > .social_activity_links > ul > li > .tabs > .scl_activity_links{margin-left:0;}
        </style>
    <![endif]-->
    <!--[if Ie 8]>
        <style type="text/css">            
            .cirlce_chart{position:relative;}
            .cirlce_chart > .value{margin-top:35px; margin-left:15px; width:80px; text-align:center;}
        </style>
    <![endif]-->
    <script src="../Contents/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //        
            //  $('span').live('click', function() {
            //    display("<tt>live</tt> caught a click!");
            //  });



            $("#showblog").click(function () {
                $('.inner_search_bottom_left_inner_blog').hide();
                $('#reply_post').show();
                $(this).hide();
            });




            $('.menu ul li a').click(function () {
                $('.menu ul li a').removeClass('active');
                $(this).addClass('active');
            });


            $('.mailbox_bot ul li').click(function () {
                $('.mailbox_bot ul li').removeClass('active');
                $(this).addClass('active');
            });
        });

    </script>
    <script type="text/javascript">

        $(function () {

            var indicator = $('#indicator'),
					indicatorHalfWidth = indicator.width() / 2,
					lis = $('#tabs').children('li');

            $("#tabs").tabs("#content section", {
                effect: 'fade',
                fadeOutSpeed: 0,
                fadeInSpeed: 400,
                onBeforeClick: function (event, index) {
                    var li = lis.eq(index),
					    newPos = li.position().left + (li.width() / 2) - indicatorHalfWidth;
                    indicator.stop(true).animate({ left: newPos }, 600, 'easeInOutExpo');
                }
            });

        });

    </script>
    <!-- stylesheets -->
    <link rel="stylesheet" type="text/css" href="../Contents/css/style.css" />
    <script type="text/javascript" src="../Contents/js/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.reveal.js"></script>
    <!-- javascript -->
    <script type="text/javascript" src="../Contents/js/jquery.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.tools.min.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.easing.1.3.js"></script>
    <!--[if lt IE9]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <link rel="stylesheet" type="text/css" href="../Contents/css/common.css">
    <link rel="stylesheet" type="text/css" href="../Contents/css/mopTip-2.2.css">
    <script type="text/javascript" src="../Contents/js/mopTip-2.2.js"></script>
    <script type="text/javascript" src="../Contents/js/jquery.pngFix-1.2.js"></script>


    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "advanced",
        plugins: "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,imagemanager,filemanager",
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: false,
        template_external_list_url: "js/template_list.js",
        external_link_list_url: "js/link_list.js",
        external_image_list_url: "js/image_list.js",
        media_external_list_url: "js/media_list.js"
    });
</script>






    <script type="text/javascript">
        $(document).ready(function () {
            $("#demo1Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo1" });
            $("#demo2Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo2" });
            $("#demo3Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo3" });
            $("#demo4Btn").mopTip({ 'w': 150, 'style': "overClick", 'get': "#demo4" });
            $("#demo5Btn").mopTip({ 'w': 150, 'style': "overOut", 'get': "#demo5" });
            $('.inner_search_bottom_left_inner_blog').hide();
            //ReadMore Click Call



            $('.readmore').click(function () {

                
                $("#b_title").html('');
                $("#b_date").html('');
                $("#b_content").html('');

                $('#reply_post').hide();
                $("#showblog").show();
                var blog_id = $(this).attr('blog_id');
                $('.inner_search_bottom_left_inner_blog').show();
                $("#b_title").html($(this).attr('blog_title'));
                $("#b_date").html($(this).attr('blog_date'));
                $("#b_img1").attr('src', $(this).attr('blog_img'));
                //var blog_id = $(this).attr('blog_id');

                $.ajax({
                    url: "../AjaxBlog.aspx",
                    type: "post",
                    data: '&blog_id=' + blog_id + '&type=B_Single',
                    success: function (data) {
                        //alert(data);
                        if (data != "") {
                            $("#b_content").html(data);
                        }
                        else {

                        }
                    },
                    error: function () {
                        alert("failure");
                        //$("#result").html('There is error while submit');
                    }
                });

                //return false;

                $("#post_comment").attr('blog_id', blog_id);
                $("#blog_comment").empty();
                $.ajax({
                    url: "../AjaxComment.aspx",
                    type: "post",
                    data: "{'blog_id':'" + blog_id + "','type':'C_GetAllComment'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    // data: 'blog_id=' + blog_id + '&type=C_GetAllComment',
                    success: function (data) {
                        try {
                            if (msg["type"].toString().contains("logout")) {
                                // alert("logout");
                                window.location = "../Default.aspx?hint=logout";
                                return false;
                            }
                        }
                        catch (e) {
                        }  
                        debugger;
                        if (data != "") {

                            for (var item in data) {
                                // alert(data[item].Id);
                                var id = data[item].Id;
                                var name = data[item].Id;
                                var content = data[item].Id;
                                $("#blog_comment").append('<div class="inner_search_bottom_left_inner"><img src="' + data[item].CommentAuthorUrl + '"><span>' + data[item].CommentAuthor + '<a href="#">11/30/2013 7:20:36 PM</a></span><div style="float:right; margin:5px 0px 0px 0px; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;">11/30/2013 7:20:36 PM</div><p style="width:84%;">' + data[item].CommentContent + '</p></div>');
                                // alert(data[item].Id);
                            }


                        }
                        else {
                            alert('No comment found');
                        }

                    },
                    error: function () {
                        // alert("failure");
                        //$("#result").html('There is error while submit');
                    }
                });







            });
            $('.hide').click(function () {
                $('.inner_search_bottom_left_inner_blog').hide();
            });

            $(".reply_post_submit").on('click', function () {

                //alert("asd");
                var value = $(this).val();
                if (value == "") {
                    alert("Something wrong");
                    return false;
                }
                else if (value == "Post Blog") {
                    var name = $("#name").val();
                    var email = $("#email").val();
                    tinyMCE.triggerSave();
                    var content = encodeURIComponent($("#blog").val().trim());
                    $.ajax({
                        url: "../AjaxBlog.aspx",
                        type: "post",
                        data: 'title=' + name + '&email=' + email + '&content=' + content.toString() + '&type=B_post',
                        success: function (data) {
                            if (data == "success") {
                                alert('Blog posted successfully');
                                //window.location = self.location;
                                window.location.href = "Blog.aspx";
                            }
                            else {
                                alert('problem in Blog inserting');
                            }

                        },
                        error: function () {
                            alert("failure");
                            //$("#result").html('There is error while submit');
                        }
                    });
                }
                else if (value == "Post Comment") {
                    tinyMCE.triggerSave();
                    var content = $("#Textarea1").val().trim();
                    var blog_id = $(this).attr('blog_id');



                    $.ajax({
                        url: "../AjaxBlog.aspx",
                        type: "post",
                        data: 'content=' + content + '&blog_id=' + blog_id + '&type=C_post',
                        success: function (data) {
                            if (data == "success") {
                                alert('comment posted successfully');
                            }
                            else {
                                alert('problem in comment inserting');
                            }
                        },
                        error: function () {
                            alert("failure");
                            //$("#result").html('There is error while submit');
                        }
                    });
                }
            });
        });
    </script>
</head>
<body>
    <form method="post" action="Default.aspx" id="ctl01">
    <div class="aspNetHidden">
        <input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKMTY1NDU2MTA1MmRkm9sNplIkyfmUgZccZXeD4hUQ0nuTVu3ch1RGpTFnUbM=" />
    </div>
    <div class="page">
        <header>
          <div class="header">
            <div class="letstalk_logo"> <a href="index.html"><img src="../Contents/img/letstalk_logo.png" alt="" /></a> </div>
            <nav>
              <div class="menu">
                <ul>
                  <li><a class="menu_link_bg home" href="<%= Page.ResolveUrl("~/Home.aspx")%>">
                    <div class="menu_tab_name">Home</div>
                    </a></li>
                   <%--  <li><a class="menu_link_bg group-1" href="#">
                    <div class="menu_tab_name">Group Message (s)</div>
                    </a></li>--%>
                  <li><a class="menu_link_bg message" href="<%=Page.ResolveUrl("~/Message/Messages.aspx")%>">
                    <div class="menu_tab_name">indivitual Message (s)</div>
                    </a></li>
                  <li><a class="menu_link_bg download" href="<%=Page.ResolveUrl("~/Feeds/Feed.aspx")%>">
                    <div class="menu_tab_name">Feeds</div>
                    </a></li>
                  <li><a class="menu_link_bg group" href="<%=Page.ResolveUrl("~/Publishing.aspx")%>">
                    <div class="menu_tab_name">Publishing</div>
                    </a></li>
                  <li><a class="menu_link_bg reports" href="<%=Page.ResolveUrl("~/Discovery.aspx")%>">
                    <div class="menu_tab_name" >Discovery</div>
                    </a></li>
                  <li><a class="menu_link_bg settings" href="<%=Page.ResolveUrl("~/Reports/GroupStats.aspx")%>">
                    <div class="menu_tab_name">Reports</div>
                    </a></li>

                    <li><a class="menu_link_bg group-1 active" href="<%=Page.ResolveUrl("~/Blog.aspx")%>">
                    <div class="menu_tab_name">Blogs</div>
                    </a></li>
                </ul>
              </div>
            </nav>
             <div class="logout"><a href="<%=Page.ResolveUrl("~/Default.aspx")%>"><img src="<%=Page.ResolveUrl("~/Contents/img/logout.png")%>" alt="" /></a></div>
          </div>
        </header>
        <section>
          <div class="main"> 
            
            <!--section_top-->
            <div class="section_top"> 
             	<div id="post" class="inner_search_bottom_left" runat="server">
	                    <h2>Blog</h2>
                   	  <h3>To Day Post</h3>
                      <div class="inner_search_bottom_left_inner_blog" style="display:none">
                        <a class="hide">X</a>
                      	<div style="float:right; margin:0 21px 0 0; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;">27 Dec 2012 </div>
                        <h2> Lorem Ipsum is simply dummy</h2>
                        
                        	<img src="../Contents/img/1hb7sy13.jpg" style="width:98%">                          
                            
                            
                        	<p style="width:98%">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh.

Nullam nec quam sem. Duis eu felis arc </p>                          
                             <p style="width:98%">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc </p>
                            <p style="width:98%">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh. Nullam nec quam sem. Duis eu felis arc </p>
                            <p>                             
                            

                            <div class="blog_comment" id="blog_comment" runat="server">
                                <div class="reply_post">                       	
			                        <h2>Leave a Reply </h2>			
			                        <p>Your email address will not be published. Required fields are marked *</p>
				                    <label for="text-1">Name (<span>required</span>)</label><br/>
                                    <input type="text" name="author" id="text1" value="" class="text"><br/>                
                                    <label for="text-1">Email (<span>required</span>)</label><br/>
                                    <input type="text" name="author" id="text2" value="" class="text"><br/>
                                    <label for="text-1">Comment (<span>required</span>)</label><br/>
                                    <textarea class="area" title="text-area" name="comment" id="Textarea1" cols="30" rows="10"></textarea><br/>
                                    <input type="submit" name="submit" value="Post Commen" class="reply_post_submit">
                                </div>
                            </div>

                        </div>
                      
                      
                      
                      
                      
                      
                      
                        <div class="inner_search_bottom_left_inner">
                        	<img src="../Contents/img/1hb7sy13.jpg" >
                            <span>Lorem Ipsum is simply dummy text <a href="#">1 year, 3 months ago</a></span>
                            <div style="float:right; margin:5px 0px 0px 0px; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;">27 Dec 2012 </div>
                        	<p style="width:84%;">Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum is simply dummy text of the printing and typesetting industry</p>                          
                           
                        </div>
                        
                        
                        <div class="inner_search_bottom_left_inner">
                        	<img src="../Contents/img/1hb7sy13.jpg" >
                            <span>Lorem Ipsum is simply dummy text <a href="#">1 year, 3 months ago</a></span>
                            <div style="float:right; margin:5px 0px 0px 0px; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;">27 Dec 2012 </div>
                        	<p style="width:84%;">Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum is simply dummy text of the printing and typesetting industry</p>                          
                            <ul>
                            	<li><a href="#" class="readmore">Read more</a></li>                                
                            </ul>
                        </div>
                        
                        
                        
                        
                        
                        
                        <br/>

                        <div class="reply_post">
                        	
			<h2>Leave a Reply </h2>
			
			<p>Your email address will not be published. Required fields are marked *</p>
	
			
				<label for="text-1">Name (<span>required</span>)</label><br/>

                <input type="text" name="author" id="text-1" value="" class="text"><br/>

                
                <label for="text-1">Email (<span>required</span>)</label><br/>

                <input type="text" name="author" id="text-1" value="" class="text"><br/>

                
                <label for="text-1">Comment (<span>required</span>)</label><br/>

                <textarea class="area" title="text-area" name="comment" id="comment" cols="30" rows="10"></textarea><br/>

				<input type="submit" name="submit" value="Post Commen" class="reply_post_submit">
						
			
				
				
						
			
		
                        </div>
                    </div>
              
            </div>
            <!--end section_top--> 
            
            <!--section_bot-->
            
            <!--end section_bot--> 
            
            <!-- graph chart script here--> 
            
            <script src="../Contents/js/jquery.easy-pie-chart.js" type="text/javascript"></script> 
            <script type="text/javascript">
                $(function () {
                    // fb graph chart for male and female
                    $('.femalefbchart').easyPieChart({
                        //your configuration goes here
                        barColor: '#ff569a'
                    });

                    $('.malefbchart').easyPieChart({
                        //your configuration goes here
                        barColor: '#44619d'
                    });

                    // end fb graph chart for male and female

                    //twitter graph chart for male and female

                    $('.twitermalechart').easyPieChart({
                        //your configuration goes here
                        barColor: '#14b9d6'
                    });

                    $('.twitterfemalechart').easyPieChart({
                        //your configuration goes here
                        barColor: '#ff569a'
                    });

                    // end twitter graph chart for male and female

                });
</script> 
          </div>
        </section>
        <div class="clear">
        </div>
    </div>
    </form>
</body>
</html>
