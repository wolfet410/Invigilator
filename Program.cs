using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Invigilator
{
    class Program
    {
        static void Main()
        {
            SaveScreenShot(@"c:\temp\test.jpg", ImageFormat.Jpeg);
            ShrinkImage(@"c:\temp\test.jpg");

            // Screenshots work, but now I need to trigger them when appropriate
            // Considering using the mouse, but it's tricky
            // See: http://stackoverflow.com/questions/11607133/global-mouse-event-handler
            // Timers would also work, of course, but I only want to get data when a user is using the computer

        }

        static void SaveScreenShot(string filename, ImageFormat format)
        {
            Bitmap screenShot = CaptureScreenShot();
            screenShot.Save(filename, format);
        }

        static Bitmap CaptureScreenShot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            }
            return bitmap;
        }

        static void ShrinkImage(string filename)
        {
            Image originalImage;
            originalImage = Image.FromFile(filename);

            Image smallerImage;
            smallerImage = originalImage.GetThumbnailImage(Convert.ToInt32(originalImage.Width * .7), Convert.ToInt32(originalImage.Height * .7), new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            originalImage.Dispose();
            smallerImage.Save(filename, ImageFormat.Jpeg);
            smallerImage.Dispose();
        }

        static bool ThumbnailCallback()
        {
            return true;
        }
    }
}
