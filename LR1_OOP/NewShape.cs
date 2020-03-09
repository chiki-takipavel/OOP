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
            this.points = points;
        }

        public NewShape(double sWidth, SolidColorBrush sColor, SolidColorBrush fColor, PointCollection points)
        {
            strokeWidth = sWidth;
            strokeBrush = sColor;
            fillBrush = fColor;
            this.points = points; 
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
