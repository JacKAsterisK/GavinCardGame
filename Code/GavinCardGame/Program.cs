using System;

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
                PlayAs = args[0].Split('=')[1];
            }

            using (var game = new MainGame())
                game.Run();
        }
    }
#endif
}
