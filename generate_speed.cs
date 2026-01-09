using System;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Generate a linear speed step table for 128 speed steps
        // Speed step 1 = stop (value 0)
        // Speed step 2-128 = linear from 1 to 255

        var speedtable = new XElement("speedtable");

        // For speed step 1: stop
        speedtable.Add(new XElement("step", new XAttribute("speed", 1), new XAttribute("value", 0)));

        // For speed steps 2 to 128: linear
        for (int step = 2; step <= 128; step++)
        {
            // Linear mapping: step 2 = 1, step 128 = 255
            int value = (int)Math.Round((step - 1) * 255.0 / 127.0);
            speedtable.Add(new XElement("step", new XAttribute("speed", step), new XAttribute("value", value)));
        }

        Console.WriteLine(speedtable.ToString());
    }
}