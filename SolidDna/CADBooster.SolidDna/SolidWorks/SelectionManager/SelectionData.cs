namespace CADBooster.SolidDna
{
    public struct SelectionData
    {
        public Point3D Point { get; set; }

        public int Mark { get; set; }

        public SelectionMode Mode { get; set; }

        public readonly static SelectionData Default = new SelectionData(Point3D.Zero, -1, SelectionMode.Override);

        public SelectionData(Point3D point3D, int mark, SelectionMode mode)
        {
            Point = point3D;
            Mark = mark;
            Mode = mode;
        }
    }
}

