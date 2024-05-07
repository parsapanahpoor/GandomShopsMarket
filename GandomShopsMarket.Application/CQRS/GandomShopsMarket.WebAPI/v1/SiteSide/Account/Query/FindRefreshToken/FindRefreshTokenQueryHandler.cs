using GandomShopsMarket.Application.Utilities.Security;
using GandomShopsMarket.Domain.Entities.Account;
using GandomShopsMarket.Domain.IRepositories.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Query.FindRefreshToken;

public record FindRefreshTokenQueryHandler : IRequestHandler<FindRefreshTokenQuery, FindRefreshTokenQueryResult>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public FindRefreshTokenQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<FindRefreshTokenQueryResult?> Handle(FindRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        string RefreshTokenHash = request.RefreshToken.Getsha256Hash();

        //Get UserToken By Refresh Token
        var refreshToken = await _userQueryRepository.Get_UserToken_ByRefreshToken(request.RefreshToken);
        if (refreshToken == null) return null;

        return new FindRefreshTokenQueryResult()
        {
            TokenExpireTime  = refreshToken.RefreshTokenExpireTime,
            UserId = refreshToken.UserId,
            RefreshTokenId = refreshToken.Id
        };
    }
}
