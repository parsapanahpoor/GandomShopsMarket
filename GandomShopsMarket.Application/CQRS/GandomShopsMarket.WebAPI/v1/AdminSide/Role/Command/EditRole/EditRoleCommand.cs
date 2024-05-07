using GandomShopsMarket.Domain.DTO.AdminSide.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Command.EditRole;

public class EditRoleCommand : IRequest<EditRoleResult>
{
    #region properties

    public string Title { get; set; }

    public string RoleUniqueName { get; set; }

    public List<ulong>? Permissions { get; set; }

    public ulong Id { get; set; }

    #endregion
}
