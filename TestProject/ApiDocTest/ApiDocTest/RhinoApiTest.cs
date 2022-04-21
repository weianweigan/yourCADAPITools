using Rhino.Geometry;

namespace ApiDocTest
{
    public class RhinoApiTest
    {
        private Point3d _pt;

        public Point3d Point { get; set; }

        public void Test()
        {
            var plane = new Plane();
            var or = plane.Origin; ;
            plane.PointAt(0,0);
        }
    }
        
}