Console.WriteLine("Insert the day bro... ");
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
    case "9":
        Console.WriteLine(Day9.Part(true));
        Console.WriteLine(Day9.Part(false));
        break;
    case "10":
        Console.WriteLine(Day10.Part(true));
        Console.WriteLine(Day10.Part(false));
        break;
    case "11":
        Console.WriteLine(Day11.Part(true));
        Console.WriteLine(Day11.Part(false));
        break;
    case "12":
        Console.WriteLine(Day12.Part(true));
        Console.WriteLine(Day12.Part(false));
        break;
    case "13":
        Console.WriteLine(Day13.Part(true));
        Console.WriteLine(Day13.Part(false));
        break;
    case "14":
        Console.WriteLine(Day14.Part(true));
        Console.WriteLine(Day14.Part(false));
        break;
    case "15":
        Console.WriteLine(Day15.Part(true));
        Console.WriteLine(Day15.Part(false));
        break;
    case "16":
        Console.WriteLine(Day16.Part(true));
        Console.WriteLine(Day16.Part(false));
        break;
    case "17":
        Console.WriteLine(Day17.Part(true));
        Console.WriteLine(Day17.Part(false));
        break;
    case "18":
        Console.WriteLine(Day18.Part(true));
        Console.WriteLine(Day18.Part(false));
        break;
    case "19":
        Console.WriteLine(Day19.Part(true));
        //Console.WriteLine(Day18.Part(false));
        break;
    default:
        Console.WriteLine("Default");
        break;
}