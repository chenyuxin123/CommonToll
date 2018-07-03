using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSFramework;
using FSFramework.Toll;

namespace FSFrameworkUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            decimal d = 106.0254564m;
            string str = d.ToRmbUpper();
            string str2 = d.ToRmbLower();
            string str3 = 
            Console.WriteLine(str);
        }

        public class Student
        {
            public string name { get; set; }
            public string age { get; set; }
        }
    }
}
