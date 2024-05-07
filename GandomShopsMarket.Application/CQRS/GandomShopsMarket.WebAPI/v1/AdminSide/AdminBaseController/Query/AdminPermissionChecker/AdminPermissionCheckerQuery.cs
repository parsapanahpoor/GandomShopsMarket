namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.AdminBaseController.Query.AdminPermissionChecker;

public record AdminPermissionCheckerQuery : IRequest<bool>
{
    #region properties

    public ulong UserId { get; set; }

    public string? PermissionName { get; set; }

    #endregion
}
