﻿//-----------------------------------------------------------------------
// <copyright file="DateValidator.cs" company="Sam Muylaert">
//     Copyright (c) Sam Muylaert. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;

/// <summary>
/// Maps a activation link database object to a dto object.
/// </summary>
public static class DateValidator
{
    public static bool IsValidDate(this string date)
    {
        DateTime value;

        if (DateTime.TryParse(date, out value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}