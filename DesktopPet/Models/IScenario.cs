using DesktopPet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    /// <summary>
    /// Scenario interface
    /// </summary>
    public interface IScenario
    {
        /// <summary>Scenario name</summary>
        string Title { get; }
        /// <summary>Path to used gif file</summary>
        string Gif { get; }
        /// <summary>Interval in ms between window refresh</summary>
        int TimerInterval { get; }
        /// <summary>Initialization. Is called every start of the scenario</summary>
        /// <param name="pos">current window position</param>
        /// <param name="screen">current screen size</param>
        /// <returns></returns>
        InitResult Initialize(int2 windPos, int2 screenSize);
        /// <summary>Refresh actions</summary>
        /// <param name="windPos">current window position</param>
        /// <param name="screenSize">current screen size</param>
        /// <returns></returns>
        MoveResult OnMove(int2 pos, int2 screen);
        /// <summary>Start/Stop pause</summary>
        /// <param name="pause">is paused</param>
        void OnPause(bool pause);
    }

    /// <summary>
    /// Two integers for size, position, etc
    /// </summary>
    public struct int2 : IEquatable<int2>
    {
        /// <summary>X</summary>
        public int x;
        /// <summary>Y</summary>
        public int y;

        /// <summary>Construct</summary>
        /// <param name="X">X value</param>
        /// <param name="Y">Y value</param>
        public int2(int X, int Y) { x = X; y = Y; }

        /// <summary>Compare two int2 objects</summary>
        /// <param name="other">other int2 obj</param>
        /// <returns>true if equals</returns>
        public bool Equals(int2 other)
        {
            return x == other.x && y == other.y;
        }
    }

    /// <summary>Init scenario parameters</summary>
    public struct InitResult
    {
        /// <summary>LeftTop window corner position</summary>
        public int2 WindowPos { get; private set; }
        /// <summary>Window size</summary>
        public int2 WinodwSize { get; private set; }

        public InitResult(int2 pos, int2 size)
        {
            WindowPos = pos;
            WinodwSize = size;
        }

        public InitResult Move(int x, int y) { this.WindowPos = new int2(x, y); return this; }
        public InitResult Move(int2 pos) { this.WindowPos = pos; return this; }
        public InitResult Resize(int2 size) { this.WinodwSize = size; return this; }
        public InitResult Resize(int width, int heigth) { this.WinodwSize = new int2(width, heigth); return this; }
    }

    /// <summary>Move window parameters</summary>
    public struct MoveResult
    {
        /// <summary>LeftTop window corner position</summary>
        public int2 WindowPos { get; private set; }
        /// <summary>Window size</summary>
        public int2 WinodwSize { get; private set; }
        /// <summary>Is window resize required</summary>
        public bool ResizeRequest { get; private set; }
        /// <summary>Is window content changed</summary>
        public bool RefreshRequest { get; private set; }
        /// <summary>Is title changed</summary>
        public bool TitleRefreshRequest { get; private set; }
        public bool FinishRequest { get; private set; }

        public MoveResult(int2 pos) { this.WindowPos = pos; }
        public MoveResult(int x, int y) { this.WindowPos = new int2(x, y); }

        public MoveResult Move(int x, int y) { this.WindowPos = new int2(x, y); return this; }
        public MoveResult Move(int2 pos) { this.WindowPos = pos; return this; }
        public MoveResult Resize(int2 size) { this.WinodwSize = size; ResizeRequest = true; return this; }
        public MoveResult Resize(int width, int heigth) { this.WinodwSize = new int2(width, heigth); ResizeRequest = true; return this; }
        public MoveResult Refresh() { RefreshRequest = true; return this; }
        public MoveResult TitleRefresh() { TitleRefreshRequest = true; return this; }
        public MoveResult Finish() { FinishRequest = true; return this; }
    }
}
