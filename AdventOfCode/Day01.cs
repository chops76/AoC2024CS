using System.Globalization;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly List<List<int>> _input;

    public Day01()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

    private List<List<int>> ParseInput()
    {
        var allLines = File.ReadAllText(InputFilePath);
        var result = allLines
                         .Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None)
                         .Select(block => block.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                                               .Select(int.Parse)
                                               .ToList())
                         .ToList();
        return result;
    }

    public int Part_1() {
        return _input.Max(sublist => sublist.Sum());
    }

    public int Part_2() {
        return _input.OrderByDescending(sublist => sublist.Sum())
               .Take(3)
               .Sum(sublist => sublist.Sum());
    }
}