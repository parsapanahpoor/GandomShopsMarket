using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Application.Utilities.Extensions;
using GandomShopsMarket.Domain.IRepositories.Role;
using GandomShopsMarket.Presentation.HttpManager;

namespace GandomShopsMarket.Presentation.Areas.Admin.ActionFilterAttributes;
public class CheckUserHasAnyRole : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var service = (IRoleQueryRepository)context.HttpContext.RequestServices.GetService(typeof(IRoleQueryRepository))!;

        var hasUserAnyRole = service.IsUser_Admin(context.HttpContext.User.GetUserId(), default).Result;

        if (!hasUserAnyRole)
        {
            context.Result = JsonResponseStatus.NotPermission();
        }
    }
}
