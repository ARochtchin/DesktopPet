using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DesktopPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopPet
{
    public class Helpers
    {
        public static int2 GetImageSize(string uri)
        {
            var bitmap = new Bitmap(AssetLoader.Open(new Uri(uri)));
            return new int2((int)bitmap.PixelSize.Width, (int)bitmap.PixelSize.Height);
        }
    }
}
