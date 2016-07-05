﻿using System;

namespace VoxelSplitter
{
    public class VoxelStore<T>
    {
        private Dimension _dimension;
        private Func<int, int, int, T> _accessor;
        public T[,,] Data { get; private set; }
        public int SideLengthA { get; private set; }
        public int SideLengthB { get; private set; }
        public int SideLengthC { get; private set; }

        public Dimension Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;
                ComputeDimensions();
            }
        }


        public VoxelStore(T[,,] rawdata)
        {
            Data = rawdata;
            Dimension = Dimension.X;
        }


        public T[,] GetSlice(int slice)
        {
            var newX = 0;
            var newY = 0;
            var newZ = 0;
            T[,] result = new T[SideLengthA, SideLengthB];
            for (int x = 0; x < SideLengthA; x++)
            {
                for (int y = 0; y < SideLengthB; y++)
                {
                    result[x, y] = _accessor(x,y,slice);
                }
            }

            return result;
        }

        private void ComputeDimensions()
        {

            switch (Dimension)
            {
                case Dimension.Y:
                    {
                        SideLengthA = Data.GetLength(1);
                        SideLengthB = Data.GetLength(0);
                        SideLengthC = Data.GetLength(2);
                        _accessor = (a, b, c) => Data[b, a, c];

                        break;
                    }
                case Dimension.Z:
                    {
                        SideLengthA = Data.GetLength(1);
                        SideLengthB = Data.GetLength(2);
                        SideLengthC = Data.GetLength(0);
                        _accessor = (a, b, c) => Data[c, a, b];
                        break;
                    }
                case Dimension.X:
                default:
                    {

                        SideLengthA = Data.GetLength(0);
                        SideLengthB = Data.GetLength(2);
                        SideLengthC = Data.GetLength(1);
                        _accessor = (a, b, c) => Data[a, c, b];
                        break;
                    }
            }
        }
    }
}
