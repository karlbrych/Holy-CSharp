using System;
using System.Text;

namespace HolyCSharp_Transpiler
{
    public class Transpiler
    {
        // Transpile an expression from HolyCS to C#
        //HOLY shit this is a pile of bad code, nvm will fix later
        //TODO: make some other basic arithmetics than sum, file organisation 
        public string TranspileExpression(string expression)
        {
            expression = expression.Trim();

            // Handle addition operations
            if (expression.Contains("+"))
            {
                var parts = expression.Split('+');
                string left = parts[0].Trim();
                string right = parts[1].Trim();
                return $"{left} + {right}";
            }

            // If the expression is a number, wrap it in HolyObject constructor
            if (int.TryParse(expression, out int num))
            {
                return $"new HolyObject({num})";
            }
            if (expression.StartsWith("HolyObject."))
            {
                return expression;
            }
            // If the expression starts with new HolyObject it is already valid
            if (expression.StartsWith("new HolyObject"))
            {
                return expression;
            }

            // Otherwise assume it's a simple value and pass it to constructor
            return $"new HolyObject({expression})";
        }
        public string TranspileVariableDeclaration(string holycsCode)
        {
            // Example: "var x = new HolyObject(5);"
            if (holycsCode.StartsWith("var"))
            {
                // Find the variable name and the right-hand side
                int equalsIndex = holycsCode.IndexOf('=');
                string leftPart = holycsCode.Substring(0, equalsIndex).Trim(); // "var x"
                string[] leftTokens = leftPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string variableName = leftTokens[1];

                // Get the expression part (everything after '=' minus the ending ';')
                string expression = holycsCode.Substring(equalsIndex + 1).Trim().TrimEnd(';');

                string transpiledExpr = TranspileExpression(expression);

                return $"HolyObject {variableName} = {transpiledExpr};";
            }

            return holycsCode;
        }

        // Method to transpile the print function (including PrintLine)
        public string TranspilePrint(string holycsCode)
        {
            // Decide which Console method to use
            bool isLine = false;
            if (holycsCode.StartsWith("LinePrint"))
            {
                isLine = true;
            }
            else if (!holycsCode.StartsWith("Print"))
            {
                return holycsCode;
            }

            int start = holycsCode.IndexOf('(')+1;
            int end = holycsCode.IndexOf(')');
            string textToPrint = holycsCode.Substring(start, end - start).Trim().Trim('\'');

            // Handle concatenation within print statements
            if (textToPrint.Contains(" + "))
            {
                string[] parts = textToPrint.Split(new string[] { " + " }, StringSplitOptions.None);
                StringBuilder sb = new StringBuilder(isLine ? "Console.WriteLine(" : "Console.Write(");
                for (int i = 0; i < parts.Length; i++)
                {
                    if (i > 0) sb.Append(" + ");
                    sb.Append(parts[i].Trim());
                }
                sb.Append(");");
                return sb.ToString();
            }
            return isLine
                ? $"Console.WriteLine({textToPrint});"
                : $"Console.Write({textToPrint});";
        }

        // Transpileeeerrr!!!!
        public string Transpile(string holycsCode)
        {
            StringBuilder csharpCode = new StringBuilder();
            string[] lines = holycsCode.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("var"))
                {
                    csharpCode.AppendLine(TranspileVariableDeclaration(trimmed));
                }
                else if (trimmed.StartsWith("Print") || trimmed.StartsWith("LinePrint"))
                {
                    csharpCode.AppendLine(TranspilePrint(trimmed));
                }
                else if (trimmed.StartsWith("new HolyObject"))
                {
                    // If the line is a constructor call, just append it.
                    csharpCode.AppendLine(trimmed + ";");
                }
                else
                {
                    
                    if (trimmed.Contains(" + "))
                    {
                        
                        csharpCode.AppendLine(TranspileExpression(trimmed) + ";");
                    }
                    else
                    {
                        csharpCode.AppendLine(trimmed);
                    }
                }
            }

            return csharpCode.ToString();
        }
    }
}
