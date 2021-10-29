using System.Threading.Tasks;

namespace Mang.Infrastructure.Sms
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string phone, string captcha);
    }
}