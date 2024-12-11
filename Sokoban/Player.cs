using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Player
    {
        public static Texture2D PlayerTexture { get; set; }
        private static Vector2 position;
         
        public static Vector2 GetPossition()
        {
            return position;
        }

        public static void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }

        public void Move(int direction)
        {
            switch (direction)
            {
                case GameConstants.UP when CanMoveInDirection(direction):
                    position.Y -= GameConstants.TILE_SIZE_HEIGHT;
                    break;

                case GameConstants.UP when IsBoxOnDirection(direction) && IsSpotOrFloorAfterBox(direction):
                    position.Y -= GameConstants.TILE_SIZE_HEIGHT;
                    Level.RemoveBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    Level.AddBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT - 1));
                    break;

                case GameConstants.DOWN when CanMoveInDirection(direction):
                    position.Y += GameConstants.TILE_SIZE_HEIGHT;
                    break;

                case GameConstants.DOWN when IsBoxOnDirection(direction) && IsSpotOrFloorAfterBox(direction):
                    position.Y += GameConstants.TILE_SIZE_HEIGHT;
                    Level.RemoveBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    Level.AddBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT + 1));
                    break;

                case GameConstants.LEFT when CanMoveInDirection(direction):
                    position.X -= GameConstants.TILE_SIZE_WIDTH;
                    break;

                case GameConstants.LEFT when IsBoxOnDirection(direction) && IsSpotOrFloorAfterBox(direction):
                    position.X -= GameConstants.TILE_SIZE_WIDTH;
                    Level.RemoveBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    Level.AddBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH - 1, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    break;

                case GameConstants.RIGHT when CanMoveInDirection(direction):                   
                    position.X += GameConstants.TILE_SIZE_WIDTH;
                    break;

                case GameConstants.RIGHT when IsBoxOnDirection(direction) && IsSpotOrFloorAfterBox(direction):
                    position.X += GameConstants.TILE_SIZE_WIDTH;
                    Level.RemoveBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    Level.AddBox(new Vector2(position.X / GameConstants.TILE_SIZE_WIDTH + 1, position.Y / GameConstants.TILE_SIZE_HEIGHT));
                    break;
            }
            
        }

        private bool IsSpotOrFloorAfterBox(int direction)
        {
            var spotOrFloorAfterBox = false;
            switch (direction)
            {
                case GameConstants.UP :
                    spotOrFloorAfterBox = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y - 2 * GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y - 2 * GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR;
                    
                    break;

                case GameConstants.DOWN:
                    spotOrFloorAfterBox = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y + 2 * GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y + 2 * GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR;
                                            
                    break;

                case GameConstants.LEFT:
                    spotOrFloorAfterBox = Level.LevelMap[(int)(position.X - 2 * GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                            (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT
                            || Level.LevelMap[(int)(position.X - 2 * GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                                (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR;
                    
                    break;

                case GameConstants.RIGHT:
                    spotOrFloorAfterBox = Level.LevelMap[(int)(position.X + 2 * GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                            (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT
                            || Level.LevelMap[(int)(position.X + 2 * GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                                (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR;
                    
                    break;
            }
            return spotOrFloorAfterBox;
        }

        private bool IsBoxOnDirection(int direction)
        {
            
            var boxOrBoxOnSpotWithDirection = false;
         
            switch (direction)
            {
                case GameConstants.UP:
                    boxOrBoxOnSpotWithDirection = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y - GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y - GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX_ON_SPOT;
                    break;

                case GameConstants.DOWN:
                    boxOrBoxOnSpotWithDirection = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y + GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y + GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX_ON_SPOT;
                    break;

                case GameConstants.LEFT:
                    boxOrBoxOnSpotWithDirection = Level.LevelMap[(int)(position.X - GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                        (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX
                            || Level.LevelMap[(int)(position.X - GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                            (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX_ON_SPOT;
                    break;

                case GameConstants.RIGHT:
                    boxOrBoxOnSpotWithDirection = Level.LevelMap[(int)(position.X + GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                         (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX
                             || Level.LevelMap[(int)(position.X + GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                             (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.BOX_ON_SPOT;
                    break;
            }
            return boxOrBoxOnSpotWithDirection;
        }

        private bool CanMoveInDirection(int direction)
        {
            var canMove = true;
            switch (direction)
            {
                case GameConstants.UP:
                    canMove = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y - GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y - GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT;
                    break;

                case GameConstants.DOWN:
                    canMove = Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                        (int)(position.Y + GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR
                        || Level.LevelMap[(int)position.X / GameConstants.TILE_SIZE_WIDTH,
                            (int)(position.Y + GameConstants.TILE_SIZE_HEIGHT) / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT;
                    break;

                case GameConstants.LEFT:
                    canMove = Level.LevelMap[(int)(position.X - GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                            (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR
                            || Level.LevelMap[(int)(position.X - GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                                (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT;
                    break;

                case GameConstants.RIGHT:
                    canMove = Level.LevelMap[(int)(position.X + GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                            (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.FLOOR
                            || Level.LevelMap[(int)(position.X + GameConstants.TILE_SIZE_WIDTH) / GameConstants.TILE_SIZE_WIDTH,
                                (int)position.Y / GameConstants.TILE_SIZE_HEIGHT] == GameConstants.SPOT;
                    break;
            }
            return canMove;
        }
    }
}
