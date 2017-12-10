using System;
using System.Collections.Generic;
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
        #region Global variables
        Game game;
        #endregion

        public MainPage(){
            this.InitializeComponent();

            createGUI();
        }

        private void createGUI() {
            game = new Game();
            
            drawAllyGrid();
            drawInventoryStack();
            drawEnemyGrid();
        }

        private void drawEnemyGrid() {
            enemyGrid = mapGrid();
            drawShips(game.enemyShips, enemyGrid);
        }

        private void drawShips(List<Ship> enemyShips, Grid enemyGrid) {
            foreach(Ship ship in enemyShips){
                Position pos = new Position(ship.head);
                // marks grid cells wich is acupied by ship
                for(int i = 0; i < ship.size; i++) {
                    Border b = new Border();
                    b.Background = new SolidColorBrush(Colors.Gray);
                    b.SetValue(Grid.RowProperty, pos.X);
                    b.SetValue(Grid.ColumnProperty, pos.Y);
                    enemyGrid.Children.Add(b);
                    pos.offset(ship.direction);
                }
            }// foreach enemShips
        }

        private void drawAllyGrid() {
            alliesGrid = mapGrid();
            drawShips(game.allyShips, alliesGrid);
        }

        private void drawInventoryStack() {
            
            StackPanel stack = new StackPanel();
            stack.VerticalAlignment = VerticalAlignment.Bottom;

            foreach( Ship ship in Game.ships) {
                StackPanel shipPane = new StackPanel();
                shipPane.HorizontalAlignment = HorizontalAlignment.Center;
                
                for(int i = 0; i < ship.size; i++) {
                    shipPane.getChildren().add( new StackPanel(50,50));
                }
                stack.getChildren().add(shipPane);
            }
        }

        // Position(x,y)
        // atack(x,y) - Game.isShiphere(Positioon)


        /* menu:
         *      new game
         *      
         */

    }
}
