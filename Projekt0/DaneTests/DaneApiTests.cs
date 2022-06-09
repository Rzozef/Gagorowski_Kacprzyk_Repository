using NUnit.Framework;
using Dane;

namespace DaneTests;

public class DaneApiTests
{
    private DaneAbstractApi api;
    private readonly uint test_width = 200;
    private readonly uint test_height = 200;
    [SetUp]
    public void Setup()
    {
        api = DaneAbstractApi.CreateApi(test_width, test_height);
    }

    [Test]
    public void CreateBallsTests()
    {
        BallAbstract ball = api.CreateBall();
        Assert.GreaterOrEqual(ball.Size, 5);
        Assert.Less(ball.Size, 20);
        Assert.GreaterOrEqual(ball.Position.X, 0);
        Assert.Less(ball.Position.X, 200 - ball.Size);
        Assert.GreaterOrEqual(ball.Position.Y, 0);
        Assert.Less(ball.Position.Y, 200 - ball.Size);
        Assert.GreaterOrEqual(ball.Speed.X, -3);
        Assert.Less(ball.Speed.X, 3);
        Assert.GreaterOrEqual(ball.Speed.Y, -3);
        Assert.Less(ball.Speed.Y, 3);
    }
}