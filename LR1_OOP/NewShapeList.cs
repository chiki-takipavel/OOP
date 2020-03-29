using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LR1_OOP
{
    [Serializable]
    public class NewShapeList
    {
        public List<NewShape> Shapes { get; set; }

        public NewShapeList()
        {
            Shapes = new List<NewShape>();
        }

        public void Draw(Canvas canvas)
        {
            for (int i = 0; i < Shapes.Count; i++)
            {
                try
                {
                    Shapes[i].Draw(canvas);
                }
                catch
                {
                    System.Windows.MessageBox.Show($"Повреждён объект {Shapes[i].GetType().Name}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
