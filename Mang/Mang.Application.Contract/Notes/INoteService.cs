using System.Threading.Tasks;
using Game.Domain.Shared;
using Mang.Application.Contract.Notes.Dtos;
using Mang.ServiceBase.Interface;

namespace Mang.Application.Contract.Notes
{
    /// <summary>
    /// 纸条服务接口
    /// </summary>
    public interface INoteService : IBaseService
    {
        /// <summary>
        /// 获取一张特定性别的纸条
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<NoteDto> GetAsync(Gender gender);

        /// <summary>
        /// 投放一张纸条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateNoteDto input);
    }
}