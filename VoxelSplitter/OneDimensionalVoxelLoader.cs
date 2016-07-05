using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxelSplitter
{
    public class OneDimensionalVoxelLoader<T>
    {
        public T[] RawData { get; private set; }
        public int SideLengthX { get; set; }
        public int SideLengthY { get; set; }
        public int SideLengthZ { get; set; }

        public OneDimensionalVoxelLoader(T[] rawdata, int sideLengthX, int sideLengthY, int sideLengthZ)
        {
            if (((int)rawdata.Length) != sideLengthX * sideLengthY * sideLengthZ)
            {
                throw new ArgumentOutOfRangeException();
            }
            SideLengthX = sideLengthX;
            SideLengthY = sideLengthY;
            SideLengthZ = sideLengthZ;
            RawData = rawdata;
        }

        public T[,,] GetVoxelDataZXY(ReadingStrategy strategy)
        {
            switch (strategy)
            {
                case ReadingStrategy.RowPerSlice:
                default:
                    return GetVoxelDataByRowsPerSlice();

            }
        }

        private T[,,] GetVoxelDataByRowsPerSlice()
        {

            var result = new T[SideLengthZ, SideLengthX, SideLengthY];
            var sliceLength = SideLengthY * SideLengthX;
            for (var z = 0; z < SideLengthZ; z++)
            {

                //var hasAValue = false;
                var slideOffset = z * sliceLength;

                for (int y = 0; y < SideLengthY; y++)
                {
                    var columnOffset = SideLengthX * y;
                    for (var x = 0; x < SideLengthX; x++)
                    {

                        result[z, x, y] = RawData[slideOffset + columnOffset + x];
                    }

                }

            }
            return result;
        }

    }

}
