using System;
using System.Collections.Generic;
using System.Linq;
using VoxelSplitter.Enum;

namespace VoxelSplitter.Loader
{
    public class TwoDimensionalVoxelLoader<T> : IVoxelLoader<T>
    {
        public IReadOnlyList<T[,]> RawData { get; private set; }
        public int SideLengthX { get; private set; }
        public int SideLengthY { get; private set; }
        public int SideLengthZ { get; private set; }

        /// <summary>
        /// Creates an instance from rawData
        /// </summary>
        /// <param name="rawdata">A array of type T</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="rawdata"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="rawdata"/> has no values OR the dimensions of the arrays are not the same for every array.</exception>

        public TwoDimensionalVoxelLoader(IReadOnlyList<T[,]> rawdata)
        {
            if (rawdata == null)
            {
                throw new ArgumentNullException();
            }
            RawData = rawdata;
            if (!RawData.Any())
            {
                throw new ArgumentException();
            }
            var areAllDimensionsEqual = RawData.Select(t => t.GetLength(0)).Distinct().Count() == 1 &&
                                        RawData.Select(t => t.GetLength(1)).Distinct().Count() == 1;
            if (!areAllDimensionsEqual)
            {
                throw new ArgumentException();
            }
            ComputeDimensions();
        }

        private void ComputeDimensions()
        {
            SideLengthZ = RawData.Count;
            SideLengthX = RawData.FirstOrDefault().GetLength(0);
            SideLengthY = RawData.FirstOrDefault().GetLength(1);

        }
        
        /// <summary>
        /// Transforms the rawData to a three dimensional voxel array
        /// </summary>
        /// <param name="order">The order in which the data should be returned</param>
        /// <returns>The voxel array in the order specified by <paramref name="order"/></returns>
        public VoxelStore<T> GetVoxelDataInOrder(ArrayOrder order)
        {
            switch (order)
            {
                case ArrayOrder.Xyz:
                    return new VoxelStore<T>(VoxelDataFromRawData(SideLengthX, SideLengthY, SideLengthZ, (a, b, c) => RawData[b][a, c])) { DimensionToSliceOver = Dimension.Z };

                case ArrayOrder.Yzx:
                    return new VoxelStore<T>(VoxelDataFromRawData(SideLengthY, SideLengthZ, SideLengthX, (a, b, c) => RawData[a][b, c])) { DimensionToSliceOver = Dimension.X };

                case ArrayOrder.Zxy:
                default:
                    return new VoxelStore<T>(VoxelDataFromRawData(SideLengthZ, SideLengthX, SideLengthY, (a, b, c) => RawData[c][a, b])) { DimensionToSliceOver = Dimension.Y };

            }
        }

        private T[,,] VoxelDataFromRawData(int dimensionA, int dimensionB, int dimensionC, Func<int, int, int, T> accessFunc)
        {

            var result = new T[dimensionA, dimensionB, dimensionC];
            for (var z = 0; z < dimensionA; z++)
            {
                for (var y = 0; y < dimensionC; y++)
                {
                    for (var x = 0; x < dimensionB; x++)
                    {
                        result[z, x, y] = accessFunc(x, y, z);
                    }
                }
            }
            return result;
        }
    }
}
