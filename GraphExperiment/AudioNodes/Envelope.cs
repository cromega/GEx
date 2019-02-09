namespace GraphExperiment {
    public class Envelope : AudioNode {
        public int A ;
        public int D;
        public double S;
        public int R;

        protected override Packet Update(Packet packet) {
            double value = 0;
            switch (packet.Signal) {
                case Signal.Active:
                    value = GetValue(packet.TimeMS);
                    break;
                case Signal.End:
                    var tick = Get<int>("ReleasedFor", 0);
                    var timeMS = tick / 44.1;
                    value = GetReleasedValue(timeMS);
                    packet.Signal = timeMS < R ? Signal.Active : Signal.End;
                    Save("ReleasedFor", ++tick);
                    break;
            }
            packet.Sample *= value;
            return packet;
        }

        private double GetValue(double time) {
            if (time < A) {
                return time / A;
            } else if (time < A + D) {
                var phase = (time - A) / D;
                return 1.0 - phase * (1 - S);
            } else {
                return S;
            }
        }

        private double GetReleasedValue(double time) {
            if (time == 0) { return S; }
            // may not be needed
            if (time > R) { return 0; }

            var phase = 1.0 - time / R;
            return phase * S;
        }

        public static Envelope Parse(string data) {
            var parts = data.Split(',');
            var e = new Envelope();
            e.A = int.Parse(parts[0]);
            e.D = int.Parse(parts[1]);
            e.S = float.Parse(parts[2]);
            e.R = int.Parse(parts[3]);

            return e;
        }
    }
}

