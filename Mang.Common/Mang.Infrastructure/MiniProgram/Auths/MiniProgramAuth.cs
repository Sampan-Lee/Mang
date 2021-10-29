using System.Net.Http;
using System.Threading.Tasks;
using Mang.Public.Extension;
using Mang.Public.Util;

namespace Mang.Infrastructure.MiniProgram.Auths
{
    /// <summary>
    /// 小程序授权服务
    /// </summary>
    public class MiniProgramAuth : IMiniProgramAuth
    {
        private readonly HttpClient _httpClient;

        public MiniProgramAuth(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<SessionModel> CodeToSession(string code)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            CodeToSessionModel codeToSession = new CodeToSessionModel()
            {
                appid = Appsettings.app("MiniProgram", "AppId"),
                secret = Appsettings.app("MiniProgram", "AppSecret"),
                js_code = code,
                grant_type = "authorization_code"
            };

            SessionModel session;
            int count = 0;
            do
            {
                session = await _httpClient.GetAsync<SessionModel>(MiniProgramUrls.CodeToSession, codeToSession);
                count++;
            } while (session.errcode == "-1" && count == 3);

            if (session.errcode != "0")
            {
                throw new BusinessException(session.errmsg);
            }

            return session;
        }
    }
}