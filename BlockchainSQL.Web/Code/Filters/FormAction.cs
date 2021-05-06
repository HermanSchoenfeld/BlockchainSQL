using BlockchainSQL.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlockchainSQL.Web.Code
{
    public class FormActionAttribute : ActionFilterAttribute
    {
        public const string OmitFormTag = "OmitFormElement";
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
	        var controller = (BaseController)filterContext.Controller;
	        controller.ViewData[OmitFormTag] = true;
        }
    }
}