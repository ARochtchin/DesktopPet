using DesktopPet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DesktopPet.Models
{
    class ScenarioManager
    {
        PackModel[] _packs;

        int2 _screenSize = new int2(1920, 1080);
        int2 _currentPosition = new int2(0, 0);
        int2 _currentWindowSize = new int2(100, 100);
        Timer scenarioTimer;
        Random rnd = new Random();

        IScenario _current;
        ScenarioNan _sleepScenario = ScenarioNan.Instance;

        internal ScenarioManager()
        {
            ScenarioCollection packsCollection = new ScenarioCollection();
            _packs = packsCollection.GetPacks().ConvertAll(x => new PackModel(x)).ToArray();

            scenarioTimer = new Timer();
            scenarioTimer.Elapsed += Timer_Elapsed;
        }

        internal void SetScreenSize(int2 screenSize)
        {
            _screenSize = screenSize;
        }

        internal void Sleep()
        {
            ChangeScenario(_sleepScenario);
        }

        internal void ShowRandom()
        {
            //generate id
            List<IScenario> activeScenarios = new List<IScenario>();
            foreach (var pack in _packs)
            {
                if (pack.IsUsed)
                {
                    foreach (var scenario in pack.Scenarios)
                    {
                        if (scenario.IsUsed)
                            activeScenarios.Add(scenario.Scenario);
                    }
                }
            }
            //rand
            ChangeScenario(activeScenarios[rnd.Next(activeScenarios.Count)]);
        }

        internal bool Pause(bool pause)
        {
            var res = current.OnPause(pause);
            
            Raise_GIFrefresh(current.Gif);
            
            if (pause) { scenarioTimer.Stop(); }
            else { scenarioTimer.Start(); }
            return res;
        }

        internal PackModel[] GetPacks() { return _packs; }

        internal void SetWindowPosition(int2 pos)
        { _currentPosition = pos; }

        internal int SleepTime
        {
            get => _sleepScenario.SleepTime;
            set
            {
                _sleepScenario.SleepTime = value;
                ChangeScenario(_sleepScenario);
            }
        }

        internal IScenario current
        {
            get
            {
                return _current;
            }
            set
            {
                ChangeScenario(value);
            }
        }

        void ChangeScenario(IScenario scenario)
        {
            if (scenario == null)
                scenario = _sleepScenario;
            _current = scenario;

            var res = current.Initialize(_currentPosition, _screenSize);
            MoveWindow(res.WindowPos);
            ResizeWindow(res.WinodwSize);

            scenarioTimer.Interval = current.TimerInterval;
            scenarioTimer.Start();
            Raise_activeChange();
        }

        void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var moveResult = current.OnMove(_currentPosition, _screenSize);
            if (moveResult.RefreshRequest) { Raise_GIFrefresh(current.Gif); }
            if (moveResult.TitleRefreshRequest) { Raise_activeChange(); }
            if (moveResult.ResizeRequest) { ResizeWindow(moveResult.WinodwSize); }
            MoveWindow(moveResult.WindowPos);
            if (moveResult.FinishRequest)
            {
                scenarioTimer.Stop();
                if (_current is ScenarioNan)
                    ShowRandom();
                else
                    Sleep();
            }
        }

        void MoveWindow(int2 pos)
        {
            if (_currentPosition.Equals(pos))
                return;
            Raise_moveWindow(pos);
            _currentPosition = pos;
        }

        void ResizeWindow(int2 size)
        {
            if (size.x <= 0 || size.y <= 0)
                return;
            if (_currentWindowSize.Equals(size))
                return;
            _currentWindowSize = size;
            Raise_resizeWindow(size);
        }

        #region Events
        private void Raise_GIFrefresh(string path)
        {
            if (eGIF_Refresh != null) eGIF_Refresh(path);
        }
        private void Raise_moveWindow(int2 pos)
        {
            if (eMove != null) eMove(pos);
        }
        private void Raise_resizeWindow(int2 pos)
        {
            if (eResize != null) eResize(pos);
        }
        private void Raise_activeChange()
        {
            if (eActiveChange != null) eActiveChange(current);
            Raise_GIFrefresh(current.Gif);
        }

        internal event Action<string> eGIF_Refresh;
        internal event Action<int2> eMove;
        internal event Action<int2> eResize;
        internal event Action<IScenario> eActiveChange;
        #endregion
    }
}
