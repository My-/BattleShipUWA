﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BattleShipUWA {
    public sealed partial class MapGrid : UserControl {

        private Grid _grid;


        public MapGrid() {
            this.InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            // make grid 
            /*
             * Lines vizible
             * each cell blue collor
             * 
             */

        }

        // add events on tap

        // populate grid 
        // Game.creatEnemyShipYard / Game.createAllyShipYard
        // 
        // extra: allow modify ship plasement
    }
}
