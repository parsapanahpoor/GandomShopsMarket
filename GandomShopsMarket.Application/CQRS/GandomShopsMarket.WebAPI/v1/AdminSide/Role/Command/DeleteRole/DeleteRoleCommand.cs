namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Command.DeleteRole;

public record DeleteRoleCommand : IRequest<bool>
{
    public ulong RoleId { get; set; }
}
