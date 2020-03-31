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
            images_path = new List<string>();
            images_style = new List<int>();
        }
        public List<string> images_path { get; set; }
        public List<int> images_style { get; set; }
    }
}
