using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Overall
    {
        /* private readonly static Object ADDLOCK = new Object();
         private readonly static Object GETLOCK = new Object();
         private readonly static Object REMOVELOCK = new Object();
         private readonly static Object CONTAINSKEYLOCK = new Object();
         private readonly static Object WHERELOCK = new Object();*/

        public static ConcurrentDictionary<string, User> KeyValues = new ConcurrentDictionary<string, User>();

        public static void Add(string key, User value)
        {
            KeyValues.TryAdd(key, value);
        }

        public static User Get(string key)
        {
            KeyValues.TryGetValue(key, out User user);
            return user;
        }

        public static void Remove(string key)
        {
            KeyValues.TryRemove(key, out _);
        }

        public static bool ContainsKey(string key)
        {
            return KeyValues.ContainsKey(key);
        }

        /*  public static IEnumerable<KeyValuePair<string, User>> Where(int uid)
          {
              if (KeyValues.Count > 0)
              {
                  return KeyValues.Where(e => e.Value.Uid == uid);
              }
              return null;
          }*/

        public static IEnumerable<KeyValuePair<string, User>> WhereUserType(int type)
        {
            if (KeyValues.Count > 0)
            {
                return KeyValues.Where(e => e.Value.User_Type == type);
            }
            return null;
        }

        public static IEnumerable<KeyValuePair<string, User>> WhereUidAndUserType(int uid, int type)
        {
            if (KeyValues.Count > 0)
            {
                return KeyValues.Where(e => e.Value.Uid == uid && e.Value.User_Type == type);
            }
            return null;
        }
    }
}
