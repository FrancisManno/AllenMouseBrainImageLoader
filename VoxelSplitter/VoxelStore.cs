using System;

namespace VoxelSplitter
{
    public class VoxelStore<T>
    {
        public T[,,] Data { get; private set; }


        public VoxelStore(T[,,] rawdata)
        {
            Data = rawdata;
        }



        public T[,] GetSlice(Dimension dimensionToSliceOver, int slice)
        {
            var newX = 0;
            var newY = 0;
            var newZ = 0;
            T[,] result = new T[0,0];
            if (dimensionToSliceOver == Dimension.Z)
            {

                newZ = Data.GetLength(0);
                newX = Data.GetLength(1);
                newY = Data.GetLength(2);
                result = new T[newX, newY];
                for (int x = 0; x < newX; x++)
            {
                for (int y = 0; y < newY; y++)
                {
                    result[x, y] = Data[slice, x, y];
                }
            }
            }
            if (dimensionToSliceOver == Dimension.Y)
            {

                newZ = Data.GetLength(2);
                newX = Data.GetLength(1);
                newY = Data.GetLength(0);
                result = new T[newX, newY];
                for (int x = 0; x < newX; x++)
                {
                    for (int y = 0; y < newY; y++)
                    {
                        result[x, y] = Data[ x, y,slice];
                    }
                }
            }


            if (dimensionToSliceOver == Dimension.X)
            {

                newZ = Data.GetLength(1);
                newX = Data.GetLength(0);
                newY = Data.GetLength(2);
                result = new T[newX, newY];
                for (int x = 0; x < newX; x++)
                {
                    for (int y = 0; y < newY; y++)
                    {
                        result[x, y] = Data[x, slice,y];
                    }
                }
            }



            return result;
        }



    }
}
