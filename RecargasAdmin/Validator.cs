using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

public static class Validator
{
    private static bool numberBetween(int number, int max, int min = 0)
    {
        if (number < min) return false;
        if (number > max) return false;
        return true;
    }
    public static bool Email(string email)
    {
        return Regex.IsMatch(email, @"^([a-z0-9])(([-a-z0-9._])*([a-z0-9]))*\@([a-z0-9])(([a-z0-9-])*([a-z0-9]))+(\.([a-z0-9])([-a-z0-9_-])?([a-z0-9])+)+$", RegexOptions.IgnoreCase);
    }

    public static bool EmailCrypted(string email)
    {
        return Regex.IsMatch(email, @"^([a-z0-9])(([-a-z0-9._])*([a-z0-9*]))*\@([a-z0-9])(([a-z0-9-])*([a-z0-9]))+(\.([a-z0-9])([-a-z0-9_-])?([a-z0-9])+)+$", RegexOptions.IgnoreCase);
    }

    public static bool Alpha(string str)
    {
        return Regex.IsMatch(str, @"^[a-zA-Z]+$");
    }

    public static bool AlphaNumeric(string str)
    {
        return Regex.IsMatch(str, @"^[0-9a-zA-Z]+$");
    }

    public static bool Password(string str)
    {
        return Regex.IsMatch(str, @"^[0-9a-zA-Z*@.&/?$-]+$");
    }

    public static bool AlphaNumericEspace(string str)
    {
        return Regex.IsMatch(str, @"^[0-9a-zA-Z ]+$");
    }

    public static bool Guid(string str)
    {
        return Regex.IsMatch(str, @"^[0-9a-zA-Z-]+$");
    }

    public static bool Length(string str, int max, int min = 0)
    {
        int strLength = str.Length;
        if (!numberBetween(strLength, max, min)) return false;
        return true;
    }

    public static bool UnsignedNumber(string number)
    {
        return Regex.IsMatch(number, @"^\+?[0-9]+$");
    }

    public static bool IP(string ip)
    {
        return IPAddress.TryParse(ip, out _);
    }

    public static bool Date(string date)
    {
        var formats = new[] { "yyyyMMdd", "yyyy-MM-dd" };
        return DateTime.TryParseExact(date,formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
