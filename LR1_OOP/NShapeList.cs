﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace LR1_OOP
{
    public class NShapeList
    {
        public List<NShape> Shapes { get; set; }

        public NShapeList()
        {
            Shapes = new List<NShape>();
        }

        public void Draw(Canvas canvas)
        {
            for (int i = 0; i <= Shapes.Count; i++)
            {
                Shapes[i].Draw(canvas);
            }
        }

    }
}
