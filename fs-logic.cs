using System.Text.RegularExpressions;

namespace OpenTrainDrive;

public static class FsLogic
{
    public static string ApplySvgField(string file, string? fieldKey, string? fieldValue)
    {
        if (string.IsNullOrWhiteSpace(file) || string.IsNullOrWhiteSpace(fieldKey))
        {
            return file;
        }

        var normalized = NormalizeSvgFile(file);
        var key = fieldKey.Trim().ToLowerInvariant();
        var value = (fieldValue ?? string.Empty).Trim().ToLowerInvariant();

        return key switch
        {
            "turnout" => value switch
            {
                "diverge" => ToSwitchTurnoutGreen(normalized),
                "straight" => ToSwitchStraightGreen(normalized),
                _ => normalized
            },
            "route" => value == "on" ? ToNumberGreen(ToTrackGreen(normalized)) : normalized,
            "signal" => value switch
            {
                "green" => ToSignalGreen(normalized),
                "red" => ToSignalRed(normalized),
                _ => normalized
            },
            _ => normalized
        };
    }

    public static string ApplySvgFields(string file, IEnumerable<KeyValuePair<string, string?>> fields)
    {
        var current = file;
        foreach (var field in fields)
        {
            current = ApplySvgField(current, field.Key, field.Value);
        }
        return current;
    }

    public static string NormalizeSvgFile(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Zugnummernanzeiger.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Zugnummernanzeiger.svg", "Iltis_Zugnummernanzeiger_White.svg", StringComparison.Ordinal);
        }
        return file;
    }

    public static string ToSwitchStraight(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Switch_Straight.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Switch_Straight_White.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Switch_Straight_Green.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Straight_Green.svg", "Iltis_Switch_Straight_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Turnout.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Turnout.svg", "Iltis_Switch_Straight.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Turnout_White.svg", "Iltis_Switch_Straight_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Turnout_Green.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Turnout_Green.svg", "Iltis_Switch_Straight_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_White.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_White.svg", "Iltis_Switch_Straight_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch.svg", "Iltis_Switch_Straight_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Turnout_Left.svg", StringComparison.Ordinal) ||
            file.Contains("Iltis_Turnout_Right.svg", StringComparison.Ordinal) ||
            file.Contains("Iltis_Turnout.svg", StringComparison.Ordinal))
        {
            return Regex.Replace(file, @"Iltis_Turnout_(Left|Right)\.svg|Iltis_Turnout\.svg", "Iltis_Switch_Straight.svg");
        }
        return file;
    }

    public static string ToSwitchTurnout(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Switch_Turnout.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Switch_Turnout_Green.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Turnout_Green.svg", "Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Straight.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Straight.svg", "Iltis_Switch_Turnout.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Straight_White.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Straight_White.svg", "Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_Straight_Green.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_Straight_Green.svg", "Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch_White.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch_White.svg", "Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Switch.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Switch.svg", "Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal);
        }
        if (file.Contains("Iltis_Turnout_Left.svg", StringComparison.Ordinal) ||
            file.Contains("Iltis_Turnout_Right.svg", StringComparison.Ordinal) ||
            file.Contains("Iltis_Turnout.svg", StringComparison.Ordinal))
        {
            return Regex.Replace(file, @"Iltis_Turnout_(Left|Right)\.svg|Iltis_Turnout\.svg", "Iltis_Switch_Turnout.svg");
        }
        return file;
    }

    public static string ToSwitchStraightGreen(string file)
    {
        var white = ToSwitchStraight(file);
        if (string.IsNullOrWhiteSpace(white))
        {
            return white;
        }
        if (white.Contains("Iltis_Switch_Straight_White.svg", StringComparison.Ordinal))
        {
            return white.Replace("Iltis_Switch_Straight_White.svg", "Iltis_Switch_Straight_Green.svg", StringComparison.Ordinal);
        }
        return white.Replace("Iltis_Switch_Straight.svg", "Iltis_Switch_Straight_Green.svg", StringComparison.Ordinal);
    }

    public static string ToSwitchTurnoutGreen(string file)
    {
        var white = ToSwitchTurnout(file);
        if (string.IsNullOrWhiteSpace(white))
        {
            return white;
        }
        if (white.Contains("Iltis_Switch_Turnout_White.svg", StringComparison.Ordinal))
        {
            return white.Replace("Iltis_Switch_Turnout_White.svg", "Iltis_Switch_Turnout_Green.svg", StringComparison.Ordinal);
        }
        return white.Replace("Iltis_Switch_Turnout.svg", "Iltis_Switch_Turnout_Green.svg", StringComparison.Ordinal);
    }

    public static string ToTrackGreen(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Straight_Green.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Straight_White", StringComparison.Ordinal))
        {
            return Regex.Replace(file, @"Iltis_Straight_White[^/]*\.svg", "Iltis_Straight_Green.svg", RegexOptions.IgnoreCase);
        }
        return file;
    }

    public static string ToNumberGreen(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Zugnummernanzeiger_Green.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Zugnummernanzeiger_White.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Zugnummernanzeiger_White.svg", "Iltis_Zugnummernanzeiger_Green.svg", StringComparison.Ordinal);
        }
        return file;
    }

    public static string ToSignalGreen(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Signal_Green.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Signal_Red.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Signal_Red.svg", "Iltis_Signal_Green.svg", StringComparison.Ordinal);
        }
        return file;
    }

    public static string ToSignalRed(string file)
    {
        if (string.IsNullOrWhiteSpace(file))
        {
            return file;
        }
        if (file.Contains("Iltis_Signal_Red.svg", StringComparison.Ordinal)) return file;
        if (file.Contains("Iltis_Signal_Green.svg", StringComparison.Ordinal))
        {
            return file.Replace("Iltis_Signal_Green.svg", "Iltis_Signal_Red.svg", StringComparison.Ordinal);
        }
        return file;
    }
}
