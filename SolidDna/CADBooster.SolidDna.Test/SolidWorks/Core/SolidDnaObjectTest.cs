using Moq;
using NUnit.Framework;
using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna.Test
{
    [TestFixture]
    internal class SolidDnaObjectTest
    {
        [Test]
        public void Constructor_WithoutT_SetsNothing()
        {
            var solidDnaObject = new SolidDnaObject();

            Assert.That(solidDnaObject, Is.Not.Null);
            Assert.That(solidDnaObject.UnsafeObject, Is.Null);
        }

        [Test]
        public void Constructor_FromNull_DoesNotThrow()
        {
            var solidDnaObject = new SolidDnaObject<object>(null);

            Assert.That(solidDnaObject, Is.Not.Null);
            Assert.That(solidDnaObject.UnsafeObject, Is.Null);
        }

        [Test]
        public void CreateOrNull_NonNullInput_ReturnsObject()
        {
            var feature = new Mock<Feature>();

            var solidDnaObject = new SolidDnaObject<Feature>(feature.Object);

            Assert.That(solidDnaObject, Is.Not.Null);
            Assert.That(solidDnaObject.UnsafeObject, Is.Not.Null);
        }

        [Test]
        public void CreateOrNull_NonNullInput2_ReturnsObject()
        {
            var note = new Mock<INote>();

            var solidDnaObject = new SolidDnaObject<INote>(note.Object);

            Assert.That(solidDnaObject, Is.Not.Null);
            Assert.That(solidDnaObject.UnsafeObject, Is.Not.Null);
        }

        [Test]
        public void Dispose_SkipsNonComObject()
        {
            var note = new Mock<INote>();
            var solidDnaObject = new SolidDnaObject<INote>(note.Object);

            Assert.That(solidDnaObject, Is.Not.Null);
            Assert.That(solidDnaObject.UnsafeObject, Is.Not.Null);

            solidDnaObject.Dispose();

            Assert.That(solidDnaObject.UnsafeObject, Is.Null);
        }

        [Test]
        public void Dispose_CallTwice_SkipsSecond()
        {
            var note = new Mock<INote>();
            var solidDnaObject = new SolidDnaObject<INote>(note.Object);

            solidDnaObject.Dispose();
            solidDnaObject.Dispose();

            Assert.That(solidDnaObject.UnsafeObject, Is.Null);
        }
    }
}
