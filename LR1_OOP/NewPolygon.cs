using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    public class NewPolygon : NewShape
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
            polygon.Stroke = new SolidColorBrush(StrokeColor);
            polygon.Fill = new SolidColorBrush(FillColor);
            polygon.FillRule = FillRule.Nonzero;
            polygon.StrokeThickness = StrokeWidth;
            polygon.Points = Points;
            canvas.Children.Add(polygon);
        }
    }
}
