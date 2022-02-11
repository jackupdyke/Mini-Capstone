using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        Catering catering;
        [TestInitialize]
        public void TestInitialize()
        {
            catering = new Catering();
        }
        [TestMethod]
        public void Check_that_catering_object_is_created()
        {
            
            //Assert
            Assert.IsNotNull(catering);
        }

        [TestMethod]
        public void SortProductCodeTest()
        {
            //Arrange
            List<CateringItem> testList = new List<CateringItem>();
            testList.Add(new CateringItem("A1", "Cake", "25", 1.50M));
            testList.Add(new CateringItem("D2", "Ribs", "25", 3.50M));
            testList.Add(new CateringItem("B3", "Wine", "25", 4.00M));
            testList.Add(new CateringItem("E1", "Spaghetti", "25", 2.45M));

            List<CateringItem> expectedList = new List<CateringItem>();
            expectedList.Add(new CateringItem("A1", "Cake", "25", 1.50M));
            expectedList.Add(new CateringItem("B3", "Wine", "25", 4.00M));
            expectedList.Add(new CateringItem("D2", "Ribs", "25", 3.50M));
            expectedList.Add(new CateringItem("E1", "Spaghetti", "25", 2.45M));
            //Act
            testList = catering.SortProductCode(testList);

           
            
            //Assert
            Assert.AreEqual(expectedList[0].ProductCode, testList[0].ProductCode);
            Assert.AreEqual(expectedList[1].Description, testList[1].Description);
            Assert.AreEqual(expectedList[2].Qty, testList[2].Qty);
            Assert.AreEqual(expectedList[3].Price, testList[3].Price);
        }

        [TestMethod]

        public void GetItemsTest()
        {
            
            CateringItem[] testArray = catering.GetItems();


            CateringItem[] expectedArray = new CateringItem[18]
            {
                new CateringItem("A1", "Tropical Fruit Bowl", "25", 3.50M),
                new CateringItem("A2", "Meatballs", "25", 2.95M),
                new CateringItem("A3", "Bacon Wrapped Shrimp", "25", 4.15M),
                new CateringItem("A4", "Bruschetta", "25", 2.65M),
                new CateringItem("B1", "Soda", "25", 1.50M),
                new CateringItem("B2", "Wine", "25", 3.05M),
                new CateringItem("B3", "Beer", "25", 2.55M),
                new CateringItem("B4", "Sparkling Water", "25", 2.35M),
                new CateringItem("B5", "Punch", "25", 0.90M),
                new CateringItem("D1", "NY Style Cheesecake", "25", 2.55M),
                new CateringItem("D2", "Cake", "25", 1.80M),
                new CateringItem("D3", "Brownies", "25", 1.10M),
                new CateringItem("D4", "Jolly Ranger Tart", "25", 0.85M),
                new CateringItem("D5", "Apple Pie", "25", 2.50M),
                new CateringItem("E1", "Baked Chicken", "25", 8.85M),
                new CateringItem("E2", "Pork Loin", "25", 9.45M),
                new CateringItem("E3", "BBQ Ribs", "25", 11.65M),
                new CateringItem("E4", "Beef and Gravy", "25", 5.15M)

            };

            

            Assert.AreEqual(expectedArray[0].Description, testArray[0].Description);
            Assert.AreEqual(expectedArray[3].Price, testArray[3].Price);
            Assert.AreEqual(expectedArray[9].ProductCode, testArray[9].ProductCode);
        }

        [TestMethod]
        
        

        public void AddBalanceTest()
        {


            decimal testBalance = catering.AddBalance(50.0M);

            decimal expectedBalance = 50.0M;

            Assert.AreEqual(expectedBalance, testBalance);

            testBalance = catering.AddBalance(2.0M);

            Assert.AreEqual(expectedBalance, testBalance);

        }

        [TestMethod]
        public void AddToShoppingCartTest()
        {

            string testMessage = catering.AddToShoppingCart("A1", 3);

            string expectedMessage = "Insufficient Funds";

            Assert.AreEqual(expectedMessage, testMessage);

            catering.AddBalance(100);


            testMessage = catering.AddToShoppingCart("B5", 26);

            expectedMessage = "Insufficient stock";

            Assert.AreEqual(expectedMessage, testMessage);

            catering.AddToShoppingCart("B3", 25);

            testMessage = catering.AddToShoppingCart("B3", 1);

            expectedMessage = "Item sold out";

            Assert.AreEqual(expectedMessage, testMessage);

            testMessage = catering.AddToShoppingCart("A3", 1);

            expectedMessage = "added";

            Assert.AreEqual(expectedMessage, testMessage);

        }

        [TestMethod]

        public void GetCartTest()
        {

            catering.AddBalance(100);

            catering.AddToShoppingCart("B1", 2);
            catering.AddToShoppingCart("B3", 1);
            catering.AddToShoppingCart("E1", 4);

            Dictionary<string, int> testDictionary = catering.GetCart();

            Dictionary<string, int> expectedDictionary = new Dictionary<string, int>();
            expectedDictionary.Add("B1", 2);
            expectedDictionary.Add("B3", 1);
            expectedDictionary.Add("E1", 4);

            CollectionAssert.AreEqual(expectedDictionary, testDictionary);
               
            
        }
        
        [TestMethod]
        public void GetTotalCost()
        {
            catering.AddBalance(100);

            catering.AddToShoppingCart("B1", 2);
            catering.AddToShoppingCart("B3", 1);
            catering.AddToShoppingCart("E1", 4);

            decimal testTotalCost = catering.GetTotalCost();
            decimal expectedTotalCost = 40.95M;

            Assert.AreEqual(expectedTotalCost, testTotalCost);
        }

        [TestMethod]
        public void GetChangeReturnedTest()
        {
            catering.AddBalance(100);

            catering.AddToShoppingCart("B1", 2);
            catering.AddToShoppingCart("B3", 1);
            catering.AddToShoppingCart("E1", 4);

            string testChangeReturned = catering.GetChangeReturned();

            string expectedChangeReturned = "(1) fifties, (1) fives, (4) ones, (1) nickels";

            Assert.AreEqual(expectedChangeReturned, testChangeReturned);
        }


    }
}
