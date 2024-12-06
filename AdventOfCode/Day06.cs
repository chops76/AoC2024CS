using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.AccessControl;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly List<List<char>> _input;

    public Day06()
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

    public int Part_1() {
        var board = _input;
        int xPos = 0;
        int yPos = 0;
        int dir = 0;

        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[y].Count; x++)
            {
                if (board[y][x] == '^')
                {
                    xPos = x;
                    yPos = y;
                }
            }
        }

        var visited = new HashSet<(int, int)>();
        visited.Add((xPos, yPos));

        while (xPos > 0 && yPos > 0 && xPos < board[0].Count - 1 && yPos < board.Count - 1)
        {
            if (dir == 0)
            {
                if (board[yPos - 1][xPos] == '#')
                {
                    dir = 1;
                }
                else
                {
                    yPos -= 1;
                }
            }
            else if (dir == 1)
            {
                if (board[yPos][xPos + 1] == '#')
                {
                    dir = 2;
                }
                else
                {
                    xPos += 1;
                }
            }
            else if (dir == 2)
            {
                if (board[yPos + 1][xPos] == '#')
                {
                    dir = 3;
                }
                else
                {
                    yPos += 1;
                }
            }
            else
            {
                if (board[yPos][xPos - 1] == '#')
                {
                    dir = 0;
                }
                else
                {
                    xPos -= 1;
                }
            }

            visited.Add((xPos, yPos));
        }

        return visited.Count;
    }

    private static List<List<char>> CloneBoard(List<List<char>> board)
    {
        var newBoard = new List<List<char>>();
        foreach (var row in board)
        {
            newBoard.Add(new List<char>(row)); // Clone each row
        }
        return newBoard;
    }

    public static bool FindLoop(List<List<char>> board, int startX, int startY)
    {
        int xPos = startX;
        int yPos = startY;
        int dir = 0;

        var visited = new HashSet<(int, int, int)>();
        visited.Add((xPos, yPos, dir));

        while (xPos > 0 && yPos > 0 && xPos < board[0].Count - 1 && yPos < board.Count - 1)
        {
            if (dir == 0)
            {
                if (board[yPos - 1][xPos] == '#')
                {
                    dir = 1;
                }
                else
                {
                    yPos -= 1;
                }
            }
            else if (dir == 1)
            {
                if (board[yPos][xPos + 1] == '#')
                {
                    dir = 2;
                }
                else
                {
                    xPos += 1;
                }
            }
            else if (dir == 2)
            {
                if (board[yPos + 1][xPos] == '#')
                {
                    dir = 3;
                }
                else
                {
                    yPos += 1;
                }
            }
            else 
            {
                if (board[yPos][xPos - 1] == '#')
                {
                    dir = 0;
                }
                else
                {
                    xPos -= 1;
                }
            }

            if (visited.Contains((xPos, yPos, dir)))
            {
                return true;
            }

            visited.Add((xPos, yPos, dir));
        }

        return false;
    }

    public int Part_2() {
        int xStart = 0;
        int yStart = 0;
        var board = _input;

        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[y].Count; x++)
            {
                if (board[y][x] == '^')
                {
                    xStart = x;
                    yStart = y;
                }
            }
        }

        int found = 0;

        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[y].Count; x++)
            {
                if (board[y][x] == '.')
                {
                    var newBoard = CloneBoard(board);
                    newBoard[y][x] = '#';

                    if (FindLoop(newBoard, xStart, yStart))
                    {
                        found += 1;
                    }
                }
            }
        }

        return found;
    }
}