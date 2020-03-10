using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;

namespace LR1_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SolidColorBrush brushStroke;
        private SolidColorBrush brushFill;
        private double widthStroke;
        private PointCollection pointsList;
        private int countPoints;
        private NewShapeList listShapes;
        private List<Type> listShapesTypes;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CountPoints
        {
            get { return countPoints; }
            set 
            {
                if (value != countPoints)
                { 
                    countPoints = value;
                    OnPropertyChanged("CountPoints");
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
            countPoints = 2;
            txtCountPoints.DataContext = this;

            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");
            comboItems.Add("Многоугольник");

            listShapes = new NewShapeList();
            listShapesTypes = new List<Type>();
            listShapesTypes.Add(typeof(NewLine));
            listShapesTypes.Add(typeof(NewRectangle));
            listShapesTypes.Add(typeof(NewEllipse));
            listShapesTypes.Add(typeof(NewPolygon));

            /*listShapes.Shapes.Add(new NewLine(widthStroke, brushStroke, brushFill, pointsList));
            listShapes.Shapes.Add(new NewRectangle(widthStroke, brushStroke, brushFill, pointsList));
            listShapes.Shapes.Add(new NewEllipse(widthStroke, brushStroke, brushFill, pointsList));
            listShapes.Shapes.Add(new NewPolygon(widthStroke, brushStroke, brushFill, pointsList));*/

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
                Type shapeType = listShapesTypes[cmbShapes.SelectedIndex];
                ConstructorInfo constructorInfo = shapeType.GetConstructor(new Type[] { typeof(double), typeof(SolidColorBrush), 
                                                                                            typeof(SolidColorBrush), typeof(PointCollection) });
                object objShape = constructorInfo.Invoke(new object[] { widthStroke, brushStroke, brushFill, pointsList });
                MethodInfo methodInfo = shapeType.GetMethod("Draw");
                object magicValue = methodInfo.Invoke(objShape, new object[] { canvasField });
                Convert.ChangeType(objShape, shapeType);
                listShapes.Shapes.Add((NewShape) objShape);
                /*listShapes.Shapes[cmbShapes.SelectedIndex].StrokeBrush = brushStroke;
                listShapes.Shapes[cmbShapes.SelectedIndex].FillBrush = brushFill;
                listShapes.Shapes[cmbShapes.SelectedIndex].StrokeWidth = widthStroke;
                listShapes.Shapes[cmbShapes.SelectedIndex].Points = pointsList;
                listShapes.Shapes[cmbShapes.SelectedIndex].Draw(canvasField);*/
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
            if (cmbShapes.SelectedItem.ToString() == "Многоугольник")
            {
                CountPoints = 3;
                panelCountPoints.Visibility = Visibility.Visible;
            }
            else
            {
                CountPoints = 2;
                panelCountPoints.Visibility = Visibility.Hidden;
            }
        }

        private void txtCountPoints_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            pointsList.Clear();
        }

        private void mainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) && (e.Key == Key.Z))
            {
                int canvasElemCount = canvasField.Children.Count;
                int listShapesCount = listShapes.Shapes.Count;
                if (canvasElemCount != 0)
                {
                    canvasField.Children.RemoveAt(canvasElemCount - 1);
                }
                if (listShapesCount != 0)
                {
                    listShapes.Shapes.RemoveAt(listShapesCount - 1);
                }
            }
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}