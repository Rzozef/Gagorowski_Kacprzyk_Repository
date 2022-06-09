using System;
using System.IO;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataSerializerTests
{
    private DataSerializer serializer;
    private DaneAbstractApi api;
    private BallAbstract ball;
    [SetUp]
    public void Setup()
    {
        api = new DaneApi(200, 200);
        serializer = new DataSerializer(api);
        ball = api.CreateBall();
    }

    [Test]
    public void SerializeTests()
    {
        Assert.That(serializer.Serialize(ball), Is.TypeOf(typeof(String)));
    }
}