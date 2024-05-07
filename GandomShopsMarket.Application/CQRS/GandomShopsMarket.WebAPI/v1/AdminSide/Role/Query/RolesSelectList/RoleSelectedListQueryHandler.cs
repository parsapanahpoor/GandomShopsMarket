using GandomShopsMarket.Domain.DTO.Common;
using GandomShopsMarket.Domain.IRepositories.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.RolesSelectList;


internal class RoleSelectedListQueryHandler : IRequestHandler<RoleSelectedListQuery, List<SelectListViewModel>>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public RoleSelectedListQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<List<SelectListViewModel>> Handle(RoleSelectedListQuery request, CancellationToken cancellationToken)
    {
        return await _roleQueryRepository.GetSelectRolesList(cancellationToken);
    }
}
