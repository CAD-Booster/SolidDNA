using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CADBooster.SolidDna
{
    /// <summary>
    /// Provides extension methods for working with SolidWorks model elements
    /// </summary>
    public static class ModelExtensions
    {
        #region Annotations

        /// <summary>
        /// Enumerates all annotations in the model (unsafe/non-wrapped version)
        /// </summary>
        /// <param name="model">The SolidWorks model</param>
        /// <returns>Enumerable of raw annotation objects</returns>
        public static IEnumerable<Annotation> EnumerateUnsafeAnnotations(this Model model)
        {
            var annotation = model.UnsafeObject.IGetFirstAnnotation2();

            while (annotation != null)
            {
                yield return annotation;
                annotation = annotation.GetNext3();
            }
        }

        /// <summary>
        /// Enumerates all notes in the model with disposable registration
        /// </summary>
        /// <param name="model">The SolidWorks model</param>
        /// <param name="disposable">Container for disposable management</param>
        /// <returns>Enumerable of wrapped notes</returns>
        public static IEnumerable<Note> EnumerateNotes(this Model model, ICompositeDisposable disposable)
            => model
                .EnumerateUnsafeAnnotations()
                .EnumerateNotes(disposable);

        /// <summary>
        /// Filters annotations to notes (unsafe/non-wrapped version)
        /// </summary>
        /// <param name="annotations">Annotations to filter</param>
        /// <param name="disposable">Optional disposable container</param>
        /// <returns>Enumerable of raw note objects</returns>
        public static IEnumerable<INote> EnumerateUnsafeNotes(this IEnumerable<Annotation> annotations, ICompositeDisposable disposable = null)
            => annotations
                .WrapDnaObjects(disposable.GetDummyIfNull())
                .Select(x => x.UnsafeObject.GetSpecificAnnotation() as INote)
                .Where(x => x != null);

        /// <summary>
        /// Converts annotations to wrapped notes with disposable registration
        /// </summary>
        /// <param name="annotations">Annotations to convert</param>
        /// <param name="disposable">Container for disposable management</param>
        /// <returns>Enumerable of wrapped notes</returns>
        public static IEnumerable<Note> EnumerateNotes(this IEnumerable<Annotation> annotations, ICompositeDisposable disposable)
            => annotations
                .EnumerateUnsafeNotes(disposable)
                .WrapDnaObject(disposable);

        /// <summary>
        /// Wraps raw notes in managed Note objects
        /// </summary>
        /// <param name="dnaObjects">Raw notes to wrap</param>
        /// <param name="disposable">Container for disposable management</param>
        /// <returns>Enumerable of wrapped notes</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Note> WrapDnaObject(this IEnumerable<INote> dnaObjects, ICompositeDisposable disposable)
            => dnaObjects.WrapDnaObjects(disposable, x => new Note(x));
        #endregion

        #region Features

        /// <summary>
        /// Enumerates all features in the model (unsafe/non-wrapped version)
        /// </summary>
        /// <param name="model">The SolidWorks model</param>
        /// <returns>Enumerable of raw feature objects</returns>
        public static IEnumerable<Feature> EnumerateUnsafeFeatures(this Model model)
        {
            var feature = model.UnsafeObject.FirstFeature();

            while (feature != null)
            {
                var castedFeature = feature as Feature;
                yield return castedFeature;
                feature = castedFeature.GetNextFeature();
            }
        }

        /// <summary>
        /// Enumerates all features with disposable registration
        /// </summary>
        /// <param name="model">The SolidWorks model</param>
        /// <param name="disposable">Container for disposable management</param>
        /// <returns>Enumerable of wrapped features</returns>
        public static IEnumerable<ModelFeature> EnumerateFeatures(this Model model, ICompositeDisposable disposable = null)
            => model
                .EnumerateUnsafeFeatures()
                .WrapDnaObject(disposable.GetDummyIfNull());

        /// <summary>
        /// Wraps raw features in managed ModelFeature objects
        /// </summary>
        /// <param name="dnaObjects">Raw features to wrap</param>
        /// <param name="disposable">Container for disposable management</param>
        /// <returns>Enumerable of wrapped features</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ModelFeature> WrapDnaObject(this IEnumerable<Feature> dnaObjects, ICompositeDisposable disposable)
            => dnaObjects.WrapDnaObjects(disposable, x => new ModelFeature(x));

        #region ParentFeatures

        /// <summary>
        /// Enumerates parent features of a feature (unsafe/non-wrapped version)
        /// </summary>
        /// <param name="feature">The feature to get parents for</param>
        /// <returns>Enumerable of raw parent features</returns>
        public static IEnumerable<Feature> EnumerateUnsafeParentFeatures(this ModelFeature feature)
        {
            var parents = (object[])feature.UnsafeObject.GetParents();

            if (parents is null)
                yield break;

            foreach (var parent in parents)
                yield return (Feature)parent;
        }

        /// <summary>
        /// Enumerates parent features with optional disposable registration
        /// </summary>
        /// <param name="feature">The feature to get parents for</param>
        /// <param name="disposable">Optional container for disposable management</param>
        /// <returns>Enumerable of wrapped parent features</returns>
        public static IEnumerable<ModelFeature> EnumerateParentFeatures(this ModelFeature feature, ICompositeDisposable disposable = null)
            => feature
                .EnumerateUnsafeParentFeatures()
                .WrapDnaObject(disposable.GetDummyIfNull());
        #endregion

        #region ChildFeatures

        /// <summary>
        /// Enumerates child features of a feature (unsafe/non-wrapped version)
        /// </summary>
        /// <param name="feature">The feature to get children for</param>
        /// <returns>Enumerable of raw child features</returns>
        public static IEnumerable<Feature> EnumerateUnsafeChildFeatures(this ModelFeature feature)
        {
            var children = (object[])feature.UnsafeObject.GetChildren();

            if (children is null)
                yield break;

            foreach (var child in children)
                yield return (Feature)child;
        }

        /// <summary>
        /// Enumerates child features with optional disposable registration
        /// </summary>
        /// <param name="feature">The feature to get children for</param>
        /// <param name="disposable">Optional container for disposable management</param>
        /// <returns>Enumerable of wrapped child features</returns>
        public static IEnumerable<ModelFeature> EnumerateChildFeatures(this ModelFeature feature, ICompositeDisposable disposable = null)
            => feature
                .EnumerateUnsafeParentFeatures()
                .WrapDnaObject(disposable.GetDummyIfNull());

        #endregion

        #endregion
    }
}
