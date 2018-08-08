using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GexUI {
    static class Utils {
        private static string[] ClassLookupPaths = new string[] {
            "GraphExperiment.{0},GraphExperiment",
            "GexUI.{0},GexUI",
        };

        public static Type GetControlType(string className) {
            foreach (string ns in ClassLookupPaths) {
                var fullClassName = String.Format(ns, className);
                var type = Type.GetType(fullClassName);
                if (type != null) {
                    return type;
                }
            }

            throw new Exception(String.Format("Type {0} was not found", className));
        }
    }
}
