using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Presentation.Areas.Admin.ActionFilterAttributes;
using GandomShopsMarket.Presentation.Filter;

namespace GandomShopsMarket.Presentation.Areas.Admin.Controllers.v1;

[ApiController]
[CatchExceptionFilter]
[Authorize]
[CheckUserHasAnyRole]

public class AdminBaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}