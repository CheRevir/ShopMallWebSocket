using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Text;
using Util;

namespace Model
{
    public abstract class BaseDB
    {
        public readonly static DbSession Context = new DbSession("DosConn");
        public const string TABLE_HEADER = "Mall_Chat_";
        private const string sql_query = "select * from ";
        private const string sql_insert = "insert into ";
        private const string sql_insert_field = "(UID, User_Type, News_Type, News, News_State, Time, Service) values(";

        public abstract string GetTable();

        public List<Message> GetAll()
        {
            return Context.FromSql(sql_query + GetTable() + " order by Time ASC").ToList<Message>();
        }

        public List<Message> GetByUid(int uid)
        {
            return Context.FromSql(sql_query + GetTable() + " where UID = " + uid + " order by Time ASC").ToList<Message>();
        }

        public List<Message> GetByUidDesc(int uid)
        {
            return Context.FromSql(sql_query + GetTable() + " where UID = " + uid + " order by Time DESC").ToList<Message>();
        }

        public Message GetByChatUid(int chat_uid)
        {
            return Context.FromSql(sql_query + GetTable() + " where Chat_UID=" + chat_uid).ToFirst<Message>();
        }

        public List<Message> GetByServiceUId(int uid)
        {
            return Context.FromSql(sql_query + GetTable() + " where Service=" + uid + " order by Time ASC").ToList<Message>();
        }

        public List<Message> GetByServiceUIdDesc(int uid)
        {
            return Context.FromSql(sql_query + GetTable() + " where Service=" + uid + " order by Time DESC").ToList<Message>();
        }

        public void Add(Message message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sql_insert).Append(GetTable()).Append(sql_insert_field)
                .Append(message.UID).Append(",")
                .Append(message.User_Type).Append(",")
                .Append(message.News_Type).Append(", '")
                .Append(message.News).Append("' ,")
                .Append(message.News_State).Append(",'")
                .Append(message.Time).Append("' ,")
                .Append(message.Service).Append(" )");
            Context.FromSql(sb.ToString()).ExecuteNonQuery();
        }

        public void Update(int chat_uid, int news_state)
        {
            Context.FromSql("update " + GetTable() + " set News_state=" + news_state + " where Chat_UID=" + chat_uid).ExecuteNonQuery();
        }

        public void UpdateService(int chat_uid, int service_uid)
        {
            Context.FromSql("update " + GetTable() + " set Service=" + service_uid + " where Chat_UID=" + chat_uid).ExecuteNonQuery();
        }
    }
}
