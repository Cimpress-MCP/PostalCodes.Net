[![Build Status](https://img.shields.io/travis/Cimpress-MCP/PostalCodes.Net/master.svg)](https://travis-ci.org/Cimpress-MCP/PostalCodes.Net)
[![Build status](https://img.shields.io/appveyor/ci/rnowosielski/postalcodes-net-u1ews/master.svg)](https://ci.appveyor.com/project/rnowosielski/postalcodes-net-u1ews)
[![Coverage Status](https://coveralls.io/repos/Cimpress-MCP/PostalCodes.Net/badge.svg?branch=master)](https://coveralls.io/r/Cimpress-MCP/PostalCodes.Net?branch=master)
[![Nuget Package](https://img.shields.io/nuget/dt/PostalCodes.svg)](http://www.nuget.org/packages/PostalCodes/)
[![Nuget Package](https://img.shields.io/nuget/v/PostalCodes.svg)](http://www.nuget.org/packages/PostalCodes/)
[![PostalCodes Release](https://img.shields.io/github/release/Cimpress-MCP/PostalCodes.Net.svg)](https://github.com/Cimpress-MCP/PostalCodes.Net/releases)


# PostalCodes.Net
Library for managing postal codes for .NET

## Branching model

This package follows the git-flow branching model:
* Develop features of a ```feature/*``` branch.
* Create pull request to a ```master``` branch.
* Once ```master``` is branched into ```release/<major>.<minor>.<patch>``` an automatic job builds and if successful publishes the NuGet package.
* After successful build the [artifact](https://www.nuget.org/packages/PostalCodes/) will be available via NuGet or directly from [GitHub Releases](https://github.com/Cimpress-MCP/PostalCodes.Net/releases).

## Prerequisites ##

If you want to open the solution file in Visual Studio, you may need the Sandcastle Help File Builder Visual Studio extension available in the bundle [here](https://github.com/EWSoftware/SHFB/releases).

## Building ##

The following snippets assume your working directory is in the root of the repository:

```
> git clone https://github.com/Cimpress-MCP/PostalCodes.Net.git
> cd PostalCodes.Net
```

### On Windows

In order to build the project you can use plain msbuild:

```
> nuget restore
> msbuild
```

### On Mac

```
> nuget restore
> xbuild
```

### On Linux
Install the nuget and mono-xbuild packages from [Mono Project package repository](http://www.mono-project.com/docs/getting-started/install/linux/). 
```
> nuget restore
> xbuild
```

##Reading the docs

### On Windows

[Microsoft Compiled HTML Help](http://en.wikipedia.org/wiki/Microsoft_Compiled_HTML_Help) is supported by default on Windows.

### On Mac

One alternative to read [Microsoft Compiled HTML Help](http://en.wikipedia.org/wiki/Microsoft_Compiled_HTML_Help) on a Mac is [iChm](https://code.google.com/p/ichm/) or [Chmox](http://chmox.sourceforge.net).

### On Linux

A popular cross-platform alternative to read [Microsoft Compiled HTML Help](http://en.wikipedia.org/wiki/Microsoft_Compiled_HTML_Help) is [xCHM](http://xchm.sourceforge.net/).

##Usages

### Creating postal code objects

```
var country = CountryFactory.Instance.CreateCountry("PL");
var postalCode = PostalCodeFactory.Instance.CreatePostalCode(
	country, "44-100");
```

in case of invalid postal code you will get an ```ArgumentException``` with the proper message explaining the reason for the failure.

