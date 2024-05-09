using MediatR;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Presentation.Filter;
using Swashbuckle.AspNetCore.Annotations;

namespace GandomShopsMarket.Presentation.Controllers.v1;

[ApiController]
[CatchExceptionFilter]

public class SiteBaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
