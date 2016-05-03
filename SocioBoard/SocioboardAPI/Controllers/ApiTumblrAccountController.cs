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
    public class ApiTumblrAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiTumblrAccountController));
        TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();

        [HttpGet]
        public IHttpActionResult GetTumblrAccountDetailsById(string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.TumblrAccount objTumblrAccount = new Domain.Socioboard.Domain.TumblrAccount();
                objTumblrAccount.tblrUserName = ProfileId;
                objTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(ProfileId);
                return Ok(objTumblrAccount);
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
