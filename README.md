![Build Status](https://ci.appveyor.com/api/projects/status/3yy3dok9mnsld6d8/branch/develop)

# PostalCodes.Net
Library for managing postal codes for .NET

## Branching model

This package follows the git-flow branching model:
* Develop features of a ```feature/*``` branch.
* Create pull request to a ```master``` branch.
* Once ```master``` is branched into ```release/<major>.<minor>.<patch>``` an automatic job builds and if successful publishes the NuGet package.