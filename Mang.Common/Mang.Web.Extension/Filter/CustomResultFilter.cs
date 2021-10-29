using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Mang.Web.Extension.Model;

namespace Mang.Web.Extension.Filter
{
    /// <summary>
    /// 参数校验Filter
    /// </summary>
    public class ArgumentValidateFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            JsonResultModel<string> messageModel = new JsonResultModel<string>();
            if (!context.ModelState.IsValid)
            {
                //new ValidationError(key, x.ErrorMessage)
                var result = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors.Select(x => x.ErrorMessage))
                    .ToList();
                messageModel.errorMsg = result.FirstOrDefault();
                messageModel.code = HttpStatusCode.ArgumentError;
                messageModel.status = false;
                //messageModel.data = result.FirstOrDefault(); //string.Join("|", result);//目前统一转化成字符串显示
                context.Result = new ObjectResult(messageModel);
            }

            base.OnResultExecuting(context);
        }
    }
}