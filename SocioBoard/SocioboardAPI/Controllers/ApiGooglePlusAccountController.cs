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
    public class ApiGooglePlusAccountController : ApiController
    {
         ILog logger = LogManager.GetLogger(typeof(ApiGooglePlusAccountController));
         GooglePlusAccountRepository ObjGooglePlusAccountsRepo = new GooglePlusAccountRepository();


         [HttpGet]
         public IHttpActionResult GetGooglePlusAccount(string ProfileId)
         {
             try
             {
                 Domain.Socioboard.Domain.GooglePlusAccount objGpAccount = ObjGooglePlusAccountsRepo.getUserDetails(ProfileId);
                 return Ok(objGpAccount);
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
                 return BadRequest("Something Went Wrong");
             }
         }
    }
}
