using System;

namespace Model
{
    public class User
    {
        public string Session_Id { get; set; }
        public int Uid { get; set; }
        public string Name { get; set; }
        public int User_Type { get; set; }
        private int select = -1;
        public int Select { get { return select; } set { select = value; } }
        public DateTime Time { get; set; }

        public User(string session_Id, int uid, string name, int user_Type, DateTime time)
        {
            Session_Id = session_Id;
            Uid = uid;
            Name = name;
            User_Type = user_Type;
            Time = time;
        }
    }
}
