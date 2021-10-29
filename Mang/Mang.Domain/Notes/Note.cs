using System;
using FreeSql.DataAnnotations;
using Game.Domain.Shared;

namespace Mang.Domain
{
    [Table(Name = "note")]
    public class Note : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(MapType = typeof(int))]
        public Gender Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}