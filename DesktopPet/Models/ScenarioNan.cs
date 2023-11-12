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

        public InitResult Initialize(int2 pos, int2 screen)
        {
            _elapsed = _sleepTime;
            return new InitResult(new int2(-200, -200), new int2(1, 1));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (--_elapsed > 0)
                return new MoveResult(pos).TitleRefresh();
            else
                return new MoveResult(pos).Finish();
        }

        public void OnPause(bool pause)
        {
        }
    }
}
