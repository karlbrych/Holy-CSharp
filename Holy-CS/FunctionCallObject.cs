using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holy_CS
{
    public class FunctionCallObject : LangObject
    {
        public string FunctionName { get; }
        public List<LangObject> Arguments { get; }

        public FunctionCallObject(string functionName, List<LangObject> arguments)
        {
            FunctionName = functionName;
            Arguments = arguments;
        }

        public override LangObject Evaluate(Context context)
        {
            if (FunctionName == "PrintLine")
            {
                foreach (var arg in Arguments)
                    Console.WriteLine(arg.Evaluate(context));
                return new NullObject();
            }
            if (FunctionName=="Print")
            {
                foreach(var arg in Arguments)
                    Console.Write(arg.Evaluate(context));
                return new NullObject();
            }
            throw new Exception($"Unknown function: {FunctionName}");
        }
    }

}
