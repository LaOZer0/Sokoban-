using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public interface IObject
    {
        Vector2 Pos { get; }
        void Update();
    }
}
