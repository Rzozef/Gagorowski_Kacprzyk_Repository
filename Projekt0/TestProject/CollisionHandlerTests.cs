using System.Numerics;
using Dane;
using NUnit.Framework;
using Logika;
using BallAbstract = Dane.BallAbstract;

namespace LogikaTest;

public class CollisionHandlerTests
{
    private CollisionHandler handler;
    private DaneAbstractApi dane;
    private BallAbstract ball1;
    private BallAbstract ball2;
    private BallAbstract ball3;
    private BallAbstract ball4;
    [SetUp]
    public void Setup()
    {
        dane = DaneAbstractApi.CreateApi(200, 200);
        handler = new CollisionHandler(200, 200, DaneAbstractApi.CreateApi(200, 200));
        ball1 = BallAbstract.CreateBall(-1, 1, 5, 5, new Vector2(2, 2), dane);
        ball2 = BallAbstract.CreateBall(5, 1, 5, 5, new Vector2(2, 2), dane);
        ball3 = BallAbstract.CreateBall(1, -1, 5, 5, new Vector2(2, 2), dane);
        ball4 = BallAbstract.CreateBall(1, 5, 5, 5, new Vector2(1, 2), dane);
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
    [Test]
    public void HandleBallsCollisionTests()
    {
        handler.HandleBallsCollision(ball2);
        Assert.AreNotEqual(ball2.Speed, new Vector2(2, 2));
    }
}
