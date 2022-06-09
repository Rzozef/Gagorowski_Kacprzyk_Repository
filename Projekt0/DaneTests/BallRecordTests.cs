using System;
using System.Numerics;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class BallRecordTests
{
    private BallAbstract ball;
    private BallRecord record;
    [SetUp]
    public void Setup()
    {
        ball = BallAbstract.CreateBall(10, 10, 10, 10, new Vector2(10, 10));
        record = new BallRecord(ball, DateTime.Now);
    }

    [Test]
    public void CreateRecordTests()
    {
        Assert.AreEqual(record.X, 10);
        Assert.AreEqual(record.Y, 10);
        Assert.AreEqual(record.Speed, new Vector2(10, 10));
    }
}