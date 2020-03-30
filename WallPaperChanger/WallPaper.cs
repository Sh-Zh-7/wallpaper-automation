using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WallPaperChanger
{

    public sealed class Wallpaper
    {
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);


        private static System.Drawing.Image GetImage(string path)
        {
            Byte[] image;
            System.Drawing.Image result;
            using (FileStream fs = File.OpenRead(path))
            {
                long file_length = fs.Length;
                image = new Byte[file_length];
                fs.Read(image, 0, (int)file_length);
                result = System.Drawing.Image.FromStream(fs);
            }
            return result;
        }
        public static void Set(string path, Style style)
        {
            System.Drawing.Image img = GetImage(path);
            //string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            //img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Centered)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            if (style == Style.Tiled)
            {
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                path,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
