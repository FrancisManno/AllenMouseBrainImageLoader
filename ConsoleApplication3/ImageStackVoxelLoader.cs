using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using VoxelSplitter;
using VoxelSplitter.Enum;
using VoxelSplitter.Loader;

namespace ConsoleApplication3
{
    public class ImageStackVoxelLoader
    {
        private TwoDimensionalVoxelLoader<SmallColor> _backingLoader;
        public int SideLengthX { get; set; }
        public int SideLengthY { get; set; }
        public int SideLengthZ { get; set; }
   

        public ImageStackVoxelLoader(string path)
        {
            if (path == null)
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
