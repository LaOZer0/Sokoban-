using Microsoft.Xna.Framework;
using System;

public interface IGameplayView
{
    event EventHandler CycleFinished;
    event EventHandler<ControlsEventArgs> PlayerMoved;

    void LoadGameCycleParametrs(Vector2 position);
}

public class ControlsEventArgs : EventArgs
{
    public IGameplayModel.Direction Direction { get; set;}
}