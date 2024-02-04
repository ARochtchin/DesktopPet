using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class SatelliteSpace : IScenario
    {
        public string Title => "Satellite in space";

        public string Gif => "avares://Space/Images/satellite_solar_panels.gif";

        public int TimerInterval => 50;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            var gif_size = DesktopPet.Helpers.GetImageSize(Gif);
            return new InitResult(new int2(-gif_size.x, screenSize.y - gif_size.y/2), gif_size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (pos.x < screen.x)
            {
                return new MoveResult(pos.x + 7, pos.y - 2);
            }
            else
            {
                return new MoveResult(pos).Finish();
            }
        }

        public bool OnPause(bool pause)
        {
            return true;
        }
    }
}
