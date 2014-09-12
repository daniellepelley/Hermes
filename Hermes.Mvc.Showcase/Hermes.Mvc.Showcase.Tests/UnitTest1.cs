using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using WatiN.Core.Native.Chrome;

namespace Hermes.Mvc.Showcase.Tests
{
    [TestClass]
    public class BrowserTest
    {
        [TestMethod]
        public void Test1()
        {
            using (WatiN.Core.Browser browser = new IE())
            {
                browser.GoTo("http://localhost:55186/");

                browser.Close();
                browser.Dispose();
            }
        }
    }
}
