using GandomShopsMarket.Domain.Entities.Account;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Query.CreateToken;

public record CreateTokenQuery : IRequest<User>
{
    #region properties

    public ulong UserId { get; set; }

    #endregion
}
