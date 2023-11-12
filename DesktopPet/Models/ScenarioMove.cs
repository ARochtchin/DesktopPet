using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    class ScenarioMove : IScenario
    {
        string title = "Move";
        public ScenarioMove(bool forewardHoney, bool backwardHoney)
        {
            _gifForward = forewardHoney? "avares://DesktopPet/Images/Vinni_move_honey.gif": "avares://DesktopPet/Images/Vinni_move.gif";
            _gifBack = forewardHoney ? "avares://DesktopPet/Images/Vinni_move_honey_back.gif" : "avares://DesktopPet/Images/Vinni_move_back.gif";

            if (forewardHoney && backwardHoney) title += " with honey";
            else if (forewardHoney) title += " eating honey";
            else if (backwardHoney) title += "for honey";

            TimerInterval = 50;
        }

        string _gifForward;
        string _gifBack;
        string _gifPause = "avares://DesktopPet/Images/Vinni_pause.gif";
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
            return new InitResult(new int2(-100, screenSize.y - 200), new int2(100, 100));
        }
        public MoveResult OnMove(int2 pos, int2 screen)
        {
            MoveResult res = new MoveResult(pos);// { FinishRequest = false, RefreshRequest = false };
            if (!_pause)
            {
                if (forwardDirection)
                {
                    if (pos.x < screen.x)
                    {
                        res.Move(pos.x + 5, pos.y );
                    }
                    else
                    {
                        forwardDirection = false;
                        res.Refresh();
                    }
                }
                else
                {
                    if (pos.x > -100)
                    {
                        res.Move(new int2(pos.x - 5, pos.y ));
                    }
                    else
                    {
                        forwardDirection = true;
                        res.Refresh().Finish();
                    }
                }
            }

            return res;
        }
        public void OnPause(bool pause)
        {
            _pause = pause;
        }
    }
}
