using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    class StaticItem
    {
        internal StaticItem(string name, string gifPath, int2 size, int timeS = 5)
        {
            Name = name;
            GifPath = gifPath;
            Size = size;
            TimeS = timeS;
        }

        internal StaticItem(string name, string gifPath, int timeS = 5)
        {
            Name = name;
            GifPath = gifPath;
            Size = DesktopPet.Helpers.GetImageSize(gifPath);
            TimeS = timeS;
        }

        public string Name { get; private set; }
        public string GifPath { get; private set; }
        public int2 Size { get; private set; }
        public int TimeS { get; private set; }
    }

    internal class StaticGif : IScenario
    {
        StaticItem _appItem;
        public StaticGif(StaticItem item)
        {
            _appItem = item;
        }

        public string Title => _appItem.Name;

        public string Gif => _appItem.GifPath;

        public int TimerInterval => _appItem.TimeS * 1000;

        public InitResult Initialize(int2 windPos, int2 screenSize)
        {
            var size = _appItem.Size;
            return new InitResult(new int2(screenSize.x - size.x, screenSize.y - size.y - 30), size);
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
