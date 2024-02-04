using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    class ScenarioNan : IScenario
    {
        private ScenarioNan() { }

        static ScenarioNan _instance = new ScenarioNan();
        public static ScenarioNan Instance => _instance;

        int _elapsed = 0;

        public string Title => "Wait { " + _elapsed + "s}";
        public string Gif => "avares://DesktopPet/Images/None.gif";

        public int TimerInterval { get => 1000; }

        int _sleepTime = 300;
        public int SleepTime { get => _sleepTime;
            set => _sleepTime = value > 1 ? value : 1; }

        public InitResult Initialize(int2 pos, int2 screen)
        {
            _elapsed = SleepTime;
            return new InitResult(new int2(-2000, -2000), new int2(1, 1));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            if (--_elapsed > 0)
                return new MoveResult(pos).TitleRefresh();
            else
                return new MoveResult(pos).Finish();
        }

        public bool OnPause(bool pause)
        {
            return false;
        }
    }
}
