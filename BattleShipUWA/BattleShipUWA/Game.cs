using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    class Game {
        private List<Ship> ships;

        public Game() { }

        void addShip(Ship ship) {
            ships.Add(ship);
        }

        bool isShipHere(Position pos) {
            foreach( Ship ship in ships ){
                if( ship.isShip(pos) ){ return true; }
            }
            return false;
        }


    }
}
