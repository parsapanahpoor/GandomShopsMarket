using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Presentation.Areas.Admin.ActionFilterAttributes;
using GandomShopsMarket.Presentation.Filter;
using Swashbuckle.AspNetCore.Annotations;

namespace GandomShopsMarket.Presentation.Areas.Admin.Controllers.v1;

[Area("Admin Panel")]
[ApiController]
[CatchExceptionFilter]
[Authorize]
[CheckUserHasAnyRole]

public class AdminBaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}