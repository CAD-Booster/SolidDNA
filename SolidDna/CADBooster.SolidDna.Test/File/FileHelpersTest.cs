using NUnit.Framework;

namespace CADBooster.SolidDna.Test.File;

[TestFixture]
internal class FileHelpersTest
{
    [Test]
    public void CodeBaseNormalized_ReturnsDllFolder()
    {
        var type = typeof(PlugInIntegration);

        var dllFolder = type.CodeBaseNormalized();

        Assert.That(dllFolder, Does.EndWith("SolidDna\\CADBooster.SolidDna\\bin\\Debug\\net48"));
        Assert.That(dllFolder, Does.Not.Contain("file:"));
        Assert.That(dllFolder, Does.Not.Contain("/"));
    }
}