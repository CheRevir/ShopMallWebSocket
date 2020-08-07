using System;

namespace Model
{
    public class DBChatBefore : BaseDB
    {
        private readonly DateTime Time;

        public DBChatBefore(DateTime Time)
        {
            this.Time = Time;
        }

        public override string GetTable()
        {
            int year = Time.Year;
            int month = Time.Month;
            string m;
            if (month == 1)
            {
                year -= 1;
                month = 12;
            }
            else
            {
                month -= 1;
            }
            if (month < 10)
            {
                m = "0" + month;
            }
            else
            {
                m = Convert.ToString(month);
            }
            return TABLE_HEADER + year + m;
        }
    }
}
