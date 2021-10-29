namespace Mang.Application.System
{
    public static class SystemCacheKeyPrefixDefinition
    {
        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string LoginCaptcha = "user_login_captcha_";

        /// <summary>
        /// 用户权限：后缀为UserId
        /// </summary>
        public const string UserPermission = "user_permission_";

        /// <summary>
        /// 用户菜单：后缀为UserId
        /// </summary>
        public const string UserMenu = "user_menu_";
    }
}