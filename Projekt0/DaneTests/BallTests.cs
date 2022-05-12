using NUnit.Framework;
using System.Numerics;
using Dane;

namespace DaneTests;

public class BallTests
{
    private BallAbstract test_ball;
    [SetUp]
    public void Setup()
    {
        test_ball = BallAbstract.CreateBall(10, 10, 10, 10, new Vector2(5, 5));
    }

    [Test]
    public void MoveTests()
    {
        Assert.AreEqual(10, test_ball.X);
        Assert.AreEqual(10, test_ball.Y);
        test_ball.Move();
        Assert.AreEqual(15, test_ball.X);
        Assert.AreEqual(15, test_ball.Y);
    }
}