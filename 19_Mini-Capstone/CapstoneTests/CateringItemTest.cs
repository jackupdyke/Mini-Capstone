using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class CateringItemTest
    {
        [TestMethod]
        public void CateringItemTestConstructor()
        {

            CateringItem cateringitem = new CateringItem("A1", "Soda", "25", 3.50M);

            Assert.IsNotNull(cateringitem);


        }
    }
}
