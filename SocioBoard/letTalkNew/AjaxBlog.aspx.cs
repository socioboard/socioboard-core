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
    public partial class AjaxBlog : System.Web.UI.Page
    {
        string type = string.Empty;
        Blog_PostsRepository objBlog_PostsRepository = new Blog_PostsRepository();
        Blog_CommentsRepository objBlog_CommentsRepository = new Blog_CommentsRepository();

        Blog_Posts objBlog_Posts = new Blog_Posts();
        Blog_Comments objBlog_Comments = new Blog_Comments();
        protected void Page_Load(object sender, EventArgs e)
        {
            string sdf = Request.Form["type"];
            if (Request.Form["type"] != null)
            {
                type = Request.Form["type"];
                string ret = CallBlog();
                Response.Write(ret);
            }


        }
        public string CallBlog()
        {
            User user = (User)Session["LoggedUser"];
            //if (user == null)
            //    Response.Redirect("Default.aspx");

            string ret = string.Empty;
            if (type == "B_post")
            {
                objBlog_Posts.Id = Guid.NewGuid();
                objBlog_Posts.CommentCount = 1;
                objBlog_Posts.CommentStatus = "OK";
                objBlog_Posts.PostAuthor = user.Id;
                objBlog_Posts.PostContent = Request.Form["content"];
                objBlog_Posts.PostDate = DateTime.Now;
                objBlog_Posts.PostModifiedDate = DateTime.Now;
                objBlog_Posts.PostName = Request.Form["name"];
                objBlog_Posts.PostStatus = "Ok";
                objBlog_Posts.PostTitle = Request.Form["title"];

                int res = objBlog_PostsRepository.Insert(objBlog_Posts);
                if (res > 0)
                {
                    ret = "success";
                }
                else
                {
                    ret = "error";
                }

            }
            else if (type == "C_post")
            {
                try
                {
                    objBlog_Comments.CommentApproved = "Yes";
                    objBlog_Comments.CommentAuthor = user.UserName;
                    objBlog_Comments.CommentAuthorEmail = user.EmailId;
                    objBlog_Comments.CommentAuthorIp = "No Ip";
                    objBlog_Comments.CommentAuthorUrl = user.ProfileUrl;
                    objBlog_Comments.CommentContent = Request.Form["content"];
                    objBlog_Comments.CommentDate = DateTime.Now;
                    objBlog_Comments.CommentPostId = Guid.Parse(Request.Form["blog_id"]);
                    objBlog_Comments.Id = Guid.NewGuid();

                    int res1 = objBlog_CommentsRepository.Insert(objBlog_Comments);
                    if (res1 > 0)
                    {
                        ret = "success";
                    }
                    else
                    {
                        ret = "error";
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (type == "B_Single")
            {
                //public  
                try
                {
                    objBlog_Posts.Id = Guid.Parse(Request.Form["blog_id"]);
                   // objBlog_Posts.CommentCount = 1;
                   // objBlog_Posts.CommentStatus = "OK";
                   // objBlog_Posts.PostAuthor = user.Id;
                    //objBlog_Posts.PostContent = Request.Form["content"];
                    //objBlog_Posts.PostDate = DateTime.Now;
                    //objBlog_Posts.PostModifiedDate = DateTime.Now;
                    //objBlog_Posts.PostName = Request.Form["name"];
                    //objBlog_Posts.PostStatus = "Ok";
                    //objBlog_Posts.PostTitle = Request.Form["title"];

                    ICollection<Blog_Posts> objlst = objBlog_PostsRepository.GetBlogId(objBlog_Posts);
                    if (objlst.Count > 0)
                    {
                        foreach (var item in objlst)
                        {
                            ret = item.PostContent;
                            break;
                        }
                        
                    }
                    else
                    {
                        ret = "error";
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return ret;

        }
    }
}