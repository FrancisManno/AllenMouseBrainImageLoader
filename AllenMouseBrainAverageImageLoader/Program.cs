using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using VoxelSplitter;
using VoxelSplitter.Enum;

namespace AllenMouseBrainAverageImageLoader
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Wrong number of arguments: <program>.exe <path-to-atlas-imagess> <output-path>");
            }
            if (!Directory.Exists(args[0]))
            {
                throw new DirectoryNotFoundException("Could not find the input folder for the atlas images");
            }
            if (!Directory.Exists(args[1]))
            {
                throw new DirectoryNotFoundException("Could not find the output folder for the atlas images. Please create it");
            }
            var loader = new ImageStackVoxelLoader(args[0]);
            var splitter = loader.GetVoxelDataInOrder(ArrayOrder.Zxy);
            PrintAllSlices(splitter, Dimension.X, args[1]);
            PrintAllSlices(splitter, Dimension.Y, args[1]);
            PrintAllSlices(splitter, Dimension.Z, args[1]);
        }

        private static void PrintAllSlices(VoxelStore<SmallColor> splitter, Dimension dimension, string outputPath)
        {
            splitter.DimensionToSliceOver = dimension;

            for (int j = 0; j < splitter.SideLengthZ; j++)
            {

                PrintSlice(splitter.GetSliceOfDimension(j, dimension), dimension.ToString(), j,outputPath);

            }
        }

        private static void PrintSlice(SmallColor[,] uints, string dimensionPrefix, int z,string outputPath)
        {
            var image = new Bitmap(uints.GetLength(0), uints.GetLength(1), PixelFormat.Format24bppRgb);
            for (int y = 0; y < uints.GetLength(1); y++)
            {
                for (int x = 0; x < uints.GetLength(0); x++)
                {
                    image.SetPixel(x, y, uints[x, y].ToColor());
                }
            }
            image.Save($"{outputPath}/{dimensionPrefix}_{z}.png", ImageFormat.Png);
            image.Dispose();

        }
    }
}
