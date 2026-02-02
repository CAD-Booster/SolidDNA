namespace CADBooster.SolidDna;

internal class CommandContextCreateInfoBase(int solidWorksCookie) : ICommandContextCreateInfo
{
    public int SolidWorksCookie { get; } = solidWorksCookie > 0 
        ? solidWorksCookie 
        : throw new SolidDnaException(
            SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksCommandManager,
                SolidDnaErrorCode.SolidWorksCommandManagerError,
                $"Invalid SolidWorks cookie value: {solidWorksCookie}. Cookie must be positive"));
}
