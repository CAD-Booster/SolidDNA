using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna.SolidWorks.CommandManager.Item
{
    public interface ICommandCreatable
    {
        int UserId { get; }
        string Name { get; }

        IEnumerable<ICommandCreated> Create(string path = "");
    }
}
