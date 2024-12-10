using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class GameCycle : IGameplayModel
    {
        public event EventHandler<GameplayEventArgs> Updated = delegate { };

        

        private Vector2 position = new Vector2 (300, 300);

        public void MovePlayer(IGameplayModel.Direction direction)
        {
            switch (direction)
            {
                case IGameplayModel.Direction.up:
                    {
                        position += new Vector2(0, -1);
                        break;
                    }
                case IGameplayModel.Direction.down:
                    {
                        position += new Vector2(0, 1);
                        break;
                    }
                case IGameplayModel.Direction.left:
                    {
                        position += new Vector2(-1, 0);
                        break;
                    }
                case IGameplayModel.Direction.right:
                    {
                        position += new Vector2(1, 0);
                        break;
                    }
            }
        }
        public void Update()
        {
            Updated.Invoke(this, new GameplayEventArgs { PlayerPos = position });
        }
    }
}
