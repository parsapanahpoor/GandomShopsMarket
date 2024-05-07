using GandomShopsMarket.Domain.Entities.Account;
using GandomShopsMarket.Domain.IRepositories.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Query.CreateToken;

public record CreateTokenQueryHandler : IRequestHandler<CreateTokenQuery, User>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public CreateTokenQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<User> Handle(CreateTokenQuery request, CancellationToken cancellationToken)
    {
        return await _userQueryRepository.GetByIdAsync(cancellationToken , request.UserId);
    }
}
