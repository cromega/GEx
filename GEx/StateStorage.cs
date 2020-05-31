using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEx
{
    public class StateStorage
    {
        private Dictionary<string, Hashtable> Storage;
        private Hashtable Current;

        public StateStorage() {
            Storage = new Dictionary<string, Hashtable>();
        }

        public void SetState(string id) {
            if (!Storage.ContainsKey(id)) {
                Storage[id] = new Hashtable();
            }
            Current = Storage[id];
        }

        public void Set(string name, object value) {
            Current[name] = value;
        }

        public T Get<T>(string key) {
            return (T)(Current[key]);
        }

        public T Get<T>(string key, object defaultValue) {
            var value = Current[key] ?? defaultValue;
            return (T)value;
        }
    }
}
