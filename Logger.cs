using System;

namespace chirpcore {
    public static class Logger {
        private static bool IsOn;
        public static void On() {
            IsOn = true;   
        }

        public static void Log(string message, params object[] args) {
            if (!IsOn) { return; }
            Console.WriteLine(message, args);
        }
    }
}