namespace Mang.Infrastructure.MiniProgram
{
    /// <summary>
    /// 小程序接口地址
    /// </summary>
    public static class MiniProgramUrls
    {
        private const string _base = "https://api.weixin.qq.com";

        /// <summary>
        /// 登录凭证校验地址
        /// </summary>
        public const string CodeToSession = _base + "/sns/jscode2session";
    }
}