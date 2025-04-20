using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CADBooster.SolidDna
{
    public static class ModelExtensions
    {
        public static IEnumerable<Annotation> EnumerateUnsafeAnnotations(this Model model)
        {
            var annotation = model.UnsafeObject.IGetFirstAnnotation2();

            while (annotation != null)
            {
                yield return annotation;
                annotation = annotation.GetNext3();
            }
        }

        public static IEnumerable<Note> EnumerateNotes(this Model model, ICompositeDisposable disposable)
            => model
                .EnumerateUnsafeAnnotations()
                .EnumerateNotes(disposable);

        public static IEnumerable<INote> EnumerateUnsafeNotes(this IEnumerable<Annotation> annotations, ICompositeDisposable disposable = null)
            => annotations
                .WrapDnaObjects(disposable.GetDummyIfNull())
                .Select(x => x.UnsafeObject.GetSpecificAnnotation() as INote)
                .Where(x => x != null);

        public static IEnumerable<Note> EnumerateNotes(this IEnumerable<Annotation> annotations, ICompositeDisposable disposable)
            => annotations
                .EnumerateUnsafeNotes(disposable)
                .WrapDnaObject(disposable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Note> WrapDnaObject(this IEnumerable<INote> dnaObjects, ICompositeDisposable disposable)
            => dnaObjects.Select(x => new Note(x));
    }
}
