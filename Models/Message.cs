using System;
using System.Text;
using Newtonsoft.Json;

namespace Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {
        /// <summary>
        /// 聊天ID
        /// </summary>
        [JsonProperty]
        public int Chat_UID { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty]
        public int UID { get; set; }

        [JsonProperty]
        public string Name { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [JsonProperty]
        public int User_Type { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty]
        public int News_Type { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty]
        public string News { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public int News_State { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }

        public int Service { get; set; }

        public Message() { }

        public Message(int uid, int user_type, int news_type, string news, int news_state, DateTime time, int service)
        {
            UID = uid;
            User_Type = user_type;
            News_Type = news_type;
            News = news;
            News_State = news_state;
            Time = time;
            Service = service;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Chat_UID: ").Append(Chat_UID)
                .Append("; UID: ").Append(UID)
                .Append("; User_Type: ").Append(User_Type)
                .Append("; News_Type: ").Append(News_Type)
                .Append("; News: ").Append(News)
                .Append("; News_State: ").Append(News_State)
                .Append("; Time: ").Append(Time)
                .Append("; Service: ").Append(Service);
            return sb.ToString();
        }
    }
}
