using NUnit.Framework;
using Logika;

namespace BallTests
{
    public class Tests
    {
        private Ball test_ball1;
        [SetUp]
        public void Setup()
        {
            test_ball1 = new Ball(100, 100, 20, 10, 10);
        }

        [Test]
        public void UpdatingPositionTests()
        {
            Assert.AreEqual(100, test_ball1.x);
            Assert.AreEqual(100, test_ball1.y);
            test_ball1.UpdatePosition();
            Assert.AreEqual(110, test_ball1.x);
            Assert.AreEqual(110, test_ball1.y);
        }

        [Test]
        public void ChangingDirectionTests()
        {
            Assert.AreEqual(100, test_ball1.x);
            Assert.AreEqual(100, test_ball1.y);
            test_ball1.ChangeDirection('x');
            test_ball1.UpdatePosition();
            Assert.AreEqual(90, test_ball1.x);
            Assert.AreEqual(110, test_ball1.y);
            test_ball1.ChangeDirection('y');
            test_ball1.UpdatePosition();
            Assert.AreEqual(80, test_ball1.x);
            Assert.AreEqual(100, test_ball1.y);
        }
    }
}