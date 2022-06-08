using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataWriterTests
{
    private DataWriter writer;
    private DaneAbstractApi api;
    private IList<BallAbstract> balls;
    
    [SetUp]
    public void Setup()
    {
        balls = new List<BallAbstract>();
        api = new DaneApi(200, 200);
        writer = new DataWriter("testDirectory", "testFile", balls);
    }

    [Test]
    public void WriteDataTests()
    {
        balls.Add(api.CreateBall());
        balls.Add(api.CreateBall());
        writer.WriteBallsPosition("testData", balls[0]);
        Assert.IsNotEmpty(Directory.GetCurrentDirectory() + "/testDirectory" + "/testFile0");
    }
}