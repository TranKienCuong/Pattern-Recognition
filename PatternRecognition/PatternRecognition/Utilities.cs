using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognition
{
    class Utilities
    {
        public static Tuple<int, int, int, int> FindSentenceBoundary(Bitmap image)
        {
            int top_X = 0;
            int top_Y = 0;

            int bottom_X = 0;
            int bottom_Y = 0;

            bool check = false;

            // Find top horizontal bound line
            for (int j = 0; j < image.Height; ++j)
            {
                for (int i = 0; i < image.Width; ++i)
                {
                    if (image.GetPixel(i, j).R <= 245 && image.GetPixel(i, j).B <= 245 && image.GetPixel(i, j).G <= 245)
                    {
                        top_Y = j - 1;
                        check = true;
                        break;
                    }
                }
                if (check)
                    break;
            }

            check = false;

            // Find top vertical bound line
            for (int i = 0; i < image.Width; ++i)
            {
                for (int j = 0; j < image.Height; ++j)
                {
                    if (image.GetPixel(i, j).R <= 245 && image.GetPixel(i, j).B <= 245 && image.GetPixel(i, j).G <= 245)
                    {
                        top_X = i - 1;
                        check = true;
                        break;
                    }
                }
                if (check)
                    break;
            }

            check = false;

            // Find bottom horizontal bound line
            for (int j = image.Height - 1; j >= 0; --j)
            {
                for (int i = image.Width - 1; i >= 0; --i)
                {
                    if (image.GetPixel(i, j).R <= 245 && image.GetPixel(i, j).B <= 245 && image.GetPixel(i, j).G <= 245)
                    {
                        bottom_Y = j + 1;
                        check = true;
                        break;
                    }
                }
                if (check)
                    break;
            }

            check = false;

            // Find bottom vertical bound line
            for (int i = image.Width - 1; i >= 0; --i)
            {
                for (int j = image.Height - 1; j >= 0; --j)
                {
                    if (image.GetPixel(i, j).R <= 245 && image.GetPixel(i, j).B <= 245 && image.GetPixel(i, j).G <= 245)
                    {
                        bottom_X = i + 1;
                        check = true;
                        break;
                    }
                }
                if (check)
                    break;
            }

            return new Tuple<int, int, int, int>(top_X, top_Y, bottom_X, bottom_Y);
        }

        public static Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public static void ConvertImageToBinaryMatrix(Bitmap bitmap, int[,] binaryMatrix)
        {
            for (int i = 0; i < binaryMatrix.GetLength(0); ++i)
            {
                for (int j = 0; j < binaryMatrix.GetLength(1); ++j)
                {
                    if (bitmap.GetPixel(j, i).R <= 245 && bitmap.GetPixel(j, i).B <= 245 && bitmap.GetPixel(j, i).G <= 245) // Image(rong, cao) >< Arr[cao, rong]
                    {
                        binaryMatrix[i, j] = 1;
                    }
                    else
                    {
                        binaryMatrix[i, j] = 0;
                    }
                }
            }
        }

        public static Bitmap ResizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);

            g.InterpolationMode = InterpolationMode.Bicubic;
            // Specify here
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return b;
        }
    }
}
