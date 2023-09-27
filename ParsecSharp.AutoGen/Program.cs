using CppSharp;

namespace ParsecSharp.AutoGen;

public static class Program
{
    public static void Main(string[] args)
    {
        ConsoleDriver.Run(new ParsecSharpLibrary());
    }
}