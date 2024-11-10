//-----------------------------------------------------------------------
// <copyright file="WebsiteValidator.cs" company="Sam Muylaert">
//     Copyright (c) Sam Muylaert. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;
using System.Text.RegularExpressions;

/// <summary>
/// Maps a activation link database object to a dto object.
/// </summary>
public static class WebsiteValidator
{
    public static bool IsValidWebsite(this string url)
    {
        try
        {
            string pattern = Validation.Website;
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsValidWebsiteWithPrefix(this string url)
    {
        try
        {
            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }
        catch (Exception)
        {
            return false;
        }
    }
}