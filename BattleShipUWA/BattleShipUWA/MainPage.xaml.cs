﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
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
        Grid allyGrid = new Grid(){ Name = "ally"};
        Grid enemyGrid = new Grid(){ Name = "enemy"};
        bool isEnemyTurn;
        int enemyShipsLeft = Game.getHitsToWin();
        int allyShipsLeft = Game.getHitsToWin();
        string allyMessage = "Your turn. Attack!!";
        string enemyMessage = "Prepare to die!!";

        Uri shootSound = new Uri("ms-appx:///Assets/sounds/Blast-SoundBible.com-2068539061.wav");
        Uri explosionSound = new Uri("ms-appx:///Assets/sounds/Explosion-SoundBible.com-2019248186.wav");
        //#endregion

        public MainPage(){
            this.InitializeComponent();

            game = new Game();
            isEnemyTurn = game.isEnemyMove;
            createGUI();
            if( isEnemyTurn ) { enemyAttack(); }
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
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center

            };
            sp.Children.Add(new Border() { Height = 50 });
            sp.Children.Add( new TextBlock(){
                Text = allyShipsLeft +" : "+ enemyShipsLeft,
                Name = "shipsLeft"
            });

            sp.Children.Add(new Border() { Height = 50 });

            sp.Children.Add( new TextBlock(){
                Text = allyMessage,
                Name = "Message"
            });

            mainSP.Children.Add( sp );
        }

        private void drawShips(List<Ship> shipYard, Grid grid) {
            foreach(Ship ship in shipYard){
                Position pos = new Position(ship.head);
                // marks grid cells wich is acupied by ship
                for(int i = 0; i < ship.size; i++) {
                    Border b = (Border)FindName(grid.Name +","+ pos.X +","+ pos.Y);
                    b.Background = new SolidColorBrush(Colors.Gray);
                    b.BorderThickness = new Thickness(1);
                    b.Child = new TextBlock() { Text = ""+ ship.size };
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
                        Name = grid.Name +"," + row + "," + col
                    };

                    border.SetValue(Grid.RowProperty,row);
                    border.SetValue(Grid.ColumnProperty, col);
                    grid.Children.Add(border);
                    if( grid.Name.Equals("enemy") ){ border.Tapped += Border_Tapped; }                    
                }
            }
            mainSP.Children.Add(grid);            
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

        //private async void playGame() {
        //    while(enemyShipsLeft > 0 && allyShipsLeft > 0) {
                
        //        if( isEnemyTurn ){
        //            displayMesage(enemyMessage);
        //            // add delay ~1s
        //            enemyAttack();
        //            isEnemyTurn = false; }
        //        else{
        //            displayMesage(allyMessage);
        //            await allyAttack();
        //            isEnemyTurn = true; }
        //    }// while game is on

        //}

        private async Task allyAttack() {
            
            //throw new NotImplementedException();
        }

        private void displayMesage(string message) {
            TextBlock tb = (TextBlock)FindName("Message");
            tb.Text = message;
        }


         private void Border_Tapped(object sender, TappedRoutedEventArgs e) {
            if( isEnemyTurn ){ return; }
            Util.playSound(shootSound);
            

            String senderName = ((Border)sender).Name;
            //int row = Convert.ToInt32(senderName.Substring(2, 1));
            //int col = Convert.ToInt32(senderName.Substring(4, 1));

            List<string> parts = senderName.Split(',').ToList<string>();
            int row = Convert.ToInt32(parts[1]);
            int col = Convert.ToInt32(parts[2]);

            Debug.WriteLine("Taped@("+ row +", "+ col +")");  
            game.markShooPosition(new BattleShipUWA.Position(row, col), false);          

            if( game.isEnemyHere(new Position(row, col)) ){                
                ((Border)sender).Background = new SolidColorBrush(Colors.Red);
                if( --enemyShipsLeft < 1 ){ winner("ally"); }
                TextBlock tb = (TextBlock)FindName("shipsLeft");
                tb.Text = allyShipsLeft +" : "+ enemyShipsLeft;
                Util.playSound(explosionSound);
            }else {
                ((Border)sender).Background = new SolidColorBrush(Colors.Yellow);
            } 

            //Task.Delay(1000).ContinueWith(t=> enemyAccack()); // https://stackoverflow.com/a/34458726/5322506
            enemyAttack();
            game.saveGame();      
                
        }

        private void enemyAttack() {
            Position pos;

            do { pos = Position.getRandom(Game.LIMIT); }
            while(game.isShootWasDone(pos));

            Util.playSound(shootSound);
            game.markShooPosition(pos, true);
            Border b = (Border)FindName("ally,"+ pos.X +","+ pos.Y);

            if( game.isShipHere(pos, game.allyShips) ){
                allyShipsLeft--;
                b.Background = new SolidColorBrush(Colors.OrangeRed);
                if( allyShipsLeft < 1 ) { winner("enemy"); }
                Util.playSound(explosionSound);
                TextBlock tb = (TextBlock)FindName("shipsLeft");
                tb.Text = allyShipsLeft +" : "+ enemyShipsLeft;}                
            else{
                b.Background = new SolidColorBrush(Colors.WhiteSmoke); }           
            
        }

        
    }
}
