using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holy_CS
{
    public abstract class LangObject
    {
        public abstract LangObject Evaluate(Context context);
    }
}
