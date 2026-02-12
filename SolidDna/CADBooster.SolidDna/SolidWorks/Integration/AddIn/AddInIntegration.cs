using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CADBooster.SolidDna;

/// <summary>
/// Static class that handles integration between multiple add-ins and the SolidWorks application instance.
/// </summary>
public static class AddInIntegration
{
    #region Public Properties

    /// <summary>
    /// Represents the current SolidWorks application
    /// </summary>
    public static SolidWorksApplication SolidWorks { get; private set; }

    #endregion

    #region Private properties

    /// <summary>
    /// A list of all add-ins that are currently active.
    /// Private so we can call <see cref="TearDown"/> when the list becomes empty.
    /// </summary>
    private static List<SolidAddIn> ActiveAddIns { get; } = [];

    #endregion

    #region Stand Alone Methods

    /// <summary>
    /// Attempt to set the SolidWorks property to the active SolidWorks instance
    /// </summary>
    /// <returns></returns>
    public static bool ConnectToActiveSolidWorksForStandAlone()
    {
        try
        {
            // Try and get the active SolidWorks instance
            SolidWorks = new SolidWorksApplication((SldWorks) Marshal.GetActiveObject("SldWorks.Application"), 0);

            // Log it
            Logger.LogDebugSource($"Acquired active instance SolidWorks in Stand-Alone mode");

            // Return if successful
            return SolidWorks != null;
        }
        // If we failed to get active instance...
        catch (COMException)
        {
            // Log it
            Logger.LogDebugSource($"Failed to get active instance of SolidWorks in Stand-Alone mode");

            // Return failure
            return false;
        }
    }

    #endregion

    #region Add-in methods

    /// <summary>
    /// Store the active SolidWorks instance and its cookie.
    /// Remember to call <see cref="TearDown"/> once done.
    /// </summary>
    /// <returns></returns>
    public static void ConnectToActiveSolidWorksForAddIn(SldWorks solidworks, AddInCookie cookie)
    {
        if (SolidWorks != null)
        {
            Logger.LogDebugSource("SolidWorks instance was already set");
            return;
        }

        try
        {
            Logger.LogDebugSource($"Storing the SOLIDWORKS instance...");

            // Initialize the SolidDNA wrapper for the SolidWorks application
            SolidWorks = new SolidWorksApplication(solidworks, cookie.Value);

            Logger.LogDebugSource($"SolidWorks instance set");
        }
        catch (Exception e)
        {
            Logger.LogDebugSource("Failed to set active instance of SolidWorks in add-in mode", exception: e);
        }
    }

    #endregion

    #region List of active add-ins

    /// <summary>
    /// Add a newly loaded add-in to the list of active ones.
    /// </summary>
    /// <param name="addIn"></param>
    public static void AddAddIn(SolidAddIn addIn) => ActiveAddIns.Add(addIn);

    /// <summary>
    /// Remove an unloaded add-in from the list of active ones.
    /// </summary>
    /// <param name="addIn"></param>
    public static void RemoveAddInAndTearDownSolidWorksWhenLast(SolidAddIn addIn)
    {
        ActiveAddIns.Remove(addIn);

        // If the list is now empty, we tear down SOLIDWORKS
        if (!ActiveAddIns.Any())
            TearDown();
    }

    /// <summary>
    /// Get one of the active add-ins by its type.
    /// </summary>
    /// <param name="type">The type of the add-in that contains the new taskpane</param>
    /// <returns>Returns the first add-in with the requested name or null.</returns>
    public static SolidAddIn GetAddInWithType(Type type) => ActiveAddIns.FirstOrDefault(x => x.GetType() == type); // Get the first add-in of this type. Is null when not found.

    #endregion

    #region Tear Down

    /// <summary>
    /// Clean up the SolidWorks instance
    /// </summary>
    public static void TearDown()
    {
        // If we have a reference...
        if (SolidWorks != null)
        {
            // Log it
            Logger.LogDebugSource($"Disposing SolidWorks COM reference...");

            // Dispose SolidWorks COM
            SolidWorks?.Dispose();
        }

        // Set to null
        SolidWorks = null;
    }

    #endregion
}