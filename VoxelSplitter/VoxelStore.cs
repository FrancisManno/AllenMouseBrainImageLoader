using System;
using VoxelSplitter.Enum;

namespace VoxelSplitter
{
    public class VoxelStore<T>
    {
        private Dimension _dimensionToSliceOver;
        private Func<int, int, int, T> _accessor;
        public T[,,] Data { get; private set; }

        public int SideLengthX => Data.GetLength(0);

        public int SideLengthY => Data.GetLength(1);
        public int SideLengthZ => Data.GetLength(2);

        public Dimension DimensionToSliceOver
        {
            get { return _dimensionToSliceOver; }
            set
            {
                _dimensionToSliceOver = value;
                ComputeDimensions();
            }
        }

        /// <summary>
        /// Creates a VoxelStore
        /// </summary>
        /// <param name="rawdata">The rawdata to store</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rawdata"/> equals null</exception>
        public VoxelStore(T[,,] rawdata)
        {
            if (rawdata == null)
                throw new ArgumentNullException();
            Data = rawdata;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slice">The slice to retrieve</param>
        /// <param name="dimensionToSliceOver">The dimension to slice over</param>
        /// <returns>If <paramref name="dimensionToSliceOver"/> equals Z -> it returns an array with [x,y]. If equals Y -> [z,x], X returns [y,z]</returns>
        public T[,] GetSliceOfDimension(int slice, Dimension dimensionToSliceOver)
        {

            var result = GetEmptyArrayForDimension(dimensionToSliceOver);
            for (var x = 0; x < result.GetLength(0); x++)
            {
                for (var y = 0; y < result.GetLength(1); y++)
                {
                    result[x, y] = GetAccessFunc(dimensionToSliceOver)(x, y, slice);
                }
            }

            return result;
        }

        //public T[][,] GetAllSclicesOverDimension(Dimension dimension)
        //{
        //}

        private T[,] GetEmptyArrayForDimension(Dimension dimension)
        {
            T[,] result;
            switch (dimension)
            {
                case Dimension.X:
                    result = new T[SideLengthY,SideLengthZ];
                    break;
                case Dimension.Y:
                    result = new T[SideLengthZ,SideLengthX];
                    break;
                case Dimension.Z:
                default:
                    result = new T[SideLengthX, SideLengthY];
                    break;
            }
            return result;
        }

        private Func<int, int, int, T> GetAccessFunc(Dimension dimension)
        {
            Func<int, int, int, T> result;
            switch (dimension)
            {
                case Dimension.Y:
                    {
                        result = (a, b, c) => Data[b, c, a];

                        break;
                    }
                case Dimension.Z:
                    {

                        result = (a, b, c) => Data[a, b, c];


                        break;
                    }
                case Dimension.X:
                default:
                    {

                        result = (a, b, c) => Data[c, a, b];
                        break;
                    }
            }
            return result;
        }

        private void ComputeDimensions()
        {

            _accessor = GetAccessFunc(DimensionToSliceOver);
        }
    }
}
