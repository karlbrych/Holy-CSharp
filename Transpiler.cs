using System;
using System.Text;

namespace HolyCSharp_Transpiler
{
    public class Transpiler
    {
        public string TranspileVariableDeclaration(string holycsCode)
        {
            // Example: "var x = new HolyObject(5);"
            if (holycsCode.StartsWith("var"))
            {
                string[] parts = holycsCode.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string variableName = parts[1];
                string value = parts[3].TrimEnd(';');

                // Check if the value is an integer and handle it accordingly
                if (int.TryParse(value, out int parsedValue))
                {
                    return $"HolyObject {variableName} = new HolyObject({parsedValue});";
                }
                else if (value.StartsWith("new HolyObject"))
                {
                    return $"HolyObject {variableName} = {value};";
                }
                else
                {
                    return $"HolyObject {variableName} = new HolyObject({value});";
                }
            }

            return holycsCode;
        }

        // Method to transpile addition operation
        public string TranspileAddition(string holycsCode)
        {
            // Example: "x + y"
            var parts = holycsCode.Split('+');
            string left = parts[0].Trim();
            string right = parts[1].Trim();

            return $"{left}.Add({right})";
        }

        // Method to transpile the print function (including printLine)
        public string TranspilePrint(string holycsCode)
        {
            if (holycsCode.StartsWith("print"))
            {
                int start = holycsCode.IndexOf('(') + 1;
                int end = holycsCode.IndexOf(')');
                string textToPrint = holycsCode.Substring(start, end - start).Trim().Trim('\'');

                // Handle concatenation within print statements
                if (textToPrint.Contains(" + "))
                {
                    string[] parts = textToPrint.Split(new string[] { " + " }, StringSplitOptions.None);
                    StringBuilder sb = new StringBuilder("Console.Write(");
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (i > 0) sb.Append(" + ");
                        sb.Append(parts[i].Trim());
                    }
                    sb.Append(");");
                    return sb.ToString();
                }

                return $"Console.Write(\"{textToPrint}\");"; // print does not end with a newline
            }
            if (holycsCode.StartsWith("printLine"))
            {
                int start = holycsCode.IndexOf('(') + 1;
                int end = holycsCode.IndexOf(')');
                string textToPrint = holycsCode.Substring(start, end - start).Trim().Trim('\'');

                // Handle concatenation within printLine statements
                if (textToPrint.Contains(" + "))
                {
                    string[] parts = textToPrint.Split(new string[] { " + " }, StringSplitOptions.None);
                    StringBuilder sb = new StringBuilder("Console.WriteLine(");
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (i > 0) sb.Append(" + ");
                        sb.Append(parts[i].Trim());
                    }
                    sb.Append(");");
                    return sb.ToString();
                }

                return $"Console.WriteLine(\"{textToPrint}\");"; // printLine ends with a newline
            }

            return holycsCode;
        }

        // Transpile the entire HolyCS code
        public string Transpile(string holycsCode)
        {
            StringBuilder csharpCode = new StringBuilder();
            string[] lines = holycsCode.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.Contains(" + "))
                {
                    csharpCode.AppendLine(TranspileAddition(line));
                }
                else if (line.Contains("var"))
                {
                    csharpCode.AppendLine(TranspileVariableDeclaration(line));
                }
                else if (line.Contains("print") || line.Contains("printLine"))
                {
                    csharpCode.AppendLine(TranspilePrint(line));
                }
                else
                {
                    csharpCode.AppendLine(line); // Handle any other code (e.g., statements, function calls)
                }
            }

            return csharpCode.ToString();
        }
    }
}
