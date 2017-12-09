using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    
    class Ship {  
        public Ship(int size) {
            this.size = size;
        }

        public Ship(Position head, int[] direction, int size) {
            this.head = head;
            this.direction = direction;
            this.size = size;
        }

        public int[] direction { set; get; }
        public Position head { set; get; }
        public int size { get; }

        public bool isShip(Position pos) {
            for( int i = 0; i < size; i++ ) {
                if( head.offset(direction).equals(pos) ) {
                    return true;
                }
            }
            return false;
        }

        

    }
}
