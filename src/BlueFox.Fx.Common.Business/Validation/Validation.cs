//-----------------------------------------------------------------------
// <copyright file="Validation.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx;

public class Validation
{
    public static string Standard = "^[^<>%$*/|\\@!?\"':;)(&]*$";

    public static string SearchString = "[^<>%$*/|\\@!?\"':;)(&]*$]+";

    public static string Password = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";

    public static string Website = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
}