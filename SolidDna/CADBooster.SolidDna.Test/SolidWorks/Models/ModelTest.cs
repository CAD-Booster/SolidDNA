using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna.Test.SolidWorks.Models
{
    [TestFixture]
    internal class ModelTest
    {
        private Mock<ISolidWorksApplication> _mockApplication;
        private Mock<ModelDoc2> _mockModelDoc;
        private Mock<ModelDocExtension> _mockExtension;
        private Mock<SelectionMgr> _mockSelectionMgr;
        private Mock<Configuration> _mockConfiguration;

        [SetUp]
        public void Setup()
        {
            // Mock the application
            _mockApplication = new Mock<ISolidWorksApplication>();
            _mockApplication.Setup(x => x.SolidWorksVersion).Returns(new SolidWorksVersion("32.0.0", "sw2024_SP00", "d240101.001", ""));
            SolidWorksEnvironment.SetApplicationForTesting(_mockApplication.Object);

            // Mock the COM objects that Model needs
            _mockModelDoc = new Mock<ModelDoc2>();
            _mockExtension = new Mock<ModelDocExtension>();
            _mockSelectionMgr = new Mock<SelectionMgr>();
            _mockConfiguration = new Mock<Configuration>();

            // Setup basic ModelDoc2 behavior
            _mockModelDoc.Setup(x => x.Extension).Returns(_mockExtension.Object);
            _mockModelDoc.Setup(x => x.ISelectionManager).Returns(_mockSelectionMgr.Object);
            _mockModelDoc.Setup(x => x.IGetActiveConfiguration()).Returns(_mockConfiguration.Object);
            _mockModelDoc.Setup(x => x.GetPathName()).Returns(@"C:\Test\Part.sldprt");
            _mockModelDoc.Setup(x => x.GetType()).Returns((int)swDocumentTypes_e.swDocPART);
            _mockModelDoc.Setup(x => x.GetConfigurationCount()).Returns(1);
            _mockModelDoc.Setup(x => x.GetConfigurationNames()).Returns(new[] { "Default" });

            // Set up the interface casts that Model uses for event handlers
            // ModelDoc2 can be cast to PartDoc, AssemblyDoc, or DrawingDoc
            _mockModelDoc.As<PartDoc>();
            _mockModelDoc.As<AssemblyDoc>();
            _mockModelDoc.As<DrawingDoc>();
        }

        [TearDown]
        public void TearDown() => SolidWorksEnvironment.ResetApplicationForTesting();

        [Test]
        public void Model_CannotBeMockedDirectly_BecauseMethodsAreNotVirtual()
        {
            // Model cannot be mocked directly with Moq because its methods are not virtual.
            // Users should mock IModel instead, or create a Model with a mocked ModelDoc2.

            // This works - mock the interface:
            var mockModel = new Mock<IModel>();
            mockModel.Setup(x => x.IsPart).Returns(true);
            mockModel.Setup(x => x.FilePath).Returns(@"C:\Test\Part.sldprt");

            Assert.That(mockModel.Object.IsPart, Is.True);
            Assert.That(mockModel.Object.FilePath, Is.EqualTo(@"C:\Test\Part.sldprt"));
        }

        [TestCase(swDocumentTypes_e.swDocPART, false, false, true)]
        [TestCase(swDocumentTypes_e.swDocASSEMBLY, true, false, false)]
        [TestCase(swDocumentTypes_e.swDocDRAWING, false, true, false)]
        public void Model_WhenCreatedWithPart_SetsIsPart(swDocumentTypes_e docType, bool expectAssembly, bool expectDrawing, bool expectPart)
        {
            _mockModelDoc.Setup(x => x.GetType()).Returns((int)docType);
            var model = new Model(_mockModelDoc.Object);
            Assert.That(model.IsAssembly, Is.EqualTo(expectAssembly));
            Assert.That(model.IsDrawing, Is.EqualTo(expectDrawing));
            Assert.That(model.IsPart, Is.EqualTo(expectPart));
        }

        [Test]
        public void HasBeenSaved_WhenFilePathEmpty_ReturnsFalse()
        {
            _mockModelDoc.Setup(x => x.GetPathName()).Returns(string.Empty);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.HasBeenSaved, Is.False);
        }

        [Test]
        public void ConfigurationCount_ReturnsValueFromModelDoc()
        {
            _mockModelDoc.Setup(x => x.GetConfigurationCount()).Returns(5);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.ConfigurationCount, Is.EqualTo(5));
        }

        [Test]
        public void ConfigurationNames_ReturnsNamesFromModelDoc()
        {
            var configNames = new[] { "Default", "Config1", "Config2" };
            _mockModelDoc.Setup(x => x.GetConfigurationNames()).Returns(configNames);
            _mockModelDoc.Setup(x => x.GetType()).Returns((int)swDocumentTypes_e.swDocPART);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.ConfigurationNames, Has.Count.EqualTo(3));
            Assert.That(model.ConfigurationNames, Contains.Item("Default"));
            Assert.That(model.ConfigurationNames, Contains.Item("Config1"));
        }

        [Test]
        public void ConfigurationNames_ForDrawing_ReturnsEmptyList()
        {
            _mockModelDoc.Setup(x => x.GetType()).Returns((int)swDocumentTypes_e.swDocDRAWING);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.ConfigurationNames, Is.Empty);
        }

        [Test]
        public void NeedsSaving_ReturnsValueFromModelDoc()
        {
            _mockModelDoc.Setup(x => x.GetSaveFlag()).Returns(true);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.NeedsSaving, Is.True);
        }

        [Test]
        public void ToString_ReturnsFormattedString()
        {
            _mockModelDoc.Setup(x => x.GetPathName()).Returns(@"C:\Test\Part.sldprt");
            _mockModelDoc.Setup(x => x.GetType()).Returns((int)swDocumentTypes_e.swDocPART);

            var model = new Model(_mockModelDoc.Object);

            Assert.That(model.ToString(), Is.EqualTo("Model type: Part. File path: C:\\Test\\Part.sldprt"));
        }

        [Test]
        public void ExampleBusinessLogic_WithMockedModel_WorksCorrectly()
        {
            var mockModel = new Mock<IModel>();
            mockModel.Setup(m => m.GetCustomProperty("PartNumber", null, false)).Returns("12345");
            mockModel.Setup(m => m.IsPart).Returns(true);

            var partNumber = GetPartNumber(mockModel.Object);

            Assert.That(partNumber, Is.EqualTo("12345"));
        }

        /// <summary>
        /// Example business logic method that can be tested with mocked IModel
        /// </summary>
        private static string GetPartNumber(IModel model) => !model.IsPart ? null : model.GetCustomProperty("PartNumber");
    }
}
