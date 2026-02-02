namespace CADBooster.SolidDna;

internal class CommandContextItemCreateInfo(int solidWorksCookie, string path) : CommandContextCreateInfoBase(solidWorksCookie)
{
    public string Path { get; } = path ?? throw new SolidDnaException(
        SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
            SolidDnaErrorCode.SolidWorksCommandManagerError,
            "Context menu item path cannot be null"));
}
