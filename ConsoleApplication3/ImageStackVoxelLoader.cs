using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using VoxelSplitter;
using VoxelSplitter.Enum;
using VoxelSplitter.Loader;

namespace AllenMouseBrainAverageImageLoader
{
    /// <summary>
    /// Loads a set of images of the same dimension in a SmallColor VoxelArray
    /// </summary>
    public class ImageStackVoxelLoader
    {
        private readonly TwoDimensionalVoxelLoader<SmallColor> _backingLoader;
        public int SideLengthX { get; set; }
        public int SideLengthY { get; set; }
        public int SideLengthZ { get; set; }

        /// <summary>
        /// Loads a set of images from <paramref name="path"/> in a VoxelStore
        /// </summary>
        /// <param name="path">The path of the images</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is empty or null</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown if directory <paramref name="path"/> is not found</exception>
        /// <exception cref="FileNotFoundException">Thrown if the directory <paramref name="path"/> is empty</exception>
        /// <exception cref="ApplicationException">Thrown if no image could be loaded</exception>
        public ImageStackVoxelLoader(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException();
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            var files = new DirectoryInfo(path).GetFiles();
            var noFilesInThisDirectory = !files.Any();
            if (noFilesInThisDirectory)
            {
                throw new FileNotFoundException();
            }

            var intermediateStore = new List<SmallColor[,]>();
            SideLengthZ = files.Length;

            foreach (var fileInfo in files)
            {
                var image = (Bitmap)Image.FromFile(fileInfo.FullName);
                SideLengthX = image.Width;
                SideLengthY = image.Height;
                var imagedata = new SmallColor[SideLengthX, SideLengthY];
                for (int x = 0; x < SideLengthX; x++)
                {
                    for (int y = 0; y < SideLengthY; y++)
                    {
                        imagedata[x, y] = image.GetPixel(x, y).ToSmallColor();
                    }
                }
                image.Dispose();
                intermediateStore.Add(imagedata);
            }
            _backingLoader = new TwoDimensionalVoxelLoader<SmallColor>(intermediateStore);

            var noImageCouldBeLoaded = !_backingLoader.RawData.Any();
            if (noImageCouldBeLoaded)
            {
                throw new ApplicationException("No image could be loaded");
            }


        }

        public VoxelStore<SmallColor> GetVoxelDataInOrder(ArrayOrder order)
        {
            return _backingLoader.GetVoxelDataInOrder(order);
        }
    }

}
