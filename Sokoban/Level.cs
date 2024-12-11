using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Level
    {
        public static int[,] LevelMap {  get; set; }

        public static Texture2D WallTexture { get; set; }

        public static Texture2D FloorTexture { get; set; }

        public static Texture2D BoxTexture { get; set; }

        public static Texture2D SpotTexture { get; set; }

        public static Texture2D BoxOnSpot {  get; set; }

        private static string levelPath;

        private static List<Vector2> Boxes = new List<Vector2>();

        private static List<Vector2> Spots = new List<Vector2>();

        public Level()
        {
            LevelMap = new int[GameConstants.LEVEL_WIDTH, GameConstants.LEVEL_HEIGHT];
            for (int i = 0; i < GameConstants.LEVEL_WIDTH; i++)
            {
                for(int j = 0; j < GameConstants.LEVEL_HEIGHT; j++)
                {
                    if (i == 0 || j == 0 || i == GameConstants.LEVEL_WIDTH - 1 || j == GameConstants.LEVEL_HEIGHT - 1)
                    {
                        LevelMap[i, j] = GameConstants.WALL;
                    }
                }
            }
            LevelMap[10, 10] = GameConstants.PLAYER;
            Player.SetPosition(new Vector2(10 * GameConstants.TILE_SIZE_WIDTH, 10 * GameConstants.TILE_SIZE_HEIGHT));
            LevelMap[5, 5] = GameConstants.BOX;
            Boxes.Add(new Vector2(5, 5));

            LevelMap[6, 6] = GameConstants.SPOT;
            Spots.Add(new Vector2(6, 6));
        }

        public Level(string _levelPath)
        {
            LevelMap = new int[GameConstants.LEVEL_WIDTH, GameConstants.LEVEL_HEIGHT];
            InitLevelPath(_levelPath);
        }

        public static void InitLevelPath(string _levelPath)
        {
            Boxes.Clear();
            Spots.Clear();
            levelPath = _levelPath;
            var level = File.ReadAllLines(levelPath);
            for (int i = 0; i < level.Length; i++)
            {
                for (int j = 0; j < level[i].Length; j++)
                {
                    FillLevelMap(level[j][i] - '0', i, j);
                }
            }
        }

        public static List<Vector2> GetSpots()
        {
            return Spots;
        }

        public static void AddBox(Vector2 position)
        {
            Boxes.Add(position);
        }

        public static void RemoveBox(Vector2 position)
        {
            Boxes.Remove(position);
        }

        public static void Update()
        {
            for (int i = 0; i < GameConstants.LEVEL_WIDTH; i++)
            {
                for (int j = 0; j < GameConstants.LEVEL_HEIGHT; j++)
                {
                    if (LevelMap[i, j] ==  GameConstants.WALL)
                    {
                        continue;
                    }

                    LevelMap[i, j] = GameConstants.FLOOR;

                    UpdateBoxes(i, j);

                    UpdateSpots(i, j);

                    if (i == Player.GetPossition().X / GameConstants.TILE_SIZE_WIDTH && j == Player.GetPossition().Y / GameConstants.TILE_SIZE_HEIGHT)
                    {
                        LevelMap[i, j] = GameConstants.PLAYER;
                    }
                }
            }
        }

        private static void FillLevelMap(int entity, int x, int y)
        {
            
            switch (entity)
            {
                case GameConstants.BOX:
                    LevelMap[x, y] = GameConstants.BOX;
                    Boxes.Add(new Vector2(x, y));
                    break;
                case GameConstants.SPOT:
                    LevelMap[x, y] = GameConstants.SPOT;
                    Spots.Add(new Vector2(x, y));
                    break;
                case GameConstants.PLAYER:
                    LevelMap[x, y] = GameConstants.PLAYER;
                    Player.SetPosition(new Vector2(x * GameConstants.TILE_SIZE_WIDTH, y * GameConstants.TILE_SIZE_HEIGHT));
                    break;
                case GameConstants.WALL:
                    LevelMap[x, y] = GameConstants.WALL;
                    break;
                case GameConstants.FLOOR:
                    LevelMap[x, y] = GameConstants.FLOOR;
                    break;
            }
        }

        private static void UpdateBoxes(int x, int y)
        {
            foreach (Vector2 box in Boxes)
            {
                if (x == box.X && y == box.Y)
                {
                    LevelMap[x, y] = GameConstants.BOX;
                }
            }
        }

        private static void UpdateSpots(int x, int y)
        {
            foreach (Vector2 spot in Spots)
            {
                if (LevelMap[x, y] == GameConstants.BOX && x == spot.X && y == spot.Y)
                {
                    LevelMap[x, y] = GameConstants.BOX_ON_SPOT;
                }

                else if (x == spot.X && y == spot.Y)
                {
                    LevelMap[x, y] = GameConstants.SPOT;
                }
            }
        }
    }
}
