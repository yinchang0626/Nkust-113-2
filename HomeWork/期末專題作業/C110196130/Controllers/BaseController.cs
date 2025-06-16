using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var username = context.HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username) && context.ActionDescriptor.RouteValues["controller"] != "Account")
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
        base.OnActionExecuting(context);
    }
}