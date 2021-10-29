namespace Mang.Infrastructure.MiniProgram.Auths
{
    public class CodeToSessionModel
    {
        public string appid { get; set; }

        public string secret { get; set; }

        public string js_code { get; set; }

        public string grant_type { get; set; }
    }
}