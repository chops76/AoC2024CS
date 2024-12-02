using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.AccessControl;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly List<List<int>> _input;

    public Day02()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

    private List<List<int>> ParseInput()
    {
        return File.ReadAllLines(InputFilePath)
                .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(int.Parse)
                                        .ToList())
                .ToList();
    }

    private bool Safe(List<int> report) {
        var diffs = report
            .Zip(report.Skip(1), (a, b) => b - a)
            .ToList();

        return diffs.All(diff => diff >= 1 && diff <= 3) || diffs.All(diff => diff <= -1 && diff >= -3);
    }

    private bool DampenedSafe(List<int> report) {
        for (int i = 0; i < report.Count; ++i) {
            List<int> rpt = new List<int>(report);
            rpt.RemoveAt(i);
            var diffs = rpt
                .Zip(rpt.Skip(1), (a, b) => b - a)
                .ToList();
            if(diffs.All(diff => diff >= 1 && diff <= 3) || diffs.All(diff => diff <= -1 && diff >= -3)) {
                return true;
            }
        }

        return false;
    }

    public int Part_1() {
        return _input.Count(report => Safe(report));
    }

    public int Part_2() {
        return _input.Count(report => DampenedSafe(report));
    }
}