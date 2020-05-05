using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace LR1_OOP
{
    public abstract class NewShape
    {
        public double StrokeWidth { get; set; }

        [XmlIgnore]
        public Color StrokeColor { get; set; }

        [XmlIgnore]
        public Color FillColor { get; set; }

        [XmlElement("StrokeColor")]
        public string StrokeColorString
        {
            get { return StrokeColor.ToString(); }
            set { StrokeColor = (Color)ColorConverter.ConvertFromString(value); }
        }

        [XmlElement("FillColor")]
        public string FillColorString
        {
            get { return FillColor.ToString(); }
            set { FillColor = (Color)ColorConverter.ConvertFromString(value); }
        }

        public PointCollection Points { get; set; }

        public NewShape()
        {
            StrokeWidth = 7;
            StrokeColor = Colors.Black;
            FillColor = Colors.White;
        }

        public NewShape(double sWidth, Color sColor, Color fColor, PointCollection points)
        {
            StrokeWidth = sWidth;
            StrokeColor = sColor;
            FillColor = fColor;
            PointCollection tempPoints = new PointCollection();
            foreach (Point point in points)
            {
                tempPoints.Add(point);
            }
            this.Points = tempPoints;
        }

        public NewShape(double sWidth, SolidColorBrush sBrush, SolidColorBrush fBrush, PointCollection points)
        {
            StrokeWidth = sWidth;
            StrokeColor = sBrush.Color;
            FillColor = fBrush.Color;
            PointCollection tempPoints = new PointCollection();
            foreach (Point point in points)
            {
                tempPoints.Add(point);
            }
            this.Points = tempPoints;
        }

        public abstract void Draw(Canvas canvas);
    }
}
