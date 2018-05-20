using System;

namespace chirpcore {
    public static class Logger {
        private static int StartTime;
        private static bool IsOn;
        public static void On() {
            IsOn = true;   
            StartTime = Environment.TickCount;
        }

        public static void Log(string message, params object[] args) {
            if (!IsOn) { return; }
            var time = Environment.TickCount - StartTime;
            Console.WriteLine("{0}: {1}", time, String.Format(message, args));
        }
    }
}