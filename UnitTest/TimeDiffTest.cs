using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WallPaperChanger;

namespace UnitTest
{
    // 如果不声明TestClass类的话， 测试资源管理器也是无法识别的
    [TestClass]
    public class TimeDiffTest
    {
        [TestMethod]
        public void GetDiffTimeDayTest()
        {
            // 将易读的字符串格式强行转为DateTime
            DateTime start = DateTime.Parse("2020-3-10");
            DateTime end = DateTime.Parse("2020-3-24");
            double timeDiff = TimeDiff.GetTargetModeDiff(start, end, TimeDiff.TimeMode.Day);
            Console.WriteLine(timeDiff);
        }

        // 常数应该写在前面
        [TestMethod]
        public void GetDiffTimeHourTest()
        {
            // 将易读的字符串格式强行转为DateTime
            DateTime start = new DateTime(2020, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2020, 1, 1, 3, 0, 0);
            double timeDiff = TimeDiff.GetTargetModeDiff(start, end, TimeDiff.TimeMode.Hour);
            Assert.AreEqual(timeDiff, 3);
        }

        // 只有增加了这个注解并保存，才能被vs里面的单元测试检测到
        [TestMethod]
        public void GetDiffTimeMinuteTest()
        {
            // 将易读的字符串格式强行转为DateTime
            DateTime start = new DateTime(2020, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2020, 1, 1, 3, 0, 0);
            double timeDiff = TimeDiff.GetTargetModeDiff(start, end, TimeDiff.TimeMode.Minute);
            Assert.AreEqual(timeDiff, 3 * 60);
        }
        [TestMethod]
        public void GetDiffTimeSecondTest()
        {
            // 将易读的字符串格式强行转为DateTime
            DateTime start = new DateTime(2020, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2020, 1, 1, 3, 0, 0);
            double timeDiff = TimeDiff.GetTargetModeDiff(start, end, TimeDiff.TimeMode.Second);
            Assert.AreEqual(timeDiff, 3 * 60 * 60);
        }
    }
}
