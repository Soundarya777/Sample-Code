using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Winform_task__Geometry1_
{
    class Hatch
    {
        PointF intersection_point;
        public float Xmax; public float Ymin; public float Ymax; public float Xmin;
        public List<PointF> store_H = new List<PointF>();
        public void _Hatch(Mainform mainform, PolygonsList _polygonsList, LinesList _linesList, Polygons polys)
        {
            _linesList = new LinesList();
            _polygonsList = new PolygonsList();
            Open_Hatch(polys, _polygonsList, _linesList);
            for (int k = 0; k < _polygonsList.polygonList.Count; k++)
            {
                Max_Min(_polygonsList, /*_linesList,*/ k);
                float a1, b1, c1, a2, b2, c2, d, j;

                for (j = Ymin + 10; Ymin <= Ymax;)
                {
                    a1 = Ymin - Ymin;
                    b1 = Xmin - Xmax;
                    c1 = a1 * Xmin + b1 * Ymin;

                    for (int i = 0; i < _polygonsList.polygonList[k].points_list.Count - 1; i++)
                    {
                        a2 = _polygonsList.polygonList[k].points_list[i + 1].Y - _polygonsList.polygonList[k].points_list[i].Y;
                        b2 = _polygonsList.polygonList[k].points_list[i].X - _polygonsList.polygonList[k].points_list[i + 1].X;
                        c2 = a2 * _polygonsList.polygonList[k].points_list[i].X + b2 * _polygonsList.polygonList[k].points_list[i].Y;
                        d = a1 * b2 - a2 * b1;
                        if (d != 0)
                        {
                            float x = (b2 * c1 - b1 * c2) / d;
                            float y = (a1 * c2 - a2 * c1) / d;

                            bool intersect_check1 = (x >= Math.Min(_polygonsList.polygonList[k].points_list[i].X, _polygonsList.polygonList[k].points_list[i + 1].X)) &&
                                (x <= Math.Max(_polygonsList.polygonList[k].points_list[i].X, _polygonsList.polygonList[k].points_list[i + 1].X));

                            bool intersect_check2 = (y >= Math.Min(_polygonsList.polygonList[k].points_list[i].Y, _polygonsList.polygonList[k].points_list[i + 1].Y)) &&
                                (y <= Math.Max(_polygonsList.polygonList[k].points_list[i].Y, _polygonsList.polygonList[k].points_list[i + 1].Y));

                            if (intersect_check1 == true && intersect_check2 == true)
                            {
                                intersection_point.X = x; intersection_point.Y = y;
                                store_H.Add(intersection_point);
                            }
                        }
                    }
                    store_H = store_H.OrderBy(p => p.X).ToList();
                    //Hatch_Sort();

                    for (int i = 0; i < store_H.Count - 1;)
                    {
                        if (store_H.Count % 2 != 0)
                        {
                            bool value = Hatch_Contain_Check(_polygonsList, k, i);
                            if (value == true) { /*i++ */}
                            else
                            {
                                mainform.myGraphics.DrawLine(mainform.myPen, store_H[i], store_H[i + 1]); i = i + 2;
                            }
                        }
                        else
                        {
                            mainform.myGraphics.DrawLine(mainform.myPen, store_H[i], store_H[i + 1]);
                            i = i + 2;
                        }
                    }
                    Ymin = Ymin + 10;
                    store_H = new List<PointF>();
                }
            }
        }


        public void Max_Min(PolygonsList _polygonsList, int no)
        {
            for (int l = no; l <= no;)
            {
                Xmin = _polygonsList.polygonList[l].points_list[0].X;//point_list[0].X;
                Xmax = _polygonsList.polygonList[l].points_list[0].X;
                Ymin = _polygonsList.polygonList[l].points_list[0].Y;
                Ymax = _polygonsList.polygonList[l].points_list[0].Y;

                for (int i = 0; i < _polygonsList.polygonList[l].points_list.Count(); i++)
                {
                    Xmin = Math.Min(Xmin, _polygonsList.polygonList[l].points_list[i].X);
                    Xmax = Math.Max(Xmax, _polygonsList.polygonList[l].points_list[i].X);
                    Ymin = Math.Min(Ymin, _polygonsList.polygonList[l].points_list[i].Y);
                    Ymax = Math.Max(Ymax, _polygonsList.polygonList[l].points_list[i].Y);
                }
                l++;
            }
        }

        public void Open_Hatch(Polygons polys, PolygonsList polys_list, LinesList _lineslist)
        {
            for (int i = 0; i < polys.polygon.Count; i++)
            {
                for (int j = 0; j < polys.polygon[i].line.Count; j++)
                {
                    _lineslist.points_list.Add(polys.polygon[i].line[j].Startpoint);
                }
                _lineslist.points_list.Add(polys.polygon[i].line[0].Startpoint);
                polys_list.polygonList.Add(_lineslist);
                _lineslist = new LinesList();
            }
        }


        public bool Hatch_Contain_Check(PolygonsList _polysList, int d, int w)
        {
            bool value = false;
            for (int k = d; k <= d; k++)
            {
                for (int i = 0; i < _polysList.polygonList[k].points_list.Count - 1; i++)
                {
                    for (int j = w; j <= w; j++)
                    {
                        if (_polysList.polygonList[k].points_list.Contains(store_H[j]))
                        {
                            value = true; store_H.RemoveAt(j); break;
                        }
                        else
                        {
                            value = false;
                        }
                    }
                    if (value == true)
                    {
                        break;
                    }
                }
            }
            return value;
        }
    }
}
