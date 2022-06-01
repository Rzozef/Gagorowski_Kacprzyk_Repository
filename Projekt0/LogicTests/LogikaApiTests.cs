using System.Numerics;
using Dane;
using NUnit.Framework;
using Logika;
using BallAbstract = Dane.BallAbstract;

namespace LogicTests;

public class LogikaApiTests
{
    private LogikaAbstractApi api;
    private BallAbstract ball;
    [SetUp]
    public void Setup()
    {
        api = LogikaAbstractApi.CreateApi(20, 20);
        ball = BallAbstract.CreateBall(1, 1, 5, 5,new Vector2(5, 5), DaneAbstractApi.CreateApi(20, 20));
    }

    [Test]
    public void GetCollidingBallsTests()
    {
        api.CreateBalls(400);
        Assert.Greater(api.GetCollidingBalls(ball).Count, 0);
    }
}