using GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query;
using GandomShopsMarket.Domain.DTO.AdminSide.User;
using GandomShopsMarket.Domain.IRepositories.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query;

public record FilterUsersAdminSideQueryHandler : IRequestHandler<FilterUsersAdminSideQuery, FilterUsersDTO>
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;

    public FilterUsersAdminSideQueryHandler(IUserQueryRepository userQueryRepository)
    {
        _userQueryRepository = userQueryRepository;
    }

    #endregion

    public async Task<FilterUsersDTO> Handle(FilterUsersAdminSideQuery request, CancellationToken cancellationToken)
    {
        return await _userQueryRepository.FilterUsers(new FilterUsersDTO()
        {
            Username = request.Username,
            Mobile = request.Mobile
        }, cancellationToken);
    }
}
