using NUnit.Framework;
using System.Numerics;
using System.Threading.Tasks;
using Dane;

namespace DaneTests;

public class BallTests
{
    private DaneAbstractApi dane;
    private BallAbstract test_ball;
    [SetUp]
    public void Setup()
    {
        dane = DaneAbstractApi.CreateApi(200, 200);
        test_ball = BallAbstract.CreateBall(10, 10, 10, 10, new Vector2(5, 5), dane);
    }

    [Test]
    public void MoveTests()
    {
        Assert.AreEqual(10, test_ball.Position.X);
        Assert.AreEqual(10, test_ball.Position.Y);
        
    }
}