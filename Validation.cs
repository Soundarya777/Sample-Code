using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Winform_task__Geometry1_
{
    class Validation
    {
        PointF intersection_point; //PointF Mid_Point;
        public bool intersect;
        //public float Xmax; public float Ymin; public float Ymax; public float Xmin;
        PointF out_start, out_end;
        public List<Line> store_Off = new List<Line>();
        public List<PointF> temp = new List<PointF>();

        /// <summary>
        /// To check intersection
        /// </summary>
        /// <param name="poly"></param>
        /// <param name="line"></param>
        /// <param name="polys"></param>
        public void Do_Intersect(Polygon poly, Line line, Polygons polys)
        { 
            float a1, b1, c1, a2, b2, c2, d;
            a1 = line.Endpoint.Y - line.Startpoint.Y;
            b1 = line.Startpoint.X - line.Endpoint.X;
            c1 = a1 * line.Startpoint.X + b1 * line.Startpoint.Y;
            for (int i = 0; i <= polys.polygon.Count - 1; i++)
            {
                for (int j = 0; j <= polys.polygon[i].line.Count - 1; j++)
                {
                    a2 = polys.polygon[i].line[j].Endpoint.Y - polys.polygon[i].line[j].Startpoint.Y;
                    b2 = polys.polygon[i].line[j].Startpoint.X - polys.polygon[i].line[j].Endpoint.X;
                    c2 = a2 * polys.polygon[i].line[j].Startpoint.X + b2 * polys.polygon[i].line[j].Startpoint.Y;
                    d = a1 * b2 - a2 * b1;
                    if (d != 0)
                    {
                        float x = (b2 * c1 - b1 * c2) / d;
                        float y = (a1 * c2 - a2 * c1) / d;
                        intersection_point.X = x; intersection_point.Y = y;

                        bool intersect_check1 = x > Math.Min(polys.polygon[i].line[j].Startpoint.X, polys.polygon[i].line[j].Endpoint.X) && x < Math.Max(polys.polygon[i].line[j].Startpoint.X, polys.polygon[i].line[j].Endpoint.X);
                        bool intersect_check2 = y > Math.Min(polys.polygon[i].line[j].Startpoint.Y, polys.polygon[i].line[j].Endpoint.Y) && y < Math.Max(polys.polygon[i].line[j].Startpoint.Y, polys.polygon[i].line[j].Endpoint.Y);
                        if (intersect_check1 == true && intersect_check2 == true && CurrentLine_Point(intersection_point, line) == true)
                        {
                            if (!Contains_Point(intersection_point, poly, polys, line))
                            {
                                MessageBox.Show("Lines are intersecting");
                                intersect = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// To intersection check
        /// </summary>
        /// <param name="poly"></param>
        /// <param name="line"></param>
        public void Do_Intersect1(Polygon poly, Line line)
        {
            float a1, b1, c1, a2, b2, c2, d;
            a1 = line.Endpoint.Y - line.Startpoint.Y;
            b1 = line.Startpoint.X - line.Endpoint.X;
            c1 = a1 * line.Startpoint.X + b1 * line.Startpoint.Y;
            for (int i = 0; i <= poly.line.Count - 1; i++)
            {
                a2 = poly.line[i].Endpoint.Y - poly.line[i].Startpoint.Y;
                b2 = poly.line[i].Startpoint.X - poly.line[i].Endpoint.X;
                c2 = a2 * poly.line[i].Startpoint.X + b2 * poly.line[i].Startpoint.Y;
                d = a1 * b2 - a2 * b1;
                if (d != 0)
                {
                    float x = (b2 * c1 - b1 * c2) / d;
                    float y = (a1 * c2 - a2 * c1) / d;
                    intersection_point.X = x; intersection_point.Y = y;

                    bool intersect_check1 = x > Math.Min(poly.line[i].Startpoint.X, poly.line[i].Endpoint.X) && x < Math.Max(poly.line[i].Startpoint.X, poly.line[i].Endpoint.X);
                    bool intersect_check2 = y > Math.Min(poly.line[i].Startpoint.Y, poly.line[i].Endpoint.Y) && y < Math.Max(poly.line[i].Startpoint.Y, poly.line[i].Endpoint.Y);
                    if (intersect_check1 == true && intersect_check2 == true && CurrentLine_Point(intersection_point, line) == true)
                    {
                        if (!Contains_Point1(intersection_point, poly, line))
                        {
                            MessageBox.Show("Lines are intersecting");
                            intersect = true;
                            break;
                        }
                    }
                }
            }
        }


        private bool Contains_Point(PointF intersection_point, Polygon poly, Polygons polys, Line line)
        {
            for (int i = 0; i <= polys.polygon.Count - 1; i++)
            {
                foreach (var item in polys.polygon[i].line)
                {
                    if (item.Startpoint == intersection_point || item.Endpoint == intersection_point)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool Contains_Point1(PointF intersection_point, Polygon poly, Line line)
        {
            foreach (var item in poly.line)
            {
                if (item.Startpoint == intersection_point || item.Endpoint == intersection_point)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CurrentLine_Point(PointF intersection_point, Line line)
        {
            bool intersect_check3 = ((intersection_point.X > Math.Min(line.Startpoint.X, line.Endpoint.X) && intersection_point.X < Math.Max(line.Startpoint.X, line.Endpoint.X)) && ((intersection_point.Y > Math.Min(line.Startpoint.Y, line.Endpoint.Y) && (intersection_point.Y < Math.Max(line.Startpoint.Y, line.Endpoint.Y)))));
            return intersect_check3;
        }


        /// <summary>
        /// Alternate method temp commented out
        /// </summary>
        /// <param name="mainform"></param>
        /// <param name="_polysList"></param>
        /// <param name="_linesList"></param>
        /// <param name="poly"></param>
        /// <param name="polys"></param>
        /// <param name="_line"></param>
        //public void Hatch(Mainform mainform, PolygonsList _polygonsList, LinesList _linesList, Polygon poly, Polygons polys)
        //{
        //    _linesList = new LinesList();
        //    _polygonsList = new PolygonsList();
        //    Open_Hatch(polys, _polygonsList, _linesList);
        //    for (int k = 0; k < _polygonsList.polygonList.Count; k++)
        //    {
        //        Max_Min(_polygonsList, /*_linesList,*/ k);
        //        float a1, b1, c1, a2, b2, c2, d, j;

        //        for (j = Ymin + 10; Ymin <= Ymax;)
        //        {
        //            a1 = Ymin - Ymin;
        //            b1 = Xmin - Xmax;
        //            c1 = a1 * Xmin + b1 * Ymin;

        //            for (int i = 0; i < _polygonsList.polygonList[k].points_list.Count - 1; i++)
        //            {
        //                a2 = _polygonsList.polygonList[k].points_list[i + 1].Y - _polygonsList.polygonList[k].points_list[i].Y;
        //                b2 = _polygonsList.polygonList[k].points_list[i].X - _polygonsList.polygonList[k].points_list[i + 1].X;
        //                c2 = a2 * _polygonsList.polygonList[k].points_list[i].X + b2 * _polygonsList.polygonList[k].points_list[i].Y;
        //                d = a1 * b2 - a2 * b1;
        //                if (d != 0)
        //                {
        //                    float x = (b2 * c1 - b1 * c2) / d;
        //                    float y = (a1 * c2 - a2 * c1) / d;

        //                    bool intersect_check1 = (x >= Math.Min(_polygonsList.polygonList[k].points_list[i].X, _polygonsList.polygonList[k].points_list[i + 1].X)) &&
        //                        (x <= Math.Max(_polygonsList.polygonList[k].points_list[i].X, _polygonsList.polygonList[k].points_list[i + 1].X));

        //                    bool intersect_check2 = (y >= Math.Min(_polygonsList.polygonList[k].points_list[i].Y, _polygonsList.polygonList[k].points_list[i + 1].Y)) &&
        //                        (y <= Math.Max(_polygonsList.polygonList[k].points_list[i].Y, _polygonsList.polygonList[k].points_list[i + 1].Y));

        //                    if (intersect_check1 == true && intersect_check2 == true)
        //                    {
        //                        intersection_point.X = x; intersection_point.Y = y;
        //                        store.Add(intersection_point);
        //                    }
        //                }
        //            }
        //            //Ymin = Ymin + 40;
        //            store = store.OrderBy(p => p.X).ToList();
        //            //Hatch_Sort();

        //            for (int i = 0; i < store.Count - 1;)
        //            {
        //                if (store.Count%2!=0)
        //                {
        //                    bool value = Hatch_Contain_Check(_polygonsList, k, i);
        //                    if (value == true) { /*i++ */}
        //                    else
        //                    {
        //                        mainform.myGraphics.DrawLine(mainform.myPen, store[i], store[i + 1]); i = i + 2;
        //                    }
        //                }
        //                else
        //                {
        //                    mainform.myGraphics.DrawLine(mainform.myPen, store[i], store[i + 1]);
        //                    i = i + 2;
        //                }

        //            }
        //            Ymin = Ymin + 10;
        //            store = new List<PointF>();
        //        }
        //    }
        //}


        //public void Max_Min(PolygonsList _polygonsList, int no)
        //{
        //    for (int l = no; l <= no;)
        //    {
        //        Xmin = _polygonsList.polygonList[l].points_list[0].X;//point_list[0].X;
        //        Xmax = _polygonsList.polygonList[l].points_list[0].X;
        //        Ymin = _polygonsList.polygonList[l].points_list[0].Y;
        //        Ymax = _polygonsList.polygonList[l].points_list[0].Y;

        //        for (int i = 0; i < _polygonsList.polygonList[l].points_list.Count(); i++)
        //        {
        //            Xmin = Math.Min(Xmin, _polygonsList.polygonList[l].points_list[i].X);
        //            Xmax = Math.Max(Xmax, _polygonsList.polygonList[l].points_list[i].X);
        //            Ymin = Math.Min(Ymin, _polygonsList.polygonList[l].points_list[i].Y);
        //            Ymax = Math.Max(Ymax, _polygonsList.polygonList[l].points_list[i].Y);
        //        }
        //        l++;
        //    }
        //}

       
        public void Offset(Mainform mainform, PolygonsList _polysList, LinesList _linesList, Polygon poly, Polygons polys, Line _line)
        {
            for (int i = 0; i < _polysList.polygonList.Count; i++)
            {
                for (int j = 0; j < _polysList.polygonList[i].points_list.Count - 1; j++)
                {
                    double d = Math.Sqrt(Convert.ToDouble(Math.Pow(_polysList.polygonList[i].points_list[j].X - _polysList.polygonList[i].points_list[j + 1].X, 2)) + Convert.ToDouble(Math.Pow(_polysList.polygonList[i].points_list[j].Y - _polysList.polygonList[i].points_list[j + 1].Y, 2)));

                    float X1p = (float)(_polysList.polygonList[i].points_list[j].X + 20 * (_polysList.polygonList[i].points_list[j].Y - _polysList.polygonList[i].points_list[j + 1].Y) / d);

                    float X2p = (float)(_polysList.polygonList[i].points_list[j + 1].X + 20 * (_polysList.polygonList[i].points_list[j].Y - _polysList.polygonList[i].points_list[j + 1].Y) / d);

                    float Y1p = (float)(_polysList.polygonList[i].points_list[j].Y + 20 * (_polysList.polygonList[i].points_list[j].X - _polysList.polygonList[i].points_list[j + 1].X) / d);

                    float Y2p = (float)(_polysList.polygonList[i].points_list[j + 1].Y + 20 * (_polysList.polygonList[i].points_list[j].X - _polysList.polygonList[i].points_list[j + 1].X) / d);

                    out_start.X = X1p; out_start.Y = Y1p; out_end.X = X2p; out_end.Y = Y2p;
                    _line.Startpoint = out_start;
                    _line.Endpoint = out_end;
                    store_Off.Add(_line);
                    _line = new Line();
                }
            }
            for (int i = 0; i <= store_Off.Count - 1; i++)
            {
                float a1, b1, c1, a2, b2, c2, d;
                if (i == store_Off.Count - 1)
                {
                    a1 = store_Off[i].Endpoint.Y - store_Off[i].Startpoint.Y;
                    b1 = store_Off[i].Startpoint.X - store_Off[i].Endpoint.X;
                    c1 = a1 * store_Off[i].Startpoint.X + b1 * store_Off[i].Startpoint.Y;

                    a2 = store_Off[0].Endpoint.Y - store_Off[0].Startpoint.Y;
                    b2 = store_Off[0].Startpoint.X - store_Off[0].Endpoint.X;
                    c2 = a2 * store_Off[0].Startpoint.X + b2 * store_Off[0].Startpoint.Y;
                    d = a1 * b2 - a2 * b1;

                    if (d != 0)
                    {
                        float x = (b2 * c1 - b1 * c2) / d;
                        float y = (a1 * c2 - a2 * c1) / d;
                        intersection_point.X = x; intersection_point.Y = y;
                        temp.Add(intersection_point);
                    }
                }
                else
                {
                    a1 = store_Off[i].Endpoint.Y - store_Off[i].Startpoint.Y;
                    b1 = store_Off[i].Startpoint.X - store_Off[i].Endpoint.X;
                    c1 = a1 * store_Off[i].Startpoint.X + b1 * store_Off[i].Startpoint.Y;

                    a2 = store_Off[i + 1].Endpoint.Y - store_Off[i + 1].Startpoint.Y;
                    b2 = store_Off[i + 1].Startpoint.X - store_Off[i + 1].Endpoint.X;
                    c2 = a2 * store_Off[i + 1].Startpoint.X + b2 * store_Off[i + 1].Startpoint.Y;
                    d = a1 * b2 - a2 * b1;
                    if (d != 0)
                    {
                        float x = (b2 * c1 - b1 * c2) / d;
                        float y = (a1 * c2 - a2 * c1) / d;
                        intersection_point.X = x; intersection_point.Y = y;
                        temp.Add(intersection_point);
                    }
                }
            }
            for (int i = 0; i <= temp.Count-1; i++)
            {
                if (i==temp.Count-1)
                {
                    mainform.myGraphics.DrawLine(mainform.myPen, temp[i], temp[0]);
                }
                else
                {
                    mainform.myGraphics.DrawLine(mainform.myPen, temp[i], temp[i + 1]);
                }
            }
            store_Off = new List<Line>();
            temp = new List<PointF>();
        }
    }
}

//Temp commented out
//private List<PointF> GetEnlargedPolygon(List<PointF> old_points, float offset)
//{
//List<PointF> enlarged_points = new List<PointF>();
//int num_points = old_points.Count;
//for (int j = 0; j < num_points; j++)
//{
//    // Find the new location for point j.
//    // Find the points before and after j.
//    int i = (j - 1);
//    if (i < 0) i += num_points;
//    int k = (j + 1) % num_points;

//    // Move the points by the offset.
//    Vector v1 = new Vector(
//        old_points[j].X - old_points[i].X,
//        old_points[j].Y - old_points[i].Y);
//    v1.Normalize();
//    v1 *= offset;
//    Vector n1 = new Vector(-v1.Y, v1.X);

//    PointF pij1 = new PointF(
//        (float)(old_points[i].X + n1.X),
//        (float)(old_points[i].Y + n1.Y));
//    PointF pij2 = new PointF(
//        (float)(old_points[j].X + n1.X),
//        (float)(old_points[j].Y + n1.Y));

//    Vector v2 = new Vector(
//        old_points[k].X - old_points[j].X,
//        old_points[k].Y - old_points[j].Y);
//    v2.Normalize();
//    v2 *= offset;
//    Vector n2 = new Vector(-v2.Y, v2.X);

//    PointF pjk1 = new PointF(
//        (float)(old_points[j].X + n2.X),
//        (float)(old_points[j].Y + n2.Y));
//    PointF pjk2 = new PointF(
//        (float)(old_points[k].X + n2.X),
//        (float)(old_points[k].Y + n2.Y));

//    // See where the shifted lines ij and jk intersect.
//    bool lines_intersect, segments_intersect;
//    PointF poi, close1, close2;
//    FindIntersection(pij1, pij2, pjk1, pjk2,
//        out lines_intersect, out segments_intersect,
//        out poi, out close1, out close2);



//    enlarged_points.Add(poi);
//}

//return enlarged_points;
//}


//public bool LineMid_Inside(List<PointF> store, PolygonsList polys_list, PointF Mid_Point, int o)
//{
//    bool inside = false;
//    for (int y = o; y <= o; y++)
//    {
//        /* sorted =*//* polys_list.polygonList[y].points_list.OrderBy(p => p.X).ToList();*/
//        for (int t = 0; t < polys_list.polygonList[y].points_list.Count - 1; t++)
//        {
//            if (polys_list.polygonList[y].points_list[t].X < polys_list.polygonList[y].points_list[t + 1].X)
//            {
//                P1 = polys_list.polygonList[y].points_list[t + 1];
//                P2 = polys_list.polygonList[y].points_list[t];
//            }
//            else
//            {
//                P1 = polys_list.polygonList[y].points_list[t];
//                P2 = polys_list.polygonList[y].points_list[t + 1];
//            }
//            if ((P2.X < Mid_Point.X) == (Mid_Point.X <= P1.X) && (Mid_Point.Y - P1.Y) * (P2.X - P1.X) < (P2.Y - P1.Y) * (Mid_Point.X - P1.X))
//            {
//                inside = true; /*!inside*/;
//            }
//        }
//    }
//    return inside;
//}


//    (List<PointF> points, PointF testPoint)

//        PointF p1, p2;
//    bool inside = false;
//    PointF lastPoint = points[points.Count - 1];
//        for (int i = 0; i<points.Count; i++)
//        {
//            if (lastPoint.X<points[i].X)
//            {
//                p1 = points[i];
//                p2 = lastPoint;
//            }
//            else
//            {
//                p2 = points[i];
//                p1 = lastPoint;
//            }
//            if ((points[i].X<testPoint.X) == (testPoint.X <= lastPoint.X)
//       && (testPoint.Y - (long)p1.Y) * (p2.X - p1.X)
//       < (p2.Y - (long)p1.Y) * (testPoint.X - p1.X))
//            {
//                inside = !inside;
//            }
//            lastPoint = points[i];
//        }
//        return inside;
//    }
//}


