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
    public class ApiLinkedinAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiLinkedinAccountController));
        LinkedInAccountRepository objlinkedinaccrepo = new LinkedInAccountRepository();

        [HttpGet]
        public IHttpActionResult GetLinkedinAccountDetailsById(string ProfileId)
        {
            Domain.Socioboard.Domain.LinkedInAccount LinkedAccount = new Domain.Socioboard.Domain.LinkedInAccount();
            try
            {
                LinkedAccount = objlinkedinaccrepo.getLinkedinAccountDetailsById(ProfileId);
                return Ok(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest("Error");
            }
        }
    }
}
