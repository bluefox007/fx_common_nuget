//-----------------------------------------------------------------------
// <copyright file="CurrentEnvironment.cs" company="BlueFox">
// Copyright (c) BlueFox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BlueFox.Fx.Environment;

using System;

public static class CurrentEnvironment
{
    private const string CurrentEnv = "BlueFox.Environment";

    public static Fx.Enum.Environment GetCurrentEnvironment()
    {
        string env = Environment.GetEnvironmentVariable(CurrentEnv);

        switch (env.ToLower())
        {
            case "develop":
                return Fx.Enum.Environment.Develop;

            case "integration":
                return Fx.Enum.Environment.Integration;

            case "qa":
                return Fx.Enum.Environment.Qa;

            case "production":
                return Fx.Enum.Environment.Production;

            default:
                throw new ArgumentException("There was no value found for environment variable Environment");
        }
    }

    public static bool UnitTestMode
    {
        get
        {
            string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            return processName == "VSTestHost" || processName.StartsWith("vstest.executionengine")
                                   || processName.StartsWith("QTAgent") || processName.Equals("testhost");
        }
    }
}