[![GitHub Last commit](https://img.shields.io/github/last-commit/89netraM/TimeEdit)](https://github.com/89netraM/TimeEdit/commits) [![GitHub License](https://img.shields.io/github/license/89netraM/TimeEdit)](./LICENSE) ![Platform](https://img.shields.io/badge/platform-windows%20%7C%20macos%20%7C%20linux-lightgrey) [![Nuget Version](https://img.shields.io/nuget/v/TimeEditApi)](https://www.nuget.org/packages/TimeEditApi) [![Nuget Downloads](https://img.shields.io/nuget/dt/TimeEditApi)](https://www.nuget.org/packages/TimeEditApi)

# TimeEdit

A simple .NET library for retrieving schedules from TimeEdit.

## Install

If you want to use this library, install the `TimeEditApi` package from NuGet.  
[https://www.nuget.org/packages/TimeEditApi](https://www.nuget.org/packages/TimeEditApi)

## Usage
```C#
using MoreTec.TimeEditApi;

TimeEdit timeEdit = new TimeEdit("https://cloud.timeedit.net/chalmers/web/public/");
IImmutableList<SearchItem> searchItems = await timeEdit.Search("TDA452", 1);
Schedule schedule = await timeEdit.GetSchedule(searchItems[0].Id);

Console.WriteLine("Next lecture in \"TDA452\" starts at: " + schedule.Entries[0].StartTime);
```
