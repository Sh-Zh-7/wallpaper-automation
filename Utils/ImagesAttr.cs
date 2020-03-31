using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
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
