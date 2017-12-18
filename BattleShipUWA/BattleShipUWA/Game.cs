using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BattleShipUWA {
    class Game {
        public const int LIMIT = 10;
        private const string ALLY_SHIPS = "allyShips";
        private const string ALLY_SHOOTS = "allyShoots";
        private const string ENEMY_SHIPS = "enemyShips";
        private const string ENEMY_SHOOTS = "eemyShoots";


         public List<BitArray>  enemyShootRecord = new List<BitArray>(LIMIT) {
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT)
        };

        public List<BitArray>  allyShootRecord = new List<BitArray>(LIMIT) {
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT),
            new BitArray(LIMIT), new BitArray(LIMIT)
        };

        public static List<Ship> ships = new List<Ship> {
            new Ship(4),
            new Ship(3), new Ship(3),
            new Ship(2), new Ship(2), new Ship(2),
            new Ship(1), new Ship(1), new Ship(1), new Ship(1)
        };
        
        public bool isEnemyMove = false;
        static Random rnd = new Random(Guid.NewGuid().GetHashCode()); // https://stackoverflow.com/a/2706537; https://stackoverflow.com/a/18267477
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
                if( ship.isShip(pos) ){
                    if( Util.DEBUG ){ Debug.WriteLine("Game.isShipHere --> isShip "+ pos); }
                    return true; }
                if( offLimits(pos) ) {
                    if( Util.DEBUG ){ Debug.WriteLine("Game.isShipHere --> offLimits "+ pos); }
                    return true; }
            }
            return false;
        }

        public static int getHitsToWin() {
            int hits = 0;
            foreach( Ship ship in ships ){
                hits += ship.size; }
            return hits;
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

                do { // creates ship positions and checks them
                    Ok = true; //<------------- fix, was missing reset
                    do { head = Position.getRandom(LIMIT); }
                    while (isShipHere(head, shipYard));
                    
                    if( ship.size == 1 ){
                        if( isShipHere(head, shipYard) ){ continue; }
                        break;
                    }

                    //int value = rnd.Next(3) < 2 ? -1 : 1;  
                    int n = rnd.Next(2);
                    direction = new int[] {0, 0};
                    direction[n] = 1; 

                    pos = new Position(head);
                    for (int i = 0; i < ship.size; i++) {
                        pos.offset(direction);
                        if ( isShipHere(pos, shipYard) || offLimits(pos) ) {
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

        bool offLimits(Position pos) {
            return !inLimits(pos.X) || !inLimits(pos.Y);
        }

        public Position enemyTurn() {
            return Position.getRandom(LIMIT);
        }

        internal void markShooPosition(Position pos) {
            enemyShootRecord[pos.X].Set(pos.Y, true);
        }

        internal bool isShootWasDone(Position pos) {
            return enemyShootRecord[pos.X].Get(pos.Y);
        }

        public void saveGame() {
            saveGameDetails();
            saveShips(allyShips, ALLY_SHIPS);
            saveShips(enemyShips, ENEMY_SHIPS);
            saveShoots(enemyShootRecord, ENEMY_SHOOTS);
            saveShoots(allyShootRecord, ALLY_SHOOTS);
        }

        public Game loadGame() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            int limit;
            bool isEmemyMove, failLoading = false;
            string enemyShips = "", allyShips = "", enemyShootRecord = "", allyShootRecord = "";
            try {
                limit =  (int)localSettings.Values["limit"];
                isEmemyMove = ((int)localSettings.Values["isEnemyMove"] == 1);
                enemyShips = localSettings.Values[ENEMY_SHIPS].ToString();
                allyShips = localSettings.Values[ALLY_SHIPS].ToString();
                enemyShootRecord = localSettings.Values[ENEMY_SHOOTS].ToString();
                allyShootRecord = localSettings.Values[ALLY_SHOOTS].ToString();
            } catch (Exception ex) {
                failLoading = true;
                Debug.WriteLine("HERE: Game.loadGame() exception");
            }
            
            if(failLoading) { return null; }

            Game game = new Game();
            game.isEnemyMove = isEnemyMove;
            game.enemyShips = loadShips(enemyShips);
            game.allyShips = loadShips(allyShips);

            return null;
        }

        private List<Ship> loadShips(string shipsData) {
            Debug.WriteLine(shipsData);
            return null;
        }

        private void saveGameDetails() {
            ApplicationData.Current.LocalSettings.Values["limit"] = LIMIT;
            ApplicationData.Current.LocalSettings.Values["isEnemyMove"] = isEnemyMove ? 1 : 0;
        }

        private void saveShoots(List<BitArray> shootRecord, string name) {
            string s = "";
            foreach(BitArray ba in shootRecord) {
                s += ba.ToString();
            }
            ApplicationData.Current.LocalSettings.Values[name] = s;
        }

        private void saveShips(List<Ship> allyShips, string name){
            string s = "";
            foreach(Ship ship in allyShips){ s += ship.ToString() +"\n"; }
            ApplicationData.Current.LocalSettings.Values[name] = s;
            //ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            //localSettings.Values[name] = s;
        }


    }
}
