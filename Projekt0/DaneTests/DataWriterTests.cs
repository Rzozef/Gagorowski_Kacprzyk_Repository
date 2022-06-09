using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataWriterTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void CreateWriterTests()
    {
        Assert.That(DataWriter.Instance, Is.TypeOf(typeof(DataWriter)));
    }
}