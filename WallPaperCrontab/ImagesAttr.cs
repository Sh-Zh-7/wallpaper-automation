using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPaperCrontab
{
    public class ImagesAttr
    {
        public ImagesAttr()
        {
            // 这些引用类型的成员如果不再构造函数里面初始化
            // 是无法使用的
            images_path = new List<string>();
            images_style = new List<int>();
        }
        public List<string> images_path { get; set; } 
        public List<int> images_style { get; set; }
    }
}
