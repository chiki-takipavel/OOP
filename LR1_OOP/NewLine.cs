using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    public class NewLine : NewShape
    {
        private Point startPoint;
        private Point endPoint;

        public NewLine()
        {
        }

        public NewLine(double strokeWidth, Color strokeColor, Color fillColor, Point startPoint, Point endPoint) : base (strokeWidth, strokeColor, fillColor)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public NewLine(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, Point startPoint, Point endPoint) : base(strokeWidth, strokeColor, fillColor)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public Point EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public override void Draw(Canvas canvas)
        {
            Line line = new Line();
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
            line.Stroke = StrokeBrush;
            line.Fill = FillBrush;
            line.StrokeThickness = StrokeWidth;
            canvas.Children.Add(line);
        }

    }
}
