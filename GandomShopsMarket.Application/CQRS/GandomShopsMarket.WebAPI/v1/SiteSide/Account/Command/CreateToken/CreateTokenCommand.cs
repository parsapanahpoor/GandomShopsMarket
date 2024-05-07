using GandomShopsMarket.Domain.DTO.APIClient.Account;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Command.CreateToken;

public record CreateTokenCommand : IRequest<LoginDataDto>
{
    public ulong UserId { get; set; }

    public string TokenHash { get; set; }

    public string RefreshToken { get; set; }

    public string LastestSignInPlatformName { get; set; }

    public DateTime TokenExpireTime { get; set; }

    public DateTime RefreshTokenExpireTime { get; set; }
}
