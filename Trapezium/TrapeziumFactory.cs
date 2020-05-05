using LR1_OOP;
using System.Windows.Media;

namespace NewTrapezium
{
    public class TrapeziumFactory : NewShapeFactory
    {
        public TrapeziumFactory()
        {
            ShapeName = "Трапеция";
            PointsCount = 2;
            IsChangeablePoints = false;
        }
        public override NewShape Create(double strokeWidth, System.Windows.Media.Color strokeColor, System.Windows.Media.Color fillColor, PointCollection points)
        {
            return new NewTrapezium(strokeWidth, strokeColor, fillColor, points);
        }
    }
}
