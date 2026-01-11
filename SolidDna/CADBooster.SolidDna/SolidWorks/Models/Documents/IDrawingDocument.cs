using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks drawing document.
    /// Enables mocking for unit testing code that consumes drawing documents.
    /// </summary>
    public interface IDrawingDocument
    {
        #region Properties

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        DrawingDoc UnsafeObject { get; }

        #endregion

        #region Feature Methods

        /// <summary>
        /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name.
        /// Returns the actual model feature or null when not found.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns>The <see cref="ModelFeature"/> for the named feature</returns>
        ModelFeature GetFeatureByName(string featureName);

        /// <summary>
        /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform a function on it.
        /// </summary>
        /// <typeparam name="T">The return type of the function</typeparam>
        /// <param name="featureName">Name of the feature</param>
        /// <param name="function">The function to perform on this feature</param>
        /// <returns>The result of the function</returns>
        T GetFeatureByName<T>(string featureName, Func<ModelFeature, T> function);

        /// <summary>
        /// Get the <see cref="ModelFeature"/> of the item in the feature tree based on its name and perform an action on it.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <param name="action">The action to perform on this feature</param>
        void GetFeatureByName(string featureName, Action<ModelFeature> action);

        #endregion

        #region Sheet Methods

        /// <summary>
        /// Activate the specified drawing sheet.
        /// </summary>
        /// <param name="sheetName">Name of the sheet</param>
        /// <returns>True if the sheet was activated, false if SOLIDWORKS generated an error</returns>
        bool ActivateSheet(string sheetName);

        /// <summary>
        /// Get the name of the currently active sheet.
        /// </summary>
        /// <returns>The name of the current active sheet</returns>
        string CurrentActiveSheet();

        /// <summary>
        /// Get the sheet names of the drawing.
        /// </summary>
        /// <returns>An array of sheet names</returns>
        string[] SheetNames();

        /// <summary>
        /// Perform an action on each sheet in the drawing.
        /// </summary>
        /// <param name="sheetsCallback">The action to perform on each sheet</param>
        void ForEachSheet(Action<DrawingSheet> sheetsCallback);

        #endregion

        #region View Methods

        /// <summary>
        /// Activate the specified drawing view.
        /// </summary>
        /// <param name="viewName">Name of the drawing view</param>
        /// <returns>True if successful, false if not</returns>
        bool ActivateView(string viewName);

        /// <summary>
        /// Rotate the view so the selected line in the view is horizontal.
        /// </summary>
        void AlignViewHorizontally();

        /// <summary>
        /// Rotate the view so the selected line in the view is vertical.
        /// </summary>
        void AlignViewVertically();

        /// <summary>
        /// Perform an action on each view in the drawing.
        /// </summary>
        /// <param name="viewsCallback">The callback containing all views</param>
        void Views(Action<List<DrawingView>> viewsCallback);

        #endregion

        #region Balloon Methods

        /// <summary>
        /// Automatically inserts BOM balloons in selected drawing views.
        /// </summary>
        /// <param name="options">The balloon options</param>
        /// <param name="onSuccess">Callback containing all created notes if successful</param>
        void AutoBalloon(AutoBalloonOptions options, Action<Note[]> onSuccess = null);

        #endregion

        #region Dimension Methods

        /// <summary>
        /// Add a chamfer dimension to the selected edges.
        /// </summary>
        /// <param name="x">X dimension</param>
        /// <param name="y">Y dimension</param>
        /// <param name="z">Z dimension</param>
        /// <returns>The chamfer <see cref="ModelDisplayDimension"/> if successful. Null if not.</returns>
        ModelDisplayDimension AddChamferDimension(double x, double y, double z);

        /// <summary>
        /// Add a hole callout to the selected hole.
        /// </summary>
        /// <param name="x">X dimension</param>
        /// <param name="y">Y dimension</param>
        /// <param name="z">Z dimension</param>
        /// <returns>The hole cutout <see cref="ModelDisplayDimension"/> if successful. Null if not.</returns>
        ModelDisplayDimension AddHoleCutout(double x, double y, double z);

        /// <summary>
        /// Re-align the selected ordinate dimension if it was previously broken.
        /// </summary>
        void AlignOrdinateDimension();

        #endregion

        #region Annotation Methods

        /// <summary>
        /// Attach an existing annotation to a drawing sheet or view.
        /// </summary>
        /// <param name="option">The attach option</param>
        /// <returns>True if successful, false if not</returns>
        bool AttachAnnotation(AttachAnnotationOption option);

        /// <summary>
        /// Attempt to attach unattached dimensions, for example in an imported DXF file.
        /// </summary>
        void AttachDimensions();

        #endregion

        #region Line Style Methods

        /// <summary>
        /// Add a line style to the drawing document.
        /// </summary>
        /// <param name="styleName">The name of the style</param>
        /// <param name="boldLineEnds">True to have bold dots at each end of the line</param>
        /// <param name="segments">Segments. Positive numbers are dashes, negative are gaps</param>
        /// <returns>True if successful</returns>
        bool AddLineStyle(string styleName, bool boldLineEnds, params double[] segments);

        #endregion
    }
}
