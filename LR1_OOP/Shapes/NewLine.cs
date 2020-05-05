using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    public class NewLine : NewShape
    {
        public NewLine()
        {
        }

        public NewLine(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public NewLine(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, PointCollection points) : base(strokeWidth, strokeColor, fillColor, points)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Line line = new Line
            {
                X1 = Points[0].X,
                Y1 = Points[0].Y,
                X2 = Points[1].X,
                Y2 = Points[1].Y,
                Stroke = new SolidColorBrush(StrokeColor),
                Fill = new SolidColorBrush(FillColor),
                StrokeThickness = StrokeWidth
            };
            canvas.Children.Add(line);
        }
    }
}
