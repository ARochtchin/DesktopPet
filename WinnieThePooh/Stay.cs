using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnieThePooh
{
    class Stay : IScenario
    {
        public string Title => "Stay";

        public string Gif => "avares://WinnieThePooh/Images/pause.gif";

        public int TimerInterval => 10000;

        public InitResult Initialize(int2 pos, int2 screen)
        {
            return new InitResult(new int2(screen.x - 200, screen.y - 150), new int2(100, 100));
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
