using Api.Socioboard.Services;
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
    public class ApiGoogleAnalyticsAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiGooglePlusAccountController));
        private GoogleAnalyticsAccountRepository objGoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();

        [HttpGet]
        public IHttpActionResult GetGooglePlusAccountDetailsById(string ProfileId,string UserId)
        {
            Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount = new Domain.Socioboard.Domain.GoogleAnalyticsAccount();
            try
            {
                _GoogleAnalyticsAccount = objGoogleAnalyticsAccountRepository.getGoogleAnalyticsAccountDetailsById(ProfileId, Guid.Parse(UserId));
                return Ok(_GoogleAnalyticsAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Worng Input");
            }
        }
    }
}
