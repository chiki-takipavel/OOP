using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Windows.Input;

namespace LR1_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush brushStroke;
        private SolidColorBrush brushFill;
        private double widthStroke;
        private PointCollection pointsList;
        private int countPoints;
        private NewShapeList list;

        public int CountPoints
        {
            get { return countPoints; }
            set { countPoints = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            brushStroke = new SolidColorBrush(Colors.Black);
            brushFill = new SolidColorBrush(Colors.White);
            widthStroke = 7;
            pointsList = new PointCollection();
            countPoints = 2;
            txtCountPoints.DataContext = this;

            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");
            comboItems.Add("Многоугольник");

            list = new NewShapeList();
            list.Shapes.Add(new NewLine(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewRectangle(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewEllipse(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewPolygon(widthStroke, brushStroke, brushFill, pointsList));

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

        private void canvasField_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pointsList.Add(e.GetPosition(canvasField));
            if (pointsList.Count == CountPoints)
            {
                list.Shapes[cmbShapes.SelectedIndex].StrokeBrush = brushStroke;
                list.Shapes[cmbShapes.SelectedIndex].FillBrush = brushFill;
                list.Shapes[cmbShapes.SelectedIndex].StrokeWidth = widthStroke;
                list.Shapes[cmbShapes.SelectedIndex].Points = pointsList;
                list.Shapes[cmbShapes.SelectedIndex].Draw(canvasField);
                pointsList.Clear();
            }
        }

        private void slidStrWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            widthStroke = slidStrWidth.Value;
        }

        private void cmbShapes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CountPoints = 2;
            pointsList.Clear();
            if (cmbShapes.SelectedItem.ToString() == "Многоугольник")
            {
                txtCountPoints.Text = "3";  
                panelCountPoints.Visibility = Visibility.Visible;
            }
            else
            {
                panelCountPoints.Visibility = Visibility.Hidden;
            }
        }

        private void txtCountPoints_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            pointsList.Clear();
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void gridMain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) && (e.Key == Key.Z))
            {
                int canvasElemCount = canvasField.Children.Count;
                if (canvasElemCount != 0)
                {
                    canvasField.Children.RemoveAt(canvasElemCount - 1);
                }
            }
        }
    }
}