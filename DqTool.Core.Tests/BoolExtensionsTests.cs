using System;
using DqTool.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DqTool.Core.Tests
{
    [TestClass]
    public class BoolExtensionsTests : TestBase
    {
        [TestMethod]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void TestToggle()
        {
            TestContext.Run((bool before, bool after) =>
            {
                before.Toggle().Is(after);
            });
        }
    }
}
