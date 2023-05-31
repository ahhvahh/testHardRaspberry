using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        // Getting temperature
        string thermalZonePath = "/sys/class/thermal/thermal_zone0/temp";
        if (File.Exists(thermalZonePath))
        {
            string tempData = File.ReadAllText(thermalZonePath);
            double tempC = int.Parse(tempData) / 1000.0;
            double tempF = (tempC * 9 / 5) + 32;

            Console.WriteLine($"Temperature: {tempC:F2} °C / {tempF:F2} °F");
        }
        else
        {
            Console.WriteLine("Cannot find temperature data.");
        }

        // Getting logged in sessions
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"who\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();

        Console.WriteLine("Current active sessions:");
        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            Console.WriteLine(line);
        }
    }
}
