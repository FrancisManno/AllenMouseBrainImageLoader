using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoxelSplitter.Enum;
using VoxelSplitter.Loader;

namespace VoxelSplitter.test.Loader
{
    [TestClass()]
    public class TwoDimensionalVoxelLoaderTest
    {
        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_NoEntryInRawData_Throwsexception()
        {
            var target = new TwoDimensionalVoxelLoader<int>(new List<int[,]>());
        }

        [TestMethod(), ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTest_RawDataIsNull_Throwsexception()
        {
            var target = new TwoDimensionalVoxelLoader<int>(null);
        }

        [TestMethod()]
        public void ConstructorTest_ValidRawData_DowsNotThrowExcepion()
        {
            var testdata = new List<int[,]>();
            var innerStuff = new int[1, 1];
            innerStuff[0, 0] = 1;
            testdata.Add(innerStuff);
            var target = new TwoDimensionalVoxelLoader<int>(testdata);
        }

        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void ConstructorTest_RawDataHasDifferentDimensions_ThrowExcepion()
        {
            var testdata = new List<int[,]>();
            var innerStuff = new int[1, 1];
            innerStuff[0, 0] = 1;
            var innerStuff2 = new int[2, 2];
            innerStuff[0, 0] = 2;
            testdata.Add(innerStuff);
            testdata.Add(innerStuff2);
            var target = new TwoDimensionalVoxelLoader<int>(testdata);
        }

        [TestMethod()]
        public void VoxelDataFromRawDataTest_ValidDataXyz_ReturnsVoxelStore()
        {
            var testdata = new List<int[,]> { new int[,] { { 1 } }, new int[,] { { 2 } }, new int[,] { { 3 } } };
            var target = new TwoDimensionalVoxelLoader<int>(testdata);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Xyz);

            Assert.AreEqual(1, actual.SideLengthX);
            Assert.AreEqual(1, actual.SideLengthY);
            Assert.AreEqual(3, actual.SideLengthZ);

            Assert.AreEqual(1, actual.Data[0, 0, 0]);
            Assert.AreEqual(2, actual.Data[0, 0, 1]);
            Assert.AreEqual(3, actual.Data[0, 0, 2]);

        }

        [TestMethod()]
        public void VoxelDataFromRawDataTest_ValidDataYxz_ReturnsVoxelStore()
        {
            var testdata = new List<int[,]> { new int[,] { { 1 } }, new int[,] { { 2 } }, new int[,] { { 3 } } };
            var target = new TwoDimensionalVoxelLoader<int>(testdata);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Yzx);

            Assert.AreEqual(1, actual.SideLengthX);
            Assert.AreEqual(3, actual.SideLengthY);
            Assert.AreEqual(1, actual.SideLengthZ);

            Assert.AreEqual(1, actual.Data[0, 0, 0]);
            Assert.AreEqual(2, actual.Data[0, 1, 0]);
            Assert.AreEqual(3, actual.Data[0, 2, 0]);
        }

        [TestMethod()]
        public void VoxelDataFromRawDataTest_ValidDataZxy_ReturnsVoxelStore()
        {
            var testdata = new List<int[,]> { new int[,] { { 1 } }, new int[,] { { 2 } }, new int[,] { { 3 } } };
            var target = new TwoDimensionalVoxelLoader<int>(testdata);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            
            Assert.AreEqual(3, actual.SideLengthX);
            Assert.AreEqual(1, actual.SideLengthY);
            Assert.AreEqual(1, actual.SideLengthZ);

            Assert.AreEqual(1, actual.Data[0, 0, 0]);
            Assert.AreEqual(2, actual.Data[1, 0, 0]);
            Assert.AreEqual(3, actual.Data[2, 0, 0]);
        }
    }
}