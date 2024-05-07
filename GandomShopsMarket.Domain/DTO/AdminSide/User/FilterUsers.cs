
using GandomShopsMarket.Domain.DTO.Common;

namespace GandomShopsMarket.Domain.DTO.AdminSide.User;

public class FilterUsersDTO : BasePaging<Entities.Account.User>
{
    #region properties

    public string? Username { get; set; }

    public string? Mobile { get; set; }

    #endregion
}
