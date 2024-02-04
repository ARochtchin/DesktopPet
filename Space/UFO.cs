using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class UFO : IScenario
    {
        int counter;
        public string Title => "UFO";

        public string Gif => "avares://Space/Images/UFO.gif";

        public int TimerInterval => 50;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            counter = 0;
            var gif_size = DesktopPet.Helpers.GetImageSize(Gif);
            return new InitResult(new int2(-gif_size.x, screenSize.y - 100 - gif_size.y), gif_size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (pos.x < screen.x)
            {
                return new MoveResult(pos.x + 10, pos.y - (int)(5 * Math.Sin(++counter * Math.PI / 90.0)));
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
