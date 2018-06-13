﻿using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurkMite
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var turkmite = new TurkMite(img);
            for(int i=0; i<13000; i++)
            {
                turkmite.Step();
            }
            Cv2.ImShow("TurkMite", turkmite.Image);
            Cv2.WaitKey();
        }

        class TurkMite
        {
            public Mat Image { get; }
            private Mat.Indexer<Vec3b> indexer;
            private int x;
            private int y;
            private int direction;  // 0 up, 1 right, 2 down, 3 left
            public TurkMite(Mat image)
            {
                Image = image;
                x = image.Cols / 2;
                y = image.Rows / 2;
                direction = 0;
                indexer = image.GetGenericIndexer<Vec3b>();
            }

            readonly private Vec3b black = new Vec3b(0, 0, 0);
            readonly private Vec3b white = new Vec3b(255, 255, 255);
            readonly private (int x,int y)[] delta = new(int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };

            public void Step()
            {
                indexer[y, x] = GetNextColorAndUpdateDirection(indexer[y, x]);
                PerformMove();
            }

            private void PerformMove()
            {
                direction = (direction + 4) % 4;
                x += delta[direction].x;
                y += delta[direction].y;
                x = Math.Max(0, Math.Min(Image.Cols, x));
                y = Math.Max(0, Math.Min(Image.Rows, y));
            }

            private Vec3b GetNextColorAndUpdateDirection(Vec3b currentColor)
            {
                if (currentColor == black)
                {
                    direction++;
                    return white;
                }
                else
                {
                    direction--;
                    return black;
                }
            }

        }
    }
}
