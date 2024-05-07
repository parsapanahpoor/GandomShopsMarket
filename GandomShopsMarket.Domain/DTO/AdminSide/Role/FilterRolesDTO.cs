using GandomShopsMarket.Domain.DTO.Common;

namespace GandomShopsMarket.Domain.DTO.AdminSide.Role;

public class FilterRolesDTO : BasePaging<Entities.Role.Role>
{
    #region properties

    public string? RoleTitle { get; set; }

    #endregion
}
