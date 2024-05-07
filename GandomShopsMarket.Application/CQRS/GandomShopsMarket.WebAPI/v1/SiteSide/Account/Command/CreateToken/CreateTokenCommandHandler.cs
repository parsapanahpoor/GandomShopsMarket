using System.Reflection;
using System.Security.Claims;
using System.Text;
using GandomShopsMarket.Application.Common.IUnitOfWork;
using GandomShopsMarket.Application.Utilities.Security;
using GandomShopsMarket.Domain.DTO.APIClient.Account;
using GandomShopsMarket.Domain.Entities.Account;
using GandomShopsMarket.Domain.IRepositories.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.Account.Command.CreateToken;

public record CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, LoginDataDto>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTokenCommandHandler(IUserQueryRepository userQueryRepository,
                                     IUserCommandRepository userCommandRepository,
                                     IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<LoginDataDto> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var userToken = new UserToken()
        {
            CreateDate = DateTime.Now,
            LastestSignInPlatformName = request.LastestSignInPlatformName,
            RefreshToken = request.RefreshToken.Getsha256Hash(),
            IsDelete = false,
            RefreshTokenExpireTime = request.RefreshTokenExpireTime,
            TokenExpireTime = request.TokenExpireTime,
            TokenHash = request.TokenHash.Getsha256Hash(),
            UserId = request.UserId
        };

        //Add To Data Base 
        await _userCommandRepository.Add_UserToken(userToken , cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginDataDto()
        {
            RefreshToken = request.RefreshToken,
            Token = request.TokenHash
        };
    }
}
