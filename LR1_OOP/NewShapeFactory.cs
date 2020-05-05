using System.Windows.Media;

namespace LR1_OOP
{
    public abstract class NewShapeFactory
    {
        public string ShapeName { get; set; }
        public int PointsCount { get; set; }
        public bool IsChangeablePoints { get; set; }
        public abstract NewShape Create(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points);
    }

    public class LineFactory : NewShapeFactory
    {
        public LineFactory()
        {
            ShapeName = "Линия";
            PointsCount = 2;
            IsChangeablePoints = false;
        }
        public override NewShape Create(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points)
        {
            return new NewLine(strokeWidth, strokeColor, fillColor, points);
        }
    }

    public class RectangleFactory : NewShapeFactory
    {
        public RectangleFactory()
        {
            ShapeName = "Прямоугольник";
            PointsCount = 2;
            IsChangeablePoints = false;
        }
        public override NewShape Create(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points)
        {
            return new NewRectangle(strokeWidth, strokeColor, fillColor, points);
        }
    }

    public class EllipseFactory : NewShapeFactory
    {
        public EllipseFactory()
        {
            ShapeName = "Эллипс";
            PointsCount = 2;
            IsChangeablePoints = false;
        }
        public override NewShape Create(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points)
        {
            return new NewEllipse(strokeWidth, strokeColor, fillColor, points);
        }
    }

    public class PolygonFactory : NewShapeFactory
    {
        public PolygonFactory()
        {
            ShapeName = "Многоугольник";
            PointsCount = 3;
            IsChangeablePoints = true;
        }
        public override NewShape Create(double strokeWidth, Color strokeColor, Color fillColor, PointCollection points)
        {
            return new NewPolygon(strokeWidth, strokeColor, fillColor, points);
        }
    }
}
