using GandomShopsMarket.Application.Common.IUnitOfWork;
using GandomShopsMarket.Application.Extensions;
using GandomShopsMarket.Application.Generators;
using GandomShopsMarket.Application.Security;
using GandomShopsMarket.Application.StaticTools;
using GandomShopsMarket.Application.Utilities.Security;
using GandomShopsMarket.Domain.DTO.AdminSide.User;
using GandomShopsMarket.Domain.Entities.Role;
using GandomShopsMarket.Domain.IRepositories.Role;
using GandomShopsMarket.Domain.IRepositories.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Command.EditUser;

public record EditUserAdminSideCommandHandler : IRequestHandler<EditUserAdminSideCommand, EditUserResult>
{
    #region Ctor

    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IRoleCommandRepository _roleCommandRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditUserAdminSideCommandHandler(IUserCommandRepository userCommandRepository,
                                  IUserQueryRepository userQueryRepository,
                                  IRoleCommandRepository roleCommandRepository,
                                  IRoleQueryRepository roleQueryRepository,
                                  IUnitOfWork unitOfWork)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _roleCommandRepository = roleCommandRepository;
        _roleQueryRepository = roleQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<EditUserResult> Handle(EditUserAdminSideCommand request, CancellationToken cancellationToken)
    {
        //Get User By Id 
        var userOldInfos = await _userQueryRepository.GetByIdAsync(cancellationToken, request.Id);
        if (userOldInfos == null) return EditUserResult.Error;

        //Checkind incomin mobile 
        if (await _userQueryRepository.IsMobileExist(request.Mobile, cancellationToken) && request.Mobile != userOldInfos.Mobile)
        {
            return EditUserResult.DuplicateMobileNumber;
        }

        if (userOldInfos != null)
        {
            userOldInfos.Username = request.Username;
            userOldInfos.IsActive = request.IsActive;
            userOldInfos.UpdateDate = DateTime.Now;

            #region User Avatar

            if (request.Avatar != null && request.Avatar.IsImage())
            {
                if (!string.IsNullOrEmpty(userOldInfos.Avatar))
                {
                    userOldInfos.Avatar.DeleteImage(FilePaths.UserAvatarPathServer, FilePaths.UserAvatarPathThumbServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(request.Avatar.FileName);
                request.Avatar.AddImageToServer(imageName, FilePaths.UserAvatarPathServer, 270, 270, FilePaths.UserAvatarPathThumbServer);
                userOldInfos.Avatar = imageName;
            }

            #endregion

            _userCommandRepository.Update(userOldInfos);

            #region Delete User Roles

            await _roleCommandRepository.RemoveUserRolesByUserId(request.Id, cancellationToken);

            #endregion

            #region Add User Roles

            if (request.UserRoles != null && request.UserRoles.Any())
            {
                foreach (var roleId in request.UserRoles)
                {
                    var userRole = new UserRole()
                    {
                        RoleId = roleId,
                        UserId = request.Id
                    };

                    await _roleCommandRepository.AddUserSelectedRole(userRole, cancellationToken);
                }
            }

            #endregion

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return EditUserResult.Success;
        }

        return EditUserResult.Error;
    }
}
