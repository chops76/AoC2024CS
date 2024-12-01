using System.Globalization;
using System.Security.AccessControl;

namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly (List<int>, List<int>) _input;

    public Day01()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

private (List<int>, List<int>) ParseInput()
{
    var allLines = File.ReadAllLines(InputFilePath);
    
    List<int> left = new List<int>();
    List<int> right = new List<int>();

    foreach (var line in allLines)
    {
        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        left.Add(int.Parse(parts[0]));
        right.Add(int.Parse(parts[1]));
    }

    return (left, right);
}

    public int Part_1() {
        var sortedList1 = _input.Item1.OrderBy(x => x).ToList();
        var sortedList2 = _input.Item2.OrderBy(x => x).ToList();

        return sortedList1.Zip(sortedList2, (x, y) => Math.Abs(x - y)).Sum();;
    }

    public int Part_2() {
        var frequencyDict = _input.Item2
            .GroupBy(n => n)
            .ToDictionary(g => g.Key, g => g.Count());
        return _input.Item1.Select(x => x * frequencyDict.GetValueOrDefault(x, 0)).Sum();
    }
}