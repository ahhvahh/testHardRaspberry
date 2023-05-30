using System;
using System.IO;

class Program
{
    static void Main()
    {
        var temp = GetCpuTemperature();

        if (temp.HasValue)
        {
            Console.WriteLine($"CPU Temperature: {temp.Value}°C");
        }
        else
        {
            Console.WriteLine("Failed to get CPU temperature.");
        }
    }

    static float? GetCpuTemperature()
    {
        try
        {
            using var reader = new StreamReader("/sys/class/thermal/thermal_zone0/temp");
            var line = reader.ReadLine();
            if (line != null && float.TryParse(line, out var temp))
            {
                return temp / 1000;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CPU temperature. {ex.Message}");
        }

        return null;
    }
}
