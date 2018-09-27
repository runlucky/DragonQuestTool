using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DqLibraryTests
{
    [TestClass]
    public abstract class TestBase
    {
        public TestContext TestContext { get; set; }
    }
}
