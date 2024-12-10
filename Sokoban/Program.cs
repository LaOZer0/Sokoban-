using Sokoban;
using System;

public static class Program
{
    [STAThread]
    static void Main()
    {
        GameplayPresenter presenter = new GameplayPresenter(new GameCycle(), new GameCycleView());
        presenter.LaunchGame();
    }
}