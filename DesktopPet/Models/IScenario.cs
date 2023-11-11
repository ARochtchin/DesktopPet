using DesktopPet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet.Models
{
    public interface IScenario
    {
        string Title { get; }
        string Gif { get; }
        int TimerInterval { get; }
        MoveResult InitMove(int2 pos, int2 screen);
        MoveResult OnMove(int2 pos, int2 screen);
        void OnPause(bool pause);
    }

    public struct int2 : IEquatable<int2>
    {
        public int x;
        public int y;

        public int2(int X, int Y) { x = X; y = Y; }

        public bool Equals(int2 other)
        {
            return x == other.x && y == other.y;
        }
    }

    public struct MoveResult
    {
        public int2 WindowPos;
        public int2 WinodwSize;
        public bool ResizeRequest;
        public bool RefreshRequest;
        public bool TitleRefreshRequest;
        public bool FinishRequest;

        public MoveResult(int2 pos) { this.WindowPos = pos; }
        public MoveResult Resize(int2 size) { this.WinodwSize = size; ResizeRequest = true; return this; }
        public MoveResult Refresh() { RefreshRequest = true; return this; }
        public MoveResult TitleRefresh() { TitleRefreshRequest = true; return this; }
        public MoveResult Finish() { FinishRequest = true; return this; }
    }
}
