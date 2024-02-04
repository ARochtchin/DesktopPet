using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class MoonSatellite : IScenario
    {
        //int counter = 0;
        int2 gif_size;
        public string Title => "Moon";

        public string Gif => "avares://Space/Images/moon_satellite.gif";

        public int TimerInterval => 5000;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            //counter = 0;
            gif_size = DesktopPet.Helpers.GetImageSize(Gif);
            return new InitResult(new int2(screenSize.x - gif_size.x, screenSize.y - gif_size.y - 50), gif_size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            return new MoveResult(pos).Finish();
        }

        public bool OnPause(bool pause)
        {
            return false;
        }
    }
}
