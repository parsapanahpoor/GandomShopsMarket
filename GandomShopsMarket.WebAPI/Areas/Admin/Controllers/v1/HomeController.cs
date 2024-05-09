using GandomShopsMarket.Presentation.HttpManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Domain.IRepositories.Role;
using GandomShopsMarket.Presentation.Areas.Admin.ActionFilterAttributes;
using Swashbuckle.AspNetCore.Annotations;
namespace GandomShopsMarket.Presentation.Areas.Admin.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/Admin/[controller]")]

public class HomeController : AdminBaseController
{
    #region Admin Dashboard

	/// <summary>
	/// داشبورد پنل ادمین
	/// </summary>

    [HttpGet("AdminDashboard")]
	public async Task<IActionResult> AdminDashboard(CancellationToken cancellationToken)
	{
		return Ok(JsonResponseStatus.Success(null , "Wellcome to admin dashboard"));
	}

	#endregion
}
