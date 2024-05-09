using GandomShopsMarket.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Command.EditUser;
using GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query;
using GandomShopsMarket.Domain.DTO.AdminSide.User;
using GandomShopsMarket.Presentation.Areas.Admin.Controllers.v1;
using GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query.UserDetailQuery;
using Swashbuckle.AspNetCore.Annotations;

namespace TokenBased_Authentication.Presentation.Areas.Admin.Controllers.v1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/Admin/[controller]")]

public class UserController : AdminBaseController
{
    #region Filter Users 

    /// <summary>
    /// لیست کاربران
    /// </summary>

    [HttpGet("FilterUsers")]
    public async Task<IActionResult> FilterUsers([FromQuery] FilterUsersAdminSideQuery filter,
                                                 CancellationToken cancellationToken = default)
    {
        return JsonResponseStatus.Success(await Mediator.Send(filter));
    }

    #endregion

    #region Edit User

    /// <summary>
    /// ویرایش یک کاربر
    /// </summary>

    [HttpPut("EditUser")]
    public async Task<IActionResult> EditUser([FromForm]EditUserDTO userDTO,
                                              CancellationToken cancellation = default)
    {
        var res = await Mediator.Send(new EditUserAdminSideCommand()
        {
            Avatar = userDTO.UserAvatar,
            Id = userDTO.Id,
            IsActive = userDTO.IsActive,
            Mobile = userDTO.Mobile,
            Username = userDTO.Username,
            UserRoles = userDTO.UserRoles
        });

        switch (res)
        {
            case EditUserResult.DuplicateMobileNumber:
                return JsonResponseStatus.Error(null, "موبایل وارد شده تکراری می باشد.");

            case EditUserResult.Success:
                return JsonResponseStatus.Success(null, "ویرایش کابر موردنظر باموفقیت انجام شده است.");
        }

        return JsonResponseStatus.Error(null, "اطلاعات وارد شده صحیح نمی باشد.");
    }

    #endregion

    #region Show User Detail

    /// <summary>
    /// نمایش اطلاعات یک کاربر
    /// </summary>

    [HttpGet("UserDetail")]
    public async Task<IActionResult> UserDetail([FromQuery]UserDetailAdminSideQuery query,
                                                CancellationToken cancellationToken = default)
    {
        var res = await Mediator.Send(query);
        if (res == null) return JsonResponseStatus.Error(null, "کاربر موردنظر یافت نشده است.");

        return JsonResponseStatus.Success(res);
    }

    #endregion
}
