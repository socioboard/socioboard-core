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
    public partial class AjaxComment : System.Web.UI.Page
    {
        string type = string.Empty;
        string blog_id = string.Empty;
        Blog_Posts objBlog_Posts = new Blog_Posts();
        Blog_CommentsRepository objBlog_CommentsRepository = new Blog_CommentsRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Write("{\"type\":\"logout\"}");
                return;
            }
            string sdf = Request.Form["type"];


            System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
            string line = "";
            line = sr.ReadToEnd();
            Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(line);
            type = Server.UrlDecode((string)jo["type"]);
            blog_id = Server.UrlDecode((string)jo["blog_id"]);
            if (blog_id != null)
            {
                string ret = CallBlog();
                Response.Write(ret);
            }

        }
        public string CallBlog()
        {
            User user = (User)Session["LoggedUser"];
            string ret = string.Empty;
            try
            {
                objBlog_Posts.Id = Guid.Parse(blog_id);
                ICollection<Blog_Comments> objlst = objBlog_CommentsRepository.GetAllCommentByBlogIda(objBlog_Posts);
                if (objlst.Count > 0)
                {
                    string str = string.Empty;
                    var oserlize = new System.Web.Script.Serialization.JavaScriptSerializer();
                    str = oserlize.Serialize(objlst.ToList());

                    ret = str;
                }
                else
                {
                    ret = "";
                }
            }
            catch (Exception ex)
            {

            }
            return ret;

        }


    }
}