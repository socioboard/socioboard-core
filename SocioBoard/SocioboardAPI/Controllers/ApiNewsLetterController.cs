using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Socioboard.Controllers
{
    public class ApiNewsLetterController : ApiController
    {
        NewsLetterRepository _NewsLetterRepository = new NewsLetterRepository();
        [HttpGet]
        public IHttpActionResult Get(string userId)
        {
            try
            {
                Domain.Socioboard.Domain.NewsLetterSetting _NewsLetterSetting = _NewsLetterRepository.getNewsLetterSettings(userId);
                return Ok(_NewsLetterSetting);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateSetting(Domain.Socioboard.Domain.NewsLetterSetting _NewsLetterSetting)
        {

            Domain.Socioboard.Domain.NewsLetterSetting objNewsLetterSetting = new Domain.Socioboard.Domain.NewsLetterSetting();
            objNewsLetterSetting = _NewsLetterSetting;
            objNewsLetterSetting.Id = Guid.NewGuid();
            bool ret = _NewsLetterRepository.UpdateNewsLatterSetting(objNewsLetterSetting);

            if (ret)
            {
                return Ok();
            }
            else {
                return BadRequest();
            }


        }

    }
}
