# SAttributes

This package is written for .net core 3.1. You can cancel the requirements you have written in your DTO objects at any time.
Firstly you must type this 

```c#
...
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.ConfigureSAttributes();
...
```

commands in the startup.cs file. You can start using it later.

You can download the package from this link : https://www.nuget.org/packages/SAttribute/

You can download the example project from this link : https://github.com/EmreSeverr/SAttributeExample
