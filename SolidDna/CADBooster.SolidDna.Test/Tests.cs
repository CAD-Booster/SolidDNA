using NUnit.Framework;

namespace CADBooster.SolidDna.Test
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Test1()
        {
            var model = SolidWorksEnvironment.Application.ActiveModel;

            Assert.Pass();
        }
    }
}
