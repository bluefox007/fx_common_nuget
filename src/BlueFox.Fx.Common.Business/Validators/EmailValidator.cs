﻿//-----------------------------------------------------------------------
// <copyright file="EmailValidator.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

/// <summary>
/// Maps a activation link database object to a dto object.
/// </summary>
public static class EmailValidator
{
    public static bool IsEmailValid(this string email)
    {
        try
        {
            new System.Net.Mail.MailAddress($"{email}");
            return true;
        }
        catch
        {
            return false;
        }
    }
}