using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BattleShipUWA {
    class Util {

        public static bool DEBUG = false;


        public static void playSound(Uri soundUri) {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(soundUri);
            mediaPlayer.Play();
        }

        

    }
}
