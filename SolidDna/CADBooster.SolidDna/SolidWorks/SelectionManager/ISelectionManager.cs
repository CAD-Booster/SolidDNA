using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Interface for a SolidWorks selection manager, used for selecting, deselecting and getting selected objects.
    /// Enables mocking for unit testing code that consumes selection operations.
    /// </summary>
    public interface ISelectionManager : IDisposable
    {
        #region Properties

        /// <summary>
        /// The raw underlying COM object.
        /// WARNING: Use with caution. You must handle all disposal from this point on.
        /// </summary>
        SelectionMgr UnsafeObject { get; }

        #endregion

        #region Deselect methods

        /// <summary>
        /// Deselect all selected objects.
        /// </summary>
        void DeselectAll();

        /// <summary>
        /// Deselect the object at a given index with an optional selection mark.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">The mark that the item was originally selected with.</param>
        /// <returns>True if the object was deselected, false if not.</returns>
        bool DeselectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Deselect multiple objects at given indexes with an optional selection mark.
        /// </summary>
        /// <param name="indexes">The indexes of the selected objects. Are 1-based.</param>
        /// <param name="selectionMark">The mark that the items were originally selected with.</param>
        /// <returns>True if the objects were deselected, false if not.</returns>
        bool DeselectAtIndexes(List<int> indexes, SelectionMark selectionMark = SelectionMark.Any);

        #endregion

        #region Get selected entities methods

        /// <summary>
        /// Enumerate through all selected objects in the current model with a certain selection mark.
        /// Allows you to stop enumerating early if needed.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>An enumerable of selected objects</returns>
        IEnumerable<SelectedObject> EnumerateSelectedObjects(SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Get the selected object at a given index and with a certain selection mark. Index is 1-based.
        /// </summary>
        /// <param name="index">The index of the selected object. Is 1-based.</param>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>The selected object at the specified index</returns>
        SelectedObject GetSelectedObjectAtIndex(int index, SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Get the number of selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="mark">Only include objects with this selection mark</param>
        /// <returns>The count of selected objects</returns>
        int GetSelectedObjectCount(SelectionMark mark = SelectionMark.Any);

        /// <summary>
        /// Get all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        /// <returns>A list of all selected objects with the specified selection mark</returns>
        List<SelectedObject> GetSelectedObjects(SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Perform an action on all selected objects in the current model with any selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        void SelectedObjects(Action<List<SelectedObject>> action);

        /// <summary>
        /// Perform an action on all selected objects in the current model with a certain selection mark.
        /// </summary>
        /// <param name="action">The selected objects list to be worked on inside the action.
        /// NOTE: Do not store references to these objects outside of this action</param>
        /// <param name="selectionMark">Only include objects with this selection mark</param>
        void SelectedObjects(Action<List<SelectedObject>> action, SelectionMark selectionMark);

        #endregion

        #region Select methods

        /// <summary>
        /// Select all objects in the model. Does not include graphic bodies.
        /// </summary>
        void SelectAll();

        /// <summary>
        /// Select an object by its type and a position. Throws when it fails.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="position">The position of the object</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        void SelectByPosition(SelectionType selectionType, Point3D position, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Select an object by its type and name. Throws when it fails.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="name">The name of the object</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        void SelectByName(SelectionType selectionType, string name, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any);

        /// <summary>
        /// Select an object by its type and a vector starting at a point. Throws when it fails.
        /// Actively clears the selection first when in create mode.
        /// </summary>
        /// <param name="selectionType">The object type to select</param>
        /// <param name="startPosition">The position in 3D space to start the ray from</param>
        /// <param name="direction">The direction of the ray</param>
        /// <param name="selectionMode">Whether to start a new selection or append an existing one</param>
        /// <param name="selectionMark">Whether to mark this selected object with a number</param>
        /// <param name="rayRadius">The radius of the ray</param>
        void SelectByRay(SelectionType selectionType, Point3D startPosition, Vector3D direction, SelectionMode selectionMode = SelectionMode.Create, SelectionMark selectionMark = SelectionMark.Any, RayRadius rayRadius = RayRadius.Standard);

        #endregion
    }
}
