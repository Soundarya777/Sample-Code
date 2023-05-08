using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_task__Geometry1_
{
    /// <summary>
    /// Polygon collection data
    /// </summary>
    public class Polygons
    {
        public List<Polygon> polygon = new List<Polygon>();
    }

    public class Polygon
    {
        public List<Line> line = new List<Line>();
    }

    public class Line
    {
        public PointF Startpoint { get; set; }
        public PointF Endpoint { get; set; }
        //public Line()
        //{
        //    Startpoint = new PointF();
        //    Endpoint = new PointF();
        // }
    }

    public class PolygonsList
    {
        public List<LinesList> polygonList = new List<LinesList>();
    }
    public class LinesList
    {
        public List<PointF> points_list = new List<PointF>();
    }
}

