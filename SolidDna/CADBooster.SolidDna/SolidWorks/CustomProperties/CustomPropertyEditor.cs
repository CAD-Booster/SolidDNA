using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks custom property manager for a model
    /// </summary>
    public class CustomPropertyEditor : SolidDnaObject<CustomPropertyManager>
    {
        public bool IsTypeCheckEnabled { get; set; } = true;
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomPropertyEditor(CustomPropertyManager model) : base(model)
        {

        }

        #endregion

        /// <summary>
        /// Checks if a custom property exists
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        /// <returns></returns>
        public bool CustomPropertyExists(string name)
        {
            // TODO: Add error checking and exception catching

            return GetCustomProperties().Any(f => string.Equals(f.Name, name, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the value of a custom property by name
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        /// <param name="resolve">True to resolve the custom property value</param>
        /// <returns></returns>
        public string GetCustomProperty(string name, bool resolve = false)
        {
            // TODO: Add error checking and exception catching

            // Get custom property
            _ = BaseObject.Get5(name, false, out var val, out var resolvedVal, out _);

            // Return desired result
            return resolve ? resolvedVal : val;
        }

        public string GetStringCustomProperty(string name, bool resolve = false)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoText, true);

            _ = BaseObject.Get5(name, false, out var val, out var resolvedVal, out _);

            return resolve ? resolvedVal : val;
        }

        public DateTime GetDateCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoDate);


            _ = BaseObject.Get5(name, false, out _, out var resolvedVal, out _);

            return DateTime.Parse(resolvedVal);
        }        
        
        public int GetIntegerCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoNumber);


            _ = BaseObject.Get5(name, false, out _, out var resolvedVal, out _);

            return int.Parse(resolvedVal);
        }        
        
        public double GetDoubleCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoNumber);


            _ = BaseObject.Get5(name, false, out _, out var resolvedVal, out _);

            return double.Parse(resolvedVal, CultureInfo.InvariantCulture);
        }

        public bool GetBooleanCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoYesOrNo);

            _ = BaseObject.Get5(name, false, out _, out var resolvedVal, out _);

            switch (resolvedVal)
            {
                case "Yes":
                    return true;
                case "No":
                    return false;
                default:
                    throw new InvalidOperationException();
            }
        }

        public string GetRawEquationCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoEquation, true);

            _ = BaseObject.Get5(name, false, out var val, out _, out _);

            return val;
        }

        public double GetEvaluatedEquationCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            if (IsTypeCheckEnabled)
                ThrowIfExpectedTypeMismatch(name, swCustomInfoType_e.swCustomInfoEquation);

            _ = BaseObject.Get5(name, false, out _, out var resolvedVal, out _);

            return double.Parse(resolvedVal, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets the value of a custom property by name
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        /// <param name="value">The value of the custom property</param>
        /// <param name="type">The type of the custom property</param>
        /// <returns></returns>
        public void SetCustomProperty(string name, string value, swCustomInfoType_e type = swCustomInfoType_e.swCustomInfoText)
        {
            // TODO: Add error checking and exception catching

            // NOTE: We use Add here to create a property if one doesn't exist
            //       I feel this is the expected behaviour of Set.
            //       We replace the value if it does exist (we used to delete and add the property)
            //       to not change the order of existing custom properties
            //
            //       To mimic the Set behaviour of the SolidWorks API
            //       Simply do CustomPropertyExists() to check first if it exists
            //

            // Set new one
            _ = BaseObject.Add3(name, (int)type, value, (int)swCustomPropertyAddOption_e.swCustomPropertyReplaceValue);
        }

        public void SetStringCustomProperty(string name, string value) 
            => SetCustomProperty(name, value, swCustomInfoType_e.swCustomInfoText);

        public void SetDateCustomProperty(string name, DateTime value) 
            => SetCustomProperty(name, value.ToString("dd.MM.yyyy"), swCustomInfoType_e.swCustomInfoDate);

        public void SetIntegerCustomProperty(string name, int value) 
            => SetCustomProperty(name, value.ToString(), swCustomInfoType_e.swCustomInfoNumber);

        public void SetDoubleCustomProperty(string name, double value)
            => SetCustomProperty(name, value.ToString(CultureInfo.InvariantCulture), swCustomInfoType_e.swCustomInfoDouble);

        public void SetBooleanCustomProperty(string name, bool value) 
            => SetCustomProperty(name, value ? "Yes" : "No", swCustomInfoType_e.swCustomInfoYesOrNo);

        public void SetEquationCustomProperty(string name, string value)
            => SetCustomProperty(name, value, swCustomInfoType_e.swCustomInfoEquation);


        /// <summary>
        /// Deletes a custom property by name
        /// </summary>
        /// <param name="name">The name of the custom property</param>
        public void DeleteCustomProperty(string name)
        {
            // TODO: Add error checking and exception catching

            BaseObject.Delete2(name);
        }

        /// <summary>
        /// Gets a list of all custom properties
        /// </summary>
        /// <returns></returns>
        public List<CustomProperty> GetCustomProperties()
        {
            // TODO: Add error checking and exception catching

            // Create an empty list
            var list = new List<CustomProperty>();

            // Get all properties
            var names = (string[])BaseObject.GetNames();

            // Create custom property objects for each
            if (names?.Length > 0)
                list.AddRange(names.Select(name => new CustomProperty(this, name)).ToList());

            // Return the list
            return list;
        }

        private void ThrowIfExpectedTypeMismatch(string name, swCustomInfoType_e expectedType, bool allowUnknown = false)
        {
            var type = (swCustomInfoType_e)BaseObject.GetType2(name);

            if (allowUnknown && type == swCustomInfoType_e.swCustomInfoUnknown)
                return;

            if (type != expectedType)
            {
                throw new SolidDnaException(SolidDnaErrors.CreateError(
                        SolidDnaErrorTypeCode.SolidWorksModel, SolidDnaErrorCode.SolidWorksModelError,
                        $"Property has different type that expected. Received: {type}, expected: {expectedType}"));
            }
        }
    }
}
