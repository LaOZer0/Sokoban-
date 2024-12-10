using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;



public interface IGameplayModel
{
	event EventHandler<GameplayEventArgs> Updated;

	void Update();

	void MovePlayer(Direction direction);

	public enum Direction : byte
	{
		up,
		down,
		left,
		right
	}

}

public class GameplayEventArgs : EventArgs
{
	public Vector2 PlayerPos { get; set; }
}