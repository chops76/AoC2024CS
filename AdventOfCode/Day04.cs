using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly List<List<char>> _input;

    public Day04()
    {
        _input = ParseInput();
    }

    public override ValueTask<string> Solve_1() => new($"{Part_1()}");
    public override ValueTask<string> Solve_2() => new($"{Part_2()}");

private List<List<char>> ParseInput()
{
    return File.ReadAllLines(InputFilePath)
               .Select(line => line.ToList())
               .ToList();
}

    bool Check(List<List<char>> board, int x, int y, int xdelt, int ydelt) {
        if (x + xdelt * 3 < 0 || x + xdelt * 3 >= board[0].Count ||
            y + ydelt * 3 < 0 || y + ydelt * 3 >= board.Count) {
            return false;
        }
        return board[y][x] == 'X' && board[y + ydelt][x + xdelt] == 'M' &&
            board[y + ydelt * 2][x + xdelt * 2] == 'A' && board[y + ydelt * 3][x + xdelt * 3] == 'S';
    }

    public int Part_1() {
        int count = 0;
        for (int x = 0; x < _input[0].Count; ++x) {
            for (int y = 0; y < _input.Count; ++y) {
                for (int xdelt = -1; xdelt < 2; ++xdelt) {
                    for (int ydelt = -1; ydelt < 2; ++ydelt) {
                        if (Check(_input, x, y, xdelt, ydelt)) {
                            count += 1;
                        }
                    }
                }
            }
        }
        return count;
    }

    public int Part_2() {
        int count = 0;
        for (int x = 1; x < _input[0].Count - 1; ++x) {
            for (int y = 1; y < _input.Count - 1; ++y) {
                if (_input[y][x] == 'A' &&
                ((_input[y - 1][x - 1] == 'M' && _input[y + 1][x + 1] == 'S') ||
                 (_input[y - 1][x - 1] == 'S' && _input[y + 1][x + 1] == 'M')) &&
                ((_input[y - 1][x + 1] == 'M' && _input[y + 1][x - 1] == 'S') ||
                 (_input[y - 1][x + 1] == 'S' && _input[y + 1][x - 1] == 'M'))) {
                    count += 1;
                }
            }
        }
        return count;
    }
}