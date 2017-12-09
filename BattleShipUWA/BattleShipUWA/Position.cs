using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {    
    // https://stackoverflow.com/a/36780497
    public struct Position {
        public Position(int x, int y) {
            X = x;
            Y = y;
        }

        // https://stackoverflow.com/a/15376927
        public int X { get { return X; } set {; } } 
        public int Y { get { return Y;  } set {; } } // OMfG.. c#...

        public Position add(Position pos) {
            this.X += pos.X;
            this.Y += pos.Y;
            return this;
        }

        public bool equals(Position pos) {
            return this.X == pos.X && this.Y == pos.Y;
        }

    }

    
}
