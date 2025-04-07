using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holy_CS
{
     public class NullObject:LangObject
    {
        public override LangObject Evaluate(Context context) => this;
        public override string ToString() => "null";    
    }
}
