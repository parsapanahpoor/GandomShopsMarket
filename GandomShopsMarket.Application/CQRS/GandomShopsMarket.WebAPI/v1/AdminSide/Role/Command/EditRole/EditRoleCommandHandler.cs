﻿using GandomShopsMarket.Application.Common.IUnitOfWork;
using GandomShopsMarket.Domain.DTO.AdminSide.Role;
using GandomShopsMarket.Domain.Entities.Role;
using GandomShopsMarket.Domain.IRepositories.Role;

namespace GandomShopsMarket.Application.CQRS.GandomShopsMarket.WebAPI.v1.AdminSide.Role.Command.EditRole;

public record EditRoleCommandHandler : IRequestHandler<EditRoleCommand, EditRoleResult>
{
    #region Ctor

    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditRoleCommandHandler(IRoleCommandRepository roleCommandRepository,
                                  IRoleQueryRepository roleQueryRepository,
                                  IUnitOfWork unitOfWork)
    {
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<EditRoleResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        //Get Role By Id
        var role = await _roleQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (role == null) return EditRoleResult.RoleNotFound;

        role.Title = request.Title;
        role.RoleUniqueName = request.RoleUniqueName;

        _roleCommandRepository.Update(role);

        // remove all permissions
        var rolePermissions = await _roleQueryRepository.GetRolePermissionsIdByRoleId(role.Id, cancellationToken);
        if (rolePermissions != null)
        {
            await _roleCommandRepository.RemoveRolePermissions(role.Id, rolePermissions, cancellationToken);
        }

        // add permissions
        if (request.Permissions != null && request.Permissions.Any())
        {
            foreach (var permissionId in request.Permissions)
            {
                var rolePermission = new RolePermission
                {
                    PermissionId = permissionId,
                    RoleId = role.Id
                };

                await _roleCommandRepository.AddPermissionToRole(rolePermission);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return EditRoleResult.Success;
    }
}
