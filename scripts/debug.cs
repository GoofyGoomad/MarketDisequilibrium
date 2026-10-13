namespace MarketDisequilibrium.scripts;
using System;
using System.Runtime.InteropServices;
// Copied from - https://github.com/BhaktaRout038/Identify-Operating-System-in-CSharp
class Program
{
    static void Main()
    {
        // Get the OS description
        string osDescription = RuntimeInformation.OSDescription;
        Console.WriteLine($"Operating System: {osDescription}");
        // Check the OS platform
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("This is a Windows OS.");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Console.WriteLine("This is a Linux OS.");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Console.WriteLine("This is a macOS.");
        }
        else
        {
            Console.WriteLine("Unknown OS.");
        }
    }
}
