using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Winform_task__Geometry1_
{
    /// <summary>
    /// Hatch main window
    /// </summary>
    public partial class Mainform : Form
    {
      public PointF first_point; /*public Point second_point;*///public PointF Xmax; public PointF Ymin;
      //public  List<PointF> store = new List<PointF>();

        public Graphics myGraphics;
        public Pen myPen = new Pen(Brushes.Black, 3);
        FileOption file_option;
        Line _line;
        Polygon _polygon;
        Polygons _polygons;
        Validation _validation;
        PolygonsList _polygonsList;
        LinesList _linesList;
        Hatch _hatch;
        public bool first_line; public bool check_inter;
        public Mainform()
        {
            InitializeComponent();
            _line = new Line();
            _polygon = new Polygon();
            _polygons = new Polygons();
            _validation = new Validation();
            _hatch = new Hatch();
            _polygonsList = new PolygonsList();
            _linesList = new LinesList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            myGraphics = pictureBox1.CreateGraphics();
            MouseEventArgs mouse_event = (MouseEventArgs)e;
            if (_line.Startpoint == PointF.Empty)
            {
                _line.Startpoint = mouse_event.Location; /*points_list.Add(_line.Startpoint);*/
                _linesList.points_list.Add(_line.Startpoint);
            }
            else
            {
                _line.Endpoint = mouse_event.Location;
                if (first_line == false)
                {
                    myGraphics.DrawLine(myPen, _line.Startpoint, _line.Endpoint);
                    _polygon.line.Add(_line); _linesList.points_list.Add(_line.Endpoint); /*points_list.Add(_line.Endpoint);*/
                    first_point = _line.Endpoint;
                    _line = new Line();
                    _line.Startpoint = first_point; first_line = true;
                }
                else
                {
                   if(_polygons.polygon.Count==0)
                    {
                        _validation.Do_Intersect1(_polygon, _line);
                        if (_validation.intersect == false)
                        {
                            myGraphics.DrawLine(myPen, _line.Startpoint, _line.Endpoint);
                            _polygon.line.Add(_line); _linesList.points_list.Add(_line.Endpoint);//points_list.Add(_line.Endpoint);
                            first_point = _line.Endpoint;
                            _line = new Line();
                            _line.Startpoint = first_point;
                        }
                        else
                        {
                            _validation.intersect = false;
                        }
                    }
                    else
                    {
                        _validation.Do_Intersect1(_polygon, _line);
                        _validation.Do_Intersect(_polygon, _line, _polygons);
                        if (_validation.intersect == false&&_validation.intersect==false)
                        {
                            myGraphics.DrawLine(myPen, _line.Startpoint, _line.Endpoint);
                            _polygon.line.Add(_line); _linesList.points_list.Add(_line.Endpoint);//points_list.Add(_line.Endpoint);
                            first_point = _line.Endpoint;
                            _line = new Line();
                            _line.Startpoint = first_point;
                        }
                        else
                        {
                            _validation.intersect = false;
                        }
                    }
                }
            }
            //List<Line> objLines1 = new List<Line>();
            //objLines1.Add(new Line { Startpoint = new PointF(10, 20) });
            //objLines1.Add(new Line { Startpoint = new PointF(25, 30) });

            //List<Line> objLines2 = new List<Line>();
            //objLines2.Add(new Line { Startpoint = new PointF(40, 0) });
            //objLines2.Add(new Line { Startpoint = new PointF(50, 100) });



            //Polygons parentPoly = new Polygons();
            //parentPoly.polygon.Add(new Polygon { line = objLines1 });
            //parentPoly.polygon.Add(new Polygon { line = objLines2 });

            //Polygon firstPloygon = parentPoly.polygon[0];

            //foreach (Line currentLine in firstPloygon.line)
            //{
            //    HatchBrush hBrush = new HatchBrush(HatchStyle.Horizontal, Color.Red, Color.FromArgb(255, 128, 255, 255));
            //   // myGraphics.FillPolygon(hBrush, currentLine, currentLine);
            //}
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_option = new FileOption();
            file_option.Save(this, _polygon, _line, _polygons);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file_option = new FileOption();
            file_option.Open(this, _polygon, _line, _polygons, _polygonsList, _linesList);
        }

        private void Mainform_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (_polygon.line.Count != 0)
                {
                    _line.Endpoint = _polygon.line[0].Startpoint;
                    if (_polygons.polygon.Count == 0)
                    {
                        _validation.Do_Intersect1(_polygon, _line);
                        if (_validation.intersect == false)
                        {
                            _line.Startpoint = _polygon.line[_polygon.line.Count - 1].Endpoint; _line.Endpoint = _polygon.line[0].Startpoint;

                            myGraphics.DrawLine(myPen, _line.Startpoint, _line.Endpoint);
                            first_point = _line.Endpoint;

                            _linesList.points_list.Add(_line.Endpoint);

                            _polygonsList.polygonList.Add(_linesList);
                            _polygon.line.Add(_line);
                            _polygons.polygon.Add(_polygon);
                            _line = new Line(); _polygon = new Polygon(); _linesList = new LinesList();
                            //_line.Startpoint = first_point;
                        }
                        else
                        {
                            _validation.intersect = false;
                        }
                    }
                    else
                    {
                        _validation.Do_Intersect1(_polygon, _line);
                        _validation.Do_Intersect(_polygon, _line, _polygons);
                        if (_validation.intersect == false && _validation.intersect == false)
                        {
                            _line.Startpoint = _polygon.line[_polygon.line.Count - 1].Endpoint; _line.Endpoint = _polygon.line[0].Startpoint;

                            myGraphics.DrawLine(myPen, _line.Startpoint, _line.Endpoint);
                            first_point = _line.Endpoint;

                            _linesList.points_list.Add(_line.Endpoint);

                            _polygonsList.polygonList.Add(_linesList);
                            _polygon.line.Add(_line);
                            _polygons.polygon.Add(_polygon);
                            _line = new Line(); _polygon = new Polygon(); _linesList = new LinesList();
                            //_line.Startpoint = first_point;
                        }
                        else
                        {
                            _validation.intersect = false;
                        }
                    }
                }
            }
                pictureBox1.Focus();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            _line = new Line();
            _polygon = new Polygon();
            _polygons = new Polygons(); 
            _linesList = new LinesList();
            _polygonsList = new PolygonsList();
            _hatch = new Hatch();
            pictureBox1.Focus();
        }

        private void button_Hatch_Click(object sender, EventArgs e)
        {
            // _validation.Hatch(this, _polygonsList, _linesList, _polygon, _polygons);
            _hatch._Hatch(this, _polygonsList, _linesList, _polygons);
            pictureBox1.Focus();
        }

        private void button_Offset_Click(object sender, EventArgs e)
        {
            _validation.Offset(this, _polygonsList, _linesList, _polygon, _polygons,_line);
            pictureBox1.Focus();
        }
    }
}
