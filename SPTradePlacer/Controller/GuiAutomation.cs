using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Controller
{
    internal class GuiAutomation
    {
        private static GuiAutomation _instance = null;
        public static GuiAutomation instance
        {
            get
            {
                return _instance;
            }
        }
        static public void CreateInstance()
        {
            _instance = new GuiAutomation();
        }

        public Point? FindImageOnScreen(Bitmap screen, Bitmap template)
        {
            Image<Bgr, byte> screenImage = screen.ToImage<Bgr, byte>();
            Image<Bgr, byte> templateImage = template.ToImage<Bgr, byte>();

            Mat result = new Mat();
            CvInvoke.MatchTemplate(screenImage, templateImage, result, TemplateMatchingType.CcoeffNormed);

            double minVal = 0, maxVal = 0;
            Point minLoc = Point.Empty, maxLoc = Point.Empty;
            CvInvoke.MinMaxLoc(result, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

            if (maxVal >= 0.8)
            {
                return new Point(
                    maxLoc.X + template.Width/2,
                    maxLoc.Y + template.Height/2
                    );

            }
            return null;
        }

    }
}
