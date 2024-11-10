//-----------------------------------------------------------------------
// <copyright file="Randomizer.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

using System;
using System.Linq;

/// <summary>
/// Helper created to easily test classes.
/// </summary>
public class Randomizer
{
    private static Random random = new Random();

    /// <summary>
    /// Randomiwe the string.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <returns>A random string.</returns>
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}