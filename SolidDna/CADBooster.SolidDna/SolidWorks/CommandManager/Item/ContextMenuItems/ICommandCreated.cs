using System;

namespace CADBooster.SolidDna
{
    public interface ICommandCreated : IDisposable
    {
        string Name { get; }
    }
}
