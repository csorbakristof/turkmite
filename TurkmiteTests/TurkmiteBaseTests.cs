using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using Turkmite;

namespace TurkmiteTests
{
    [TestClass]
    public class TurkmiteBaseTests
    {
        private TestTurkmiteBase turkmite = new TestTurkmiteBase(new Mat(10, 10, MatType.CV_8UC3));

        [TestMethod]
        public void GetNextColorAndUpdateDirection_IsCalled()
        {
            turkmite.Step();
            Assert.IsTrue(turkmite.GetNextColorAndUpdateDirectionInvoked);
        }

        [TestMethod]
        public void PerformMove_DirectionCorrect()
        {
            turkmite.X = 5;
            turkmite.Y = 5;
            turkmite.D = 0;
            turkmite.PerformMove(0);
            Assert.AreEqual(5, turkmite.X);
            Assert.AreEqual(4, turkmite.Y);
            Assert.AreEqual(0, turkmite.D);
        }

        class TestTurkmiteBase : TurkmiteBase
        {
            public int X { get { return this.x; } set { this.x = value; } }
            public int Y { get { return this.y; } set { this.y = value; } }
            public int D { get { return this.direction; } set { this.direction = value; } }
            public bool GetNextColorAndUpdateDirectionInvoked = false;

            public new void PerformMove(int deltaDirection)
            {
                base.PerformMove(deltaDirection);
            }

            protected override (Vec3b newColor, int deltaDirection) GetNextColorAndUpdateDirection(Vec3b currentColor)
            {
                // Mocked to monitor invocation
                GetNextColorAndUpdateDirectionInvoked = true;
                return (new Vec3b(0,0,0), 0);
            }

            public TestTurkmiteBase(Mat img) : base(img)
            {
            }

            public override int PreferredIterationCount => 0;
        }
    }
}
