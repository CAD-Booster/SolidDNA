namespace CADBooster.SolidDna
{
    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static readonly Point3D Zero = new Point3D(0, 0, 0);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() => $"Person {{ X = {X}, Y = {Y}, Z = {Z}}}";
    }
}

