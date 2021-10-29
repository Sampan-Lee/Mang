using Mang.Public.Dto;

namespace Mang.Application.Contract.Notes.Dtos
{
    /// <summary>
    /// 纸条信息
    /// </summary>
    public class NoteDto : Dto
    {
        /// <summary>
        /// 投放纸条的用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatNum { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 纸条内容
        /// </summary>
        public string Content { get; set; }
    }
}