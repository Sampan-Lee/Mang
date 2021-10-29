using System.Threading.Tasks;

namespace Mang.Infrastructure.Sms
{
    public class FakeSms : ISmsService
    {
        public async Task<bool> SendAsync(string phone, string captcha)
        {
            return await Task.Run(() => true);
        }
    }
}