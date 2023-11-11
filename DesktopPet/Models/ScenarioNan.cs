using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    class ScenarioNan : IScenario
    {
        int _sleepTime = 60;
        int _elapsed = 0;

        public string Title => "Wait { " + _elapsed + "s}";
        public string Gif => "avares://DesktopPet/Images/None.gif";

        public int TimerInterval { get => 1000; }

        public void SetSleepTime(int seconds) { _sleepTime = seconds; }

        public MoveResult InitMove(int2 pos, int2 screen)
        {
            _elapsed = _sleepTime;
            return new MoveResult(new int2(-200, -200)).Resize(new int2(1, 1)).Refresh();
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (--_elapsed > 0)
                return new MoveResult(pos).TitleRefresh();// { FinishRequest = false, WindowPos = pos, TitleRefreshRequest=true };
            else
                return new MoveResult(pos).Finish();// { FinishRequest = true, WindowPos = pos };
        }

        public void OnPause(bool pause)
        {
        }
    }
}
