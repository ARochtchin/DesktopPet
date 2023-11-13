using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    internal class ScenarioBalloon : IScenario
    {
        enum eBalloonStage
        {
            Ground, Air
        }

        string _gif_ground = "avares://DesktopPet/Images/Vinni_balloon.gif";
        string _gif_air = "avares://DesktopPet/Images/Balloon.gif";
        int _ground_counter = 0;
        eBalloonStage _stage= eBalloonStage.Ground;

        public ScenarioBalloon()
        {
            Gif = _gif_ground;
        }

        public string Title => "Balloon";

        public string Gif { get; private set; }

        public int TimerInterval => 50;

        public InitResult Initialize(int2 pos, int2 screen)
        {
            _ground_counter = 0;
            return new InitResult(new int2(screen.x / 2, screen.y - 150), new int2(100,150));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (_stage == eBalloonStage.Ground)
            {
                if (++_ground_counter < 257)
                {
                    return new MoveResult(pos);
                }
                else
                {
                    _stage = eBalloonStage.Air;
                    Gif = _gif_air;
                    return new MoveResult(pos).Refresh();
                }
            }
            else
            {
                if (pos.y > -100)
                {
                    return new MoveResult(pos.x, pos.y - 5);
                }
                else
                {
                    return new MoveResult(pos).Finish();
                }
            }
        }

        public bool OnPause(bool pause)
        {
            return false;
        }
    }
}
