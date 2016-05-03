using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Socioboard.Controllers
{

    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiHomeController : ApiController
    {
        private DemorequestRepository demoReqRepo = new DemorequestRepository();

        [HttpPost]
        public IHttpActionResult AddDemoRequest(Domain.Socioboard.Domain.Demorequest demoRequest) 
        {
            demoRequest.Id = Guid.NewGuid();
            demoReqRepo.AddDemoRequest(demoRequest);
            return Ok("Demo Requested Added");
        }
    }
}
