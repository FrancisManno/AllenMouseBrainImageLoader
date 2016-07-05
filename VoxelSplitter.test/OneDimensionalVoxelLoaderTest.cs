using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VoxelSplitter.test
{
    [TestClass]
    public class OneDimensionalVoxelLoaderTest
    {
        [TestMethod]
        public void GetVoxelDataZXYTest_RawDataIsEmpty_DefaultValueIsSet()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 1, 1);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(0, actual[0, 0, 0]);

        }
        [TestMethod]
        public void GetVoxelDataZXYTest_SideLengthOne_DataHasCorrectSideLength()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 1, 1);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(1, actual.GetLength(0));
            Assert.AreEqual(1, actual.GetLength(1));
            Assert.AreEqual(1, actual.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataZXYTest_DataHasCorrectSideLength_variant1()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6};

            var target = new OneDimensionalVoxelLoader<int>(testData, 1, 2, 3);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(3, actual.GetLength(0));
            Assert.AreEqual(1, actual.GetLength(1));
            Assert.AreEqual(2, actual.GetLength(2));

        }


        [TestMethod]
        public void GetVoxelDataZXYTest_DataHasCorrectSideLength_variant2()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 3,1,2);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(2, actual.GetLength(0));
            Assert.AreEqual(3, actual.GetLength(1));
            Assert.AreEqual(1, actual.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataZXYTest_DataHasCorrectSideLength_variant3()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 2,3,1);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(1, actual.GetLength(0));
            Assert.AreEqual(2, actual.GetLength(1));
            Assert.AreEqual(3, actual.GetLength(2));

        }


        [TestMethod]
        public void GetVoxelDataZXYTest_CheckReadingStrategy()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var target = new OneDimensionalVoxelLoader<int>(testData, 2, 2, 2);
            var actual = target.GetVoxelDataZXY(ReadingStrategy.RowPerSlice);
            Assert.AreEqual(1, actual[0, 0, 0]);
            Assert.AreEqual(2, actual[0, 1, 0]);
            Assert.AreEqual(3, actual[0, 0, 1]);
            Assert.AreEqual(4, actual[0, 1, 1]);
            Assert.AreEqual(5, actual[1, 0, 0]);
            Assert.AreEqual(6, actual[1, 1, 0]);
            Assert.AreEqual(7, actual[1, 0, 1]);
            Assert.AreEqual(8, actual[1, 1, 1]);

        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorTest_TooManyValuesInRawData()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[10], 1, 2, 3);

        }


        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorTest_ToFewValuesInRawData()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 2, 3);

        }
    }
}
