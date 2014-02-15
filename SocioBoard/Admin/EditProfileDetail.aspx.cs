using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace SocialSuitePro.Admin
{
    public partial class EditProfileDetail : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(EditProfileDetail));
        SocialProfilesRepository objSocialRepo = new SocialProfilesRepository();
        TwitterAccountRepository objTwtRepo = new TwitterAccountRepository();
        FacebookAccountRepository objFbRepo = new FacebookAccountRepository();
        InstagramAccountRepository objInsRepo = new InstagramAccountRepository();
        LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
        GooglePlusAccountRepository objgpRepo = new GooglePlusAccountRepository();
        TwitterAccount TwtAcc;
        FacebookAccount FBAcc;
        InstagramAccount InsAcc;
        LinkedInAccount liAcc;
        GooglePlusAccount GpAcc;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["AdminProfile"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }

                    string id = Request.QueryString["id"].ToString();
                    string type = Request.QueryString["type"].ToString();
                    string userid = Request.QueryString["userid"].ToString();

                    if (type == "twt")
                    {
                        TwtAcc = objTwtRepo.getUserInformation(Guid.Parse(userid), id);
                        Session["updateData"] = TwtAcc;
                        txtName.Text = TwtAcc.TwitterScreenName;
                        //ddltatus.SelectedValue = TwtAcc.IsActive.ToString();
                    }
                    if (type == "fb")
                    {
                        FBAcc = objFbRepo.getFacebookAccountDetailsById(id, Guid.Parse(userid));
                        Session["updateData"] = FBAcc;
                        txtName.Text = FBAcc.FbUserName;
                        // ddltatus.SelectedValue = FBAcc.IsActive.ToString();
                    }
                    if (type == "li")
                    {
                        liAcc = objLiRepo.getLinkedinAccountDetailsById(id);
                        Session["updateData"] = liAcc;
                        txtName.Text = liAcc.LinkedinUserName;
                        // ddltatus.SelectedValue = liAcc.IsActive.ToString();
                    }
                    if (type == "ins")
                    {
                        InsAcc = objInsRepo.getInstagramAccountDetailsById(id, Guid.Parse(userid));
                        Session["updateData"] = InsAcc;
                        txtName.Text = InsAcc.InsUserName;
                        //   ddltatus.SelectedValue = InsAcc.IsActive.ToString();
                    }
                    if (type == "gp")
                    {
                        GpAcc = objgpRepo.getGooglePlusAccountDetailsById(id, Guid.Parse(userid));
                        Session["updateData"] = GpAcc;
                        txtName.Text = GpAcc.GpUserName;
                        //ddltatus.SelectedValue = GpAcc.IsActive.ToString();
                    }
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = false;
                string type = Request.QueryString["type"].ToString();
                if (Session["updateData"] != null)
                {
                    if (type == "twt")
                    {

                        TwitterAccount twtAcc = (TwitterAccount)Session["updateData"];
                        //if (ddltatus.SelectedValue == "1")

                        status = true;
                        twtAcc.TwitterScreenName = txtName.Text;
                        twtAcc.IsActive = status;
                        objTwtRepo.updateTwitterUser(twtAcc);


                    }
                    if (type == "fb")
                    {
                        FacebookAccount fbAcc = (FacebookAccount)Session["updateData"];
                        // if (ddltatus.SelectedValue == "1")
                        FBAcc.FbUserName = txtName.Text;
                        status = true;
                        fbAcc.IsActive = status;
                        objFbRepo.updateFacebookUser(fbAcc);
                    }
                    if (type == "ins")
                    {
                        InstagramAccount insAcc = (InstagramAccount)Session["updateData"];
                        //if (ddltatus.SelectedValue == "1")
                        InsAcc.InsUserName = txtName.Text;
                        status = true;
                        insAcc.IsActive = status;
                        objInsRepo.updateInstagramUser(insAcc);
                    }
                    if (type == "li")
                    {
                        LinkedInAccount liAcc = (LinkedInAccount)Session["updateData"];
                        // if (ddltatus.SelectedValue == "1")
                        liAcc.LinkedinUserName = txtName.Text;
                        status = true;
                        liAcc.IsActive = status;
                        objLiRepo.updateLinkedinUser(liAcc);
                    }
                    if (type == "gp")
                    {
                        GooglePlusAccount gpAcc = (GooglePlusAccount)Session["updateData"];
                        //  gpAcc.IsActive = int.Parse(ddltatus.SelectedValue);
                        GpAcc.GpUserName = txtName.Text;
                        gpAcc.IsActive = 1;
                        objgpRepo.updateGooglePlusUser(gpAcc);
                    }
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.StackTrace);
            }
        }
    }
}