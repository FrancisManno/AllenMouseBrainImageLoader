using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VoxelSplitter.test
{
    [TestClass]
    public class VoxelStoreTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTest_RawDataIsNull()
        {
            var target = new VoxelStore<int>(null);

        }

        [TestMethod]
        public void DimensionTest_DimensionXIsDefault()
        {
            var data = new int[1, 1, 1];
            data[0, 0, 0] = 1;

            var target = new VoxelStore<int>(data);
            Assert.AreEqual(Dimension.X, target.Dimension);

        }

        [TestMethod]
        public void DimensionTest_SetX()
        {
            var data = Data();
            var target = new VoxelStore<int>(data) { Dimension = Dimension.X };
            var actual = target.GetSlice(0);
            Assert.AreEqual(1, actual[0, 0]);
            Assert.AreEqual(5, actual[0, 1]);
            Assert.AreEqual(3, actual[1, 0]);
            Assert.AreEqual(7, actual[1, 1]);

            actual = target.GetSlice(1);

            Assert.AreEqual(2, actual[0, 0]);
            Assert.AreEqual(6, actual[0, 1]);
            Assert.AreEqual(4, actual[1, 0]);
            Assert.AreEqual(8, actual[1, 1]);
        }

        [TestMethod]
        public void DimensionTest_SetY()
        {
            var data = Data();
            var target = new VoxelStore<int>(data) {Dimension = Dimension.Y};
            var actual = target.GetSlice(0);
            Assert.AreEqual(1, actual[0, 0]);
            Assert.AreEqual(5, actual[0, 1]);
            Assert.AreEqual(2, actual[1, 0]);
            Assert.AreEqual(6, actual[1, 1]);

            actual = target.GetSlice(1);

            Assert.AreEqual(3, actual[0, 0]);
            Assert.AreEqual(7, actual[0, 1]);
            Assert.AreEqual(4, actual[1, 0]);
            Assert.AreEqual(8, actual[1, 1]);
        }


        [TestMethod]
        public void DimensionTest_SetZ()
        {
            var data = Data();
            var target = new VoxelStore<int>(data) { Dimension = Dimension.Z };

            var actual = target.GetSlice(0);
            Assert.AreEqual(1, actual[0, 0]);
            Assert.AreEqual(2, actual[0, 1]);
            Assert.AreEqual(3, actual[1, 0]);
            Assert.AreEqual(4, actual[1, 1]);

            actual = target.GetSlice(1);

            Assert.AreEqual(5, actual[0, 0]);
            Assert.AreEqual(6, actual[0, 1]);
            Assert.AreEqual(7, actual[1, 0]);
            Assert.AreEqual(8, actual[1, 1]);
        }
        private static int[,,] Data()
        {
            var data = new int[2, 2, 2];
            data[0, 0, 0] = 1;
            data[1, 0, 0] = 2;
            data[0, 1, 0] = 3;
            data[1, 1, 0] = 4;
            data[0, 0, 1] = 5;
            data[1, 0, 1] = 6;
            data[0, 1, 1] = 7;
            data[1, 1, 1] = 8;
            return data;
        }
    }
}
