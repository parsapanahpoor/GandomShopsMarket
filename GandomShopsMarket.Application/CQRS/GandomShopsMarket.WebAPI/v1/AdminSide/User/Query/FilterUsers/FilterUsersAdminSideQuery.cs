﻿using GandomShopsMarket.Domain.DTO.AdminSide.User;

namespace GandomShopsMarket.Application.CQRS.APIClient.v1.AdminSide.User.Query;

public record FilterUsersAdminSideQuery : IRequest<FilterUsersDTO>
{
    #region properties

    public FilterUsersDTO Filter { get; set; }

    #endregion
}
