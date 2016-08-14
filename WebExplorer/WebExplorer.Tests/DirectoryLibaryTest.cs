using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryLibary;
using DirectoryLibary.Model;

namespace WebExplorer.Tests
{
    [TestClass]
    public class DirectoryLibaryTest
    {
        [TestMethod]
        public void TestCountOfDisk()
        {
            //may be different in another PC
            int countOfDisc = DirectoryLibary.WorkWithDirectories.GetAllDrives().ToList().Count;
            Assert.AreEqual(3,countOfDisc);
        }

        [TestMethod]
        public void TestStatistic()
        {
            //may be different in another PC
            string path = @"C:\Windows\addins";
            int numberLessThan10MbFiles = 1;
            var numberOfFiles=DirectoryLibary.WorkWithDirectories.GetStatistics(path);
            Assert.AreEqual(numberLessThan10MbFiles,numberOfFiles.Less10Mb );
        }
    }
}
