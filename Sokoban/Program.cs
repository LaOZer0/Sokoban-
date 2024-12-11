using Sokoban;
using System;

public static class Program
{
    [STAThread]
    static void Main()
    {
        GameController game = new GameController();
        game.Run();
    }
}