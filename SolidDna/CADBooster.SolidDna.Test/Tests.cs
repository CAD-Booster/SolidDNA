using NUnit.Framework;
using Moq;

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
        public void Application_Version()
        {
            var application = new Mock<ISolidWorksApplication>();
            application.Setup(a => a.SolidWorksVersion).Returns(new SolidWorksVersion("32.2.0", "sw2024_SP20", "d240722.003", ""));

            AddInIntegration.SetApplicationForTesting(application.Object);

            var version = SolidWorksEnvironment.Application.SolidWorksVersion;
            Assert.That(version.BuildNumber, Is.EqualTo("d240722.003"));
            Assert.That(version.Hotfix, Is.EqualTo(""));
            Assert.That(version.Revision, Is.EqualTo("sw2024_SP20"));
            Assert.That(version.RevisionNumber, Is.EqualTo("32.2.0"));
            Assert.That(version.ServicePackMajor, Is.EqualTo(2));
            Assert.That(version.ServicePackMinor, Is.EqualTo(0));
            Assert.That(version.Version, Is.EqualTo(2024));
        }
    }
}
