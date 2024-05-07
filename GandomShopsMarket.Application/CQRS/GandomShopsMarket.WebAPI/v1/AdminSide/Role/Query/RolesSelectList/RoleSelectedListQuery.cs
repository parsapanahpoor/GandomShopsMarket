using GandomShopsMarket.Domain.DTO.Common;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.RolesSelectList;

public record RoleSelectedListQuery : IRequest<List<SelectListViewModel>>
{
}
