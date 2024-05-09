using GandomShopsMarket.Domain.DTO.AdminSide.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.FilterRoles;

public class FilterRolesQuery : IRequest<FilterRolesDTO>
{
    #region properties

    public FilterRolesDTO FilterRolesDTO { get; set; }

    #endregion
}
