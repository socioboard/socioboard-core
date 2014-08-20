using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;

namespace SocioBoard.Helper
{
    public partial class AjaxMailSender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            try
            {
                if (user == null)
                {

                    Response.Write("logout");
                }


                if (Request.QueryString != null)
                {

                    if (Request.QueryString["op"] == "resendmail")
                    {
                        //SocioBoard.Helper.MailSender.SendEMail(user.UserName, user.Password, user.EmailId, user.AccountType.ToString(), user.Id.ToString());

                        string msg = SocioBoard.Helper.MailSender.ReSendEMail(user.UserName, user.Password, user.EmailId, user.AccountType.ToString(), user.Id.ToString()).Trim();

                        Response.Write(msg+"~"+user.EmailId);
                    }



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}