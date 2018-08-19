using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphExperiment {
    public class Sample {
        public double L;
        public double R;
        public double[] AsArray {
            get { return new double[] { L, R }; }
        }

        public Sample() { }

        public Sample(double value) {
            L = value;
            R = value;
        }

        public static Sample operator + (Sample sample, double value) {
            return new Sample {
                L = sample.L >= 0 ? sample.L + value : sample.L - value,
                R = sample.R >= 0 ? sample.R + value : sample.R - value,
            };
        }

        public static Sample operator * (Sample sample, double value) {
            return new Sample { L = sample.L * value, R = sample.R * value };
        }
    }
}
