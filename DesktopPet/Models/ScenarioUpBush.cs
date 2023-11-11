using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    class ScenarioUpBush : IScenario
    {
        enum eUpBush
        {
            Up, Down, Bush
        }

        eUpBush _stage = eUpBush.Up;
        int _upCounter;
        int _bushCounter;
        string _gif_up = "avares://DesktopPet/Images/Vinni_up.gif";
        string _gif_down = "avares://DesktopPet/Images/Vinni_down.gif";
        string _gif_bush = "avares://DesktopPet/Images/Vinni_bush.gif";
        public ScenarioUpBush()
        {
            Gif = _gif_up;
        }

        public string Title => "Bush";

        public string Gif { get; private set; }

        public int TimerInterval => 50;

        public MoveResult InitMove(int2 pos, int2 screen)
        {
            _upCounter = 0;
            _bushCounter = 0;
            Gif = _gif_up;
            _stage = eUpBush.Up;
            return new MoveResult(new int2(screen.x - 100, screen.y)).Resize(new int2(100, 100));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (_stage == eUpBush.Up)
            {
                if (pos.y > 0)
                {
                    int dy = (_upCounter++ % 5 == 0) ? 4 : 2;
                    return new MoveResult(pos.x, pos.y - dy);
                }
                else
                {
                    _stage = eUpBush.Down;
                    Gif = _gif_down;
                    return new MoveResult(pos).Refresh(); 
                }
            }
            else if (_stage == eUpBush.Down)
            {
                if (pos.y < screen.y - 150)
                    return new MoveResult(pos.x, pos.y + (int)(Math.Sqrt(pos.y)) + 2);
                else
                {
                    _stage = eUpBush.Bush;
                    Gif = _gif_bush;
                    return new MoveResult(screen.x - 100, screen.y - 100).Refresh();
                }
            }
            else
            {
                if (_bushCounter < 180)
                {
                    ++_bushCounter;
                    return new MoveResult(pos);
                }
                else
                {
                    return new MoveResult(pos).Finish();
                }
            }
        }

        public void OnPause(bool pause)
        {
        }
    }
}
