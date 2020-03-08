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
        Color defaultStrColor;
        Color defaultFillColor;
        double defaultStrWidth;
        NewShapeList list;

        public MainWindow()
        {
            InitializeComponent();
            defaultStrColor = Colors.Black;
            defaultFillColor = Colors.White;
            defaultStrWidth = 7;

            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");

            list = new NewShapeList();
            list.Shapes.Add(new NewLine(defaultStrWidth, defaultStrColor, defaultFillColor, new Point(56, 345), new Point(467, 475)));
            list.Shapes.Add(new NewRectangle(defaultStrWidth, defaultStrColor, defaultFillColor, new Point(10, 10), new Point(200, 200)));
            list.Shapes.Add(new NewEllipse(defaultStrWidth, defaultStrColor, defaultFillColor, new Point(400, 400), new Point(600, 600)));

            slidStrWidth.Value = defaultStrWidth;
            rectStrokeColor.Fill = new SolidColorBrush(defaultStrColor);
            rectFillColor.Fill = new SolidColorBrush(defaultFillColor);
        }

        private void btnStrokeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                SolidColorBrush brushColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                list.Shapes[cmbShapes.SelectedIndex].StrokeBrush = brushColor;
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
                list.Shapes[cmbShapes.SelectedIndex].FillBrush = brushColor;
                rectFillColor.Fill = brushColor;
            }
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            list.Shapes[cmbShapes.SelectedIndex].Draw(canvasField);
        }

        private void slidStrWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            list.Shapes[cmbShapes.SelectedIndex].StrokeWidth = slidStrWidth.Value;
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }

}
