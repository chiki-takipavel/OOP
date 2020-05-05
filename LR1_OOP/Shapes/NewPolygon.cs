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
            Polygon polygon = new Polygon
            {
                Stroke = new SolidColorBrush(StrokeColor),
                Fill = new SolidColorBrush(FillColor),
                FillRule = FillRule.Nonzero,
                StrokeThickness = StrokeWidth,
                Points = Points
            };
            canvas.Children.Add(polygon);
        }
    }
}
