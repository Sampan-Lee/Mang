namespace Mang.Web.Extension.Model
{
    public static class HttpStatusCode
    {
        public const int None = 110,
            OK = 200,
            BadRequest = 400,
            UnAuthorized = 401,
            Forbidden = 403,
            UnComplete = 403,
            NotFound = 404,
            ServerError = 500,
            BusinessError = 1101,
            ArgumentError = 1102,
            ParamsError = 1103,
            LoginFail = 1120,
            UnknowError = 1199;
    }
}