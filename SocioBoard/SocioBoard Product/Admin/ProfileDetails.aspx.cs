using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using log4net;
namespace SocialSuitePro.Admin
{
    public partial class ProfileDetails : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ProfileDetails));
        SocialProfilesRepository objSocialRepo = new SocialProfilesRepository();
        TwitterAccountRepository objTwtRepo = new TwitterAccountRepository();
        FacebookAccountRepository objFbRepo = new FacebookAccountRepository();
        InstagramAccountRepository objInsRepo = new InstagramAccountRepository();
        LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
        GooglePlusAccountRepository objgpRepo = new GooglePlusAccountRepository();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AdminProfile"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            try
            {
                string strUser = string.Empty;
                ArrayList lstTwtAcc = objTwtRepo.getAllTwitterAccounts();
                foreach (TwitterAccount item in lstTwtAcc)
                {
                    //strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.TwitterUserId + "&type=twt&userid=" + item.UserId + "\">Edit</a></td><td>" + item.TwitterScreenName + "</td><td>Twitter</td><td>" + item.FollowersCount + "</td><td class=\"center\">" + item.IsActive + "</td></tr>";
                    strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.TwitterUserId + "&type=twt&userid=" + item.UserId + "\">Edit</a></td><td>" + item.TwitterScreenName + "</td><td>Twitter</td><td>" + item.FollowersCount + "</td></tr>";
                }
                ArrayList lstFBAcc = objFbRepo.getAllFacebookAccounts();
                foreach (FacebookAccount item in lstFBAcc)
                {
                    //strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.FbUserId + "&type=fb&userid=" + item.UserId + "\">Edit</a></td><td>" + item.FbUserName + "</td><td>Facebook</td><td>" + item.Friends + "</td><td class=\"center\">" + item.IsActive + "</td></tr>";
                    strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.FbUserId + "&type=fb&userid=" + item.UserId + "\">Edit</a></td><td>" + item.FbUserName + "</td><td>Facebook</td><td>" + item.Friends + "</td></tr>";
                }
                ArrayList lstliAcc = objLiRepo.getAllLinkedinAccounts();
                foreach (LinkedInAccount item in lstliAcc)
                {
                    //  strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.LinkedinUserId + "&type=li&userid=" + item.UserId + "\">Edit</a></td><td>" + item.LinkedinUserName + "</td><td>LinkedIn</td><td>" + item.Connections + "</td><td class=\"center\">" + item.IsActive + "</td></tr>";
                    strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.LinkedinUserId + "&type=li&userid=" + item.UserId + "\">Edit</a></td><td>" + item.LinkedinUserName + "</td><td>LinkedIn</td><td>" + item.Connections + "</td></tr>";
                }
                ArrayList lstInsAcc = objInsRepo.getAllInstagramAccounts();
                foreach (InstagramAccount item in lstInsAcc)
                {
                    //strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.InstagramId + "&type=ins&userid=" + item.UserId + "\">Edit</a></td><td>" + item.InsUserName + "</td><td>Instagram</td><td>" + item.Followers + "</td><td class=\"center\">" + item.IsActive + "</td></tr>";
                    strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.InstagramId + "&type=ins&userid=" + item.UserId + "\">Edit</a></td><td>" + item.InsUserName + "</td><td>Instagram</td><td>" + item.Followers + "</td></tr>";
                }
                ArrayList lstGpAcc = objgpRepo.getAllGooglePlusAccounts();
                foreach (GooglePlusAccount item in lstGpAcc)
                {
                    if (item.GpUserName != null)
                    {
                        //strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.GpUserId + "&type=gp&userid=" + item.UserId + "\">Edit</a></td><td>" + item.GpUserName + "</td><td>Google Plus</td><td>" + item.PeopleCount + "</td><td class=\"center\">" + item.IsActive + "</td></tr>";
                        strUser += "<tr class=\"gradeX\"><td><a href=\"EditProfileDetail.aspx?id=" + item.GpUserId + "&type=gp&userid=" + item.UserId + "\">Edit</a></td><td>" + item.GpUserName + "</td><td>Google Plus</td><td>" + item.PeopleCount + "</td></tr>";
                    }
                }
                Users.InnerHtml = strUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}