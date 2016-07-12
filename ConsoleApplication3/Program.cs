using System.Drawing;
using System.Drawing.Imaging;
using VoxelSplitter;
using VoxelSplitter.Enum;

namespace AllenMouseBrainAverageImageLoader
{
    class Program
    {

        static void Main(string[] args)
        {
            var loader = new ImageStackVoxelLoader(@"C:\Users\Torben\Desktop\MouseCommon\Spaces\P56\AtlasSlices");
            var splitter = loader.GetVoxelDataInOrder(ArrayOrder.Zxy);
            PrintAllSlices(splitter, Dimension.X);
            PrintAllSlices(splitter, Dimension.Y);
            PrintAllSlices(splitter, Dimension.Z);
        }

        private static void PrintAllSlices(VoxelStore<SmallColor> splitter, Dimension dimension)
        {
            splitter.DimensionToSliceOver = dimension;

            for (int j = 0; j < splitter.SideLengthZ; j++)
            {

                PrintSlice(splitter.GetSliceOfDimension(j, dimension), dimension.ToString(), j);

            }
        }

        private static void PrintSlice(SmallColor[,] uints, string dimensionPrefix, int z)
        {
            var image = new Bitmap(uints.GetLength(0), uints.GetLength(1), PixelFormat.Format24bppRgb);
            for (int y = 0; y < uints.GetLength(1); y++)
            {
                for (int x = 0; x < uints.GetLength(0); x++)
                {
                    image.SetPixel(x, y, uints[x, y].ToColor());
                }
            }
            image.Save($"{dimensionPrefix}_{z}.png", ImageFormat.Png);
            image.Dispose();

        }
    }
}
