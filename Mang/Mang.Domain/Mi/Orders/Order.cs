using Nest;

namespace Game.Domain.Mi.Orders
{
    [ElasticsearchType(IdProperty = "Id", Name = "xmddz_order-*")]
    public class Order
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Text(Name = "_id")]
        public string Id { get; set; }

        /// <summary>
        /// 账号ID
        /// </summary>
        [Number(NumberType.Long, Name = "data.accId")]
        public long AccId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [PropertyName("data.advGameId")]
        public string AdvGameId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Money { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GameVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppGameId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long LoginTime { get; set; }
    }
}