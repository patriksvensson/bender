# Bender

A tool that generates [Bicep](https://github.com/Azure/bicep) files 
from [Aspire](https://github.com/dotnet/aspire) manifests.

## Building

We're using [Cake](https://github.com/cake-build/cake) as a 
[dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) 
for building. So make sure that you've restored Cake by running 
the following in the repository root:

```
> dotnet tool restore
```

After that, running the build is as easy as writing:

```
> dotnet cake
```