using NUnit.Framework;

namespace Logika
{
    namespace Tests
    {
/*        public class LogikaApiTests
        {
            private LogikaAbstractApi api;
            private BallsRepositoryApi<BallAbstract> balls_copy;
            [SetUp]
            public void Setup()
            {
                api = LogikaAbstractApi.CreateApi(100, 100);
                balls_copy = BallsRepositoryApi<BallAbstract>.CreateApi();
            }

            [Test]
            public void CreateBallsTests()
            {
                Assert.AreEqual(0, api.GetBalls().Count);
                api.CreateBalls(3);
                Assert.AreEqual(3, api.GetBalls().Count);
            }

            [Test]
            public void MoveBallsTests()
            {
                api.CreateBalls(3);
                for (int i = 0; i < api.GetBalls().Count; i++)
                {
                    var ball = api.GetBalls()[i];
                    BallAbstract ballClone = BallAbstract.CreateApi(ball.X, ball.Y, ball.Size);
                    balls_copy.Add(ballClone);
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
        }*/
    }
}