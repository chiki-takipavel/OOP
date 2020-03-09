using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    class NewPolygon : NewShape
    {
        public NewPolygon()
        {
        }

        public NewPolygon(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public NewPolygon(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Polygon polygon = new Polygon();
            polygon.Stroke = StrokeBrush;
            polygon.Fill = FillBrush;
            polygon.FillRule = FillRule.Nonzero;
            polygon.StrokeThickness = StrokeWidth;
            PointCollection tempList = new PointCollection();
            foreach (Point point in Points)
            {
                tempList.Add(point);
            }
            polygon.Points = tempList;
            canvas.Children.Add(polygon);
        }
    }
}
