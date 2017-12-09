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

        public Position(Position pos) {
            X = pos.X;
            Y = pos.Y;
        }

        // https://stackoverflow.com/a/15376927
        public int X { get { return X; } set {; } } 
        public int Y { get { return Y;  } set {; } } // OMfG.. c#...

        public Position offset(int[] pos) {
            this.X += pos[0];
            this.Y += pos[1];
            return this;
        }

        public bool equals(Position pos) {
            return this.X == pos.X && this.Y == pos.Y;
        }

        public static Position getRandom(int limit) {
            Random rnd = new Random(); // https://stackoverflow.com/a/2706537
            return new Position(rnd.Next(limit), rnd.Next(limit));
        }

    }

    
}
