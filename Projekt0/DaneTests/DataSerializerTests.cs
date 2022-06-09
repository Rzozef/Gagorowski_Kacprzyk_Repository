using System;
using System.IO;
using System.Numerics;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataSerializerTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CreateSerializerTests()
    {
        Assert.That(DataSerializer.Instance, Is.TypeOf(typeof(DataSerializer)));
    }
}