using System;
using System.Diagnostics;

namespace KEngine.Core {
    public static class Logger {

        [Conditional("DEBUG")]
        public static void LogLifecycle(object obj) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(obj);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static void LogEvent(object obj) {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(obj);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static void LogW(object obj) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
    }
}
