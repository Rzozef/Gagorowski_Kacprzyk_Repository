using NUnit.Framework;

using Prezentacja.Model;

namespace Prezentacja
{
    namespace Model
    {
        namespace Tests
        {
            public class BallAbstractTests
            {
                private BallAbstract _ballParent;
                private BallAbstract _ballChild;
                [SetUp]
                public void Setup()
                {
                    _ball = BallAbstract.CreateBall();
                }

                [Test]
                public void Test1()
                {
                    Assert.Pass();
                }
            }
        }
    }
}