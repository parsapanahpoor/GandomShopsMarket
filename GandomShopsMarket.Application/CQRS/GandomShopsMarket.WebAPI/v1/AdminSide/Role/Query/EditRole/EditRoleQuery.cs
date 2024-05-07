using GandomShopsMarket.Domain.DTO.AdminSide.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.EditRole;

public record EditRoleQuery : IRequest<EditRoleDTO>
{
    public ulong RoleId { get; set; }
}
