using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GavinCardGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static string PlayAs;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var _arg in args)
                {
                    if (_arg == "debugger")
                    {
                        var _dir = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Code"));

                        if (true)
                        {
                            Process.Start($"{_dir}PlayServer.bat");
                            PlayAs = "client";
                        }
                        else
                        {
                            Process.Start($"{_dir}PlayClient.bat");
                            PlayAs = "server";
                        }
                    }
                    else if (_arg.Contains("playas"))
                        PlayAs = args[0].Split('=')[1];
                }
            }

            using (var game = new MainGame())
                game.Run();
        }
    }
#endif
}
