using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {    
    // https://stackoverflow.com/a/36780497
    public struct Position {
        public Position(int x, string y) {
            X = x;
            Y = y;
        }

        public int X { get; }
        public string Y { get; }
    }
}
