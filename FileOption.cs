using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using System.Xml;

namespace Winform_task__Geometry1_
{
    public class FileOption
    { 

        public void Save(Mainform mainform, Polygon poly, Line line, Polygons polys)
        {
            SaveFileDialog save_fd = new SaveFileDialog();
            save_fd.FileName = "*.xml";
            save_fd.Filter = "XML Documents(*.xml)|*.txt| All Files (*.*)|*.*";
            if (save_fd.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xml_serializer = new XmlSerializer(polys.polygon/*poly.line*/.GetType());
                using (StreamWriter stream_writer = new StreamWriter(save_fd.FileName))
                {
                    xml_serializer.Serialize(stream_writer, polys.polygon/*poly.line*/);
                    stream_writer.Close();
                    mainform.Text = Path.GetFileName(save_fd.FileName);
                }
            }
        }

        public void Open(Mainform mainform, Polygon poly, Line line, Polygons polys, PolygonsList poly_list, LinesList _lineslist)
        {
            mainform.first_line = true;
            OpenFileDialog open_fd = new OpenFileDialog();
            if (open_fd.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xml_serializer = new XmlSerializer(polys.polygon./*poly.line.*/GetType());
                using (FileStream file_stream = new FileStream(open_fd.FileName, FileMode.Open))
                {
                    polys.polygon = (List<Polygon>)xml_serializer.Deserialize(file_stream);
                    mainform.pictureBox1.Refresh();
                    Draw(poly, mainform, line, polys);
                    mainform.Text = Path.GetFileName(open_fd.FileName);
                }
            }
        }

        public void Draw(Polygon poly, Mainform mainform, Line line, Polygons polys)
        {
            mainform.myGraphics = mainform.pictureBox1.CreateGraphics();
            for (int i = 0; i <= polys.polygon.Count - 1; i++)
            {
                for (int j = 0; j <= polys.polygon[i].line.Count - 1; j++)
                {
                    mainform.myGraphics.DrawLine(mainform.myPen, polys.polygon[i].line[j].Startpoint, polys.polygon[i].line[j].Endpoint);
                }
            }
        }
        

        public void Hatch_old(Mainform mainform,PolygonsList _polygonsList, LinesList _linesList)
        {
            HatchBrush hBrush = new HatchBrush(HatchStyle.Horizontal, Color.Red, Color.FromArgb(255, 128, 255, 255));
            for (int i = 0; i <= _polygonsList.polygonList.Count-1; i++)
            {
                mainform.myGraphics.FillPolygon(hBrush, _polygonsList.polygonList[i].points_list.ToArray());
            }
        }
    }
 }

