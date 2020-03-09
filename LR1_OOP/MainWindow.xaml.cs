using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace LR1_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush brushStroke;
        SolidColorBrush brushFill;
        double widthStroke;
        NewShapeList list;

        public MainWindow()
        {
            InitializeComponent();
            brushStroke = new SolidColorBrush(Colors.Black);
            brushFill = new SolidColorBrush(Colors.White);
            widthStroke = 7;

            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");

            list = new NewShapeList();
            list.Shapes.Add(new NewLine(widthStroke, brushStroke, brushFill, new Point(56, 345), new Point(467, 475)));
            list.Shapes.Add(new NewRectangle(widthStroke, brushStroke, brushFill, new Point(10, 10), new Point(200, 200)));
            list.Shapes.Add(new NewEllipse(widthStroke, brushStroke, brushFill, new Point(400, 400), new Point(600, 600)));

            slidStrWidth.Value = widthStroke;
            rectStrokeColor.Fill = brushStroke;
            rectFillColor.Fill = brushFill;
        }

        private void btnStrokeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                SolidColorBrush brushColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                brushStroke = brushColor;
                rectStrokeColor.Fill = brushColor;
            }
        }

        private void btnFillColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                SolidColorBrush brushColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                brushFill = brushColor;
                rectFillColor.Fill = brushColor;
            }
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            list.Shapes[cmbShapes.SelectedIndex].FillBrush = brushFill;
            list.Shapes[cmbShapes.SelectedIndex].StrokeBrush = brushStroke;
            list.Shapes[cmbShapes.SelectedIndex].StrokeWidth = widthStroke;
            list.Shapes[cmbShapes.SelectedIndex].Draw(canvasField);
        }

        private void slidStrWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            widthStroke = slidStrWidth.Value;
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }

}
