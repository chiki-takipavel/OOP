using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    public class NewEllipse : NewShape
    {
        public NewEllipse()
        {
        }

        public NewEllipse(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public NewEllipse(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Path path = new Path
            {
                Stroke = new SolidColorBrush(StrokeColor),
                Fill = new SolidColorBrush(FillColor),
                StrokeThickness = StrokeWidth
            };
            EllipseGeometry ellipse = new EllipseGeometry(new Rect(new Point(Points[0].X, Points[0].Y), new Point(Points[1].X, Points[1].Y)));
            path.Data = ellipse;
            canvas.Children.Add(path);
        }
    }
}
