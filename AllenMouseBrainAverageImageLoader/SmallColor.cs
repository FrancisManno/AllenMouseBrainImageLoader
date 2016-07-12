namespace AllenMouseBrainAverageImageLoader
{
    /// <summary>
    /// Helper struct to handle colors more memory efficient.
    /// The .Net Color struct uses 24 bytes per pixel. 
    /// This scruct only uses 4 bytes without loosing the readability.
    /// </summary>
    public struct SmallColor
    {
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Alpha;
    }
}
