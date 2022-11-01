using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BihariJe_WebApp.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate | AttributeTargets.All)]
    public class NotVisitAgainAttribute :Attribute,IActionFilter
    {
        private bool _status;

        public NotVisitAgainAttribute(bool status)
        {
            _status = status;

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
               // context.HttpContext.Response.Redirect();

            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
        // Console.WriteLine("jdhhdhdhdjdhhd");



    }
}
