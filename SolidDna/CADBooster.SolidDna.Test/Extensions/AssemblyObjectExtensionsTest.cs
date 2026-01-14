using NUnit.Framework;
using System;

namespace CADBooster.SolidDna.Test;

[TestFixture]
internal class AssemblyObjectExtensionsTest
{
    [Test]
    [Obsolete("Obsolete")]
    public void AssemblyPath()
    {
        var directoryPath = new SolidDnaError().AssemblyPath();

        Assert.That(directoryPath, Does.EndWith("SolidDna\\CADBooster.SolidDna\\bin\\Debug\\net48"));
    }

    [Test]
    public void AssemblyDirectoryPath()
    {
        var directoryPath = new SolidDnaError().AssemblyDirectoryPath();

        Assert.That(directoryPath, Does.EndWith("SolidDna\\CADBooster.SolidDna\\bin\\Debug\\net48"));
    }

    [Test]
    public void AssemblyFilePath_Object()
    {
        var filePath = new SolidDnaError().AssemblyFilePath();

        Assert.That(filePath, Does.EndWith("SolidDna\\CADBooster.SolidDna\\bin\\Debug\\net48\\CADBooster.SolidDna.dll"));
    }

    [Test]
    public void AssemblyFilePath_Type()
    {
        var filePath = typeof(SolidDnaError).AssemblyFilePath();

        Assert.That(filePath, Does.EndWith("SolidDna\\CADBooster.SolidDna\\bin\\Debug\\net48\\CADBooster.SolidDna.dll"));
    }
}