﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    class Game {
        const int LIMIT = 10;
        static Random rnd = new Random(Guid.NewGuid().GetHashCode()); // https://stackoverflow.com/a/2706537; https://stackoverflow.com/a/18267477

        public static List<Ship> ships = new List<Ship> {
            new Ship(4),
            new Ship(3), new Ship(3),
            new Ship(2), new Ship(2), new Ship(2),
            new Ship(1), new Ship(1), new Ship(1), new Ship(1)
        }; 

        public List<Ship> allyShips = new List<Ship>();
        public List<Ship> enemyShips = new List<Ship>();       

        public Game() {
            createAllyShipYard();
            createEnemyShipYard();
        }

        void addShip(Ship ship) {
            allyShips.Add(ship);
        }

        public bool isShipHere(Position pos, List<Ship> shipYard) {
            foreach( Ship ship in shipYard){
                if( ship.isShip(pos) ){ return true; }
                //if( !inLimits(pos.X) || !inLimits(pos.Y) ) { return true; }
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
            //rnd = new Random(Guid.NewGuid().GetHashCode()); // https://stackoverflow.com/a/2706537
            foreach ( Ship ship in ships ){
                Position head, pos;
                bool Ok = true;
                int[] direction = new int[]{0, 0};

                do {
                    Ok = true; //<------------- fix, was missing reset
                    do { head = Position.getRandom(LIMIT); }
                    while (isShipHere(head, shipYard));
                    
                    if( ship.size == 1 ){
                        if( isShipHere(head, shipYard) ){ continue; }
                        break;
                    }

                    int value = rnd.Next(3) < 2 ? -1 : 1;  
                    int n = rnd.Next(2);
                    direction[n] = value; 

                    pos = new Position(head);
                    for (int i = 0; i < ship.size -1; i++) {
                        pos.offset(direction);
                        if ( isShipHere(pos, shipYard) ) {
                            Ok = false;
                            break;
                        }
                    }
                } while( !Ok );

                Ship sh = new Ship(head, direction, ship.size);

                //Debug.WriteLine("Game.createShipYard --> "+ ship);
                Debug.WriteLine("Game.createShipYard --> "+ sh);
                //Debug.WriteLine("");

                shipYard.Add( sh );
                
            }// foreach ships
        }

        public bool isEnemyHere(Position pos){
            return isShipHere(pos, enemyShips);
        }

        bool inLimits(int n) {
            return 0 <= n && n < LIMIT;
        }
    }
}
