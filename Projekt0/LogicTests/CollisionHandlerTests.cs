using System.Numerics;
using Dane;
using NUnit.Framework;
using Logika;
using BallAbstract = Dane.BallAbstract;

namespace LogicTests;

public class Tests
{
    private CollisionHandler handler;
    private DaneAbstractApi api;
    private BallAbstract ball1;
    private BallAbstract ball2;
    private BallAbstract ball3;
    private BallAbstract ball4;
    [SetUp]
    public void Setup()
    {
        api = DaneAbstractApi.CreateApi(200, 200);
        handler = new CollisionHandler(200, 200, api);
        ball1 = BallAbstract.CreateBall(-1, 1, 5, 5, new Vector2(2, 2), api);
        ball2 = BallAbstract.CreateBall(5, 1, 5, 5, new Vector2(2, 2), api);
        ball3 = BallAbstract.CreateBall(1, -1, 5, 5, new Vector2(2, 2), api);
        ball4 = BallAbstract.CreateBall(1, 5, 5, 5, new Vector2(1, 2), api);
    }

    [Test]
    public void HandleBorderCollisionTests()
    {
        handler.HandleBorderCollision(ball1);
        Assert.AreEqual(ball1.X, 1);
        Assert.AreEqual(ball1.Y, 1);
        handler.HandleBorderCollision(ball2);
        Assert.AreEqual(ball2.X, 5);
        Assert.AreEqual(ball2.Y, 1);
        handler.HandleBorderCollision(ball3);
        Assert.AreEqual(ball3.X, 1);
        Assert.AreEqual(ball3.Y, 1);
        handler.HandleBorderCollision(ball4);
        Assert.AreEqual(ball4.X, 1);
        Assert.AreEqual(ball4.Y, 5);
    }
}