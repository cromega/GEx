using System;

namespace chirpcore {
    public static class Osc {
//         impl Osc {
//     fn noise(buf: &mut [i16]) {
//         for x in 0..buf.len() / 2 {
//             let sample = thread_rng().gen::<i16>();
//             buf[x * 2] = sample;
//             buf[x * 2 + 1] = sample;
//         }
//     }

//     fn sine(buf: &mut [i16], freq: f64) {
//         let mut table: [f64; LOOKUP_TABLE_LENGTH] = [0.0; LOOKUP_TABLE_LENGTH];
//         for i in 0..table.len() {
//             table[i] = (PI * 2 as f64 * i as f64 / 1000.0).sin();
//         }

//         let increment = LOOKUP_TABLE_LENGTH as f64 * freq / 44100 as f64;
//         let mut phase_index = 0;
//         for i in 0..(buf.len() / 2) {
//             phase_index = (phase_index as f64 + increment).round() as usize % LOOKUP_TABLE_LENGTH;
//             let sample = (table[phase_index] * 32767.0).round() as i16;
//             buf[i * 2] = sample;
//             buf[i * 2 + 1] = sample;
//         }
//     }
// }
        public static void Noise(short[] buffer) {
            var rnd = new Random();

            for (int i=0; i<buffer.Length; i++) {
                buffer[i] = (short)rnd.Next(0, short.MaxValue);
            }
        }
    }
}