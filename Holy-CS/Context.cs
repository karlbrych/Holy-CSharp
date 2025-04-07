using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holy_CS
{
     public class Context
    {
        private Dictionary<string, LangObject> variables = new();
        public LangObject Get(string name) => variables.TryGetValue(name, out var val) ? val : new NullObject();
        public void Set(string name, LangObject value) => variables[name] = value;
    }
}
