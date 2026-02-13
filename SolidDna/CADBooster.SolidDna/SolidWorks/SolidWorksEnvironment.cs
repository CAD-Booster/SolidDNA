using System;

namespace CADBooster.SolidDna;

/// <summary>
/// A class providing shorthand access to commonly used values.
/// </summary>
public static class SolidWorksEnvironment
{
    #region Private Members

    /// <summary>
    /// Backing field for mock/test application.
    /// When set, overrides the real SolidWorks instance.
    /// </summary>
    private static ISolidWorksApplication mTestApplication;

    #endregion

    #region Public Properties

    /// <summary>
    /// The currently running instance of SolidWorks.
    /// Is being replaced by <see cref="IApplication"/> to support mocking for unit testing.
    /// Returns the concrete type for backward compatibility and returns null when a mock is set via <see cref="SetApplicationForTesting"/>
    /// </summary>
    [Obsolete("Use IApplication instead. This property will be removed in a future version.", false)]
    public static SolidWorksApplication Application => mTestApplication == null ? AddInIntegration.SolidWorks : null;

    /// <summary>
    /// The currently running instance of SolidWorks as an interface.
    /// Use this property when you need to support mocking in unit tests.
    /// </summary>
    public static ISolidWorksApplication IApplication => mTestApplication ?? AddInIntegration.SolidWorks;

    #endregion

    #region Public methods

    /// <summary>
    /// Set a mock or test application instance. Should not be called when SolidWorks is running and throws when you do so.
    /// When set, <see cref="IApplication"/> returns the mock and <see cref="Application"/> returns null.
    /// </summary>
    /// <param name="application">The mock application instance</param>
    public static void SetApplicationForTesting(ISolidWorksApplication application)
    {
        if (AddInIntegration.SolidWorks != null)
        {
            Logger.LogDebugSource("Cannot set a test application when SolidWorks is running.");
            throw new SolidDnaException(SolidDnaErrors.CreateError(SolidDnaErrorTypeCode.SolidWorksApplication, SolidDnaErrorCode.SolidWorksApplicationCannotSetApplicationAtRuntime));
        }

        mTestApplication = application;
    }

    #endregion
}