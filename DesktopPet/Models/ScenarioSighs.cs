using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{

    class ScenarioSighs : IScenario
    {
        bool _left;
        public ScenarioSighs(bool left)
        {
            _left = left;
            if (left)
                Gif = "avares://DesktopPet/Images/Vinni_sighs_left.gif";
            else
                Gif = "avares://DesktopPet/Images/Vinni_sighs_right.gif";
        }

        public string Title => "Sighs";

        public string Gif { get; private set; }

        public int TimerInterval => 6000;

        public MoveResult InitMove(int2 pos, int2 screen)
        {
            var initPos = _left ? new int2 { x = 0, y = screen.y - 150 } : new int2 { x = screen.x - 200, y = screen.y - 150 };
            return new MoveResult(initPos).Resize(new int2(100, 100));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            return new MoveResult(pos).Finish();
        }

        public void OnPause(bool pause)
        {
        }
    }
}
