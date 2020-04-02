using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    [TestClass]
    class EnvironmentTest
    {
        [TestMethod]
        void GetEnvironmentVariableTest()
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("Test"));
        }
    }
}
