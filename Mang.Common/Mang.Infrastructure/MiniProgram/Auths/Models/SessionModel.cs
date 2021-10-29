namespace Mang.Infrastructure.MiniProgram.Auths
{
    public class SessionModel
    {
        public string openid { get; set; }

        public string session_key { get; set; }

        public string unionid { get; set; }

        public string errcode { get; set; }

        public string errmsg { get; set; }
    }
}