using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPaperChanger
{
    public enum Style : int
    {
        Tiled,
        Centered,
        Stretched
    }
    public class Changer
    {
        static public string Json2Str(string path)
        {
            string result = "";
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader fileStream = new StreamReader(fs))
                {
                    string line;
                    while ((line = fileStream.ReadLine()) != null)
                    {
                        result += line;
                    }
                }
            }
            return result;

        }

        static public void SetWallPaperByJson(string json_path)
        {
            // 先获取从路径中得到的json文件
            string json_str = Json2Str(json_path);
            // 反序列化该json文件，得到各个属性（图片路径的List，壁纸的展示格式）
            JObject jObject = new JObject();
            ImagesAttr images_attr = JsonConvert.DeserializeObject<ImagesAttr>(json_str);
            // TODO:计算相应日期，该显示哪一个图片
            // 这里我并没有直接计算，而是直接显示了相应的图片
            string target_path = images_attr.images_path[0];
            Style target_style = (Style)images_attr.images_style[0];
            // 根据计算出来的图片和显示格式切换桌面
            Wallpaper.Set(target_path, target_style);
        }

        static public void Main(string[] args)
        {
            SetWallPaperByJson(@"./test.json");
        }
    }
}
