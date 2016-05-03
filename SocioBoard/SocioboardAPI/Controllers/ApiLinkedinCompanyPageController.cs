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
    public class ApiLinkedinCompanyPageController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiTumblrAccountController));
        LinkedinCompanyPageRepository objLinkedCmpnyPgeRepo = new LinkedinCompanyPageRepository();

        [HttpGet]
        public IHttpActionResult GetLinkedinCompanyPageDetailsByUserIdAndPageId(string ProfileId)
        {
            Domain.Socioboard.Domain.LinkedinCompanyPage LinkedinCompanyPage = new Domain.Socioboard.Domain.LinkedinCompanyPage();
            try
            {
                LinkedinCompanyPage = objLinkedCmpnyPgeRepo.getCompanyPageInformation(ProfileId);
                return Ok(LinkedinCompanyPage);
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
