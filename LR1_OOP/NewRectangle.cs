using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    class NRectangle : NShape
    {
        private Point startPoint;
        private Point endPoint;

        public NRectangle()
        {
        }

        public NRectangle(double strokeWidth, Color strokeColor, Color fillColor, Point startPoint, Point endPoint) : base(strokeWidth, strokeColor, fillColor)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public NRectangle(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, Point startPoint, Point endPoint) : base(strokeWidth, strokeColor, fillColor)
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
            Path path = new Path();
            path.Stroke = StrokeBrush;
            path.Fill = FillBrush;
            path.StrokeThickness = StrokeWidth;
            RectangleGeometry rectangle = new RectangleGeometry(new Rect(new Point(startPoint.X, startPoint.Y), new Point(endPoint.X, endPoint.Y)));
            path.Data = rectangle;
            canvas.Children.Add(path);
        }

    }

}
