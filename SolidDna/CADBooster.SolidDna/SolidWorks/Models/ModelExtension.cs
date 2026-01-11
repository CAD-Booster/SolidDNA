using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks model extension of any type (Drawing, Part or Assembly)
    /// </summary>
    public class ModelExtension : SolidDnaObject<ModelDocExtension>
    {
        #region Public Properties

        /// <summary>
        /// The parent Model for this extension
        /// </summary>
        public Model Parent { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModelExtension(ModelDocExtension model, Model parent) : base(model)
        {
            Parent = parent;
        }

        #endregion

        #region Custom Properties

        /// <summary>
        /// Gets a configuration-specific custom property editor for the specified configuration.
        /// If no configuration is specified the default custom property manager is returned.
        /// 
        /// NOTE: Custom Property Editor must be disposed of once finished.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public CustomPropertyEditor CustomPropertyEditor(string configuration = null)
        {
            // TODO: Add error checking and exception catching

            return new CustomPropertyEditor(BaseObject.CustomPropertyManager[configuration]);
        }

        #endregion

        #region Mass

        /// <summary>
        /// Gets the mass properties of a part/assembly.
        /// </summary>
        /// <param name="doNotThrowOnError">If true, don't throw on errors, just return empty mass</param>
        /// <returns></returns>
        public MassProperties GetMassProperties(bool doNotThrowOnError = true)
        {
            // Wrap any error
            return SolidDnaErrors.Wrap(() =>
            {
                // Make sure we are a part
                if (!Parent.IsPart && !Parent.IsAssembly)
                {
                    return doNotThrowOnError
                        ? new MassProperties()
                        : throw new InvalidOperationException(Localization.GetString("SolidWorksModelGetMassModelNotPartError"));
                }

                // Explicitly set a custom status value because SolidWorks does not always set it
                const int statusDefault = -1;
                var status = statusDefault;

                const int highestAccuracy = 2;
                var solidWorksVersion = SolidWorksEnvironment.Application.SolidWorksVersion;
                double[] massPropertiesArray;

                if (solidWorksVersion == null || solidWorksVersion.Version < 2016)
                    massPropertiesArray = (double[])BaseObject.GetMassProperties(highestAccuracy, ref status);
                else
                {
                    // SolidWorks 2016 introduced GetMassProperties2
                    massPropertiesArray = (double[])BaseObject.GetMassProperties2(highestAccuracy, out status, false);
                }

                // Make sure it succeeded. SOLIDWORKS does not always update the status when it returns null
                if (status == (int)swMassPropertiesStatus_e.swMassPropertiesStatus_UnknownError || status == statusDefault)
                {
                    return doNotThrowOnError
                        ? new MassProperties()
                        : throw new InvalidOperationException(Localization.GetString("SolidWorksModelGetMassModelStatusFailed"));
                }
                // If we have no mass, return empty
                if (status == (int)swMassPropertiesStatus_e.swMassPropertiesStatus_NoBody)
                    return new MassProperties();

                // Otherwise we have the properties so return them
                return new MassProperties(massPropertiesArray);
            },
                SolidDnaErrorTypeCode.SolidWorksModel,
                SolidDnaErrorCode.SolidWorksModelGetMassPropertiesError);
        }

        #endregion

        #region Dispose

        public override void Dispose()
        {
            // Clear reference to be safe
            Parent = null;

            base.Dispose();
        }

        #endregion
    }
}
