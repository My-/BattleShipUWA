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

        public bool isShipHere(Position pos) {
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
            //rnd = new Random(Guid.NewGuid().GetHashCode()); // https://stackoverflow.com/a/2706537
            foreach ( Ship ship in ships ){
                Position head, pos;
                bool Ok = true;
                int[] direction = new int[]{0, 0};

                do {
                    do { head = Position.getRandom(LIMIT); }
                    while (isShipHere(head));
                    
                    if( ship.size == 1 ){
                        if( isShipHere(head) ){ continue; }
                        break;
                    }

                    int value = rnd.Next(3) < 2 ? -1 : 1;  
                    int n = rnd.Next(2);
                    direction[n] = value; 

                    pos = new Position(head);
                    for (int i = 0; i < ship.size; i++) {
                        pos = pos.offset(direction);
                        if ( isShipHere(pos) ) {
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

        bool inLimits(int n) {
            return 0 <= n && n < LIMIT;
        }
    }
}
