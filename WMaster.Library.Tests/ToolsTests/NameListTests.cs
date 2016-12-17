using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMaster.Tool;

namespace WMaster.Library.Tests
{
    [TestClass]
    public class NameListTests
    {
        private const string _fileName = "RivalGangFirstNames.txt";

        [TestMethod]
        public void NameList_LoadListInConstructor_RandomName()
        {
            NameList nl = new NameList(NameListTests._fileName);
            string name = nl.Random();

            Assert.IsNotNull(name, "Pas de nom retourné");
            Assert.IsFalse(string.Empty.Equals(name), "Retourne un nom vide");
        }

        [TestMethod]
        public void NameList_LoadListInFunction_RandomName()
        {
            NameList nl = new NameList();
            nl.Load(NameListTests._fileName);

            string name = nl.Random();

            Assert.IsNotNull(name, "Pas de nom retourné");
            Assert.IsFalse(string.Empty.Equals(name), "Retourne un nom vide");
        }
    }
}
