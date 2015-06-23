using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.App_Start
{
    public class CustomExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        private ILog logger = LogManager.GetLogger(typeof(CustomExceptionAttribute));

        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewBag.OnException = "IExceptionFilter.OnException filter called";
            logger.Error(filterContext.Exception.StackTrace);
        }

        public void OnException(ExceptionContext filterContext, string aj)
        {
            filterContext.Controller.ViewBag.OnException = "IExceptionFilter.OnException filter called";
            logger.Error(filterContext.Exception.StackTrace);
        }
    }
}