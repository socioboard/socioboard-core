using Api.Socioboard.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiTwitterAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiGroupMembersController));
        private TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();
        
        [HttpGet]
        public IHttpActionResult GetTwitterAccountDetailsById(string ProfileId)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
            try
            {
                objTwitterAccount = objTwitterAccountRepository.getUserInformation(ProfileId);
                return Ok(objTwitterAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }
    }
}
