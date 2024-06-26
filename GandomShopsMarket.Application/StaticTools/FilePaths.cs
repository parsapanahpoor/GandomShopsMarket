﻿namespace GandomShopsMarket.Application.StaticTools;

public static class FilePaths
{
    #region Site

    public static string SiteFarsiName = "وب سایت فروشگاهی شرکت گندوم ";
    public static string SiteAddress = "https://localhost:7075";
    public static string merchant = "123456789";

    #endregion

    #region UserAvatar

    public static readonly string UserAvatarPath = "/content/images/user/main/";
    public static readonly string UserAvatarPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

    public static readonly string UserAvatarPathThumb = "/content/images/user/thumb/";
    public static readonly string UserAvatarPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

    public static readonly string DefaultUserAvatar = "/content/images/user/DefaultAvatar.png";

    #endregion
}
