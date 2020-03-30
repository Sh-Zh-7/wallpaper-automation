using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using WallPaperChanger;

namespace UnitTest
{
    [TestClass]
    public class WallPaperChangerTest
    {
        [TestMethod]
        public void Json2StrTest()
        {
            string json_str = Program.Json2Str(@"./test.json");
            Console.WriteLine(json_str);
        }

        //[TestMethod]
        //public void SetWallPaperByJsonTest()
        //{
        //    Program.SetWallPaperByJson(@"./test.json");
        //}

        [TestMethod]
        public void SetWallPaperTest()
        {
            DateTime startTime = DateTime.ParseExact("03-30 19:00:00", "MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            TimeDiff.TimeMode timeMode = (TimeDiff.TimeMode)Convert.ToInt32("1");
            int interval = Convert.ToInt32("5");
            // ����Ҫ���������н�����Ӧ��������������ʼʱ�䣬ʱ��ģʽ�͸�ģʽ�µ�ʱ����
            Program.SetWallPaper(@"./test.json", startTime, timeMode, interval);
        }
    }
}
