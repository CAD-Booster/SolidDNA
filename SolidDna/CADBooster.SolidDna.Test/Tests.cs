using Moq;
using NUnit.Framework;

namespace CADBooster.SolidDna.Test
{
    [TestFixture]
    public class Tests
    {
        private Mock<ISolidWorksApplication> _mockApplication;

        [SetUp]
        public void Setup()
        {
            _mockApplication = new Mock<ISolidWorksApplication>();
            SolidWorksEnvironment.SetApplicationForTesting(_mockApplication.Object);
        }

        [TearDown]
        public void TearDown() => SolidWorksEnvironment.ResetApplicationForTesting();

        [Test]
        public void ActiveModel_WhenMocked_ReturnsNull()
        {
            // Arrange
            _mockApplication.Setup(x => x.ActiveModel).Returns((Model)null);

            // Act
            var model = SolidWorksEnvironment.IApplication.ActiveModel;

            // Assert
            Assert.That(model, Is.Null);
        }

        [Test]
        public void SolidWorksCookie_WhenMocked_ReturnsExpectedValue()
        {
            // Arrange
            _mockApplication.Setup(x => x.SolidWorksCookie).Returns(12345);

            // Act
            var cookie = SolidWorksEnvironment.IApplication.SolidWorksCookie;

            // Assert
            Assert.That(cookie, Is.EqualTo(12345));
        }

        [Test]
        public void ShowMessageBox_WhenCalled_InvokesMockMethod()
        {
            // Arrange
            _mockApplication
                .Setup(x => x.ShowMessageBox(
                    It.IsAny<string>(),
                    It.IsAny<SolidWorksMessageBoxIcon>(),
                    It.IsAny<SolidWorksMessageBoxButtons>()))
                .Returns(SolidWorksMessageBoxResult.Ok);

            // Act
            var result = SolidWorksEnvironment.IApplication.ShowMessageBox("Test message");

            // Assert
            Assert.That(result, Is.EqualTo(SolidWorksMessageBoxResult.Ok));
            _mockApplication.Verify(x => x.ShowMessageBox(
                "Test message",
                SolidWorksMessageBoxIcon.Information,
                SolidWorksMessageBoxButtons.Ok), Times.Once);
        }
    }
}
