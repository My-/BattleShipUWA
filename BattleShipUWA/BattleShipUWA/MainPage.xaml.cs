using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BattleShipUWA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page{
        //#region Global variables
        Game game;
        Grid allyGrid = new Grid();
        Grid enemyGrid = new Grid();
        bool isEnemyTurn = false;
        int enemyShipsLeft = Game.getHitsToWin();
        int allyShipsLeft = Game.getHitsToWin();
        string allyMessage = "Your turn. Attack!!";
        string enemyMessage = "Prepare to die!!";
        //#endregion

        public MainPage(){
            this.InitializeComponent();

            game = new Game();
            createGUI();
            playGame();
        }

        private void createGUI() {
            drawGrid(allyGrid);
            addMiddlePart();
            drawGrid(enemyGrid);

            //drawShips(game.enemyShips, enemyGrid);
            drawShips(game.allyShips, allyGrid);
        }

        private void addMiddlePart(){
            StackPanel sp = new StackPanel() {
                Width = 150,
                Orientation = Orientation.Vertical
            };
            
            sp.Children.Add( new TextBlock(){
                Text = "Left: "+ enemyShipsLeft,
                Name = "shipsLeft"
            });

            sp.Children.Add( new TextBlock(){
                Text = allyMessage,
                Name = "Message"
            });

            mainSP.Children.Add( sp );
        }

        private void drawShips(List<Ship> enemyShips, Grid enemyGrid) {
            foreach(Ship ship in enemyShips){
                Position pos = new Position(ship.head);
                // marks grid cells wich is acupied by ship
                for(int i = 0; i < ship.size; i++) {
                    Border b = new Border() {
                        Background = new SolidColorBrush(Colors.Gray),
                        BorderThickness = new Thickness(1),
                        Child = new TextBlock() { Text = ""+ ship.size }
                    };
                    
                    b.SetValue(Grid.RowProperty, pos.X);
                    b.SetValue(Grid.ColumnProperty, pos.Y);
                    enemyGrid.Children.Add(b);
                    pos.offset(ship.direction);
                }
            }// foreach enemShips
        }

        private void drawGrid(Grid grid) {
            //grid = new Grid();
            grid.Width = 500;
            grid.Height = 500;
            for (int i = 0; i < 10; i++) {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int row = 0; row < 10; row++){
                for (int col = 0; col < 10; col++){
                    Border border = new Border(){
                        Height = 50,
                        Width = 50,
                        BorderThickness = new Thickness(5),
                        Background = new SolidColorBrush(Colors.Blue),
                        Name = "b_" + row + "_" + col
                    };
                    border.SetValue(Grid.RowProperty,row);
                    border.SetValue(Grid.ColumnProperty, col);
                    grid.Children.Add(border);
                    border.Tapped += Border_Tapped;
                }
            }
            mainSP.Children.Add(grid);            
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e) {
            if( isEnemyTurn ){ return; }

            String senderName = ((Border)sender).Name;
            int row = Convert.ToInt32(senderName.Substring(2, 1));
            int col = Convert.ToInt32(senderName.Substring(4, 1));
            Debug.WriteLine("Taped@("+ row +", "+ col +")");
            

            if( game.isEnemyHere(new Position(row, col)) ){
                ((Border)sender).Background = new SolidColorBrush(Colors.Red);
                if( --enemyShipsLeft < 1 ){ winner("ally"); }
                TextBlock tb = (TextBlock)FindName("shipsLeft");
                tb.Text = "Left: "+ enemyShipsLeft;
            }else {
                ((Border)sender).Background = new SolidColorBrush(Colors.Yellow);
            }       
                
        }

        private void winner(string winner) {
            TextBlock tb = (TextBlock)FindName("shipsLeft");

            switch(winner.ToLower()) {
                case "enemy":
                    tb.Text = "You lost";
                    Debug.WriteLine("You lost");
                    break;
                case "ally":
                    tb.Text = "You won!!";
                    Debug.WriteLine("You Won!");
                    break;
            }
            
            
        }

        private void playGame() {
            while(enemyShipsLeft > 0 && allyShipsLeft > 0) {
                
                if( isEnemyTurn ){
                    displayMesage(enemyMessage);
                    // add delay ~1s
                    enemyAccack();
                    isEnemyTurn = false; }
                else{
                    displayMesage(allyMessage);
                    isEnemyTurn = true; }
            }// while game is on

        }

        private void displayMesage(string message) {
            TextBlock tb = (TextBlock)FindName("Message");
            tb.Text = message;
        }

        private void enemyAccack() {
            Position pos;
            do {
                pos = Position.getRandom(Game.LIMIT);
            }while(isStupidMove(pos));
            game.markShooPosition(pos);

            Border b = (Border)FindName("b_"+ pos.X +"_"+ pos.Y);

            if( game.isShipHere(pos, game.enemyShips) ){
                allyShipsLeft--;
                b.Background = new SolidColorBrush(Colors.OrangeRed);
                if( allyShipsLeft < 1 ) { winner("enemy"); } }
            else{
                b.Background = new SolidColorBrush(Colors.WhiteSmoke); }           
            
        }

        private bool isStupidMove(Position pos) {
            return game.isShootWasDone(pos);
        }
    }
}
