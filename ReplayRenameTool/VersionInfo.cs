using System;
using System.Diagnostics;

namespace ReplayRenameTool
{
    internal class VersionInfo
    {
        private static readonly string? _appPath = typeof(App).Assembly.Location;

        public static string? AppName =>
            !string.IsNullOrEmpty(_appPath) ? FileVersionInfo.GetVersionInfo(_appPath).ProductName : string.Empty;

        public static string? AppVersion =>
            !string.IsNullOrEmpty(_appPath) ? FileVersionInfo.GetVersionInfo(_appPath).ProductVersion : string.Empty;

        public static string? Developer =>
            !string.IsNullOrEmpty(_appPath) ? FileVersionInfo.GetVersionInfo(_appPath).CompanyName : string.Empty;

        public static string OSVersion => Environment.OSVersion.ToString();

        public static string DotNetVersion => $".NET {Environment.Version}";
    }
}
