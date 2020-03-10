using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LR1_OOP
{
    public abstract class NewShape
    {
        private double strokeWidth;
        private SolidColorBrush strokeBrush;
        private SolidColorBrush fillBrush;
        private PointCollection points;

        public NewShape()
        {
            
        }

        public NewShape(double sWidth, Color sColor, Color fColor, PointCollection points)
        {
            strokeWidth = sWidth;
            strokeBrush = new SolidColorBrush(sColor);
            fillBrush = new SolidColorBrush(fColor);
            PointCollection tempList = new PointCollection();
            foreach (Point point in points)
            {
                tempList.Add(point);
            }
            this.points = tempList;
        }

        public NewShape(double sWidth, SolidColorBrush sBrush, SolidColorBrush fBrush, PointCollection points)
        {
            strokeWidth = sWidth;
            strokeBrush = sBrush;
            fillBrush = fBrush;
            PointCollection tempList = new PointCollection();
            foreach (Point point in points)
            {
                tempList.Add(point);
            }
            this.points = tempList; 
        }

        public double StrokeWidth
        {
            get { return strokeWidth; }
            set { strokeWidth = value; }
        }

        public SolidColorBrush StrokeBrush
        {
            get { return strokeBrush; }
            set { strokeBrush = value; }
        }

        public SolidColorBrush FillBrush
        {
            get { return fillBrush; }
            set { fillBrush = value; }
        }

        public PointCollection Points
        {
            get { return points; }
            set { points = value; }
        }

        public abstract void Draw(Canvas canvas);
    }
}
