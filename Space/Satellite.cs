using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class Satellite : IScenario
    {
        public string Title => "Satellite";

        public string Gif => "avares://Space/Images/Satellite.gif";

        public int TimerInterval => 100;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            var gif_size = DesktopPet.Helpers.GetImageSize(Gif);
            return new InitResult(new int2(-gif_size.x, screenSize.y - 100 - gif_size.y), gif_size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (pos.x < screen.x)
            {
                return new MoveResult(pos.x + 5, pos.y);
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
