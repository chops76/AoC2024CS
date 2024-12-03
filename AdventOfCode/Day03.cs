using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly String _input;

    public Day03()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

    private String ParseInput()
    {
        return File.ReadAllText(InputFilePath);
    }

    public int Part_1() {
        string pattern = @"\.?mul\((\d+),(\d+)\)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(_input);
        int sum = 0;
        foreach (Match match in matches)
        {
            string left = match.Groups[1].Value;
            string right = match.Groups[2].Value;
            sum += int.Parse(left) * int.Parse(right);
        }
        return sum;
    }

    public int Part_2() {
        string pattern = @".?(?:mul\((\d+),(\d+)\))|(do\(\))|(don't\(\))";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(_input);
        int sum = 0;
        bool counting = true;
        foreach (Match match in matches)
        {
            if (match.Groups[3].Success) {
                counting = true;
            } else if (match.Groups[4].Success) {
                counting = false;
            } else if (counting) {
                string left = match.Groups[1].Value;
                string right = match.Groups[2].Value;
                sum += int.Parse(left) * int.Parse(right);
            }
        }
        return sum;
    }
}