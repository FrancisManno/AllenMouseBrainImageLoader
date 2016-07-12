﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using VoxelSplitter;
using VoxelSplitter.Enum;
using VoxelSplitter.Loader;

namespace AllenMouseBrainAnnotationLoader
{
    class Program
    {

        const int DIMENSION_SLICES = 456;
        const int DIMENSION_HEIGHT = 320;
        const int DIMENSION_WIDTH = 528;

        public static Dictionary<uint, Color> Colors { get; set; }

        static void Main(string[] args)
        {
            Colors = new Dictionary<uint, Color>();
            var content = File.ReadAllBytes(@"C:\users\torben\Desktop\atlasVolume.raw");
            var contentReadable = ConvertRawDataFromUint32(content);


            var loader = new OneDimensionalVoxelLoader<uint>(contentReadable, DIMENSION_WIDTH, DIMENSION_HEIGHT, DIMENSION_SLICES);
            var splitter = loader.GetVoxelDataInOrder(ArrayOrder.Zxy);

            PrintAllSlices(splitter, Dimension.X);
            PrintAllSlices(splitter, Dimension.Y);
            PrintAllSlices(splitter, Dimension.Z);
        }

        private static UInt32[] ConvertRawDataFromUint32(byte[] content)
        {
            var result = new UInt32[(long)((float)content.Length / 4f)];
            var index = 0;
            for (var i = 0; i < content.Length; i = i + 4)
            {
                var uInt32 = BitConverter.ToUInt32(content, i);
                result[index] = uInt32;
                index++;
            }
            return result;
        }

        private static void PrintAllSlices(VoxelStore<uint> splitter, Dimension dimension)
        {
            splitter.DimensionToSliceOver = dimension;

            for (int j = 0; j < splitter.SideLengthZ; j++)
            {

                PrintSlice(splitter.GetSliceOfDimension(j, dimension), dimension.ToString(), j);

            }
        }

        private static void PrintSlice(uint[,] uints, string dimensionPrefix, int z)
        {
            var image = new Bitmap(uints.GetLength(0), uints.GetLength(1), PixelFormat.Format24bppRgb);
            for (int y = 0; y < uints.GetLength(1); y++)
            {
                for (int x = 0; x < uints.GetLength(0); x++)
                {
                    if (uints[x, y] > 0u)
                        image.SetPixel(x, y, ChooseColor(uints[x, y]));
                }
            }
            image.Save($"{dimensionPrefix}_{z}.png", ImageFormat.Png);
            image.Dispose();

        }

        private static Color ChooseColor(uint u)
        {
            if (!Colors.ContainsKey(u))
                Colors.Add(u, GetRandomColor());
            return Colors[u];
        }

        private static Color GetRandomColor()
        {
            var random = new Random();
            var red = random.Next(255);
            var green = random.Next(255);
            var blue = random.Next(255);
            return Color.FromArgb(red, green, blue);
        }
    }
}
