﻿using GandomShopsMarket.Domain.Entities.Role;
namespace GandomShopsMarket.Application.StaticTools;

public static class PermissionsList
{
    public static List<Permission> Permissions
    {
        get
        {
            List<Permission> list = new List<Permission>();

            var date = new DateTime(2021, 08, 12);

            #region Add Permissions

            #region Dashboard

            list.Add(new Permission
            {
                Id = 1,
                CreateDate = date,
                IsDelete = false,
                ParentId = null,
                PermissionUniqueName = "Dashboard",
                Title = "داشبورد"
            });

            #endregion

            #region Manage Access

            list.Add(new Permission
            {
                Id = 2,
                CreateDate = date,
                IsDelete = false,
                ParentId = null,
                PermissionUniqueName = "ManageAccess",
                Title = "مدیریت دسترسی ها"
            });

            list.Add(new Permission
            {
                Id = 3,
                CreateDate = date,
                IsDelete = false,
                ParentId = 2,
                PermissionUniqueName = "CreateRole",
                Title = "ایجاد نقش جدید"
            });

            list.Add(new Permission
            {
                Id = 4,
                CreateDate = date,
                IsDelete = false,
                ParentId = 2,
                PermissionUniqueName = "FilterRoles",
                Title = "لیست نقش ها"
            });

            list.Add(new Permission
            {
                Id = 5,
                CreateDate = date,
                IsDelete = false,
                ParentId = 2,
                PermissionUniqueName = "EditRole",
                Title = "ویرایش نقش"
            });

            list.Add(new Permission
            {
                Id = 6,
                CreateDate = date,
                IsDelete = false,
                ParentId = 2,
                PermissionUniqueName = "DeleteRole",
                Title = "حذف نقش"
            });

            #endregion

            #region Manage Account

            list.Add(new Permission
            {
                Id = 7,
                CreateDate = date,
                IsDelete = false,
                ParentId = null,
                PermissionUniqueName = "ManageAccount",
                Title = "مدیریت کاربران"
            });

            list.Add(new Permission
            {
                Id = 8,
                CreateDate = date,
                IsDelete = false,
                ParentId = 7,
                PermissionUniqueName = "UsersList",
                Title = "لیست کاربران"
            });

            list.Add(new Permission
            {
                Id = 9,
                CreateDate = date,
                IsDelete = false,
                ParentId = 7,
                PermissionUniqueName = "EditUserInfo",
                Title = "ویرایش کاربر"
            });

            list.Add(new Permission
            {
                Id = 10,
                CreateDate = date,
                IsDelete = false,
                ParentId = 7,
                PermissionUniqueName = "ChangePassword",
                Title = "تغییر رمز عبور کاربر"
            });

            list.Add(new Permission
            {
                Id = 11,
                CreateDate = date,
                IsDelete = false,
                ParentId = 7,
                PermissionUniqueName = "LoginWithUser",
                Title = "ورود با کاربر  "
            });

            #endregion

            #endregion

            // Last Id Use is : 11

            return list;
        }
    }
}
