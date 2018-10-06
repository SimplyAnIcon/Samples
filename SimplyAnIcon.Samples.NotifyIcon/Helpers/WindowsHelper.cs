using System;
using System.IO;
using System.Reflection;
using SimplyAnIcon.Samples.NotifyIcon.Helpers.Interfaces;

namespace SimplyAnIcon.Samples.NotifyIcon.Helpers
{
    public class WindowsHelper : IWindowsHelper
    {
        public string AppRoamingDataPath()
        {
            var di = new DirectoryInfo(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetEntryAssembly().GetName().Name));

            if(!di.Exists)
                di.Create();

            return di.FullName;
        }
    }
}
