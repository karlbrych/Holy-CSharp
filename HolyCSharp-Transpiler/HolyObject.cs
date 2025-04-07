using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyCSharp_Transpiler
{
    class HolyObject
    {
        public object Value { get; set; }
        public HolyObject(object value) {
            Value = value;
        }
        public static HolyObject operator +(HolyObject a, HolyObject b)
        {
            if (a.Value is int && b.Value is int)
                return new HolyObject((int)a.Value + (int)b.Value);
            else if (a.Value is float && b.Value is int)
                return new HolyObject((float)a.Value + (float)b.Value);
                throw new InvalidOperationException("Unsupported types for addition.");
        }
    }
}
