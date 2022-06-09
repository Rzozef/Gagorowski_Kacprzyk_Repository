using NUnit.Framework;
using System.Numerics;
using System.Threading.Tasks;
using Dane;

namespace DaneTests;

public class BallTests
{
    private DaneAbstractApi dane;
    private BallAbstract test_ball;
    private readonly float test_x = 10;
    private readonly float test_y = 10;
    private readonly float test_size = 10;
    private readonly float test_mass = 10;

    [SetUp]
    public void Setup()
    {
        dane = DaneAbstractApi.CreateApi(200, 200);
        test_ball = BallAbstract.CreateBall(test_x, test_y, test_size, test_mass, new Vector2(5, 5));
    }

    [Test]
    public void CreatingBallTests()
    {
        Assert.AreEqual(test_x, test_ball.Position.X);
        Assert.AreEqual(test_y, test_ball.Position.Y);
        Assert.AreEqual(test_size, test_ball.Size);
        Assert.AreEqual(test_mass, test_ball.Mass);
    }

    [Test]
    public void SetPositiveTests()
    {
        test_ball.Position = new Vector2(1, 1);
        test_ball.Speed = new Vector2(1, 1);
        Assert.AreEqual(test_ball.Position.X, 1);
        Assert.AreEqual(test_ball.Speed.X, 1);
        Assert.AreEqual(test_ball.Position.Y, 1);
        Assert.AreEqual(test_ball.Speed.Y, 1);
    }
}