using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LR1_OOP
{
    class NewEllipse : NewShape
    {
        private Point startPoint;
        private Point endPoint;

        public NewEllipse()
        {
        }

        public NewEllipse(double strokeWidth, Color strokeColor, Color fillColor, Point startPoint, Point endPoint) : base(strokeWidth, strokeColor, fillColor)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        public NewEllipse(double strokeWidth, SolidColorBrush strokeColor, SolidColorBrush fillColor, Point startPoint, Point endPoint) : base(strokeWidth, strokeColor, fillColor)
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
            EllipseGeometry ellipse = new EllipseGeometry(new Rect(new Point(startPoint.X, startPoint.Y), new Point(endPoint.X, endPoint.Y)));
            path.Data = ellipse;
            canvas.Children.Add(path);
        }

    }

}
