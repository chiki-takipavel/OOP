using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    public class NewRectangle : NewShape
    {
        public NewRectangle()
        {
        }

        public NewRectangle(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public NewRectangle(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Path path = new Path();
            path.Stroke = new SolidColorBrush(StrokeColor);
            path.Fill = new SolidColorBrush(FillColor);
            path.StrokeThickness = StrokeWidth;
            RectangleGeometry rectangle = new RectangleGeometry(new Rect(new Point(Points[0].X, Points[0].Y), new Point(Points[1].X, Points[1].Y)));
            path.Data = rectangle;
            canvas.Children.Add(path);
        }

    }

}
