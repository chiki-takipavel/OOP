using LR1_OOP;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewTrapezium
{
    public class NewTrapezium : NewShape
    {
        public NewTrapezium()
        {
        }

        public NewTrapezium(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public NewTrapezium(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Points.Add(new Point());
            Points.Add(new Point());
            Point[] pointsArray = Points.ToArray();
            pointsArray[3] = pointsArray[1];
            pointsArray[2].Y = pointsArray[0].Y;
            pointsArray[1].Y = pointsArray[2].Y;
            pointsArray[1].X = ((pointsArray[3].X - pointsArray[0].X) / 4) + pointsArray[0].X;
            pointsArray[2].X = ((pointsArray[3].X - pointsArray[0].X) / 4 * 3) + pointsArray[0].X;
            pointsArray[0].Y = pointsArray[3].Y;
            Points.Clear();
            foreach (Point p in pointsArray)
            {
                Points.Add(p);
            }
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
