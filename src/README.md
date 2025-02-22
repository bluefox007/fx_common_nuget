﻿# BlueFox.Fx.Common Nuget

Nuget package created to handle all default helper classes and factories.

## Links

[Build and upload](#build-and-upload)
[Deployment](#Deployment)
[Extra information](#Extra-information)



## Build and upload 

For releases we use Obfuscator https://www.preemptive.com/dotfuscator/pro/userguide/en/getting_started_protect.html

You need to install the SDK and install the extension in VS under tools.

[Extra information](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio)

To create a Nuget package of this project.  

```
- Modify version number in Business.csproj.nuspec.
- Right click project "BlueFox.Fx.Business".
- Click "pack".
- Navigate to "xxx\BlueFox.Fx.Common.Business\bin\Release\BlueFox.Fx.x.x.x.nupkg".
```

If you have a problem including all references to the project try:

**Update Nuget**: Go to;  [Download page](https://www.nuget.org/downloads)

When having problems installing the Nuget in a project run; 

```
>> nuget.exe locals -clear all
```

On every build in release a new package is generated.

## Deployment

**Check sources**

```
>> nuget sources
```

**Add new source**

```
>> nuget sources Add -Name "feedName" -Source "https://sammylord.pkgs.visualstudio.com/_packaging/Packages/nuget/v3/index.json" -username ***username*** -password ***(api key)***
```

**To upload the package to VSTS**

- Navigate to the directory of the generated package.
- Generate access token on VSTS, if not already done.

```
>> nuget push BlueFox.Fx.Common.x.x.x.nupkg -source "feedName" -ApiKey ***(api key)***
```

## Extra information

If you want to learn more about markdown please visit this [link](https://guides.github.com/features/mastering-markdown/)

