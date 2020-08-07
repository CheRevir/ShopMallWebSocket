using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class IniFile
    {
        private static readonly string file = Environment.CurrentDirectory + "\\Config.ini";

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(string section, string name, int defValue, string file);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string name, string defValue, StringBuilder retValue, int size, string file);

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string setcion, string name, string value, string file);

        public static void PutInt(string section, string name, int value)
        {
            WritePrivateProfileString(section, name, value.ToString(), file);
        }

        public static int GetInt(string section, string name, int defValue)
        {
            return GetPrivateProfileInt(section, name, defValue, file);
        }

        public static void PutString(string section, string name, string value)
        {
            WritePrivateProfileString(section, name, value, file);
        }

        public static string GetString(string section, string name, string defValue)
        {
            StringBuilder sb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, defValue, sb, 2048, file);
            return sb.ToString();
        }

        public static void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, file);
        }

        public static void DeleteAllSection()
        {
            WritePrivateProfileString(null, null, null, file);
        }
    }
}
