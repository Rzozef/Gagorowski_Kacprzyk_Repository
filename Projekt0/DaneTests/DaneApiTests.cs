using NUnit.Framework;
using Dane;

namespace DaneTests;

public class DaneApiTests
{
    private DaneAbstractApi api;
    [SetUp]
    public void Setup()
    {
        api = DaneAbstractApi.CreateApi(200, 200);
    }

    [Test]
    public void CreateBallsTests()
    {
        BallAbstract ball = api.CreateBall();
        Assert.Greater(ball.Size, 5);
        Assert.Less(ball.Size, 20);
        Assert.Greater(ball.Position.X, 0);
        Assert.Less(ball.Position.X, 200 - ball.Size);
        Assert.Greater(ball.Position.Y, 0);
        Assert.Less(ball.Position.Y, 200 - ball.Size);
        Assert.Greater(ball.Speed.X, -3.01);
        Assert.Less(ball.Speed.X, 3);
        Assert.Greater(ball.Speed.Y, -3.01);
        Assert.Less(ball.Speed.Y, 3);
    }
}