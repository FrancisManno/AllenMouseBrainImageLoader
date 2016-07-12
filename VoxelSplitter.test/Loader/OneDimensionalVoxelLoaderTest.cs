using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoxelSplitter.Enum;
using VoxelSplitter.Loader;

namespace VoxelSplitter.test.Loader
{
    [TestClass]
    public class OneDimensionalVoxelLoaderTest
    {
        [TestMethod]
        public void GetVoxelDataInOrderTest_RawDataIsEmpty_DefaultValueIsSet()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 1, 1);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(0, actual.Data[0, 0, 0]);

        }

        [TestMethod,ExpectedException(typeof(NotImplementedException))]
        public void GetVoxelDataInOrderTest_WrongReadingStrategy()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 1, 1,ReadingStrategy.None);
            target.GetVoxelDataInOrder(ArrayOrder.Xyz);
        }


        [TestMethod]
        public void GetVoxelDataInOrderTest_SideLengthOne_DataHasCorrectSideLength()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[1], 1, 1, 1);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(1, actual.Data.GetLength(0));
            Assert.AreEqual(1, actual.Data.GetLength(1));
            Assert.AreEqual(1, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant1_Zxy()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6 };

            var target = new OneDimensionalVoxelLoader<int>(testData, 1, 2, 3);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(3, actual.Data.GetLength(0));
            Assert.AreEqual(1, actual.Data.GetLength(1));
            Assert.AreEqual(2, actual.Data.GetLength(2));

        }


        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant1_Xyz()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6 };

            var target = new OneDimensionalVoxelLoader<int>(testData, 1, 2, 3);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Xyz);
            Assert.AreEqual(1, actual.Data.GetLength(0));
            Assert.AreEqual(2, actual.Data.GetLength(1));
            Assert.AreEqual(3, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant1_Yzx()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6 };

            var target = new OneDimensionalVoxelLoader<int>(testData, 1, 2, 3);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Yzx);
            Assert.AreEqual(2, actual.Data.GetLength(0));
            Assert.AreEqual(3, actual.Data.GetLength(1));
            Assert.AreEqual(1, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant2_Zxy()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 3, 1, 2);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(2, actual.Data.GetLength(0));
            Assert.AreEqual(3, actual.Data.GetLength(1));
            Assert.AreEqual(1, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant2_Xyz()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 3, 1, 2);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Xyz);
            Assert.AreEqual(3, actual.Data.GetLength(0));
            Assert.AreEqual(1, actual.Data.GetLength(1));
            Assert.AreEqual(2, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant2_Yzx()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 3, 1, 2);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Yzx);
            Assert.AreEqual(1, actual.Data.GetLength(0));
            Assert.AreEqual(2, actual.Data.GetLength(1));
            Assert.AreEqual(3, actual.Data.GetLength(2));

        }


        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant3_Zxy()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 2, 3, 1);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(1, actual.Data.GetLength(0));
            Assert.AreEqual(2, actual.Data.GetLength(1));
            Assert.AreEqual(3, actual.Data.GetLength(2));

        }


        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant3_Xyz()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 2, 3, 1);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Xyz);
            Assert.AreEqual(2, actual.Data.GetLength(0));
            Assert.AreEqual(3, actual.Data.GetLength(1));
            Assert.AreEqual(1, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_DataHasCorrectSideLength_variant3_Yzx()
        {

            var target = new OneDimensionalVoxelLoader<int>(new int[6], 2, 3, 1);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Yzx);
            Assert.AreEqual(3, actual.Data.GetLength(0));
            Assert.AreEqual(1, actual.Data.GetLength(1));
            Assert.AreEqual(2, actual.Data.GetLength(2));

        }

        [TestMethod]
        public void GetVoxelDataInOrderTest_CheckReadingStrategy()
        {
            var testData = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var target = new OneDimensionalVoxelLoader<int>(testData, 2, 2, 2, ReadingStrategy.RowPerSlice);
            var actual = target.GetVoxelDataInOrder(ArrayOrder.Zxy);
            Assert.AreEqual(1, actual.Data[0, 0, 0]);
            Assert.AreEqual(2, actual.Data[0, 1, 0]);
            Assert.AreEqual(3, actual.Data[0, 0, 1]);
            Assert.AreEqual(4, actual.Data[0, 1, 1]);
            Assert.AreEqual(5, actual.Data[1, 0, 0]);
            Assert.AreEqual(6, actual.Data[1, 1, 0]);
            Assert.AreEqual(7, actual.Data[1, 0, 1]);
            Assert.AreEqual(8, actual.Data[1, 1, 1]);

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
