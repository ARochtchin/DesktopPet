using DesktopPet.Models;

namespace WinnieThePooh
{
    class Sighs : IScenario
    {
        bool _left;
        public Sighs(bool left)
        {
            _left = left;
            if (left)
                Gif = "avares://WinnieThePooh/Images/sighs_left.gif";
            else
                Gif = "avares://WinnieThePooh/Images/sighs_right.gif";
        }

        public string Title => "Sighs";

        public string Gif { get; private set; }

        public int TimerInterval => 6000;

        public InitResult Initialize(int2 pos, int2 screen)
        {
            var initPos = _left ? new int2 { x = 0, y = screen.y - 150 } : new int2 { x = screen.x - 200, y = screen.y - 150 };
            return new InitResult(initPos, new int2(100, 100));
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            return new MoveResult(pos).Finish();
        }

        public bool OnPause(bool pause)
        {
            return true;
        }
    }
}