using System;
using System.IO;
using System.Linq;

var data = await File.ReadAllLinesAsync("data.txt");
var earliestDepart = Convert.ToInt32(data[0]);
var schedules = data[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Where(s => s != "x")
                    .Select(s => Convert.ToInt32(s));

var closestTime = earliestDepart + schedules.Max();
var closestBus = 0;

foreach (var schedule in schedules)
{
    for (var time = earliestDepart; time < closestTime; time++)
    {
        var onSchedule = time % schedule;
        if (onSchedule == 0)
        {
            closestTime = time;
            closestBus = schedule;
            break;
        }
    }
}

Console.WriteLine($"Closest Bus {closestBus} will be there at {closestTime}");
Console.WriteLine($"Answer {(closestTime - earliestDepart) * closestBus}");

