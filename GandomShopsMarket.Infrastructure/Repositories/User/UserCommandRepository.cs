using GandomShopsMarket.Domain.Entities.Account;
using GandomShopsMarket.Domain.IRepositories.User;
using GandomShopsMarket.Infrastructure.Repositories;
namespace GandomShopsMarket.Infrastructure.Repositories.User;

public class UserCommandRepository : CommandGenericRepository<GandomShopsMarket.Domain.Entities.Account.User>, IUserCommandRepository
{
    #region Ctor

    private readonly GandomShopsMarketDbContext _context;

    public UserCommandRepository(GandomShopsMarketDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public void Update_SMSCode(SmsCode smsCode)
    {
        _context.SmsCodes.Update(smsCode);
    }

    public async Task Add_UserToken(UserToken userToken, CancellationToken cancellationToken)
    {
        await _context.UserTokens.AddAsync(userToken);
    }

    public async Task Add_SMSCode(SmsCode smsCode, CancellationToken cancellationToken)
    {
        await _context.SmsCodes.AddAsync(smsCode);
    }

    public void Delete_UserToken(UserToken userToken)
    {
        _context.UserTokens.Remove(userToken);
    }

    public void DeleteRange_UserTokens(List<UserToken> userTokens)
    {
        _context.UserTokens.RemoveRange(userTokens);
    }
}
