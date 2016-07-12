using System;
using VoxelSplitter.Enum;

namespace VoxelSplitter.Loader
{
    /// <summary>
    /// Translates one dimensional data in a VoxelArray based on side length
    /// </summary>
    /// <typeparam name="T">The type of the class</typeparam>
    public class OneDimensionalVoxelLoader<T> : IVoxelLoader<T>
    {
        public T[] RawData { get; private set; }
        public int SideLengthX { get; private set; }
        public int SideLengthY { get; private set; }
        public int SideLengthZ { get; private set; }
        public ReadingStrategy Strategy { get; private set; }

        /// <summary>
        /// Creates an instance from rawData and constraining sidelength
        /// </summary>
        /// <param name="rawdata">A array of type T</param>
        /// <param name="strategy">The strategy how to interprete the <paramref name="rawdata"/></param>
        /// <param name="sideLengthX">The sidelength of the first dimension of <paramref name="rawdata"/></param>
        /// <param name="sideLengthY">The sidelength of the second. dimension of <paramref name="rawdata"/></param>
        /// <param name="sideLengthZ">The sidelength of the third dimension of <paramref name="rawdata"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length of <paramref name="rawdata"/> not equals <paramref name="sideLengthX"/> * <paramref name="sideLengthY"/> * <paramref name="sideLengthZ"/></exception>
        public OneDimensionalVoxelLoader(T[] rawdata, int sideLengthX, int sideLengthY, int sideLengthZ, ReadingStrategy strategy = ReadingStrategy.RowPerSlice)
        {
            if (((int)rawdata.Length) != sideLengthX * sideLengthY * sideLengthZ)
            {
                throw new ArgumentOutOfRangeException();
            }
            Strategy = strategy;
            SideLengthX = sideLengthX;
            SideLengthY = sideLengthY;
            SideLengthZ = sideLengthZ;
            RawData = rawdata;
        }
        /// <summary>
        /// Transforms the rawData to a three dimensional voxel array
        /// </summary>
        /// <param name="order">The order in which the data should be returned</param>
        /// <returns>The voxel array in the order specified by <paramref name="order"/></returns>
        public VoxelStore<T> GetVoxelDataInOrder(ArrayOrder order)
        {
            if (Strategy == ReadingStrategy.RowPerSlice)
            {

                switch (order)
                {
                    case ArrayOrder.Xyz:
                        return new VoxelStore<T>(GetVoxelDataByRowsPerSlice(SideLengthX, SideLengthY, SideLengthZ));

                    case ArrayOrder.Yzx:
                        return new VoxelStore<T>(GetVoxelDataByRowsPerSlice(SideLengthY, SideLengthZ, SideLengthX));

                    case ArrayOrder.Zxy:
                    default:
                        return new VoxelStore<T>(GetVoxelDataByRowsPerSlice(SideLengthZ, SideLengthX, SideLengthY));

                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private T[,,] GetVoxelDataByRowsPerSlice(int dimensionA, int dimensionB, int dimensionC)
        {
            var result = new T[dimensionA, dimensionB, dimensionC];
            var sliceLength = dimensionB * dimensionC;
            for (var z = 0; z < dimensionA; z++)
            {

                var slideOffset = z * sliceLength;

                for (int y = 0; y < dimensionC; y++)
                {
                    var columnOffset = dimensionB * y;
                    for (var x = 0; x < dimensionB; x++)
                    {

                        result[z, x, y] = RawData[slideOffset + columnOffset + x];
                    }

                }

            }
            return result;
        }

    }

}
