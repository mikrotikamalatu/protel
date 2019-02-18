namespace BTEAManual
{
    using System.IO;
    using System.Text;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Linq;

    public class IniFile
    {
        private static readonly string exeName = Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly string paramFile = new FileInfo(exeName + ".ini").FullName.ToString();
        private const int size = 255;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(
            string section,
            string key,
            string value,
            string fileName
            );

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder returnValue,
            int size,
            string fileName
            );

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            string returnValue,
            int size,
            string fileName
            );

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
        static extern int GetPrivateProfileSections(
            byte[] returnBuffer,
            int size,
            string fileName
            );

        public static void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, paramFile);
        }

        public static string Read(string section, string key)
        {
            var sb = new StringBuilder(size);
            GetPrivateProfileString(section, key, null, sb, size, paramFile);

            return sb.ToString();
        }

        public static void RemoveSection(string section)
        {
            Write(section, null, null);
        }

        public static void RemoveKey(string section, string key)
        {
            Write(section, key, null);
        }

        public static List<string> GetSections()
        {
            var buffer = new byte[size];
            GetPrivateProfileSections(buffer, buffer.Length, paramFile);

            return new List<string>(Encoding.Default.GetString(buffer)
                .Split('\0')
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        public static bool SectionExists(string section)
        {
            return GetSections().Contains(section);
        }

        public static List<string> GetKeys(string section)
        {
            string returnValue = new string(' ', size);
            GetPrivateProfileString(section, null, null, returnValue, size, paramFile);

            return new List<string>(returnValue
                .Split('\0')
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        public static bool KeyExists(string section, string key)
        {
            return Read(section, key).Length > 0;
        }
    }
}