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
    public class ApiInstagramAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiInstagramAccountController));
        InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();


        [HttpGet]
        public IHttpActionResult GetInstagramAccount(string ProfileId)
        {
            Domain.Socioboard.Domain.InstagramAccount objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
            try
            {
                objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(ProfileId);
                return Ok(objInstagramAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Please Try Again");
            }
        }

    }
}
