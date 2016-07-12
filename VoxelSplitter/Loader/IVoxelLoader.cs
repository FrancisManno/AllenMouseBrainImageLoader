using VoxelSplitter.Enum;

namespace VoxelSplitter.Loader
{
    /// <summary>
    /// Generic interface for loading data into voxelArrays
    /// </summary>
    /// <typeparam name="T">The type of data</typeparam>
    public interface IVoxelLoader<T>
    {
        int SideLengthX { get; }
        int SideLengthY { get; }
        int SideLengthZ { get; }

        /// <summary>
        /// Transforms the rawData to a three dimensional voxel array
        /// </summary>
        /// <param name="order">The order in which the data should be returned</param>
        /// <returns>The voxel array in the order specified by <paramref name="order"/></returns>
        VoxelStore<T> GetVoxelDataInOrder(ArrayOrder order);
    }
}