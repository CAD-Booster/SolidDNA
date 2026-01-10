namespace CADBooster.SolidDna
{
    /// <summary>
    /// A class providing shorthand access to commonly used values
    /// </summary>
    public static class SolidWorksEnvironment
    {
        /// <summary>
        /// Backing field for mock/test application.
        /// When set, overrides the real SolidWorks instance.
        /// </summary>
        private static ISolidWorksApplication _testApplication;

        /// <summary>
        /// The currently running instance of SolidWorks.
        /// Returns the concrete type for backward compatibility.
        /// Note: Returns null when a mock is set via <see cref="SetApplicationForTesting"/>
        /// </summary>
        public static SolidWorksApplication Application => _testApplication == null ? AddInIntegration.SolidWorks : null;

        /// <summary>
        /// The currently running instance of SolidWorks as an interface.
        /// Use this property when you need to support mocking in unit tests.
        /// </summary>
        public static ISolidWorksApplication IApplication => _testApplication ?? AddInIntegration.SolidWorks;

        /// <summary>
        /// Sets a mock or test application instance.
        /// When set, <see cref="IApplication"/> returns the mock and <see cref="Application"/> returns null.
        /// </summary>
        /// <param name="application">The mock application instance</param>
        public static void SetApplicationForTesting(ISolidWorksApplication application) => _testApplication = application;

        /// <summary>
        /// Resets to use the default SolidWorks instance from AddInIntegration.
        /// Call this after unit tests to restore normal behavior.
        /// </summary>
        public static void ResetApplicationForTesting() => _testApplication = null;
    }
}
