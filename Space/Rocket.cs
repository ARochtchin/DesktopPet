using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class Rocket : IScenario
    {
        int2 gif_size;
        public string Title => "Rocket";

        public string Gif => "avares://Space/Images/Rocket.gif";

        public int TimerInterval => 50;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            gif_size = DesktopPet.Helpers.GetImageSize(Gif);
            return new InitResult(new int2(0, screenSize.y), gif_size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (pos.x < screen.x && pos.y>=-gif_size.y)
            {
                return new MoveResult(pos.x + 21, pos.y - 16);
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
