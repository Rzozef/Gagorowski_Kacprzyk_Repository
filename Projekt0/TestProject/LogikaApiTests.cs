using NUnit.Framework;
using Logika;

namespace LogikaApiTests
{
    public class Tests
    {
        private LogikaAbstractApi api;
        private BallsRepository<Ball> balls_copy;
        [SetUp]
        public void Setup()
        {
            api = LogikaAbstractApi.CreateApi(100, 100);
        }

        [Test]
        public void CreateBallsTests()
        {
            Assert.AreEqual(0, api._balls.Count);
            api.CreateBalls(3);
            Assert.AreEqual(3, api._balls.Count);
        }

        public void MoveBallsTests()
        {
            api.CreateBalls(3);
            for(int i = 0; i < api._balls.Count; i++)
            {
                balls_copy.Add(api._balls[i]);
            }
            
            api.MoveBalls();
            for (int i = 0; i < api._balls.Count; i++)
                if (api._balls[i].X == balls_copy[i].X)
                {
                    Assert.AreNotEqual(api._balls[i].Y, balls_copy[i].Y);
                }
                else
                {
                    Assert.AreNotEqual(api._balls[i].X, balls_copy[i].X);
                }
        }
    }
}