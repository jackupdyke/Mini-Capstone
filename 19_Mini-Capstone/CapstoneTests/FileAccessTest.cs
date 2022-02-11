using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    class FileAccessTest
    {
        [TestMethod]
        public void FileAccessTest1()
        {
            FileAccess fileAccess = new FileAccess();

            Assert.IsNotNull(fileAccess);

        }

    }
}
