![Build Status](https://ci.appveyor.com/api/projects/status/3yy3dok9mnsld6d8/branch/master)

# PostalCodes.Net
Library for managing postal codes for .NET

## Branching model

This package follows the git-flow branching model:
* Develop features of a ```feature/*``` branch.
* Create pull request to a ```master``` branch.
* Once ```master``` is branched into ```release/<major>.<minor>.<patch>``` an automatic job builds and if successful publishes the NuGet package.
* After successful build the [artifact](https://www.nuget.org/packages/PostalCodes/) will be available via NuGet or directly from [GitHub Releases](https://github.com/Cimpress-MCP/PostalCodes.Net/releases)

## Prerequisites ##

If you want to open the solution file in Visual Studio, you may need the Sandcastle Help File Builder Visual Studio extension available in the bundle [here](https://github.com/EWSoftware/SHFB/releases)

## Building ##

In order to build the project you can use plain msbuild

```
> nuget restore
> msbuild
```

