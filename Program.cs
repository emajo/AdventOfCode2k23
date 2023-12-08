﻿Console.WriteLine("Insert the day bro... ");
var day = Console.ReadLine();

switch (day)
{
    case "1":
        Console.WriteLine(Day1.Part(true));
        Console.WriteLine(Day1.Part(false));
        break;
    case "2":
        Console.WriteLine(Day2.Part(true));
        Console.WriteLine(Day2.Part(false));
        break;
    case "3":
        Console.WriteLine(Day3.Part(true));
        Console.WriteLine(Day3.Part(false));
        break;
    case "4":
        Console.WriteLine(Day4.PartOne());
        Console.WriteLine(Day4.PartTwo());
        break;
    case "5":
        Console.WriteLine(Day5.PartOne());
        Console.WriteLine(Day5.PartTwo());
        break;
    case "6":
        Console.WriteLine(Day6.Part(true));
        Console.WriteLine(Day6.Part(false));
        break;
    case "7":
        Console.WriteLine(Day7.Part(true));
        Console.WriteLine(Day7.Part(false));
        break;
    case "8":
        Console.WriteLine(Day8.Part(true));
        Console.WriteLine(Day8.Part(false));
        break;
    default:
        Console.WriteLine("Default");
        break;
}