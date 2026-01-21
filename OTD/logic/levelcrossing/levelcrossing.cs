using System;
using System.Collections.Generic;

public enum LevelcrossingAspect
{
   Open,
   Closed,
   Closure,
}
public class Levelcrossing
{
    public bool PowerOn { get; set; }
    public bool Error { get; set; }
    public bool ManualClose { get; set; }
    public bool AutomaticClose { get; set; }
    public bool Closure { get; set; }
    public string LevelcrossingName { get; set; } = "unnamed";
    public string SvgDirectory { get; set; } = "SVG";
    public string? ColorOverride { get; set; }
    public string? ClosureSvgFile { get; set; }
    public Dictionary<LevelcrossingAspect, string> SvgFileOverrides { get; } = new();
    public Dictionary<string, string> SvgColorOverrides { get; } = new(StringComparer.OrdinalIgnoreCase);
    public SvgApi Svg { get; } = new SvgApi();
    public string? SvgToken => Svg.Token;
    public string? SvgFile => Svg.File;
    public string? SvgPath => Svg.Path;

    public LevelcrossingAspect Aspect { get; private set; } = LevelcrossingAspect.Open;
    public bool FlashingLights { get; private set; }

    public void UpdateLevelcrossing()
    {
        FlashingLights = false;

        if (ManualClose)
        {
            FlashingLights = true;
            Aspect = LevelcrossingAspect.Closed;
            Console.WriteLine(LevelcrossingName + ": Manual close activated.");
        }
        else if (AutomaticClose)
        {
            FlashingLights = true;
            Aspect = LevelcrossingAspect.Closed;
            Console.WriteLine(LevelcrossingName + ": Automatic close activated.");
        }
        else
        {
            Aspect = LevelcrossingAspect.Open;
        }

        if (Closure)
        {
            Aspect = LevelcrossingAspect.Closure;
        }

        UpdateSvg();
    }

    public void UpdateSvg()
    {
        SvgUpdate.ForLevelcrossing(
            Svg,
            Aspect,
            SvgDirectory,
            ColorOverride,
            SvgFileOverrides,
            SvgColorOverrides,
            ClosureSvgFile);
    }

}
