namespace BTEAManual
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Windows.Forms;
    using System.Net.NetworkInformation;
    using System.Text.RegularExpressions;
    using System.Runtime.CompilerServices;

    public class Logger
    {
        private static FileStream stream;
        private static FileInfo log;
        private static string appName = Assembly.GetEntryAssembly().GetName().Name;
        private static string logFileName = new FileInfo(appName + ".log").FullName.ToString();
        private static int processId = Process.GetCurrentProcess().Id;

        public static void Write(string logMessage, bool newLine = true,
            [CallerMemberName] string member = "",
            [CallerFilePath] string path = "")
        {
            log = new FileInfo(logFileName);

            if (!log.Exists)
                stream = log.Create();
            else
                stream = new FileStream(logFileName, FileMode.Append);

            using (var writer = new StreamWriter(stream))
            {
                if (newLine)
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {"".PadLeft(3)} {logMessage}");
                else
                    writer.Write($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {"".PadLeft(3)} {logMessage} ");
            }
        }

        public static void WriteStatus(string status)
        {
            stream = new FileStream(logFileName, FileMode.Append);

            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine($"[{status}]");
            }
        }

        public static void WriteError(string errorMessage, bool newLine = true, bool writeCallerNumber = true,
            [CallerMemberName] string member = "",
            [CallerFilePath] string path = "",
            [CallerLineNumber] int line = 0)
        {
            log = new FileInfo(logFileName);

            if (!log.Exists)
                stream = log.Create();
            else
                stream = new FileStream(logFileName, FileMode.Append);

            using (var writer = new StreamWriter(stream))
            {
                if (newLine)
                {
                    if (writeCallerNumber)
                        writer.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {line.ToString().PadLeft(3)} {Regex.Replace(errorMessage, @"\r\n?|\n", "")}");
                    else
                        writer.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {"".PadLeft(3)} {errorMessage}");
                }
                else
                {
                    if (writeCallerNumber)
                        writer.Write($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {line.ToString().PadLeft(3)} {Regex.Replace(errorMessage, @"\r\n?|\n", "")}");
                    else
                        writer.Write($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {processId.ToString().PadLeft(5, ' ')} {Path.GetFileNameWithoutExtension(path).PadLeft(16, ' ')} {member.PadLeft(21, ' ') + "()"} {"".PadLeft(3)} {Regex.Replace(errorMessage, @"\r\n?|\n", "")}");
                }

            }
        }

        public static void WriteInfo()
        {
            if (File.Exists(logFileName))
            {
                stream = new FileStream(logFileName, FileMode.Append);

                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine();
                }

                return;
            }

            var localIP = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet && i.OperationalStatus == OperationalStatus.Up)
                .SelectMany(i => i.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.Address.ToString())
                .ToList();

            string separator = "*************************************************************************************************************************";
            string exeName = $"AppName\t\t: {appName}";
            string version = $"Version\t\t: {Application.ProductVersion}";
            string cpu = $"CPU\t\t: {DeviceInfo.CPU()[0]} [Cores={DeviceInfo.CPU()[1]}, Speed={DeviceInfo.CPU()[2]}]";
            string os = $"OS\t\t: {DeviceInfo.Name} {DeviceInfo.Edition} {DeviceInfo.Version} {DeviceInfo.OSBits.ToString()}";
            string ipAddress = $"IPAddress\t: {(localIP.Count > 1 ? string.Join(",", localIP) : localIP[0])}";
            string path = $"Path\t\t: {AppDomain.CurrentDomain.BaseDirectory}";
            string hostName = $"HostName\t: {Environment.MachineName}";

            log = new FileInfo(logFileName);

            if (!log.Exists)
                stream = log.Create();
            else
                stream = new FileStream(logFileName, FileMode.Append);

            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(separator);
                writer.WriteLine(exeName);
                writer.WriteLine(version);
                writer.WriteLine(cpu);
                writer.WriteLine(os);
                writer.WriteLine(ipAddress);
                writer.WriteLine(path);
                writer.WriteLine(hostName);
                writer.WriteLine(separator);
            }
        }
    }
}