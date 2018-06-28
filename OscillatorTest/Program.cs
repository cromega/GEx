using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirpesizer;
using System.IO;

namespace OscillatorTest {
    class Program {
        static void Main(string[] args) {
            var newosc = new Oscillator(OscillatorType.Sine);
            var oldosc = new StatefulOscillator(OscillatorType.Sine);

            var num = 100;
            int samples = 44100 / num;
            var audio = new SoundSystem(samples);
            var buf = new short[samples * 2];
            var log = File.CreateText("log.txt");

            for (int i=0; i<num; i++) {
                var freq = 220 + i * 2;
                for (int j=0; j<samples; j++) {
                    var value = oldosc.Next(freq) * short.MaxValue;
                    buf[j * 2] = (short)value;
                    buf[j * 2 + 1] = (short)value;
                }
                audio.Write(buf);
            }

            System.Threading.Thread.Sleep(500);

            for (int i=0; i<num; i++) {
                var freq = 220 + i * 2;
                for (int j=0; j<samples; j++) {
                    newosc.SetFrequency(freq);
                    var value = newosc.Next() * short.MaxValue;
                    buf[j * 2] = (short)value;
                    buf[j * 2 + 1] = (short)value;
                }
                audio.Write(buf);
            }

            log.Close();
            System.Threading.Thread.Sleep(500);
        }
    }
}
