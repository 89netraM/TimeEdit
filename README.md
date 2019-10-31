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