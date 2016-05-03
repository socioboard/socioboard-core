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
        NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();
        Domain.Socioboard.Domain.NewsLetter objNewsLetter = new Domain.Socioboard.Domain.NewsLetter();
        
        
        
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllNewsLetters()
        {
            MailSender _MailSender=new MailSender();
            //List<NewsLetter> = ObjNewsLetterRepository.getAllNewsLetter();
            try
            {
                List<Domain.Socioboard.Domain.NewsLetter> lstNewsLetter = new List<Domain.Socioboard.Domain.NewsLetter>();
                lstNewsLetter = objNewsLetterRepository.getAllNewsLetters();
                if(lstNewsLetter.Count == 0)
                {
                    return "No record found";
                }
                foreach (Domain.Socioboard.Domain.NewsLetter item in lstNewsLetter)
                {
                    //string ret = string.Empty;
                    try
                    {
                        objNewsLetter=new Domain.Socioboard.Domain.NewsLetter();
                        string ret = _MailSender.SendAddNewsLatterMail(item.Email, item.NewsLetterBody, item.Subject);
                        if(ret=="Success")
                        {
                            objNewsLetter.Id = item.Id;
                            objNewsLetter.SendStatus = true;
                            objNewsLetterRepository.UpdateNewsLetter(objNewsLetter);
                        }
                        //SendMail(item);
                        //SendNewsLetter(item.NewsLetterBody, item.Subject, item.UserId.ToString(), item.Id.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }
                return "New Latter Sent successfully";
            }
            catch (Exception ex)
            {
                return "Please try Again";
            }      
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendNewsLetter(string body, string Subject, string userid,string NewsLetterId, string email, string name)
        {
            
            UserRepository objUserRepository = new UserRepository();
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objNewsLetter = new Domain.Socioboard.Domain.NewsLetter();
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
                    objNewsLetter.Id = Guid.Parse(NewsLetterId);
                    objNewsLetter.SendStatus = true;
                    objNewsLetterRepository.UpdateNewsLetter(objNewsLetter);
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
                objNewsLetterRepository.AddNewsLetter(objnewsLatter);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Something went wrong!";
            }
        }
    }
}
