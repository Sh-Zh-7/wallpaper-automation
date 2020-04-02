using System.Collections.Generic;

namespace Utils
{
    public enum Style : int
    {
        Tiled,
        Centered,
        Stretched
    }
    public class ImagesAttr
    {
        public ImagesAttr()
        {
            imagesPath = new List<string>();
            imagesStyle = new List<int>();
        }
        public List<string> imagesPath { get; set; }
        public List<int> imagesStyle { get; set; }
    }
}
