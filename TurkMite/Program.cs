using OpenCvSharp;
using System;

namespace Turkmite
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var turkmite = new ThreeColorTurkmite(img);
            for(int i=0; i<turkmite.PreferredIterationCount; i++)
                turkmite.Step();
            Cv2.ImShow("Turkmite", turkmite.Image);
            Cv2.WaitKey();
        }
    }
}
