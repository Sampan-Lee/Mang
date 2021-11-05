using Mang.Web.Extension.Model;

namespace Mang.Web.Extension.Authentication
{
    /// <summary>
    /// 中间件异常
    /// </summary>
    public class ApiResponse
    {
        public int code { get; } = 404;
        public string value { get; } = "No Found";
        public JsonResultModel<string> JsonResultModel { get; }

        public ApiResponse(int StatusCode, string msg = null)
        {
            code = StatusCode;

            switch (StatusCode)
            {
                case HttpStatusCode.UnAuthorized:
                {
                    value = "未登录!";
                }
                    break;
                case HttpStatusCode.Forbidden:
                {
                    value = "先填写个人信息!";
                }
                    break;
                case HttpStatusCode.ServerError:
                {
                    value = msg;
                }
                    break;
            }

            JsonResultModel = new JsonResultModel<string>()
            {
                status = false,
                code = code,
                errorMsg = value
            };
        }
    }
}