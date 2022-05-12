using NUnit.Framework;
using Dane;

namespace DaneTests;

public class DaneApiTests
{
    private DaneAbstractApi api;
    [SetUp]
    public void Setup()
    {
        api = DaneAbstractApi.CreateApi(100, 100);
    }

    [Test]
    public void CreateBallsTests()
    {
        Assert.AreEqual(api.GetBalls().Count, 0);
        api.CreateBalls(3);
        Assert.AreEqual(api.GetBalls().Count, 3);
        api.CreateBalls(2);
        Assert.AreEqual(api.GetBalls().Count, 5);
    }
    [Test]
    public void GetCollidingBallsTests()
    {
        api.CreateBalls(30);
        Assert.AreEqual(30, api.GetBalls().Count);
        //Assert.Greater(api.GetCollidingBalls(api.GetBalls()[0]).Count, 0);
    }
}