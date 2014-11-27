using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for AdminNews
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminNews : System.Web.Services.WebService
    {
        NewsRepository objNewsRepo = new NewsRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllNews()
        {
            try
            {
                List<Domain.Socioboard.Domain.News> lstNews = objNewsRepo.getAllNews();
                return new JavaScriptSerializer().Serialize(lstNews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateNews(string ObjPackage)
        {
            try
            {
                Domain.Socioboard.Domain.News objNews = (Domain.Socioboard.Domain.News)(new JavaScriptSerializer().Deserialize(ObjPackage, typeof(Domain.Socioboard.Domain.News)));
                objNewsRepo.UpdateNews(objNews);
                return new JavaScriptSerializer().Serialize("Updated Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetNewsById(string NewsId)
        {
            try
            {
                Guid Newsid= Guid.Parse(NewsId);
                Domain.Socioboard.Domain.News objNews = objNewsRepo.getNewsDetailsbyId(Newsid);
                return new JavaScriptSerializer().Serialize(objNews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddNews(string ObjPackage,string News)
        {
            try
            {
                if (objNewsRepo.checkNewsExists(News))
                {
                    Domain.Socioboard.Domain.News objNews = (Domain.Socioboard.Domain.News)(new JavaScriptSerializer().Deserialize(ObjPackage, typeof(Domain.Socioboard.Domain.News)));
                    objNewsRepo.UpdateNews(objNews);
                    return new JavaScriptSerializer().Serialize("Success");
                }
                else
                {
                    Domain.Socioboard.Domain.News objNews = (Domain.Socioboard.Domain.News)(new JavaScriptSerializer().Deserialize(ObjPackage, typeof(Domain.Socioboard.Domain.News)));
                    objNewsRepo.AddNews(objNews);
                    return new JavaScriptSerializer().Serialize("Success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

    }
}
