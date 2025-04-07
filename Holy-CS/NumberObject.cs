using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holy_CS
{

    public class NumberObject : LangObject
    {
        public double Value { get; }
        public NumberObject(double value) => Value = value;
        public override LangObject Evaluate(Context context) => this;
        public override string ToString() => Value.ToString();
    }

    public class VariableAccessObject : LangObject
    {
        public string Name { get; }
        public VariableAccessObject(string name) => Name = name;
        public override LangObject Evaluate(Context context) => context.Get(Name);
    }

    public class AssignmentObject : LangObject
    {
        public string Name { get; }
        public LangObject Expression { get; }
        public AssignmentObject(string name, LangObject expression)
        {
            Name = name;
            Expression = expression;
        }
        public override LangObject Evaluate(Context context)
        {
            var value = Expression.Evaluate(context);
            context.Set(Name, value);
            return value;
        }
    }


}
