using System;
using System.IO;
using System.Linq;

var data = await File.ReadAllLinesAsync("data.txt");
var index = 0;
var schedules = data[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(s =>
                    {
                        Bus result;
                        if (s == "x")
                        {
                            result = new Bus(-1, index);
                        }
                        else
                        {
                            result = new Bus(Convert.ToInt32(s), index);
                        }

                        index++;
                        return result;
                    })
                    .Where(b => b.BusID != -1)
                    .ToArray();

var minute = 0L;
var busOne = schedules[0];
long step = busOne.BusID;
while (true)
{
    if (step < 0)
    {
        throw new Exception("Oh no");
    }

    Console.Write($"Testing ${minute}");
    if (Success(minute, 1))
    {
        Console.WriteLine($"Found answer {minute}");
        break;
    }

    minute += step;
}

//https://stackoverflow.com/a/20824923/53236
static long GCF(long a, long b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

static long LCM(long a, long b) => (a / GCF(a, b)) * b;

bool Success(long offSet, int depth)
{
    if (depth >= schedules.Length)
    {
        Console.WriteLine("| No futher busses to test :) ");
        return true;
    }

    var bus = schedules[depth];
    var test = offSet + bus.MinuteOffset;
    Console.Write($" | Testing Bus ${bus.BusID} @ {test}");

    if (test % bus.BusID == 0)
    {
        step = LCM(step, bus.BusID);
        return Success(offSet, depth + 1);
    }

    Console.WriteLine(" | Failed");
    return false;
}

public record Bus(int BusID, int MinuteOffset);
