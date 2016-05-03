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
    public class ApiYoutubeAccountController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiYoutubeAccountController));
        YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();


        [HttpGet]
        public IHttpActionResult GetYoutubeAccount(string ProfileId)
        {
            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount = new Domain.Socioboard.Domain.YoutubeAccount();
            try
            {
                objYoutubeAccount.Ytuserid = ProfileId;

                objYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(ProfileId);
               
                return Ok(objYoutubeAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest("Something Went Wrong");
            }
        }
    }
}
