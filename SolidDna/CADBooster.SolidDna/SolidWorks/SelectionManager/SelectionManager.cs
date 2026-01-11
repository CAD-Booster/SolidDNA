using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Represents a SolidWorks selection manager. Used for selecting, deselecting and getting selected objects.
    /// </summary>
    public class SelectionManager : SolidDnaObject<SelectionMgr>, ISelectionManager
    {
        #region Private members

        /// <summary>
        /// The model this selection manager belongs to.
        /// </summary>
        private readonly Model mModel;

        /// <summary>
        /// The model extension this selection manager belongs to.
        /// </summary>
        private readonly ModelExtension mModelExtension;

        #endregion

        #region Constructor

        /// <summary>
        /// Old constructor
        /// </summary>
        [Obsolete("Use the full constructor that takes a model and model extension. This enables selecting and deselecting methods.")]
        public SelectionManager(SelectionMgr selectionMgr) : base(selectionMgr)
        {

        }

        /// <summary>
        /// Create a selection manager tied to a model and model extension.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="model"></param>
        /// <param name="modelExtension"></param>
        public SelectionManager(SelectionMgr manager, Model model, ModelExtension modelExtension) : base(manager)
        {
            mModel = model;
            mModelExtension = modelExtension;
        }

        #endregion

        #region Deselect

        /// <summary>
        /// Deselect all selected objects.
        /// </summary>
        public void DeselectAll() => mModel.UnsafeObject.ClearSelection2(true);

        /// <summary>
        /// Deselect the object at a given index with an optional selection mark.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">The mark that the item was originally selected with.
        /// Use <see cref="SelectionMark.Any"/> to ignore the mark and <see cref="SelectionMark.None"/> to deselect an object that was selected without a mark.</param>
        /// <returns>True if the object was deselected, false if not.</returns>
        public bool DeselectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any)
        {
            // Deselect the object
            var result = BaseObject.DeSelect2(index, (int)selectionMark);

            // Return whether it was successful: 1 means successful, 0 means not successful.
            return result == 1;
        }

        /// <summary>
        /// Deselect multiple objects at given indexes with an optional selection mark.
        /// </summary>
        /// <param name="indexes">The indexes of the selected objects. Are 1-based.</param>
        /// <param name="selectionMark">The mark that the items were originally selected with.
        /// Use <see cref="SelectionMark.Any"/> to ignore the mark and <see cref="SelectionMark.None"/> to deselect objects that were selected without a mark.</param>
        /// <returns>True if the objects were deselected, false if not.</returns>
        public bool DeselectAtIndexes(List<int> indexes, SelectionMark selectionMark = SelectionMark.Any)
        {
            // Deselect the objects
            var result = BaseObject.DeSelect2(indexes.ToArray(), (int)selectionMark);

            // Return whether it was successful: 1 means successful, 0 means not successful.
            return result == 1;
        }

        #endregion

        #region Get selected entities

        /// <summary>
        /// Enumerate through all selected objects in the current model with a certain selection mark.
        /// Allows you to stop enumerating early if needed.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>An enumerable of selected objects</returns>
        public IEnumerable<SelectedObject> EnumerateSelectedObjects(SelectionMark selectionMark = SelectionMark.Any)
        {
            // Get selection count
            var count = GetSelectedObjectCount(selectionMark);

            // If we have none, we are done
            if (count <= 0)
                yield break;

            // Otherwise, get all selected objects
            for (var i = 1; i <= count; i++)
            {
                // Get the object and its type.
                var selected = GetSelectedObjectAtIndex(i, selectionMark);

                // Return it
                yield return selected;
            }
        }

        /// <summary>
        /// Get the selected object at a given index and with a certain selection mark. Index is 1-based.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns></returns>
        public SelectedObject GetSelectedObjectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any) =>
            new SelectedObject(BaseObject.GetSelectedObject6(index, (int)selectionMark), (swSelectType_e)BaseObject.GetSelectedObjectType3(index, (int)selectionMark));

        /// <summary>
        /// Get the number of selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="mark">Only include objects with this selection mark</param>
        /// <returns>The count of selected objects</returns>
        public int GetSelectedObjectCount(SelectionMark mark = SelectionMark.Any) => BaseObject.GetSelectedObjectCount2((int)mark);

        /// <summary>
        /// Get all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>A list of all selected objects with the specified selection mark</returns>
        public List<SelectedObject> GetSelectedObjects(SelectionMark selectionMark = SelectionMark.Any)
        {
            // Create list
            var list = new List<SelectedObject>();

            // Get selection count
            var count = GetSelectedObjectCount(selectionMark);

            // Get all selected objects
            for (var i = 1; i <= count; i++)
            {
                // Get the object and its type. Add it to the list.
                list.Add(GetSelectedObjectAtIndex(i, selectionMark));
            }

            // Return the list
            return list;
        }

        /// <summary>
        /// Perform an action on all selected objects in the current model with any selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        public void SelectedObjects(Action<List<SelectedObject>> action) => SelectedObjects(action, SelectionMark.Any);

        /// <summary>
        /// Perform an action on all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        /// <param name="selectionMark"></param>
        public void SelectedObjects(Action<List<SelectedObject>> action, SelectionMark selectionMark)
        {
            // Create list
            var list = new List<SelectedObject>();

            try
            {
                // Get selection count
                var count = GetSelectedObjectCount(selectionMark);

                // If we have none, we are done
                if (count <= 0)
                {
                    action([]);
                    return;
                }

                // Get all selected objects
                for (var i = 1; i <= count; i++)
                {
                    // Get the object and its type. Add it to the list.
                    list.Add(GetSelectedObjectAtIndex(i, selectionMark));
                }

                // Call the action
                action(list);
            }
            finally
            {
                // Dispose of all items
                list.ForEach(f => f.Dispose());
            }
        }

        #endregion

        #region Select entities

        /// <summary>
        /// Select all objects in the model. Does not include graphic bodies.
        /// </summary>
        public void SelectAll() => mModelExtension.UnsafeObject.SelectAll();

        /// <summary>
        /// Select an object by its type and a position. Throws when it fails.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="position">The position of the object</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        public void SelectByPosition(SelectionType selectionType, Point3D position, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any) =>
            SelectByNameOrPosition(selectionType, "", position, selectionMode, selectionMark);

        /// <summary>
        /// Select an object by its type and name. Throws when it fails.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="name">The name of the object</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        public void SelectByName(SelectionType selectionType, string name, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any) =>
            SelectByNameOrPosition(selectionType, name, new Point3D(), selectionMode, selectionMark);

        /// <summary>
        /// Select an object by its type and a vector starting at a point. Throws when it fails.
        /// Actively clears the selection first when in create mode.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="startPosition">The position in 3D space to start the ray from</param>
        /// <param name="direction">The direction of the ray</param>
        /// <param name="rayRadius">The radius of the ray</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        public void SelectByRay(SelectionType selectionType, Point3D startPosition, Vector3D direction, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any, RayRadius rayRadius = RayRadius.Standard)
        {
            SolidDnaErrors.Wrap(() =>
            {
                // This should not be necessary, but it is.
                if (selectionMode == SelectionMode.Create)
                    DeselectAll();

                // Convert the selection mode to an append boolean
                var append = selectionMode == SelectionMode.Append;

                // Convert the ray radius to a double
                var radius = GetRayRadius(rayRadius);

                // Select the object
                var selected = mModelExtension.UnsafeObject.SelectByRay(startPosition.X, startPosition.Y, startPosition.Z, direction.X, direction.Y, direction.Z, radius, (int)selectionType, append, (int)selectionMark, 0);

                // Check if it was successful
                if (!selected)
                    throw new Exception("Object not selected");

            }, SolidDnaErrorTypeCode.SolidWorksModel, SolidDnaErrorCode.SolidWorksModelObjectNotSelectedError);
        }

        /// <summary>
        /// Select an object by its type plus name or position. Throws when it fails.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="name">The name of the object</param>
        /// <param name="position">The position of the object</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        private void SelectByNameOrPosition(SelectionType selectionType, string name, Point3D position, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any)
        {
            SolidDnaErrors.Wrap(() =>
            {
                // Convert the selection mode to an append boolean
                var append = selectionMode == SelectionMode.Append;

                // Select the object
                var success = mModelExtension.UnsafeObject.SelectByID2(name, selectionType.StringValue, position.X, position.Y, position.Z, append, (int)selectionMark, null, 0);

                // Check if it was successful
                if (!success)
                    throw new Exception("Object not selected");

            }, SolidDnaErrorTypeCode.SolidWorksModel, SolidDnaErrorCode.SolidWorksModelObjectNotSelectedError);
        }

        /// <summary>
        /// Convert the ray radius enum to a double value.
        /// </summary>
        /// <param name="rayRadius"></param>
        /// <returns></returns>
        private static double GetRayRadius(RayRadius rayRadius)
        {
            switch (rayRadius)
            {
                case RayRadius.ExtraExtraSmall:
                    return 0.0001;
                case RayRadius.ExtraSmall:
                    return 0.0002;
                case RayRadius.Small:
                    return 0.0004;
                case RayRadius.Large:
                    return 0.0016;
                case RayRadius.ExtraLarge:
                    return 0.0032;
                case RayRadius.Standard:
                default:
                    return 0.0008;
            }
        }

        #endregion
    }
}
