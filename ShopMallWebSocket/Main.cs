using Model;
using Newtonsoft.Json;
using SuperSocket.SocketBase.Config;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace ShopMallWebSocket
{
    public partial class Main : Form
    {
        private readonly WebSocketServer socket;

        public Main()
        {
            InitializeComponent();

            socket = new WebSocketServer();
            int socket_port = IniFile.GetInt("WebSocketServer", "port", 1024);
            ServerConfig config = new ServerConfig
            {
                Port = socket_port,
            };
            if (!socket.Setup(config))
            {
                MessageBox.Show("WebSocket 设置WebSocket服务侦听地址失败");
                return;
            }
            if (!socket.Start())
            {
                MessageBox.Show("WebSocket 启动WebSocket服务侦听失败");
                return;
            }
            socket.NewSessionConnected += Socket_NewSessionConnected;
            socket.NewMessageReceived += Socket_NewMessageReceived;
            socket.SessionClosed += Socket_SessionClosed;

            Thread thread = new Thread(new ThreadStart(CheckSend))
            {
                Name = "CheckSend"
            };
            thread.Start();
            Thread thread2 = new Thread(new ThreadStart(CleanSession))
            {
                Name = "CleanSession"
            };
            thread2.Start();
            OnlineCount();
        }

        private void Socket_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine(session.Host);
        }

        private void Socket_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine(value);
            if (value == null || value == "") return;
            if (("Ping".Equals(value) && Overall.ContainsKey(session.SessionID)))
            {
                Overall.Get(session.SessionID).Time = DateTime.Now;
                session.Send("Ping");
                return;
            }
            int length = value.IndexOf("{") - 1;
            string json = "";
            if (length <= 0)
            {
                length = value.Length;
            }
            else if (length + 1 < value.Length)
            {
                json = value.Substring(length + 1);
            }
            else
            {
                length = value.Length;
            }
            string header = value.Substring(0, length);
            string[] headers = header.Split(':');
            switch (headers[0])
            {
                //Admin:id:password
                case "Admin":
                    if (headers.Count() > 2)
                    {
                        Admin(session, headers);
                    }
                    break;
                //Client:UBI:UPN:PSSID:Time:Chat_UID
                case "Client":
                    if (headers.Count() > 5)
                    {
                        Client(session, headers);
                    }
                    break;
                //Message:Admin|Client:uid:json
                case "Message":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 1)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        Message(session, headers, json);
                    }
                    break;
                //Callback:Admin|Client:uid:chat_uid
                case "Callback":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 3)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        Callback(headers);
                    }
                    break;
                //Query:uid:chat_uid:count
                case "Query":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 3)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        Query(session, headers);
                    }
                    break;
                //Occupation:Before|Now:uid
                case "Occupation":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 2)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        Occupation(session, headers);
                    }
                    break;
                //Remove:uid
                case "Remove":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 1)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        Remove(session, headers);
                    }
                    break;
                //QueryUser:数组
                case "QueryUser":
                    if (Overall.ContainsKey(session.SessionID) && headers.Count() > 1)
                    {
                        Overall.Get(session.SessionID).Time = DateTime.Now;
                        QueryUser(session, headers);
                    }
                    break;
            }
        }

        private void Socket_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            if (Overall.ContainsKey(session.SessionID))
            {
                if (Overall.Get(session.SessionID).User_Type == 0)
                {
                    int select = Overall.Get(session.SessionID).Select;
                    if (select != -1)
                    {
                        IEnumerable<KeyValuePair<string, User>> keyValuePairs = Overall.WhereUserType(0);
                        if (keyValuePairs != null)
                        {
                            foreach (KeyValuePair<string, User> keyValue in keyValuePairs)
                            {
                                if (session.SessionID != keyValue.Key)
                                {
                                    Send(keyValue.Key, "Occupation:Remove:" + select);
                                }
                            }
                        }
                    }
                }
                Overall.Remove(session.SessionID);
            }
            this.label1.Invoke(new Count(OnlineCount));
        }

        private delegate void Count();

        private void OnlineCount()
        {
            int count = Overall.KeyValues.Count;
            int admin_count = 0;
            IEnumerable<KeyValuePair<string, User>> keyValue = Overall.WhereUserType(0);
            if (keyValue != null)
            {
                admin_count = keyValue.Count();
            }
            this.label1.Text = "客服：" + admin_count + "   " + "用户：" + (count - admin_count);
        }

        private void CheckSend()
        {
            while (true)
            {
                if (Overall.KeyValues.Count > 0)
                {
                    DBChat db = new DBChat(DateTime.Now);
                    List<Model.Message> list = db.GetBefore().GetAll().Where(e => e.News_State == 0).ToList();
                    list.AddRange(db.GetNow().GetAll().Where(e => e.News_State == 0).ToList());
                    foreach (Model.Message message in list)
                    {
                        if (Utils.ConvertDateTimeToLong(DateTime.Now) - Utils.ConvertDateTimeToLong(message.Time) > 10000)
                        {
                            foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                            {
                                if (keyValue.Value.User_Type == 0 && message.User_Type == 1)
                                {
                                    Mall_User mall_User = BaseDB.Context.From<Mall_User>().Where(e => e.UID == message.UID).First();
                                    if (mall_User != null)
                                    {
                                        message.Name = mall_User.User_Name;
                                        Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                                    }
                                }
                                else if (keyValue.Value.User_Type == 1 && message.UID == keyValue.Value.Uid && message.User_Type == 0)
                                {
                                    Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }

        private void CleanSession()
        {
            while (true)
            {
                if (Overall.KeyValues.Count > 0)
                {
                    foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                    {
                        if (Utils.ConvertDateTimeToLong(DateTime.Now) - Utils.ConvertDateTimeToLong(keyValue.Value.Time) > 1000 * 60 * 60 * 2)
                        {
                            WebSocketSession socketSession = socket.GetAppSessionByID(keyValue.Key);
                            if (socketSession != null)
                            {
                                socketSession.Close();
                            }
                            else
                            {
                                Overall.Remove(keyValue.Key);
                            }
                        }
                    }
                }
                Thread.Sleep(1000 * 60 * 10);
            }
        }

        private void Send(string id, string message)
        {
            WebSocketSession socketSession = socket.GetAppSessionByID(id);
            if (socketSession != null)
            {
                socketSession.Send(message);
            }
            else if (Overall.ContainsKey(id))
            {
                Overall.Remove(id);
            }
        }

        private void Admin(WebSocketSession session, string[] values)
        {
            string id = values[1];
            Mall_Admin admin = BaseDB.Context.From<Mall_Admin>().Where(e => e.Admin == id).ToFirst();
            if (admin != null)
            {
                string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(values[2], "MD5").ToLower();
                if (password == admin.Passwrod)
                {
                    session.Send("LoginSuccess");
                    IEnumerable<KeyValuePair<string, User>> keyValues = Overall.WhereUidAndUserType(admin.Admin_UID, 0);
                    if (keyValues != null && keyValues.Count() > 0 && keyValues.First().Key != null)
                    {
                        WebSocketSession socketSession = socket.GetAppSessionByID(keyValues.First().Key);
                        if (socketSession != null)
                        {
                            socketSession.Send("Logout");
                            socketSession.Close();
                        }
                        Overall.Remove(keyValues.First().Key);
                    }
                    Overall.Add(session.SessionID, new User(session.SessionID, admin.Admin_UID, admin.Admin_Name, 0, DateTime.Now));
                    this.label1.Invoke(new Count(OnlineCount));

                    DBChat db = new DBChat(DateTime.Now);
                    List<Model.Message> list = db.GetBefore().GetAll().Where(e => e.User_Type == 1 && e.News_State != 2).ToList();
                    list.AddRange(db.GetNow().GetAll().Where(e => e.User_Type == 1 && e.News_State != 2).ToList());
                    foreach (Model.Message message in list)
                    {
                        Mall_User mall_User = BaseDB.Context.From<Mall_User>().Where(e => e.UID == message.UID).First();
                        if (mall_User != null)
                        {
                            message.Name = mall_User.User_Name;
                            session.Send("Message:" + JsonConvert.SerializeObject(message));
                        }
                    }

                    IEnumerable<KeyValuePair<string, User>> keyValuePairs = Overall.WhereUserType(0);
                    if (keyValuePairs != null)
                    {
                        foreach (KeyValuePair<string, User> keyValue in keyValuePairs)
                        {
                            if (session.SessionID != keyValue.Key && keyValue.Value.Select != -1)
                            {
                                session.Send("Occupation:Select:" + keyValue.Value.Select);
                            }
                        }
                    }
                }
                else
                {
                    session.Send("LoginError");
                }
            }
            else
            {
                session.Send("LoginError");
            }
        }

        private void Client(WebSocketSession session, string[] values)
        {
            int uid;
            int chat_uid;
            try
            {
                uid = Convert.ToInt32(DES.DESDecrypt(values[1]));
                chat_uid = Convert.ToInt32(values[5]);
            }
            catch (Exception)
            {
                return;
            }
            Mall_User user = BaseDB.Context.From<Mall_User>().Where(d => d.UID == uid).ToFirst();
            if (user != null)
            {
                string id = DES.DESDecrypt(values[2]);
                string password = DES.DESDecrypt(values[3]);
                if (password == user.Password)
                {
                    session.Send("LoginSuccess:" + uid);
                    Overall.Add(session.SessionID, new User(session.SessionID, uid, user.User_Name, 1, DateTime.Now));
                    this.label1.Invoke(new Count(OnlineCount));

                    DBChat db = new DBChat(DateTime.Now);
                    List<Model.Message> list = db.GetBefore().GetByUid(uid);
                    list.AddRange(db.GetNow().GetByUid(uid));
                    if (chat_uid == -1)
                    {
                        int count = 5;
                        if (list.Count < count)
                        {
                            count = list.Count;
                        }
                        for (int n = count; n > 0; n--)
                        {
                            session.Send("Message:" + JsonConvert.SerializeObject(list[list.Count - n]));
                        }
                    }
                    else if (chat_uid == -2)
                    {
                        list = list.Where(e => e.User_Type == 0 && e.News_State == 0).ToList();
                        foreach (Model.Message message in list)
                        {
                            session.Send("Message:" + JsonConvert.SerializeObject(message));
                        }
                    }
                    else if (chat_uid > 0)
                    {
                        string[] time = values[4].Split('-');
                        if (time.Length == 2)
                        {
                            int y = 0;
                            int m = 0;
                            string[] date = DateTime.Now.ToString("yyyy-MM").Split('-');
                            int year, month;
                            try
                            {
                                y = Convert.ToInt32(time[0]);
                                m = Convert.ToInt32(time[1]);
                                year = Convert.ToInt32(date[0]);
                                month = Convert.ToInt32(date[1]);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                            if (y == year && m == month)
                            {
                                list = db.GetNow().GetByUid(uid).Where(e => e.Chat_UID > chat_uid).ToList();
                                foreach (Model.Message message in list)
                                {
                                    session.Send("Message:" + JsonConvert.SerializeObject(message));
                                }
                            }
                            else if (y == year && month - m == 1 || year - y == 1 && m == 12 && month == 1)
                            {
                                list = db.GetBefore().GetByUid(uid).Where(e => e.Chat_UID > chat_uid).ToList();
                                foreach (Model.Message message in list)
                                {
                                    session.Send("Message:" + JsonConvert.SerializeObject(message));
                                }
                            }
                        }
                    }
                }
                else
                {
                    session.Send("LoginError");
                }
            }
            else
            {
                session.Send("LoginError");
            }
        }

        private void Message(WebSocketSession session, string[] values, string json)
        {
            if (json == "") return;
            //Message:Admin:uid:json
            if ("Admin" == values[1] && values.Count() > 2)
            {
                int user_uid;
                try
                {
                    user_uid = Convert.ToInt32(values[2]);
                }
                catch (Exception)
                {
                    return;
                }
                if (user_uid < 1) return;
                int admin_uid = Overall.Get(session.SessionID).Uid;

                Model.Message message = JsonConvert.DeserializeObject<Model.Message>(json);
                message.UID = user_uid;
                message.Name = Overall.Get(session.SessionID).Name;
                message.User_Type = 0;
                message.News_State = 0;
                message.Service = admin_uid;
                message.Time = DateTime.Now;

                //将客户发送消息状态该为已回复
                DBChat db = new DBChat(DateTime.Now);
                List<Model.Message> list_now = db.GetNow().GetByUid(user_uid).Where(e => e.User_Type == 1 && e.News_State != 2).ToList();
                foreach (Model.Message m in list_now)
                {
                    db.GetNow().Update(m.Chat_UID, 2);
                    db.GetNow().UpdateService(m.Chat_UID, admin_uid);
                }

                List<Model.Message> list_before = db.GetBefore().GetByUid(user_uid).Where(e => e.User_Type == 1 && e.News_State != 2).ToList();
                foreach (Model.Message m in list_before)
                {
                    db.GetBefore().Update(m.Chat_UID, 2);
                    db.GetBefore().UpdateService(m.Chat_UID, admin_uid);
                }

                //保存客服发送消息
                db.GetNow().Add(message);

                Model.Message m1 = db.GetNow().GetByUid(user_uid).Where(e => e.User_Type == 0 && e.Service == admin_uid).Last();
                if (m1 != null)
                {
                    message.Chat_UID = m1.Chat_UID;
                    session.Send("Callback:" + JsonConvert.SerializeObject(message));

                    IEnumerable<KeyValuePair<string, User>> keyValuePairs = Overall.WhereUidAndUserType(user_uid, 1);
                    if (keyValuePairs != null)
                    {
                        foreach (KeyValuePair<string, User> keyValue in keyValuePairs)
                        {
                            Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                        }
                    }
                    foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                    {
                        if (keyValue.Value.User_Type == 0 && keyValue.Key != session.SessionID)
                        {
                            Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                        }
                    }
                }
            }
            //Message:Client:json
            else if ("Client" == values[1])
            {
                int uid = Overall.Get(session.SessionID).Uid;

                Model.Message message = JsonConvert.DeserializeObject<Model.Message>(json);
                message.Time = DateTime.Now;
                message.UID = uid;
                message.User_Type = 1;
                message.News_State = 0;
                message.Service = -1;

                //保存用户发送消息
                DBChat db = new DBChat(DateTime.Now);
                db.GetNow().Add(message);

                Model.Message m = db.GetNow().GetByUid(uid).Where(e => e.User_Type == 1).Last();
                if (m != null && message.Time.ToString().Equals(m.Time.ToString()))
                {
                    message.Chat_UID = m.Chat_UID;
                    session.Send("Callback:" + JsonConvert.SerializeObject(message));
                    Mall_User mall_User = BaseDB.Context.From<Mall_User>().Where(e => e.UID == message.UID).First();
                    if (mall_User != null)
                    {
                        message.Name = mall_User.User_Name;
                        foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                        {
                            if (keyValue.Key != session.SessionID && keyValue.Value.User_Type == 0)
                            {
                                Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                            }
                            if (keyValue.Key != session.SessionID && keyValue.Value.Uid == uid && keyValue.Value.User_Type == 1)
                            {
                                Send(keyValue.Key, "Message:" + JsonConvert.SerializeObject(message));
                            }
                        }
                    }
                }
            }
        }

        private void Callback(string[] values)
        {
            //Callback:Admin:uid:chat_uid
            if ("Admin" == values[1])
            {
                int uid;
                int chat_uid;
                try
                {
                    uid = Convert.ToInt32(values[2]);
                    chat_uid = Convert.ToInt32(values[3]);
                }
                catch (Exception)
                {
                    return;
                }
                //将客户发送消息状态该为已接收
                DBChat db = new DBChat(DateTime.Now);
                Model.Message m_now = db.GetNow().GetByChatUid(chat_uid);
                if (m_now != null && m_now.UID == uid && m_now.User_Type == 1 && m_now.News_State == 0)
                {
                    db.GetNow().Update(chat_uid, 1);
                }
                else
                {
                    Model.Message m_before = db.GetBefore().GetByChatUid(chat_uid);
                    if (m_before != null && m_before.UID == uid && m_before.User_Type == 1 && m_before.News_State == 0)
                    {
                        db.GetBefore().Update(chat_uid, 1);
                    }
                }
            }
            //Callback:Client:uid:chat_uid
            else if ("Client" == values[1])
            {
                int uid;
                int chat_uid;
                try
                {
                    uid = Convert.ToInt32(values[2]);
                    chat_uid = Convert.ToInt32(values[3]);
                }
                catch (Exception)
                {
                    return;
                }
                //将客服发送消息状态该为已接收
                DBChat db = new DBChat(DateTime.Now);
                Model.Message m_now = db.GetNow().GetByChatUid(chat_uid);
                if (m_now != null && m_now.UID == uid && m_now.User_Type == 0 && m_now.News_State == 0)
                {
                    db.GetNow().Update(chat_uid, 1);
                }
                else
                {
                    Model.Message m_before = db.GetBefore().GetByChatUid(chat_uid);
                    if (m_before != null && m_before.UID == uid && m_before.User_Type == 0 && m_before.News_State == 0)
                    {
                        db.GetBefore().Update(chat_uid, 1);
                    }
                }
            }
        }

        private void Query(WebSocketSession session, string[] values)
        {
            int uid;
            int chat_uid;
            int count;
            try
            {
                uid = Convert.ToInt32(values[1]);
                chat_uid = Convert.ToInt32(values[2]);
                count = Convert.ToInt32(values[3]);
            }
            catch (Exception)
            {
                session.Send("QueryError");
                return;
            }

            DBChat db = new DBChat(DateTime.Now);
            Model.Message m = db.GetNow().GetByChatUid(chat_uid);
            if (m != null && m.UID == uid)
            {
                List<Model.Message> list = db.GetNow().GetByUidDesc(uid).Where(e => e.Chat_UID < chat_uid).ToList();
                list.AddRange(db.GetBefore().GetByUidDesc(uid));
                SendMessageQuery(session, list, count);
            }
            else
            {
                SendMessageQuery(session, db.GetBefore().GetByUidDesc(uid).Where(e => e.Chat_UID < chat_uid).ToList(), count);
            }
        }

        private void SendMessageQuery(WebSocketSession session, List<Model.Message> list, int count)
        {
            foreach (Model.Message message in list)
            {
                if (count <= 0) break;
                count--;
                if (message.User_Type == 0)
                {
                    Mall_Admin mall_Admin = BaseDB.Context.From<Mall_Admin>().Where(e => e.Admin_UID == message.Service).First();
                    if (mall_Admin != null)
                    {
                        message.Name = mall_Admin.Admin_Name;
                    }
                }
                session.Send("MessageQuery:" + JsonConvert.SerializeObject(message));
            }
        }

        private void Occupation(WebSocketSession session, string[] values)
        {
            int uid;
            try
            {
                uid = Convert.ToInt32(values[2]);
            }
            catch (Exception)
            {
                return;
            }

            if ("Before".Equals(values[1]))
            {
                foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                {
                    if (keyValue.Value.User_Type == 0)
                    {
                        Send(keyValue.Key, "Occupation:Remove:" + uid);
                        keyValue.Value.Select = -1;
                    }
                }
            }
            else if ("Now".Equals(values[1]))
            {
                Overall.Get(session.SessionID).Select = uid;
                foreach (KeyValuePair<string, User> keyValue in Overall.KeyValues)
                {
                    if (keyValue.Value.User_Type == 0 && keyValue.Key != session.SessionID)
                    {
                        Send(keyValue.Key, "Occupation:Select:" + uid);
                    }
                }
            }
        }

        private void Remove(WebSocketSession session, string[] values)
        {
            int uid;
            try
            {
                uid = Convert.ToInt32(values[1]);
            }
            catch (Exception)
            {
                return;
            }

            Overall.Get(session.SessionID).Select = -1;
            IEnumerable<KeyValuePair<string, User>> keyValuePairs = Overall.WhereUserType(0);
            if (keyValuePairs != null)
            {
                foreach (KeyValuePair<string, User> keyValue in keyValuePairs)
                {
                    Send(keyValue.Key, "Remove:" + uid);
                }
            }
            DBChat db = new DBChat(DateTime.Now);
            List<Model.Message> list = db.GetBefore().GetByUid(uid).Where(e => e.User_Type == 1 && e.News_State != 2).ToList();
            list.AddRange(db.GetNow().GetByUid(uid).Where(e => e.User_Type == 1 && e.News_State != 2).ToList());
            foreach (Model.Message message in list)
            {
                db.GetBefore().Update(message.Chat_UID, 2);
                if (message.Service == -1)
                {
                    db.GetBefore().UpdateService(message.Chat_UID, Overall.Get(session.SessionID).Uid);
                }
            }
        }

        private void QueryUser(WebSocketSession session, string[] values)
        {
            string[] vs = values[1].Split(',');
            DBChat db = new DBChat(DateTime.Now);
            int uid = Overall.Get(session.SessionID).Uid;
            List<Model.Message> list = db.GetNow().GetByServiceUIdDesc(uid);
            list.AddRange(db.GetBefore().GetByServiceUIdDesc(uid));
            foreach (Model.Message message in list)
            {
                IEnumerable<string> vs1 = vs.Where(e => e == Convert.ToString(message.UID));
                if (vs1 == null || vs1 != null && vs1.Count() <= 0)
                {
                    List<Model.Message> m_now = db.GetNow().GetByUidDesc(message.UID).Where(e => e.User_Type == 1).ToList();
                    if (m_now.Count > 0)
                    {
                        Mall_User mall_User = BaseDB.Context.From<Mall_User>().Where(e => e.UID == m_now[0].UID).First();
                        if (mall_User != null)
                        {
                            m_now[0].Name = mall_User.User_Name;
                            session.Send("QueryUser:" + JsonConvert.SerializeObject(m_now[0]));
                            List<Model.Message> list_now = db.GetNow().GetByUid(message.UID).Where(e => e.Chat_UID > m_now[0].Chat_UID).ToList();
                            foreach (Model.Message m in list_now)
                            {
                                Mall_Admin mall_Admin = BaseDB.Context.From<Mall_Admin>().Where(e => e.Admin_UID == m.Service).First();
                                if (mall_Admin != null)
                                {
                                    m.Name = mall_Admin.Admin_Name;
                                }
                                session.Send("MessageQueryUser:" + JsonConvert.SerializeObject(m));
                            }
                        }
                    }
                    else
                    {
                        List<Model.Message> m_before = db.GetBefore().GetByUidDesc(message.UID).Where(e => e.User_Type == 1).ToList();
                        if (m_before.Count > 0)
                        {
                            Mall_User mall_User = BaseDB.Context.From<Mall_User>().Where(e => e.UID == m_before[0].UID).First();
                            if (mall_User != null)
                            {
                                m_before[0].Name = mall_User.User_Name;
                                session.Send("QueryUser:" + JsonConvert.SerializeObject(m_before[0]));
                            }
                            List<Model.Message> list_before = db.GetBefore().GetByUid(message.UID).Where(e => e.Chat_UID > m_before[0].Chat_UID).ToList();
                            list_before.AddRange(db.GetNow().GetByUid(message.UID));
                            foreach (Model.Message m in list_before)
                            {
                                Mall_Admin mall_Admin = BaseDB.Context.From<Mall_Admin>().Where(e => e.Admin_UID == m.Service).First();
                                if (mall_Admin != null)
                                {
                                    m.Name = mall_Admin.Admin_Name;
                                }
                                session.Send("MessageQueryUser:" + JsonConvert.SerializeObject(m));
                            }
                        }
                    }
                    break;
                }
            }
        }
    }
}
