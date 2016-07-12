using System.Drawing;

namespace ConsoleApplication3
{
    public static class ColorHelper
    {
        public static SmallColor ToSmallColor(this Color color)
        {
            return new SmallColor()
            {
                Alpha = color.A,
                Red = color.R,
                Green = color.G,
                Blue = color.B
            };
        }

        public static Color ToColor(this SmallColor smallColor)
        {
            return Color.FromArgb(smallColor.Alpha, smallColor.Red, smallColor.Green, smallColor.Blue);
        }
    }
}
