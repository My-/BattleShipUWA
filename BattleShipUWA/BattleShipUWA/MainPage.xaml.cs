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
        Grid allyGrid;
        Grid enemyGrid;
        //#endregion

        public MainPage(){
            this.InitializeComponent();

            createGUI();
        }

        private void createGUI() {
            game = new Game();

            drawGrid(allyGrid);
            addMiddlePart();
            drawGrid(enemyGrid);
            //drawShips(game.enemyShips, enemyGrid);
        }

        private void addMiddlePart(){
            mainSP.Children.Add(new StackPanel() {
                Width = 100                
            });
        }

        private void drawShips(List<Ship> enemyShips, Grid enemyGrid) {
            foreach(Ship ship in enemyShips){
                Position pos = new Position(ship.head);
                // marks grid cells wich is acupied by ship
                for(int i = 0; i < ship.size -1; i++) {
                    Border b = new Border();
                    b.Background = new SolidColorBrush(Colors.Gray);
                    b.SetValue(Grid.RowProperty, pos.X);
                    b.SetValue(Grid.ColumnProperty, pos.Y);
                    enemyGrid.Children.Add(b);
                    pos.offset(ship.direction);
                }
            }// foreach enemShips
        }

        private void drawGrid(Grid grid) {
            grid = new Grid();
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
            String senderName = ((Border)sender).Name;
            int row = Convert.ToInt32(senderName.Substring(2, 1));
            int col = Convert.ToInt32(senderName.Substring(4, 1));
            Debug.WriteLine("Taped@("+ row +", "+ col +")");
            if( game.isEnemyHere(new Position(row, col)) ){
                ((Border)sender).Background = new SolidColorBrush(Colors.Red);
            }       
                
        }

        //private void drawInventoryStack() {
            
        //    StackPanel stack = new StackPanel();
        //    stack.VerticalAlignment = VerticalAlignment.Bottom;

        //    foreach( Ship ship in Game.ships) {
        //        StackPanel shipPane = new StackPanel();
        //        shipPane.HorizontalAlignment = HorizontalAlignment.Center;
                
        //        for(int i = 0; i < ship.size; i++) {
        //            shipPane.Children.add( new StackPanel(50,50));
        //        }
        //        stack.Children.add(shipPane);
        //    }
        //}

        // Position(x,y)
        // atack(x,y) - Game.isShiphere(Positioon)


        /* menu:
         *      new game
         *      
         */

    }
}
