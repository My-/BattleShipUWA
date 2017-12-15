using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    
    class Ship {  

        bool DEBUG = Util.DEBUG;

        public Ship(int size) {
            this.head = new Position(0, 0);
            this.direction = new int[]{0, 0};
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
            Position p = new Position(head);
            if( DEBUG ){ Debug.WriteLine("\tShip.isShip --> head "+ p); }
            if( head.equals(pos) ){ return true; }
            for( int i = 0; i < size -1; i++ ) {
                p.offset(direction);
                if( DEBUG ){ Debug.WriteLine("\tShip.isShip --> body "+ p); }
                if( p.equals(pos) ){ return true; } 
            }
            return false;
        }

        
        public override string ToString(){
            return "Ship{ head: "+ head +", direction: ("+ direction[0] +", "+ direction[1] +"), size: "+ size +"}";
        } 
    }
}
