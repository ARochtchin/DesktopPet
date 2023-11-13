using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnieThePooh
{
    class UpBush : IScenario
    {
        enum eUpBush
        {
            Up, Down, Bush
        }

        eUpBush _stage = eUpBush.Up;
        bool _pause= false;
        int _upCounter;
        int _bushCounter;
        string _gif_up = "avares://WinnieThePooh/Images/Winni_up.gif";
        string _gif_down = "avares://WinnieThePooh/Images/Winni_down.gif";
        string _gif_bush = "avares://WinnieThePooh/Images/Winni_bush.gif";
        string _gif_pause = "avares://WinnieThePooh/Images/pause.gif";
        public UpBush()
        {
            _gif = _gif_up;
        }

        public string Title => "Bush";

        string _gif;
        public string Gif { get => _pause ? _gif_pause: _gif; }

        public int TimerInterval => 50;

        public InitResult Initialize(int2 pos, int2 screen)
        {
            _upCounter = 0;
            _bushCounter = 0;
            _gif = _gif_up;
            _stage = eUpBush.Up;
            return new InitResult(new int2(screen.x - 100, screen.y), new int2(100, 100));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if(_pause ) { return new MoveResult(pos); }
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
                    _gif = _gif_down;
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
                    _gif = _gif_bush;
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

        public bool OnPause(bool pause)
        {
            _pause = pause;
            return true;
        }
    }
}
