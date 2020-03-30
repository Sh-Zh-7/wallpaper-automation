using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WallPaperChanger;

namespace UnitTest
{
    [TestClass]
    public class WallPaperChangerTest
    {
        [TestMethod]
        public void Json2StrTest()
        {
            string json_str = Changer.Json2Str(@"./test.json");
            Console.WriteLine(json_str);
        }

        [TestMethod]
        public void SetWallPaperByJsonTest()
        {
            Changer.SetWallPaperByJson(@"./test.json");
        }
    }
}
