using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.IO;

namespace letTalkNew.Event
{
    public partial class AjaxEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedUser"] == null)
                {
                    Response.Write("{\"type\":\"logout\"}");
                    return;
                }

                Newtonsoft.Json.Linq.JObject jo;
                StreamReader sr;

                string fbUserId = string.Empty;
                string accToken = string.Empty;

                //fbUserId = "100006693371290";//Request.Form["fbUserId"];

                //accToken = "CAACEdEose0cBAHXM4ZCBzbB90nnzFH1TZBCS0b5N4JJuDlbsq9OIgSQF3NtXvvmNUMp8SIbZA0jwhwtvVhILGZCZBQzJHt1O9dBwnYib9ZBRgA21Nk4poJ0Mu7SShe4dLlQ0UKB3ZARyGaU4fFyAloRAUI0CmaaZBsy3LZCa7lwfPhFu2ZBvEcrZB6tRDB7BZBRODDEZD";//GetFBAccessTokenByFBUserId(fbUserId);

                sr = new System.IO.StreamReader(Request.InputStream);
                string data = "";
                data = sr.ReadToEnd();
                jo = Newtonsoft.Json.Linq.JObject.Parse(data);

                fbUserId = Server.UrlDecode((string)jo["fbUserId"]);
                accToken = GetFBAccessTokenByFBUserId(fbUserId);

                if (Request.QueryString["op"] == "createEvent")
                {
                   

                    string name = Server.UrlDecode((string)jo["name"]);
                    string details = Server.UrlDecode((string)jo["details"]);
                    string where = Server.UrlDecode((string)jo["where"]);
                    string when = Server.UrlDecode((string)jo["when"]);


                    string eventId = CreateEvent(fbUserId, accToken, name, details, where, when);

                    Response.Write(eventId);
                }
                else if (Request.QueryString["op"] == "getEventDetails")
                {
                    string evntDetails = GetEventDetails(fbUserId, accToken);
                    jo=Newtonsoft.Json.Linq.JObject.Parse(evntDetails);

                    Response.Write(jo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }

        public string GetFBAccessTokenByFBUserId(string fbUserId)
        {
            string fbAccessToken = string.Empty;
            try
            {
                
                FacebookAccountRepository objFBAccountRepo = new FacebookAccountRepository(); 
               
                FacebookAccount fbAccount = objFBAccountRepo.getUserDetails(fbUserId);

                fbAccessToken = fbAccount.AccessToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return fbAccessToken;
        }

        public string CreateEvent(string userId,string accToken,string name,string details,string where,string when)
        {
            string createEvent = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accToken;

                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add("name", name);
                dicParam.Add("details", details);
                dicParam.Add("where", where);
                dicParam.Add("start_time", when);
                dicParam.Add("privacy", "OPEN");

                string path = userId + "/events";

                object eventId = fb.Post(path, dicParam);

                createEvent = eventId.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return createEvent;
        }

        public string GetEventDetails(string userId, string accToken)
        {
            string createEvent = string.Empty;
            try
            {
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = accToken;

                string path = userId + "/events";

                object eventId = fb.Get(path);

                createEvent = eventId.ToString();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return createEvent;
        }

        public string EditEvent(string userId, string accToken)
        {
            string editEvent = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return editEvent;
        }
    }
}