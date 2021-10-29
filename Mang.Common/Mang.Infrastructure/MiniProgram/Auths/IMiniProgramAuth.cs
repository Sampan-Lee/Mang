using System.Threading.Tasks;

namespace Mang.Infrastructure.MiniProgram.Auths
{
    public interface IMiniProgramAuth
    {
        Task<SessionModel> CodeToSession(string code);
    }
}