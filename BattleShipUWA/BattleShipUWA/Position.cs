using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {    
    // https://stackoverflow.com/a/36780497
    public struct Position {
        static Random rnd = new Random(Guid.NewGuid().GetHashCode()); // https://stackoverflow.com/a/2706537; https://stackoverflow.com/a/18267477

        private int x;
        private int y;


        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position(Position pos) {
            this.x = pos.X;
            this.y = pos.Y;
        }

        //// https://stackoverflow.com/a/15376927
        //public int X { get;} 
        //public int Y { get; } // OMfG.. c#...

        public int X {
            get {
                return x;
            }
            set {
                x = value;
            }
        }

        public int Y {
            get {
                return y;
            }
            set {
                y = value;
            }
        }

        public Position offset(int[] pos) {
            X += pos[0];
            Y += pos[1];
            return this;
        }

        public bool equals(Position pos) {
            return this.X == pos.X && this.Y == pos.Y;
        }

        public static Position getRandom(int limit) {
            //Random rnd = new Random(); // https://stackoverflow.com/a/2706537
            int x = rnd.Next(limit);
            int y = rnd.Next(limit);

            //Debug.WriteLine("Position.getrandom --> x: "+ x +", y: "+ y);

            return new Position(x, y);
        }


        public override string ToString(){
            return "Position{ x: "+ X +", y: "+ Y +"}";
        } 

    }

    
}
