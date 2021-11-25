using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Mang.Web.Extension.Model;

namespace Mang.Web.Extension.Filter
{
    /// <summary>
    /// 参数校验Filter
    /// </summary>
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                JsonResultModel<string> messageModel = new JsonResultModel<string>();
                var result = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors.Select(x => x.ErrorMessage))
                    .ToList();
                messageModel.errorMsg = result.FirstOrDefault();
                messageModel.code = HttpStatusCode.ArgumentError;
                messageModel.status = false;
                context.Result = new ObjectResult(messageModel);
            }
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.Result = new ObjectResult((context.Result as ObjectResult).Value.ToSuccess());
            base.OnResultExecuting(context);
        }
    }
}