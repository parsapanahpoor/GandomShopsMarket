using GandomShopsMarket.Domain.DTO.AdminSide.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query.UserDetailQuery;

public record UserDetailAdminSideQuery : IRequest<UserDetailAdminSideDTO>
{
    #region proeprties

    public ulong UserId { get; set; }

    #endregion
}
