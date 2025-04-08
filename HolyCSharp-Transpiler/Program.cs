using HolyCSharp_Transpiler;

internal class Program
{
    private static void Main(string[] args)
    {
        var holycsCode = File.ReadAllText("example.holycs");

       Transpiler transpiler = new Transpiler();
        string csharpCode = transpiler.Transpile(holycsCode);

        Console.WriteLine("Generated C# Code:");
        Console.WriteLine(csharpCode);
        Console.WriteLine("");
        Console.WriteLine("Original HolyCS code:");
        Console.WriteLine(holycsCode);
        File.WriteAllLines("example.cs", new string[] { csharpCode });
    }
}
