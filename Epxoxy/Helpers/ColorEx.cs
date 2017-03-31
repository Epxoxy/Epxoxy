using System.Windows.Media;

namespace Epxoxy.Helpers
{
    public static class ColorEx
    {
        public static Color LightToTransparent(Color baseColor, float percLighter)
        {
            if (percLighter == 0.0f)
            {
                return Colors.Transparent;
            }
            else if (percLighter == 1.0f)
            {
                return baseColor;
            }
            else
            {
                Color lightLight = Colors.White;

                int dr = baseColor.R - lightLight.R;
                int dg = baseColor.G - lightLight.G;
                int db = baseColor.B - lightLight.B;

                return Color.FromRgb((byte)(baseColor.R - (byte)(dr * percLighter)),
                                      (byte)(baseColor.G - (byte)(dg * percLighter)),
                                      (byte)(baseColor.B - (byte)(db * percLighter)));
            }
        }
        public static Color DarkToBlack(Color baseColor, float percDarker)
        {
            if (percDarker == 0.0f)
            {
                return baseColor;
            }
            else if (percDarker == 1.0f)
            {
                return Colors.Black;
            }
            else
            {
                Color darkDark = Colors.Black;

                int dr = baseColor.R - darkDark.R;
                int dg = baseColor.G - darkDark.G;
                int db = baseColor.B - darkDark.B;

                return Color.FromRgb((byte)(baseColor.R - (byte)(dr * percDarker)),
                                      (byte)(baseColor.G - (byte)(dg * percDarker)),
                                      (byte)(baseColor.B - (byte)(db * percDarker)));
            }
        }

        public static Color DarkenBy(Color baseColor, double percent)
        {
            return ChangeColorBrightness(baseColor, (float)percent / 100.0f);
        }
        public static Color LightenBy(Color baseColor, double percent)
        {
            return ChangeColorBrightness(baseColor, -1f * ((float)percent / 100.0f));
        }
        public static Color ChangeColorBrightness(Color baseColor, double percent)
        {
            return ChangeColorBrightness(baseColor, (float)percent / 100.0f);
        }
        public static Color ChangeColorBrightness(Color baseColor, float correctionFactor)
        {
            float red = (255 - baseColor.R) * correctionFactor + baseColor.R;
            float green = (255 - baseColor.G) * correctionFactor + baseColor.G;
            float blue = (255 - baseColor.B) * correctionFactor + baseColor.B;
            return Color.FromArgb(baseColor.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
