using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;

namespace SocioboardDataServices
{
    class LinkedInData : ISocialSiteData
    {
        public string GetData(object UserId,string LinkedinId)
        {
            string ret = string.Empty;
            try
            {
                Guid userId = (Guid)UserId;
                Api.LinkedinAccount.LinkedinAccount ApiObjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                List<Domain.Socioboard.Domain.LinkedInAccount> lstLinkedInAccount = (List<Domain.Socioboard.Domain.LinkedInAccount>)(new JavaScriptSerializer().Deserialize(ApiObjLinkedinAccount.GetAllLinkedinAccountsOfUser(userId.ToString()), typeof(List<Domain.Socioboard.Domain.LinkedInAccount>)));
                //List<LinkedInAccount> lstLinkedinAccount = new List<LinkedInAccount>();
                foreach (LinkedInAccount item in lstLinkedInAccount)
                {
                    try
                    {
                        Api.Linkedin.Linkedin ApiObjLinkedin = new Api.Linkedin.Linkedin();
                        ret = ApiObjLinkedin.getLinkedInData(item.UserId.ToString(), item.LinkedinUserId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ret;
        }

       

        public void GetSearchData(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
