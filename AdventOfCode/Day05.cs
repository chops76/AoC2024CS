using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly (Dictionary<int, List<int>>, Dictionary<int, List<int>>, List<List<int>>) _input;

    public Day05()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

    private (Dictionary<int, List<int>>, Dictionary<int, List<int>>, List<List<int>>) ParseInput()
    {
        var input = File.ReadAllText(InputFilePath);
        var spl = input.Split(new string[] { "\n\n" }, StringSplitOptions.None);

        var hm1 = new Dictionary<int, List<int>>();
        var hm2 = new Dictionary<int, List<int>>();

        foreach (var line in spl[0].Split('\n'))
        {
            var ds = line.Split('|').Select(s => s.Trim()).ToArray();
            if (int.TryParse(ds[0], out int lnum) && int.TryParse(ds[1], out int rnum))
            {
                if (!hm1.ContainsKey(lnum))
                {
                    hm1[lnum] = new List<int>();
                }
                hm1[lnum].Add(rnum);

                if (!hm2.ContainsKey(rnum))
                {
                    hm2[rnum] = new List<int>();
                }
                hm2[rnum].Add(lnum);
            }
        }

        var lines = spl[1].Split('\n')
                           .Select(s => s.Split(',')
                                        .Select(v => int.TryParse(v.Trim(), out int result) ? result : 0)
                                        .ToList())
                           .ToList();

        return (hm1, hm2, lines);
    }

    private bool CheckValid(List<int> line, Dictionary<int, List<int>> after, Dictionary<int, List<int>> before)
    {
        for (int i = 0; i < line.Count; i++)
        {
            var left = line.GetRange(0, i);
            var right = line.GetRange(i + 1, line.Count - (i + 1));

            var leftSet = new HashSet<int>(left);
            var rightSet = new HashSet<int>(right);

            if (after.ContainsKey(line[i]))
            {
                var afterSet = new HashSet<int>(after[line[i]]);
                if (leftSet.Intersect(afterSet).Any())
                {
                    return false;
                }
            }

            if (before.ContainsKey(line[i]))
            {
                var beforeSet = new HashSet<int>(before[line[i]]);
                if (rightSet.Intersect(beforeSet).Any())
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int Part_1() {
        var (after, before, lines) = _input;

        int sum = 0;
        foreach (var line in lines)
        {
            if (CheckValid(line, after, before))
            {
                sum += line[line.Count / 2];
            }
        }

        return sum;
    }
   

    public int Part_2() {
        var (after, before, lines) = _input;
        int sum = 0;

        foreach (var line in lines)
        {
            if (CheckValid(line, after, before))
            {
                continue;
            }

            var l = new List<int>(line);
            l.Sort((a, b) =>
            {
                if (after.ContainsKey(a) && after[a].Contains(b))
                {
                    return -1;
                }
                if (before.ContainsKey(a) && before[a].Contains(b))
                {
                    return 1;
                }
                return 0;
            });

            sum += l[l.Count / 2];
        }

        return sum;
    }
}