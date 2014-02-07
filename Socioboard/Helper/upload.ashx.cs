using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SocioBoard.Helper
{
    /// <summary>
    /// Summary description for upload
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files["formData"];
                string savepath = "";
                string tempPath = "";
                tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                savepath = context.Server.MapPath(tempPath);
                string filename = Guid.NewGuid().ToString();
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);
                postedFile.SaveAs(savepath + @"\" + filename);
                context.Response.Write(tempPath + "/" + filename);
                context.Response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}