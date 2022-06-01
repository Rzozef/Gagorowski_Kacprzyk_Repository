using System.Numerics;
using Dane;
using Logika;
using NUnit.Framework;

namespace LogicTests;

public class CollisionHandlerTests
{
    private LogikaAbstractApi api;
    private CollisionHandler handler;
    private Dane.BallAbstract ball1;
    private Dane.BallAbstract ball2;
    [SetUp]
    public void Setup()
    {
        api = LogikaAbstractApi.CreateApi(20, 20);
        ball1 = Dane.BallAbstract.CreateBall(-5, 1, 5, 5,new Vector2(5, 5), DaneAbstractApi.CreateApi(20, 20));
        ball2 = Dane.BallAbstract.CreateBall(5, 5, 5, 5,new Vector2(5, 5), DaneAbstractApi.CreateApi(20, 20));
        handler = new CollisionHandler(20, 20, api);
    }
    
    [Test]
    public void HandleBorderCollisionTests()
    {
        handler.HandleBorderCollision(ball1);
        Assert.AreNotEqual(-5, ball1.X);
    }
    
    [Test]
    public void HandleBallsCollisionTests()
    {
        api.CreateBalls(200);
        handler.HandleBallsCollision(ball2);
        Assert.AreNotEqual(new Vector2(5,5), ball2.Speed);
    }
}