using OpenCvSharp;
using System;

namespace Turkmite
{
    public abstract class TurkmiteBase
    {
        public Mat Image { get; }
        public abstract int PreferredIterationCount { get; }
        private Mat.Indexer<Vec3b> indexer;
        private int x;
        private int y;
        private int direction;  // 0 up, 1 right, 2 down, 3 left
        public TurkmiteBase(Mat image)
        {
            Image = image;
            x = image.Cols / 2;
            y = image.Rows / 2;
            direction = 0;
            indexer = image.GetGenericIndexer<Vec3b>();
        }

        readonly private (int x, int y)[] delta = new(int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };

        public void Step()
        {
            int deltaDirection;
            (indexer[y, x], deltaDirection) =
                GetNextColorAndUpdateDirection(indexer[y, x]);
            PerformMove(deltaDirection);
        }

        private void PerformMove(int deltaDirection)
        {
            direction += deltaDirection;
            direction = (direction + 4) % 4;
            x += delta[direction].x;
            y += delta[direction].y;
            x = Math.Max(0, Math.Min(Image.Cols, x));
            y = Math.Max(0, Math.Min(Image.Rows, y));
        }

        protected abstract (Vec3b newColor, int deltaDirection) GetNextColorAndUpdateDirection(Vec3b currentColor);
    }
}
