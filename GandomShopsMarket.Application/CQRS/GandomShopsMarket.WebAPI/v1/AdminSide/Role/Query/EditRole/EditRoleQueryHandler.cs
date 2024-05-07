using GandomShopsMarket.Domain.DTO.AdminSide.Role;
using GandomShopsMarket.Domain.IRepositories.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Query.EditRole;

public record EditRoleQueryHandler : IRequestHandler<EditRoleQuery, EditRoleDTO>
{
    #region Ctor

    private readonly IRoleQueryRepository _roleQueryRepository;

    public EditRoleQueryHandler(IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }

    #endregion

    public async Task<EditRoleDTO?> Handle(EditRoleQuery request, CancellationToken cancellationToken)
    {
        //get Role By Role 
        var role = await _roleQueryRepository.GetByIdAsync(cancellationToken, request.RoleId);
        if (role == null) return null;

        return new EditRoleDTO()
        {
            Id = role.Id,
            RoleUniqueName = role.RoleUniqueName,
            Title = role.Title,
            Permissions = await _roleQueryRepository.GetRolePermissionsIdByRoleId(role.Id, cancellationToken)
        };
    }
}
