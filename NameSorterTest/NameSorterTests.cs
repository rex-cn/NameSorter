using NameSorter;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NameSorter.Tests
{
    [TestClass()]
    public class NameSorterTests
    {
        [TestMethod()]
        public void CompareTest()
        {
            var nc = new NameSorterAction();
            Assert.IsTrue(nc.Compare("Johe Andrew", "Rex Book") < 0);
            Assert.IsTrue(nc.Compare("Johe Andrew", "Johe Andrew") == 0);
            Assert.IsTrue(nc.Compare("Johe Andrew", "Andrew") > 0);
            Assert.IsTrue(nc.Compare("Johe  Andrew", "Rex Book") < 0);
            Assert.IsTrue(nc.Compare("Johe  Andrew", "Johe Andrew") == 0);
            Assert.IsTrue(nc.Compare("Johe  Andrew", "Andrew") > 0);
            Assert.IsTrue(nc.Compare("Johe Dar Andrew", "Johe Dar Andrew") == 0);
            Assert.IsTrue(nc.Compare("Andrew", " Andrew") == 0);
            Assert.IsTrue(nc.Compare("Andrew ", "    Andrew ") == 0);
        }

        [TestMethod()]
        public void FormatNameTest()
        {
            var nc = new NameSorterAction();
            Assert.AreEqual(nc.FormatName("Johe Andrew"), "Johe Andrew");
            Assert.AreEqual(nc.FormatName("Johe\t Andrew"), "Johe Andrew");
            Assert.AreEqual(nc.FormatName("Johe\t\t\t\tAndrew"), "Johe Andrew");
            Assert.AreEqual(nc.FormatName("Johe\t\t    \t\tAndrew"), "Johe Andrew");
            Assert.AreEqual(nc.FormatName("  Johe\t\t    \t\tAndrew   "), "Johe Andrew");
        }
    }
}