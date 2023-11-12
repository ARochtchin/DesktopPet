using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnieThePooh
{
    class Move : IScenario
    {
        string title = "Move";
        public Move(bool forewardHoney, bool backwardHoney)
        {
            _gifForward = forewardHoney ? "avares://WinnieThePooh/Images/move_honey.gif" : "avares://WinnieThePooh/Images/move.gif";
            _gifBack = forewardHoney ? "avares://WinnieThePooh/Images/move_honey_back.gif" : "avares://WinnieThePooh/Images/move_back.gif";

            if (forewardHoney && backwardHoney) title += " with honey";
            else if (forewardHoney) title += " eating honey";
            else if (backwardHoney) title += " for honey";

            TimerInterval = 50;
        }

        string _gifForward;
        string _gifBack;
        string _gifPause = "avares://WinnieThePooh/Images/pause.gif";
        bool _pause = false;

        bool forwardDirection = true;

        public string Title => title;

        public string Gif
        {
            get
            {
                if (_pause) { return _gifPause; }
                else
                {
                    return forwardDirection ? _gifForward : _gifBack;
                }
            }
        }
        public int TimerInterval { get; }

        public InitResult Initialize(int2 pos, int2 screenSize)
        {
            forwardDirection = true;
            return new InitResult(new int2(-100, screenSize.y - 200), new int2(100, 100));
        }
        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (!_pause)
            {
                if (forwardDirection)
                {
                    if (pos.x < screen.x)
                    {
                        return new MoveResult(pos.x + 5, pos.y);
                    }
                    else
                    {
                        forwardDirection = false;
                        return new MoveResult(pos).Refresh();
                    }
                }
                else
                {
                    if (pos.x > -100)
                    {
                        return new MoveResult(pos.x - 5, pos.y);
                    }
                    else
                    {
                        forwardDirection = true;
                        return new MoveResult(pos).Finish();
                    }
                }
            }
            else
            {
                return new MoveResult(pos);
            }

        }
        public void OnPause(bool pause)
        {
            _pause = pause;
        }
    }
}
