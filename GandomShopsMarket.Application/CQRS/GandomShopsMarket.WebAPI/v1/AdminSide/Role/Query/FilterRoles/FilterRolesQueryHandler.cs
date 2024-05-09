using GandomShopsMarket.Domain.DTO.AdminSide.Role;
using GandomShopsMarket.Domain.IRepositories.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.FilterRoles;

public record FilterRolesQueryHandler : IRequestHandler<FilterRolesQuery, FilterRolesDTO>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public FilterRolesQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<FilterRolesDTO> Handle(FilterRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleQueryRepository.FilterRoles(request.FilterRolesDTO,cancellationToken);
    }
}
