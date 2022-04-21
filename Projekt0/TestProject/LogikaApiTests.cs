using NUnit.Framework;
using Logika;

namespace LogikaApiTests
{
    public class Tests
    {
        private LogikaAbstractApi api;
        private BallsRepositoryAbstract<BallAbstract> balls_copy;
        [SetUp]
        public void Setup()
        {
            api = LogikaAbstractApi.CreateApi(100, 100);
        }

        [Test]
        public void CreateBallsTests()
        {
            Assert.AreEqual(0, api.GetBalls().Count);
            api.CreateBalls(3);
            Assert.AreEqual(3, api.GetBalls().Count);
        }

        public void MoveBallsTests()
        {
            api.CreateBalls(3);
            for(int i = 0; i < api.GetBalls().Count; i++)
            {
                balls_copy.Add(api.GetBalls()[i]);
            }
            
            api.MoveBalls();
            for (int i = 0; i < api.GetBalls().Count; i++)
                if (api.GetBalls()[i].X == balls_copy[i].X)
                {
                    Assert.AreNotEqual(api.GetBalls()[i].Y, balls_copy[i].Y);
                }
                else
                {
                    Assert.AreNotEqual(api.GetBalls()[i].X, balls_copy[i].X);
                }
        }
    }
}