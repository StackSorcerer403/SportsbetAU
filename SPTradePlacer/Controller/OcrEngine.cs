using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using AutoIt;

namespace BettingBot.Controller
{
    public class OcrEngine
    {
        private static OcrEngine _instance = null;
        public static OcrEngine instance
        {
            get
            {
                return _instance;
            }
        }
        static public void CreateInstance()
        {
            _instance = new OcrEngine();
        }
        public void doClickText(string targetWord)
        {
            try
            {
                var screenshot = BrowserCtrl.instance.GetScreenShot();
                if (screenshot == null) return;
                using (var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default))
                {
                    using (var img = PixConverter.ToPix(screenshot))
                    {
                        using (var page = engine.Process(img))
                        {
                            using (var iterator = page.GetIterator())
                            {
                                iterator.Begin();
                                do
                                {
                                    string word = iterator.GetText(PageIteratorLevel.Word);

                                    if (word != null && word.Equals(targetWord, StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (iterator.TryGetBoundingBox(PageIteratorLevel.Word, out var rect))
                                        {
                                            Console.WriteLine($"Found '{targetWord}' at: x={rect.X1}, y={rect.Y1}, width={rect.Width}, height={rect.Height}");

                                        }
                                    }
                                } while (iterator.Next(PageIteratorLevel.Word));
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public Bitmap CaptureScreen()
        {

            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                         Screen.PrimaryScreen.Bounds.Y,
                                         0, 0,
                                         Screen.PrimaryScreen.Bounds.Size,
                                         CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }
    }
}
