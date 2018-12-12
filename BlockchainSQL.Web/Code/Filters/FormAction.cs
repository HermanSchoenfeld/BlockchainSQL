using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Code {
    public class FormActionAttribute : ActionFilterAttribute {
        public const string OmitFormTag = "OmitFormElement";
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewData[OmitFormTag] = true;
        }
    }
}