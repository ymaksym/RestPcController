using System;
using System.Diagnostics;
using System.Linq;

namespace RestPcController
{
    public static class WindowsServiceInstaller
    {
        public static bool TryInstall(string[] args, out int errorCode)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            var serviceNamePair = args.SingleOrDefault(arg => arg.Contains("-servicename:", StringComparison.InvariantCultureIgnoreCase));

            if (!args.Contains("install") || serviceNamePair == null)
            {
                errorCode = 0;
                return false;
            }

            var serviceName = serviceNamePair.Split(':').Last();

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentOutOfRangeException("servicename");
            }

            Console.WriteLine("Installing serivice...");

            var servicePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");

            var processInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "sc.exe",
                Arguments = $"create {serviceName} displayname=\"{serviceName}\" binpath=\"{servicePath}\" start=auto"
            };

            using var process = new Process { StartInfo = processInfo };
            process.Start();
            process.WaitForExit();
            errorCode = process.ExitCode;
            return true;
        }
    }
}
