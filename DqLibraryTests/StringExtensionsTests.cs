using DqTool.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DqTool.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests : TestBase
    {
        [TestMethod]
        [TestCase("10", 10)]
        [TestCase("0", 0)]
        [TestCase("1000000", 1000000)]
        [TestCase("a", 0)]
        [TestCase("", 0)]
        public void TestToInt()
        {
            TestContext.Run((string before, int after) =>
            {
                before.ToInt().Is(after);
            });
        }

        [TestMethod]
        [TestCase("10", 10, 0)]
        [TestCase("a", 0, 0)]
        [TestCase("a", -1, -1)]
        public void TestToIntDefault()
        {
            TestContext.Run((string before, int after, int def) =>
            {
                before.ToInt(def).Is(after);
            });
        }
    }
}
