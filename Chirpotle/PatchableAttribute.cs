using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirpotle {
    [AttributeUsage(AttributeTargets.Field)]
    class PatchableAttribute : Attribute {
        public string Name;
    }
}
