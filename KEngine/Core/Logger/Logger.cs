using System;
using System.Diagnostics;

namespace KEngine.Core {
    public static class Logger {

        [Conditional("DEBUG")]
        public static void Log(object obj) {
            Console.WriteLine(obj);
        }

        [Conditional("DEBUG")]
        public static void LogW(object obj) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(obj);
            Console.ResetColor();
        }
    }
}
