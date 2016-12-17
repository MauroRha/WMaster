using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMaster.Tool;

namespace WMaster.Library.Tests
{
    [TestClass]
    public class DoubleNameListTests
    {
        private const string _firstNameFile = "RivalGangFirstNames.txt";
        private const string _lastNameFile = "RivalGangLastNames.txt";

        [TestMethod]
        public void DoubleNameList_LoadListInConstructor_RandomName()
        {
            DoubleNameList dnl = new DoubleNameList(DoubleNameListTests._firstNameFile, DoubleNameListTests._lastNameFile);
            string name = dnl.Random();

            Assert.IsNotNull(name, "Pas de nom retourné");
            Assert.IsFalse(string.Empty.Equals(name), "Retourne un nom vide");
            Assert.IsTrue(name.Contains(" "), String.Format("Le nom ne semble pas valide, abscence d'espace : '{0}'", name));
        }

        [TestMethod]
        public void DoubleNameList_LoadListInFunction_RandomName()
        {
            DoubleNameList dnl = new DoubleNameList();
            dnl.Load(DoubleNameListTests._firstNameFile, DoubleNameListTests._lastNameFile);

            string name = dnl.Random();

            Assert.IsNotNull(name, "Pas de nom retourné");
            Assert.IsFalse(string.Empty.Equals(name), "Retourne un nom vide");
            Assert.IsTrue(name.Contains(" "), String.Format("Le nom ne semble pas valide, abscence d'espace : '{0}'", name));
        }
    }
}
