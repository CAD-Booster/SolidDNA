using SolidWorks.Interop.sldworks;
using System.Threading.Tasks;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks Taskpane
    /// </summary>
    public class Taskpane : SolidDnaObject<ITaskpaneView>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Taskpane(ITaskpaneView taskpane) : base(taskpane)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a control (Windows <see cref="System.Windows.Forms.UserControl"/>) to the taskpane
        /// that has been exposed to COM and has a given ProgId
        /// </summary>
        /// <typeparam name="T">The type of UserControl being created</typeparam>
        /// <param name="progId">The [ProgId()] attribute value adorned on the UserControl class</param>
        /// <param name="licenseKey">The license key (for specific SolidWorks add-in types)</param>
        /// <returns></returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<T> AddControlAsync<T>(string progId, string licenseKey)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // Wrap any error creating the taskpane in a SolidDna exception
            return SolidDnaErrors.Wrap(() =>
            {
                // Attempt to create the taskpane
                return (T)BaseObject.AddControl(progId, licenseKey);
            },
                SolidDnaErrorTypeCode.SolidWorksTaskpane,
                SolidDnaErrorCode.SolidWorksTaskpaneAddControlError);
        }


        #endregion
    }
}
