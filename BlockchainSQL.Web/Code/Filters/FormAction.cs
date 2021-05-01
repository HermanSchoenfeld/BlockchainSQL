using System.Web.Mvc;

namespace BlockchainSQL.Web.Code
{
    public class FormActionAttribute : ActionFilterAttribute {
        public const string OmitFormTag = "OmitFormElement";
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewData[OmitFormTag] = true;
        }
    }
}