using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    class ScenarioStay : IScenario
    {
        public string Title => "Stay";

        public string Gif => "avares://DesktopPet/Images/Vinni_pause.gif";

        public int TimerInterval => 10000;

        public MoveResult InitMove(int2 pos, int2 screen)
        {
            return new MoveResult(new int2(screen.x - 200, screen.y - 150)).Resize(new int2(100, 100));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            return new MoveResult { FinishRequest = true, WindowPos = pos };
        }

        public void OnPause(bool pause)
        {
        }
    }
}
