using System.IO;
using Dane;
using NUnit.Framework;

namespace DaneTests;

public class DataSerializerTests
{
    private DataSerializer serializer;
    [SetUp]
    public void Setup()
    {
        serializer = new DataSerializer(new DaneApi(200, 200));
    }

    [Test]
    public void SerializeTests()
    {
        Assert.That(serializer.Serialize(), Is.TypeOf(typeof(MemoryStream)));
    }
}