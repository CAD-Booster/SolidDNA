using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna;

/// <summary>
/// Represents the current SolidWorks application
/// </summary>
public partial class SolidWorksApplication : SharedSolidDnaObject<SldWorks>, ISolidWorksApplication
{
    /// <summary>
    /// An embedded class to manage SolidWorks application preferences.
    /// </summary>
    public class SolidWorksPreferences
    {
        /// <summary>
        /// Get the default assembly template path.
        /// </summary>
        public string DefaultAssemblyTemplate
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplateAssembly);
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplateAssembly, value);
        }

        /// <summary>
        /// Get the default drawing template path.
        /// </summary>
        public string DefaultDrawingTemplate
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplateDrawing);
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplateDrawing, value);
        }

        /// <summary>
        /// Get the default part template path.
        /// </summary>
        public string DefaultPartTemplate
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplatePart);
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesString(swUserPreferenceStringValue_e.swDefaultTemplatePart, value);
        }

        /// <summary>
        /// The scaling factor used when exporting as DXF
        /// </summary>
        public double DxfOutputScaleFactor
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesDouble(swUserPreferenceDoubleValue_e.swDxfOutputScaleFactor);
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesDouble(swUserPreferenceDoubleValue_e.swDxfOutputScaleFactor, value);
        }

        /// <summary>
        /// The scaling factor used when exporting as DXF
        /// </summary>
        public int DxfMultiSheetOption
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesInteger(swUserPreferenceIntegerValue_e.swDxfMultiSheetOption);
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesInteger(swUserPreferenceIntegerValue_e.swDxfMultiSheetOption, value);
        }

        /// <summary>
        /// The scaling of DXF output. If true no scaling will be done
        /// </summary>
        public bool DxfOutputNoScale
        {
            get => SolidWorksEnvironment.IApplication.GetUserPreferencesInteger(swUserPreferenceIntegerValue_e.swDxfOutputNoScale) == 1;
            set => SolidWorksEnvironment.IApplication.SetUserPreferencesInteger(swUserPreferenceIntegerValue_e.swDxfOutputNoScale, value ? 1 : 0);
        }
    }
}