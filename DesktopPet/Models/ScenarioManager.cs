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
        IScenario[] scenarios;
        int _sleepTimeS = 60;

        int2 _screenSize;
        int2 _currentPosition = new int2(0,0);
        int2 _currentWindowSize = new int2(100, 100);
        Timer scenarioTimer;
        Random rnd = new Random();

        int _currentID = -0;
        ScenarioNan _sleepScenario = new ScenarioNan();

        internal ScenarioManager()
        {
            scenarios = new IScenario[]
            {
                //new ScenarioNan(),
                new ScenarioMove(false,false),
                new ScenarioSighs(true),
                new ScenarioMove(false,true),
                new ScenarioStay(),
                new ScenarioMove(true,true),
                new ScenarioSighs(false),
                new ScenarioMove(true,false),
                new ScenarioUpBush(),
                new ScenarioBalloon()
            };

            _screenSize = new int2 { x = 1920, y = 1080 };

            scenarioTimer = new Timer();
            scenarioTimer.Elapsed += Timer_Elapsed;
        }

        internal void SetScreenSize(int2 screenSize)
        {
            _screenSize = screenSize;
        }

        internal void Sleep()
        {
            ChangeScenario(-1);
        }

        internal void ShowRandom()
        {
            ChangeScenario(rnd.Next(scenarios.Length));
        }

        internal void Pause(bool pause)
        {
            current.OnPause(pause);
            if (pause) { scenarioTimer.Stop(); }
            else { scenarioTimer.Start(); }
        }

        internal String[] GetScenarios() { return Array.ConvertAll(scenarios, x => x.Title); }

        internal int CurrentId
        {
            get => _currentID;
            set => ChangeScenario(value);
        }

        internal void SetWindowPosition(int2 pos)
        { _currentPosition = pos; }

        internal int SleepTime
        {
            get => _sleepTimeS;
            set
            {
                if (value > 0)
                {
                    _sleepTimeS = value;
                    _sleepScenario.SetSleepTime( _sleepTimeS);
                    ChangeScenario(-1);
                }
            }
        }

        IScenario current
        {
            get { return _currentID < 0 ? _sleepScenario : scenarios[_currentID]; }
        }

        void ChangeScenario(int id)
        {
            if (id >= scenarios.Length)
                return;
            _currentID = id;

            var res = current.Initialize(_currentPosition, _screenSize);
            MoveWindow(res.WindowPos);
            ResizeWindow(res.WinodwSize);

            scenarioTimer.Interval = current.TimerInterval;
            scenarioTimer.Start();
            Raise_activeChange(_currentID, current);
        }

        void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var moveResult = current.OnMove(_currentPosition, _screenSize);
            if (moveResult.RefreshRequest) { Raise_GIFrefresh(current.Gif); }
            if (moveResult.TitleRefreshRequest) { Raise_activeChange(_currentID, current); }
            if (moveResult.ResizeRequest) { ResizeWindow(moveResult.WinodwSize); }
            MoveWindow(moveResult.WindowPos);
            if (moveResult.FinishRequest)
            {
                scenarioTimer.Stop();
                if (_currentID < 0)
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
            if(size.x<=0 || size.y<=0) 
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
        private void Raise_activeChange(int id, IScenario scenario)
        {
            if (eActiveChange != null) eActiveChange(id, scenario);
            Raise_GIFrefresh(current.Gif);
        }

        internal event Action<string> eGIF_Refresh;
        internal event Action<int2> eMove;
        internal event Action<int2> eResize;
        internal event Action<int, IScenario> eActiveChange;
        #endregion
    }
}
