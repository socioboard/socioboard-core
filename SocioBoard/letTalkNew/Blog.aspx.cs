using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace letTalkNew
{
    public partial class Blog : System.Web.UI.Page
    {
        Blog_PostsRepository objBlog_PostsRepository;
        Blog_CommentsRepository objBlog_CommentsRepository;
        Blog_Posts objBlog_Posts;
        Blog_Comments objBlog_Comments;
        string blog_post = string.Empty;
        string Blog_post = string.Empty;
        string Blog_Detail = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];

            if (!IsPostBack)
            {
                objBlog_Posts = new Blog_Posts();
                objBlog_PostsRepository = new Blog_PostsRepository();
                ICollection<Blog_Posts> objBlog = objBlog_PostsRepository.GetAllBlogPosts(objBlog_Posts);
                try
                {
                    SocioBoard.Domain.User objuser;
                    SocioBoard.Model.UserRepository objuserrepo;
                    foreach (var item in objBlog)
                    {
                        objuser = new SocioBoard.Domain.User();
                        objuserrepo = new UserRepository();
                        objuser = objuserrepo.getUsersById(Guid.Parse(item.PostAuthor.ToString()));

                        blog_post += "<div class=\"inner_search_bottom_left_inner\">"
                            + "<img src=\"" + objuser.ProfileUrl + "\">"
                           + "<span>" + item.PostTitle + "<a href=\"#\">" + item.PostDate + "</a></span>"
                            + "<div style=\"float:right; margin:5px 0px 0px 0px; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;\">" + item.PostDate + "</div>"
                            + "<p style=\"width:84%;\">" + item.PostContent + "</p>"
                            + "<ul><li><a href=\"#\" class=\"readmore\" blog_img=\"" + objuser.ProfileUrl + "\" blog_id=\"" + item.Id + "\" blog_title=\"" + item.PostTitle + "\" blog_date=\"" + item.PostDate + "\" >Read more"
                            //+ "<div style=\"display:none\" blog_id=\"" + item.Id + "\" blog_date=\"" + item.PostDate + "\" >" + item.PostContent + "</div>"
                            + "</a></li></ul>"
                            + "</div>";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.StackTrace);

                }



                if (Session["LoggedUser"] == null)
                {

                    Blog_Detail += "<h2>Blog</h2><h3>To Day Post</h3>"
                   + "<div style=\"display: block;\" class=\"inner_search_bottom_left_inner_blog\" style=\"display:none\">"
                       + "<a class=\"hide\">X</a>"
                       + "<div id=\"b_date\" style=\"float:right; margin:0 21px 0 0; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;\"></div>"
                       + "<div><h2 id=\"b_title\"></h2></div>"
                      + "<div style=\"float: left; width: 100%;\">  <img src=\"\" id=\"b_img1\"></div>"
                        + "<p id=\"b_content\" style=\"width:98%\"></p>"
                           + "<p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p>"
                                + "<div class=\"blog_comment\" id=\"blog_comment\" runat=\"server\"><div style=\"float: left; font-size: x-large;margin-left: 50%;width: 100%;\">Comments</div>"
                               + "</div></div>";


                    post.InnerHtml = Blog_Detail + blog_post;
                }
                else
                {
                    // Blog_Detail += "<h2>Blog</h2><h3>To Day Post</h3>"
                    //+ "<div style=\"display: block;\" class=\"inner_search_bottom_left_inner_blog\" style=\"display:none\">"
                    //    + "<a class=\"hide\">X</a>"
                    //    + "<div id=\"b_date\" style=\"float:right; margin:0 21px 0 0; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;\">27 Dec 2012 </div>"
                    //    + "<h2 id=\"b_title\"> Lorem Ipsum is simply dummy</h2><img style=\"width:98%\" src=\"../Contents/img/1hb7sy13.jpg\">"
                    //     + "<p id=\"b_content\" style=\"width:98%\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh.</p>"
                    //         + "<p></p>"
                    //              + "<div class=\"blog_comment\" id=\"blog_comment\" runat=\"server\">"
                    //         + " <div class=\"reply_post\"><h2>Leave a Reply </h2>"
                    //         + "<label for=\"text-1\">Comment (<span>required</span>)</label><br>"
                    //         + "<textarea rows=\"10\" cols=\"30\" id=\"Textarea1\" name=\"comment\" title=\"text-area\" class=\"area\"></textarea><br>"
                    //         + "<input type=\"button\" id=\"post_comment\" class=\"reply_post_submit\" value=\"Post Comment\" name=\"submit\">"
                    //     + "</div></div></div>";


                   Blog_post = "<br><div id=\"reply_post\" class=\"reply_post\" style=\"display:none\">"
                       //<h2>Leave a Reply </h2><p>Your email address will not be published. Required fields are marked *</p>"
                  + "<label for=\"text-1\">Title (<span>required</span>)</label><br><input type=\"text\" class=\"text\" value=\"\" id=\"name\" name=\"author\"><br>"
                  // + "<label for=\"text-1\">Email (<span>required</span>)</label><br><input type=\"text\" class=\"text\" value=\"\" id=\"email\" name=\"author\"><br>"
                     + "<label for=\"text-1\">Blog (<span>Required</span>)</label><br><textarea rows=\"10\" cols=\"30\" id=\"blog\" name=\"comment\" title=\"text-area\" class=\"area\"></textarea><br>"
                    + "<input type=\"button\" id=\"blog\" class=\"reply_post_submit\" value=\"Post Blog\" name=\"submit\"></div>";



                    Blog_Detail += "<h2>Blog</h2> <input type=\"button\" id=\"showblog\" class=\"reply_post_submit\" value=\"Post A New Blog\" name=\"submit\">" + Blog_post
                       +" <h3>To Day Post</h3>"
                  + "<div style=\"display: block;\" class=\"inner_search_bottom_left_inner_blog\" style=\"display:none\">"
                      + "<a class=\"hide\">X</a>"
                      + "<div id=\"b_date\" style=\"float:right; margin:0 21px 0 0; padding:5px; color:#444; font-family:Arial, Helvetica, sans-serif; font-size:12px;\">27 Dec 2012 </div>"
                      + "<div><h2 id=\"b_title\"> Lorem Ipsum is simply dummy</h2></div>"
                    + "<div style=\"float: left; width: 100%;\">  <img src=\"\" id=\"b_img1\"></div>"
                       + "<p id=\"b_content\" style=\"width:98%\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet ligula ut arcu pellentesque condimentum. Suspendisse ac volutpat ligula. Mauris in bibendum neque. Vivamus ligula elit, fringilla vitae tincidunt sit amet, viverra ut sem. Ut dictum urna non sapien porttitor hendrerit. Morbi vel purus nibh. Cras ut purus turpis, non blandit nibh.</p>"
                           + "<p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p>"
                                + "<div class=\"blog_comment\" id=\"blog_comment\" runat=\"server\"><div style=\"float: left; font-size: x-large;margin-left: 50%;width: 100%;\">Comments</div>"
                          + "</div>"
                           + " <div class=\"reply_post\"><h2>Leave a Reply </h2>"
                           + "<label for=\"text-1\">Comment (<span>required</span>)</label><br>"
                           + "<textarea rows=\"10\" cols=\"30\" id=\"Textarea1\" name=\"comment\" title=\"text-area\" class=\"area\"></textarea><br>"
                           + "<input type=\"button\" id=\"post_comment\" class=\"reply_post_submit\" value=\"Post Comment\" name=\"submit\">"
                       + "</div></div>";






                   
                    post.InnerHtml = Blog_Detail + blog_post;
                }

            }










            //try
            //{
            //    SocioBoard.Domain.User user = (User)Session["LoggedUser"];

            //    Blog_PostsRepository objBlog_PostsRepository = new Blog_PostsRepository();
            //    Blog_CommentsRepository objBlog_CommentsRepository = new Blog_CommentsRepository();

            //    Blog_Posts objBlog_Posts = new Blog_Posts();
            //    Blog_Comments objBlog_Comments = new Blog_Comments();

            //    objBlog_Posts.Id = Guid.NewGuid();
            //    objBlog_Posts.CommentCount = 1;
            //    objBlog_Posts.CommentStatus = "OK";
            //    objBlog_Posts.PostAuthor = user.Id;
            //    objBlog_Posts.PostContent = "MyBlog1";
            //    objBlog_Posts.PostDate = DateTime.Now;
            //    objBlog_Posts.PostModifiedDate = DateTime.Now;
            //    objBlog_Posts.PostName = "MyBlog1";
            //    objBlog_Posts.PostStatus = "Ok";
            //    objBlog_Posts.PostTitle = "MyBlog1";

            //    int res = objBlog_PostsRepository.Insert(objBlog_Posts);

            //    objBlog_Comments.CommentApproved = "Yes";
            //    objBlog_Comments.CommentAuthor = user.UserName;
            //    objBlog_Comments.CommentAuthorEmail = user.EmailId;
            //    objBlog_Comments.CommentAuthorIp = "No Ip";
            //    objBlog_Comments.CommentAuthorUrl = "";
            //    objBlog_Comments.CommentContent = "My Comment 1";
            //    objBlog_Comments.CommentDate = DateTime.Now;
            //    objBlog_Comments.CommentPostId = Guid.NewGuid();
            //    objBlog_Comments.Id = Guid.NewGuid();

            //    int res1 = objBlog_CommentsRepository.Insert(objBlog_Comments);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error : " + ex.StackTrace);

            //}
        }
    }
}