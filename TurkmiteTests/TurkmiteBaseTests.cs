using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using Turkmite;

namespace TurkmiteTests
{
    [TestClass]
    public class TurkmiteBaseTests
    {
        [TestMethod]
        public void GetNextColorAndUpdateDirection_IsCalled()
        {
            var t = new TestTurkmiteBase(new Mat(10,10,MatType.CV_8UC3));
            t.Step();
            Assert.IsTrue(t.GetNextColorAndUpdateDirectionInvoked);
        }

        class TestTurkmiteBase : TurkmiteBase
        {
            public bool GetNextColorAndUpdateDirectionInvoked = false;


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
