using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for SolidWorksApplication to enable mocking and unit testing.
    /// </summary>
    public interface ISolidWorksApplication : ISharedSolidDnaObject<SldWorks>
    {
        Model ActiveModel { get; }
        SolidWorksApplicationType ApplicationType { get; }
        CommandManager CommandManager { get; }
        ConnectionStatus3DExperience ConnectionStatus3DExperience { get; }
        bool Disposing { get; }
        SolidWorksApplication.SolidWorksPreferences Preferences { get; }
        SolidWorksVersion SolidWorksVersion { get; }
        int SolidWorksCookie { get; }

        event Action<string, Model> ActiveFileSaved;
        event Action<Model> ActiveModelInformationChanged;
        event Action<Model> FileCreated;
        event Action<string, Model> FileOpened;
        event Action Idle;
        event Action SolidWorksClosing;

        void RequestActiveModelChanged();
        Model CreateAssembly(string templatePath = null);
        Model CreateDrawing(swDwgPaperSizes_e paperSize, string templatePath = null);
        Model CreateDrawing(double width, double height, string templatePath = null);
        Model CreatePart(string templatePath = null);
        IEnumerable<Model> OpenDocuments();
        Model OpenFile(string filePath, OpenDocumentOptions options = OpenDocumentOptions.None, string configuration = null);
        Model OpenFile(IDocumentSpecification documentSpecification);
        Model OpenFileFrom3DExperience(string plmId);
        void CloseFile(string filePath);
        IExportPdfData GetPdfExportData();
        List<Material> GetMaterials(string databasePath = null);
        Material FindMaterial(string databasePath, string materialName);
        double GetUserPreferencesDouble(swUserPreferenceDoubleValue_e preference);
        bool SetUserPreferencesDouble(swUserPreferenceDoubleValue_e preference, double value);
        int GetUserPreferencesInteger(swUserPreferenceIntegerValue_e preference);
        bool SetUserPreferencesInteger(swUserPreferenceIntegerValue_e preference, int value);
        string GetUserPreferencesString(swUserPreferenceStringValue_e preference);
        bool SetUserPreferencesString(swUserPreferenceStringValue_e preference, string value);
        bool GetUserPreferencesToggle(swUserPreferenceToggle_e preference);
        void SetUserPreferencesToggle(swUserPreferenceToggle_e preference, bool value);
        Bitmap GetPreviewBitmap(string modelFilePath, string configurationName);
        void SavePreviewBitmap(string modelFilePath, string configurationName, string bitmapFilePath);
        Task<Taskpane> CreateTaskpaneAsync(string iconPath, string toolTip);
        Task<Taskpane> CreateTaskpaneAsync2(string iconPathFormat, string toolTip);
        SolidWorksMessageBoxResult ShowMessageBox(string message, SolidWorksMessageBoxIcon icon = SolidWorksMessageBoxIcon.Information, SolidWorksMessageBoxButtons buttons = SolidWorksMessageBoxButtons.Ok);
    }
}
