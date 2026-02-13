using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// Handles SolidWorks-specific COM disposal based on the type of object
/// </summary>
public static class SolidDnaObjectDisposal
{
    /// <summary>
    /// Disposes specific SolidWorks COM objects based on their type. Only handles the task pane view for now.
    /// </summary>
    /// <typeparam name="T">COM object type</typeparam>
    /// <param name="comObject">The COM object to dispose</param>
    public static void Dispose<T>(object comObject)
    {
        // Taskpane View
        if (typeof(T).IsInterfaceOrHasInterface<ITaskpaneView>()) 
            ((ITaskpaneView) comObject).DeleteView();
    }
}