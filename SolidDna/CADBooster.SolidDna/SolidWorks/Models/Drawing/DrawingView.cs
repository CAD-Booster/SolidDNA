using SolidWorks.Interop.sldworks;

namespace CADBooster.SolidDna;

/// <summary>
/// A view of a drawing
/// </summary>
public class DrawingView : SolidDnaObject<View>, IDrawingView
{
    #region Public Properties

    /// <summary>
    /// The drawing view type.
    /// </summary>
    public DrawingViewType ViewType => (DrawingViewType) BaseObject.Type;

    /// <summary>
    /// The name of the view.
    /// </summary>
    public string Name => BaseObject.Name;

    /// <summary>
    /// The X position of the view origin with respect to the drawing sheet origin.
    /// </summary>
    public double PositionX => ((double[]) BaseObject.Position)[0];

    /// <summary>
    /// The Y position of the view origin with respect to the drawing sheet origin.
    /// </summary>
    public double PositionY => ((double[]) BaseObject.Position)[1];

    /// <summary>
    /// The bounding box of the view.
    /// </summary>
    public BoundingBox BoundingBox
    {
        get
        {
            var box = (double[]) BaseObject.GetOutline();
            return new BoundingBox(box[0], box[1], box[2], box[3]);
        }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="comObject">The underlying COM object</param>
    public DrawingView(View comObject) : base(comObject)
    {
    }

    #endregion

    #region Public methods

    #region Convert entity context

    /// <summary>
    /// Convert an entity that you got from a Model to a drawing view context.
    /// According to the docs, this works for every type that has a persistent ID, but it does not seem to work for configurations and display dimensions.
    /// Works for entities: geometry, features and sketches.
    /// </summary>
    /// <typeparam name="TObjectType">Input and output type of the object.</typeparam>
    /// <param name="modelContextObject">The object from a model context that must be converted to drawing view context.</param>
    /// <returns>The object converted to the drawing view context, or null when it fails.</returns>
    public TObjectType GetObjectInViewContext<TObjectType>(TObjectType modelContextObject) where TObjectType : class
    {
        if (modelContextObject == null)
            return null;

        // If we wrap a type, we should use the underlying object to get the corresponding object, then wrap it again.
        // Types that we wrap but where GetCorresponding does not work: ModelConfiguration, ModelDisplayDimension
        if (typeof(TObjectType) == typeof(ModelFeature))
        {
            // Cast it to an object first or it doesn't compile.
            // Then cast it to a ModelFeature.
            var feature = (ModelFeature) (object) modelContextObject;

            // Get the feature in the component context
            var featureInComponent = (Feature) UnsafeObject.GetCorresponding(feature.UnsafeObject);

            // Wrap it again
            return (TObjectType) (object) new ModelFeature(featureInComponent).CreateOrNull();
        }

        if (typeof(TObjectType) == typeof(FeatureSketch))
        {
            // Cast it to a FeatureSketch
            var featureSketch = (FeatureSketch) (object) modelContextObject;

            // Get the sketch in the component context. Returns a Feature, not a sketch
            var corresponding = (Feature) UnsafeObject.GetCorresponding(featureSketch.UnsafeObject);

            // Get the sketch from the feature
            var sketch = (Sketch) corresponding.GetSpecificFeature2();

            // Wrap it again
            return (TObjectType) (object) new FeatureSketch(sketch).CreateOrNull();
        }

        return (TObjectType) UnsafeObject.GetCorresponding(modelContextObject);
    }

    #endregion

    #endregion
}