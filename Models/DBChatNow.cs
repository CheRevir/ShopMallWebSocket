using System;

namespace Model
{
    public class DBChatNow : BaseDB
    {
        private readonly DateTime Time;

        public DBChatNow(DateTime Time)
        {
            this.Time = Time;
        }

        public override string GetTable()
        {
            return TABLE_HEADER + Time.ToString("yyyyMM");
        }
    }
}
