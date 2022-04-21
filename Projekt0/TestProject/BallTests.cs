using NUnit.Framework;
using Logika;

namespace Logika
{
    namespace Tests
    {
        public class BallsTests
        {
            private BallAbstract test_ball1;
            [SetUp]
            public void Setup()
            {
                test_ball1 = BallAbstract.CreateApi(100, 100, 20);
            }

            [Test]
            public void UpdatingPositionTests()
            {
                Assert.AreEqual(100, test_ball1.X);
                Assert.AreEqual(100, test_ball1.Y);
                test_ball1.UpdatePosition(Direction.UP);
                Assert.AreEqual(100, test_ball1.X);
                Assert.AreEqual(101, test_ball1.Y);
                test_ball1.UpdatePosition(Direction.DOWN);
                Assert.AreEqual(100, test_ball1.X);
                Assert.AreEqual(100, test_ball1.Y);
                test_ball1.UpdatePosition(Direction.LEFT);
                Assert.AreEqual(99, test_ball1.X);
                Assert.AreEqual(100, test_ball1.Y);
                test_ball1.UpdatePosition(Direction.RIGHT);
                Assert.AreEqual(100, test_ball1.X);
                Assert.AreEqual(100, test_ball1.Y);
            }
        }
    }
}