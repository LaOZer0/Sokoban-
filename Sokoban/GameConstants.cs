using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public static class GameConstants
    {
        public const int WINDOW_SIZE = 1000;
        public const int FLOOR = 0;
        public const int WALL = 1;
        public const int PLAYER = 2;
        public const int SPOT = 3;
        public const int BOX = 4;
        public const int BOX_ON_SPOT = 5;
        public const int LEVEL_WIDTH = 20;
        public const int LEVEL_HEIGHT = 20;
        public const int TILE_SIZE_HEIGHT = WINDOW_SIZE / LEVEL_HEIGHT;
        public const int TILE_SIZE_WIDTH = WINDOW_SIZE / LEVEL_WIDTH;
        public const int UP = 1;
        public const int DOWN = 2;
        public const int LEFT = 3;
        public const int RIGHT = 4;

    }
}
