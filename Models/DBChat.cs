using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DBChat
    {
        private DateTime Time;

        public DBChat(DateTime Time)
        {
            this.Time = Time;
        }

        public DBChatBefore GetBefore()
        {
            return new DBChatBefore(Time);
        }

        public DBChatNow GetNow()
        {
            return new DBChatNow(Time);
        }
    }
}
