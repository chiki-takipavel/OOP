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
        private SolidColorBrush brushStroke;
        private SolidColorBrush brushFill;
        private double widthStroke;
        private PointCollection pointsList;
        //private bool flagDraw;
        private int countPoints;
        private NewShapeList list;

        public int CountPoints
        {
            get { return countPoints; }
            set
            {
                if (value < 2)
                {
                    countPoints = 2;
                }
                else if (value > 20)
                {
                    countPoints = 20;
                }
                else
                {
                    countPoints = value;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            brushStroke = new SolidColorBrush(Colors.Black);
            brushFill = new SolidColorBrush(Colors.White);
            widthStroke = 7;
            pointsList = new PointCollection();
            //flagDraw = false;
            CountPoints = 2;
            txtCountPoints.DataContext = this;

            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");
            comboItems.Add("Полигон");

            list = new NewShapeList();
            list.Shapes.Add(new NewLine(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewRectangle(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewEllipse(widthStroke, brushStroke, brushFill, pointsList));
            list.Shapes.Add(new NewEllipse(widthStroke, brushStroke, brushFill, pointsList));

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
            //pointsList.Clear();
            //flagDraw = !flagDraw;
            //if (flagDraw)
            //{
            //    //cmbShapes.IsEnabled = false;
            //    btnDraw.Content = "Отмена";
            //    System.Windows.MessageBox.Show("Выберите точки на экране", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    //cmbShapes.IsEnabled = true;
            //    btnDraw.Content = "Нарисовать";
            //}
        }

        private void canvasField_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (flagDraw)
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
            pointsList.Clear();
            CountPoints = 2;
            if (cmbShapes.SelectedIndex == 3)
            {
                panelCountPoints.Visibility = Visibility.Visible;
            }
            else
            {
                panelCountPoints.Visibility = Visibility.Hidden;
            }
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }

}