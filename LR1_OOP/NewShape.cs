using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace LR1_OOP
{
    public abstract class NewShape
    {
        private double strokeWidth;
        private SolidColorBrush strokeBrush;
        private SolidColorBrush fillBrush;

        public NewShape()
        {
            
        }

        public NewShape(double sWidth, Color sColor, Color fColor)
        {
            strokeWidth = sWidth;
            strokeBrush = new SolidColorBrush(sColor);
            fillBrush = new SolidColorBrush(fColor);
        }

        public NewShape(double sWidth, SolidColorBrush sColor, SolidColorBrush fColor)
        {
            strokeWidth = sWidth;
            strokeBrush = sColor;
            fillBrush = fColor;
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

        public abstract void Draw(Canvas canvas);
    }
}
