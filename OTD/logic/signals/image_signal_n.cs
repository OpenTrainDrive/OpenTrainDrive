using System;
using System.Collections.Generic;
using System.IO;

//muss noch an Signal System N angepasst werden
public enum SignalAspect_n
{
    Magenta,
    H,
    NH,
    M,
    Ges_4_an,
    Ges_4_ex,
    Ges_5_an,
    Ges_5_ex,
    Ges_6_an, 
    Ges_6_ex,
    Ges_7_an,
    Ges_7_ex,
    Ges_8_an,
    Ges_8_ex,
    Ges_9_an,
    Ges_9_ex,
    Ges_10_an,
    Ges_10_ex,
    Ges_11_an,
    Ges_11_ex,
    Ges_12_an,
    Ges_12_ex,
    Ges_13_an,
    Ges_13_ex,
    Ges_14_an,
    Ges_14_ex,
    Ges_15_an,
    Ges_15_ex,
    Ges_16_an,
    Ges_16_ex,
    Fes,
    
    


}

public class image_Signal_n
{
    public bool PowerOn { get; set; } = true;
    public bool Error { get; set; }
    public bool ManualStop { get; set; }
    public bool RouteSet { get; set; }
    public bool OccupiedEntrance { get; set; }
    public bool ShortEntrance { get; set; }
    public bool SwitchClosure { get; set; }
    public int VelocityLimit { get; set; } = 0;
    public string SignalName { get; set; } = "Unnamed";
    public string SvgDirectory { get; set; } = "SVG";
    public string? ColorOverride { get; set; }
    public Dictionary<SignalAspect_n, string> SvgFileOverrides { get; } = new();
    public Dictionary<string, string> SvgColorOverrides { get; } = new(StringComparer.OrdinalIgnoreCase);
    public string? SvgToken { get; private set; }
    public string? SvgFile { get; private set; }
    public string? SvgPath { get; private set; }


    public SignalAspect_n Aspect { get; private set; } = SignalAspect_n.Magenta;
    public bool LampRed { get; private set; }
    public bool LampEmergencyRed { get; private set; }
    public bool LampOrange { get; private set; }
    public bool LampGreen { get; private set; }
    public bool LampGes1 { get; private set; }
    public bool LampGes2 { get; private set; }
    public bool LampGes3 { get; private set; }

    public void SignalSystemN()
    {
        LampRed = false;
        LampOrange = false;
        LampGreen = false;
        var canBeeGreen = PowerOn && !Error && RouteSet && SwitchClosure;

        if (!PowerOn || Error)
        {
            Aspect = SignalAspect_n.Magenta;
            Console.WriteLine($"{SignalName} Aspect set to Magenta because PowerOff or Error");
            UpdateSvg();
            return;
        }

        if (PowerOn && (ManualStop || !RouteSet))
        {
            Aspect = SignalAspect_n.H;
            Console.WriteLine($"{SignalName} Aspect set to H because ManualStop or Route not set");
            LampRed = true;
            UpdateSvg();
            return;
        }

        if (PowerOn && Error)
        {
            Aspect = SignalAspect_n.NH;
            Console.WriteLine($"{SignalName} Aspect set to NH because Error");
            LampEmergencyRed = true;
            UpdateSvg();
            return;
        }

        if (canBeeGreen && VelocityLimit == 0)
        {
            Aspect = SignalAspect_n.M;
            Console.WriteLine($"{SignalName} Aspect set to M because can be green and VelocityLimit 0");
            LampGreen = true;
            UpdateSvg();
            return;
        }
        if (canBeeGreen && VelocityLimit == 40)
        {
            Aspect = SignalAspect_n.Ges_4_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_4_ex because can be green and VelocityLimit 40");
            LampGreen = true;
            UpdateSvg();
            return;
        }
        if (canBeeGreen && VelocityLimit == 50)
        {
            Aspect = SignalAspect_n.Ges_5_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_5_ex because can be green and VelocityLimit 50");
            LampGreen = true;
            UpdateSvg();
            return;
        }
        if (canBeeGreen && VelocityLimit == 60)
        {
            Aspect = SignalAspect_n.Ges_6_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_6_ex because can be green and VelocityLimit 60");
            LampGreen = true;
            UpdateSvg();
            return;
        }
        if (canBeeGreen && VelocityLimit == 70)
        {
            Aspect = SignalAspect_n.Ges_7_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_7_ex because can be green and VelocityLimit 70");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
        if (canBeeGreen && VelocityLimit == 80)
        {
            Aspect = SignalAspect_n.Ges_8_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_8_ex because can be green and VelocityLimit 80");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
            if (canBeeGreen && VelocityLimit == 90)
        {
            Aspect = SignalAspect_n.Ges_9_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_9_ex because can be green and VelocityLimit 90");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
            if (canBeeGreen && VelocityLimit == 100)
        {
            Aspect = SignalAspect_n.Ges_10_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_10_ex because can be green and VelocityLimit 100");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }

         if (canBeeGreen && VelocityLimit == 110)
        {
            Aspect = SignalAspect_n.Ges_11_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_11_ex because can be green and VelocityLimit 110");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
         if (canBeeGreen && VelocityLimit == 120)
        {
            Aspect = SignalAspect_n.Ges_12_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_12_ex because can be green and VelocityLimit 120");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
            if (canBeeGreen && VelocityLimit == 130)
        {
            Aspect = SignalAspect_n.Ges_13_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_13_ex because can be green and VelocityLimit 130");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }
            if (canBeeGreen && VelocityLimit == 140)
        {
            Aspect = SignalAspect_n.Ges_14_ex;
            Console.WriteLine($"{SignalName} Aspect set to Ges_14_ex because can be green and VelocityLimit 140");
            LampGreen = true;
            UpdateSvg();
            
            return;
        }

            if (canBeeGreen && VelocityLimit == 150)
        {
                Aspect = SignalAspect_n.Ges_15_ex;
                Console.WriteLine($"{SignalName} Aspect set to Ges_15_ex because can be green and VelocityLimit 150");
                LampGreen = true;
                UpdateSvg();
                
                return;
        }
            if (canBeeGreen && VelocityLimit == 160)
        {
                Aspect = SignalAspect_n.Ges_16_ex;
                Console.WriteLine($"{SignalName} Aspect set to Ges_16_ex because can be green and VelocityLimit 160");
                LampGreen = true;
                UpdateSvg();
                
                return;
        }
        
            else
        {
            
        }
    
    }

    public void UpdateSvg()
    {
        var color = ColorOverride ?? MapAspectToColor();
        if (SvgFileOverrides.TryGetValue(Aspect, out var overrideFile) &&
            !string.IsNullOrWhiteSpace(overrideFile))
        {
            ApplySvgOverride(overrideFile, $"SVG.signaln.aspect.{Aspect.ToString().ToLowerInvariant()}");
            return;
        }

        if (SvgColorOverrides.TryGetValue(color, out var colorOverride) &&
            !string.IsNullOrWhiteSpace(colorOverride))
        {
            ApplySvgOverride(colorOverride, $"SVG.signaln.color.{color.ToLowerInvariant()}");
            return;
        }

        SvgToken = null;
        SvgFile = null;
        SvgPath = null;
    }

    private string MapAspectToColor()
    {
        return Aspect switch
        {
            SignalAspect_n.Magenta => "Magenta",
            SignalAspect_n.H => "Red",
            SignalAspect_n.NH => "Red",
            SignalAspect_n.M => "Green",
            SignalAspect_n.Ges_4_an => "Green",
            SignalAspect_n.Ges_4_ex => "Green",
            SignalAspect_n.Ges_5_an => "Green",
            SignalAspect_n.Ges_5_ex => "Green",
            SignalAspect_n.Ges_6_an => "Green",
            SignalAspect_n.Ges_6_ex => "Green",
            SignalAspect_n.Ges_7_an => "Green",
            SignalAspect_n.Ges_7_ex => "Green",
            SignalAspect_n.Ges_8_an => "Green",
            SignalAspect_n.Ges_8_ex => "Green",
            SignalAspect_n.Ges_9_an => "Green",  
            SignalAspect_n.Ges_9_ex => "Green",
            SignalAspect_n.Ges_10_an => "Green",
            SignalAspect_n.Ges_10_ex => "Green",
            SignalAspect_n.Ges_11_an => "Green",
            SignalAspect_n.Ges_11_ex => "Green",
            SignalAspect_n.Ges_12_an => "Green",
            SignalAspect_n.Ges_12_ex => "Green",
            SignalAspect_n.Ges_13_an => "Green",
            SignalAspect_n.Ges_13_ex => "Green",
            SignalAspect_n.Ges_14_an => "Green",
            SignalAspect_n.Ges_14_ex => "Green",
            SignalAspect_n.Ges_15_an => "Green",
            SignalAspect_n.Ges_15_ex => "Green",
            SignalAspect_n.Ges_16_an => "Green",
            SignalAspect_n.Ges_16_ex => "Green",   
            _ => "Green"
        };
    }

    private void ApplySvgOverride(string svgOverride, string token)
    {
        if (string.IsNullOrWhiteSpace(svgOverride))
        {
            return;
        }

        var normalized = svgOverride.Replace('\\', '/').TrimStart('/');
        if (Path.IsPathRooted(svgOverride))
        {
            normalized = Path.GetFileName(svgOverride);
        }

        var svgDir = SvgDirectory.Replace('\\', '/').Trim('/');
        if (!string.IsNullOrWhiteSpace(svgDir) &&
            normalized.StartsWith(svgDir + "/", StringComparison.OrdinalIgnoreCase))
        {
            normalized = normalized.Substring(svgDir.Length + 1);
        }

        SvgToken = token;
        SvgFile = normalized;
        SvgPath = $"/svg/{normalized}";
    }
}

