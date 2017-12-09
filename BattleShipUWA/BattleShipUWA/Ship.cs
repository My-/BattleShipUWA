using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipUWA {
    
    class Ship {
        Position head;
        Position direction;
        int size;

        public Ship(int size) {
            this.size = size;
        }

        public Ship(Position head, Position direction, int size) {
            this.head = head;
            this.direction = direction;
            this.size = size;
        }

        public void setDirection(Position pos) {
            this.direction = pos;
        }

        public void setHead(Position pos) {
            this.head = pos;
        }
    }
}
