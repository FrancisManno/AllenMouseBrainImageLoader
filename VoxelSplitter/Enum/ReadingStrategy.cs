namespace VoxelSplitter.Enum
{
    public enum ReadingStrategy
    {
        None,
        //All rows of the first slide after another. After SlidesLength*RowLength entries, the next slide is comming
        RowPerSlice,

    }
}