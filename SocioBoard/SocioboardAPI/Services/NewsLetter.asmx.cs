using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Configuration;
namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class NewsLetter : System.Web.Services.WebService
    {
        NewsLetterRepository ObjNewsLetterRepository = new NewsLetterRepository();
        Domain.Socioboard.Domain.NewsLetter ObjNewsLetter = new Domain.Socioboard.Domain.NewsLetter();
        
        
        
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllNewsLetters()
        {

            //List<NewsLetter> = ObjNewsLetterRepository.getAllNewsLetter();
            try
            {
                List<Domain.Socioboard.Domain.NewsLetter> lstNewsLetter = new List<Domain.Socioboard.Domain.NewsLetter>();
                lstNewsLetter = ObjNewsLetterRepository.getAllNewsLetters();
                foreach (Domain.Socioboard.Domain.NewsLetter item in lstNewsLetter)
                {
                    //string ret = string.Empty;
                    try
                    {
                        //SendMail(item);
                        SendNewsLetter(item.NewsLetterBody, item.Subject, item.UserId.ToString(), item.Id.ToString());
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error : " + ex.StackTrace);
                    }

                
                }
                return new JavaScriptSerializer().Serialize(lstNewsLetter);
            }
            catch (Exception ex)
            {
                
               Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }      
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendNewsLetter(string body, string Subject, string userid,string NewsLetterId)
        {
            
            UserRepository objUserRepository = new UserRepository();
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            string res = string.Empty;
            string ret = string.Empty;
            try
            {


                string from = ConfigurationManager.AppSettings["fromemail"];
                string tomail = ConfigurationManager.AppSettings["tomail"];
                string username = ConfigurationManager.AppSettings["Mandrillusername"];
                string host = ConfigurationManager.AppSettings["Mandrillhost"];
                string port = ConfigurationManager.AppSettings["Mandrillport"];
                string pass = ConfigurationManager.AppSettings["Mandrillpassword"];


                //string sss = ConfigurationSettings.AppSettings["host"];
                objUser = objUserRepository.getUsersById(Guid.Parse(userid));

                GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

                ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), from, "", objUser.EmailId, string.Empty, string.Empty, Subject, body, username, pass);
                //NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();

                if (ret.Contains("Success"))
                {
                    ObjNewsLetter.Id = Guid.Parse(NewsLetterId);
                    ObjNewsLetter.SendStatus = true;
                    ObjNewsLetterRepository.UpdateNewsLetter(ObjNewsLetter);
                    //lstbox.Items.Add("Mail send to : " + objUser.UserName);
                }
                //return ret;

                
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddNewsLatter(string ObjNewsLatter)
        {
            try
            {
                Domain.Socioboard.Domain.NewsLetter objnewsLatter = (Domain.Socioboard.Domain.NewsLetter)(new JavaScriptSerializer().Deserialize(ObjNewsLatter, typeof(Domain.Socioboard.Domain.NewsLetter)));
                ObjNewsLetterRepository.AddNewsLetter(objnewsLatter);
                return new JavaScriptSerializer().Serialize("Success");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error : " + ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went wrong!");
            }
        }
    }
}
