using GandomShopsMarket.Domain.Entities.Account;
namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Query.FindRefreshToken;

public record FindRefreshTokenQuery : IRequest<FindRefreshTokenQueryResult>
{
    #region properties

    public string RefreshToken { get; set; }

    #endregion
}
