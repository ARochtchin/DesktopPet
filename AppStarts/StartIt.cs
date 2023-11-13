using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStarts
{
    class AppItem
    {
        internal AppItem(string name, string gifPath, int2 size, int timeS=5)
        {
            Name = name;
            GifPath = gifPath;
            Size = size;
            TimeS = timeS;
        }

        internal AppItem(string name, string gifPath, int timeS=5)
        {
            Name = name;
            GifPath = gifPath;
            Size = DesktopPet.Helpers.GetImageSize(gifPath);
            TimeS = timeS;
        }

        public string Name { get; private set; }
        public string GifPath { get; private set; }
        public int2 Size { get; private set; }
        public int TimeS { get; private set;}
    }

    internal class StartIt : IScenario
    {
        AppItem _appItem;
        public StartIt(AppItem item)
        {
            _appItem= item;
        }

        public string Title => _appItem.Name;

        public string Gif => _appItem.GifPath;

        public int TimerInterval => _appItem.TimeS * 1000;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            var size  = _appItem.Size;
            return new InitResult(new int2(screenSize.x/2 - size.x / 2, screenSize.y/2 - size.y / 2), size);
        }

        public MoveResult OnMove(int2 pos, int2 screen)
        {
            return new MoveResult(pos).Finish();
        }

        public bool OnPause(bool pause)
        {
            return false;
        }
    }
}
