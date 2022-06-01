using System.IO;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataWriterTests
{
    private DataWriter writer;
    [SetUp]
    public void Setup()
    {
        writer = new DataWriter("testDirectory", "testFile");
    }

    [Test]
    public void WriteDataTests()
    {
        writer.WriteBallsPosition(Stream.Null);
        Assert.IsNotEmpty(Directory.GetCurrentDirectory() + "/testDirectory" + "/testFile");
    }
}