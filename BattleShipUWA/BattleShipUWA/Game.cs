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

        void createEnemyShipYard() {
            createShipYard(enemyShips);
        }

        void createAllyShipYard() {
            createShipYard(allyShips);
        }


        void createShipYard(List<Ship> shipYard) {
            Random rnd = new Random(); // https://stackoverflow.com/a/2706537

            foreach ( Ship ship in ships ){
                Position head, pos;
                bool Ok = true;
                int[] direction;

                do {
                    do { head = Position.getRandom(LIMIT); }
                    while (isShipHere(head));
                    
                    int n = rnd.Next(3) < 2 ? -1 : 1; // adds negative numbers to direction
                    do {  direction = new int[] { rnd.Next(2) * n, rnd.Next(2) * n }; }
                    while (direction[0] == direction[1]);

                    pos = new Position(head);
                    for (int i = 0; i < ship.size; i++) {
                        pos = pos.offset(direction);
                        if ( isShipHere(pos) ) {
                            Ok = false;
                            break;
                        }
                    }
                } while( !Ok );

                shipYard.Add( new Ship(head, direction, ship.size) );
                
            }// foreach ships
        }

        bool inLimits(int n) {
            return 0 <= n && n < LIMIT;
        }
    }
}
