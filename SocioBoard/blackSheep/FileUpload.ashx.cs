using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blackSheep
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int s = context.Request.Files.Count;
            if (s > 0)
            {
                var file = context.Request.Files[0];
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