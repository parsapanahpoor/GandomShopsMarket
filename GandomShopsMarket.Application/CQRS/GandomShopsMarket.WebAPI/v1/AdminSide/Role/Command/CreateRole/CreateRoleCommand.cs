namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Command.CreateRole;

public class CreateRoleCommand : IRequest<bool>
{
    #region properties

    public string Title { get; set; }

    public string RoleUniqueName { get; set; }

    public List<ulong>? Permissions { get; set; }

    #endregion
}
