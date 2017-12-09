using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    class Game {
        const int LIMIT = 10;

        List<Ship> ships = new List<Ship> {
            new Ship(1), new Ship(1), new Ship(1), new Ship(1),
            new Ship(2), new Ship(2), new Ship(2),
            new Ship(3), new Ship(3),
            new Ship(4) }; 

        private List<Ship> allyShips;
        private List<Ship> enemyShips;       

        public Game() { }

        void addShip(Ship ship) {
            allyShips.Add(ship);
        }

        bool isShipHere(Position pos) {
            foreach( Ship ship in allyShips ){
                if( ship.isShip(pos) ){ return true; }
            }
            return false;
        }

        void createEnemy() {
            Random rnd = new Random(); // https://stackoverflow.com/a/2706537

            foreach ( Ship ship in ships ){
                ship.head = new Position(rnd.Next(LIMIT), rnd.Next(LIMIT));
            }
        }

        bool inLimits(int n) {
            return 0 <= n && n < LIMIT;
        }
    }
}
