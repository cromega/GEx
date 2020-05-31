using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEx {
    public class Sample {
        public double L;
        public double R;

        public Sample() { }

        public Sample(double value) {
            L = value;
            R = value;
        }

        public Sample(double l, double r) {
            L = l;
            R = r;
        }

        public static Sample operator + (Sample sample, double value) {
            return new Sample(
                sample.L >= 0 ? sample.L + value : sample.L - value,
                sample.R >= 0 ? sample.R + value : sample.R - value
            );
        }

        public static Sample operator * (Sample sample, double value) {
            return new Sample(sample.L * value, sample.R * value);
        }

        public static Sample operator + (Sample sample, Sample other) {
            return new Sample(sample.L + other.L, sample.R + other.R);
        }
    }
}
