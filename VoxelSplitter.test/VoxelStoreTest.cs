using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoxelSplitter.Enum;

namespace VoxelSplitter.test
{
    [TestClass]
    public class VoxelStoreTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTest_RawDataIsNull()
        {
            var voxelStore = new VoxelStore<int>(null);
        }

        [TestMethod]
        public void DimensionTest_DimensionXIsDefault()
        {
            var data = new int[1, 1, 1];
            data[0, 0, 0] = 1;

            var target = new VoxelStore<int>(data);
            Assert.AreEqual(Dimension.X, target.DimensionToSliceOver);

        }

        [TestMethod]
        public void DimensionTest_SliceOverX()
        {
            var data = Data();
            var target = new VoxelStore<int>(data);
            var actual = target.GetSliceOfDimension(0, Dimension.X);
            Assert.AreEqual(1,  actual[0, 0]);
            Assert.AreEqual(4,  actual[1, 0]);
            Assert.AreEqual(7,  actual[0, 1]);
            Assert.AreEqual(10, actual[1, 1]);
            Assert.AreEqual(13, actual[0, 2]);
            Assert.AreEqual(16, actual[1, 2]);
            Assert.AreEqual(19, actual[0, 3]);
            Assert.AreEqual(22, actual[1, 3]);

            actual = target.GetSliceOfDimension(1, Dimension.X);

            Assert.AreEqual(2, actual[0, 0]);
            Assert.AreEqual(5, actual[1, 0]);
            Assert.AreEqual(8, actual[0, 1]);
            Assert.AreEqual(11, actual[1, 1]);
            Assert.AreEqual(14, actual[0, 2]);
            Assert.AreEqual(17, actual[1, 2]);
            Assert.AreEqual(20, actual[0, 3]);
            Assert.AreEqual(23, actual[1, 3]);

            actual = target.GetSliceOfDimension(2, Dimension.X);

            Assert.AreEqual(3,  actual[0, 0]);
            Assert.AreEqual(6,  actual[1, 0]);
            Assert.AreEqual(9,  actual[0, 1]);
            Assert.AreEqual(12, actual[1, 1]);
            Assert.AreEqual(15, actual[0, 2]);
            Assert.AreEqual(18, actual[1, 2]);
            Assert.AreEqual(21, actual[0, 3]);
            Assert.AreEqual(24, actual[1, 3]);

        }

        [TestMethod]
        public void DimensionTest_SliceOverY()
        {
            var data = Data();
            var target = new VoxelStore<int>(data);
            var actual = target.GetSliceOfDimension(0, Dimension.Y);
            Assert.AreEqual(1, actual[0, 0]);
            Assert.AreEqual(7, actual[1, 0]);
            Assert.AreEqual(13, actual[2, 0]);
            Assert.AreEqual(19, actual[3, 0]);
            Assert.AreEqual(2, actual[0, 1]);
            Assert.AreEqual(8, actual[1, 1]);
            Assert.AreEqual(14, actual[2, 1]);
            Assert.AreEqual(20, actual[3, 1]);
            Assert.AreEqual(3, actual[0, 2]);
            Assert.AreEqual(9, actual[1, 2]);
            Assert.AreEqual(15, actual[2, 2]);
            Assert.AreEqual(21, actual[3, 2]);

            actual = target.GetSliceOfDimension(1, Dimension.Y);
            Assert.AreEqual(4, actual[0, 0]);
            Assert.AreEqual(10, actual[1, 0]);
            Assert.AreEqual(16, actual[2, 0]);
            Assert.AreEqual(22, actual[3, 0]);
            Assert.AreEqual(5, actual[0, 1]);
            Assert.AreEqual(11, actual[1, 1]);
            Assert.AreEqual(17, actual[2, 1]);
            Assert.AreEqual(23, actual[3, 1]);
            Assert.AreEqual(6, actual[0, 2]);
            Assert.AreEqual(12, actual[1, 2]);
            Assert.AreEqual(18, actual[2, 2]);
            Assert.AreEqual(24, actual[3, 2]);
        }


        [TestMethod]
        public void DimensionTest_SliceOverZ()
        {
            var data = Data();
            var target = new VoxelStore<int>(data);

            var actual = target.GetSliceOfDimension(0, Dimension.Z);
            Assert.AreEqual(1, actual[0, 0]);
            Assert.AreEqual(2, actual[1, 0]);
            Assert.AreEqual(3, actual[2, 0]);
            Assert.AreEqual(4, actual[0, 1]);
            Assert.AreEqual(5, actual[1, 1]);
            Assert.AreEqual(6, actual[2, 1]);

            actual = target.GetSliceOfDimension(1, Dimension.Z);
            Assert.AreEqual(7, actual[0, 0]);
            Assert.AreEqual(8, actual[1, 0]);
            Assert.AreEqual(9, actual[2, 0]);
            Assert.AreEqual(10, actual[0, 1]);
            Assert.AreEqual(11, actual[1, 1]);
            Assert.AreEqual(12, actual[2, 1]);

            actual = target.GetSliceOfDimension(2, Dimension.Z);
            Assert.AreEqual(13, actual[0, 0]);
            Assert.AreEqual(14, actual[1, 0]);
            Assert.AreEqual(15, actual[2, 0]);
            Assert.AreEqual(16, actual[0, 1]);
            Assert.AreEqual(17, actual[1, 1]);
            Assert.AreEqual(18, actual[2, 1]);

            actual = target.GetSliceOfDimension(3, Dimension.Z);
            Assert.AreEqual(19, actual[0, 0]);
            Assert.AreEqual(20, actual[1, 0]);
            Assert.AreEqual(21, actual[2, 0]);
            Assert.AreEqual(22, actual[0, 1]);
            Assert.AreEqual(23, actual[1, 1]);
            Assert.AreEqual(24, actual[2, 1]);

        }
        [TestMethod]
        public void DimensionTest_GetSideLength()
        {
            var data = Data();
            var target = new VoxelStore<int>(data);


            Assert.AreEqual(3, target.SideLengthX);
            Assert.AreEqual(2, target.SideLengthY);
            Assert.AreEqual(4, target.SideLengthZ);
        }

        private static int[,,] Data()
        {
            var x = 3;
            var y = 2;
            var z = 4;
            var result = new int[x, y, z];
            result[0, 0, 0] = 1;
            result[1, 0, 0] = 2;
            result[2, 0, 0] = 3;
            result[0, 1, 0] = 4;
            result[1, 1, 0] = 5;
            result[2, 1, 0] = 6;

            result[0, 0, 1] = 7;
            result[1, 0, 1] = 8;
            result[2, 0, 1] = 9;
            result[0, 1, 1] = 10;
            result[1, 1, 1] = 11;
            result[2, 1, 1] = 12;

            result[0, 0, 2] = 13;
            result[1, 0, 2] = 14;
            result[2, 0, 2] = 15;
            result[0, 1, 2] = 16;
            result[1, 1, 2] = 17;
            result[2, 1, 2] = 18;

            result[0, 0, 3] = 19;
            result[1, 0, 3] = 20;
            result[2, 0, 3] = 21;
            result[0, 1, 3] = 22;
            result[1, 1, 3] = 23;
            result[2, 1, 3] = 24;
            return result;
        }
        [TestMethod]
        public void TestDataTest()
        {
            var target = Data();

            //Check if side lengths are ok
            Assert.AreEqual(3, target.GetLength(0));//x
            Assert.AreEqual(2, target.GetLength(1));//y
            Assert.AreEqual(4, target.GetLength(2));//z


        }

    }
}
