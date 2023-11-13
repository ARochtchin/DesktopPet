using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    internal class Landing : IScenario
    {
        string _gif1 = "avares://Space/Images/Landing1.gif";
        string _gif2 = "avares://Space/Images/Landing2.gif";
        string _gif_pause = "avares://Space/Images/moon_satellite.gif";
        bool _pause;
        int2 size1, size2;
        int stage;

        public Landing()
        {
            size1 = DesktopPet.Helpers.GetImageSize(_gif1);
            size2 = DesktopPet.Helpers.GetImageSize(_gif2);
        }

        public string Title => "Landing";

        public string Gif { get; private set; }

        public int TimerInterval => 50;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            stage = 1;
            Gif = _gif1;
            return new InitResult(new int2(screenSize.x - size1.x, -size1.y), size1);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (!_pause)
            {
                if (stage == 1)
                {
                    if (pos.y < screen.y)
                    {
                        return new MoveResult(pos.x - 15, pos.y + 15);
                    }
                    else
                    {
                        stage = 2;
                        Gif = _gif2;
                        return new MoveResult(150, -size2.y).Refresh().Resize(size2);
                    }
                }
                else
                {
                    if (pos.y < screen.y)
                    {
                        return new MoveResult(pos.x - 2, pos.y + 5);
                    }
                    else
                    {
                        return new MoveResult(pos).Finish();
                    }
                }
            }
            else
            {
                return new MoveResult(pos);
            }
        }

        public bool OnPause(bool pause)
        {
            if (pause)
                Gif = _gif_pause;
            else
                Gif = stage==1? _gif1: _gif2;
            _pause = pause;
            return true;
        }
    }
}
