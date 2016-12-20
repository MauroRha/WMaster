using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMaster.Tool;

namespace WMaster.Library.Tests
{
    [TestClass]
    public class WMRandomTests
    {
        [TestMethod]
        public void WMRandom_Next_WithIntValue()
        {
            const int nbRepeat = 1000;
            int BoundValue = 50;

            // Try to execut multiple time to check randomise bounds
            for (int i = 0; i < nbRepeat; i++)
            {
                int test = WMRand.Random(BoundValue);

                Assert.IsTrue(test >= 0, "WMRandom.Next(int upperLimit) return a negative value!");
                Assert.IsTrue(test < BoundValue, "WMRandom.Next(int upperLimit) return a value greater or equal to upperLimit!");
            }
        }
    }
}
