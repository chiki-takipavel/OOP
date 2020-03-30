using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace LR1_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string defaultFileName = "Shapes";
        private const string defaultExtension = ".xml";
        private const string fileFilter = "XML-файл (.xml)|*.xml|Текстовый файл (.txt)|*.txt";
        private Color colorStroke;
        private Color colorFill;
        private double widthStroke;
        private PointCollection pointsList;
        private int countPoints;
        private NewShapeList listShapes;
        private List<Type> listShapesTypes;
        private Microsoft.Win32.OpenFileDialog openFileDialog;
        private Microsoft.Win32.SaveFileDialog saveFileDialog;

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
            colorStroke = Colors.Black;
            colorFill = Colors.White;
            widthStroke = 7;
            pointsList = new PointCollection();
            countPoints = 2;
            txtCountPoints.DataContext = this;

            ObservableCollection<string> comboItems = new ObservableCollection<string>
            {
                "Линия",
                "Прямоугольник",
                "Эллипс",
                "Многоугольник"
            };
            cmbShapes.ItemsSource = comboItems;

            listShapes = new NewShapeList();

            listShapesTypes = new List<Type>
            {
                typeof(NewLine),
                typeof(NewRectangle),
                typeof(NewEllipse),
                typeof(NewPolygon)
            };

            slidStrWidth.Value = widthStroke;
            rectStrokeColor.Fill = new SolidColorBrush(colorStroke);
            rectFillColor.Fill = new SolidColorBrush(colorFill);

            openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = defaultExtension,
                Filter = fileFilter
            };
            saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = defaultFileName,
                DefaultExt = defaultExtension,
                Filter = fileFilter
            };
        }

        private void btnStrokeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                colorStroke = Color.FromArgb(color.A, color.R, color.G, color.B);
                rectStrokeColor.Fill = new SolidColorBrush(colorStroke);
            }
        }

        private void btnFillColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                colorFill = Color.FromArgb(color.A, color.R, color.G, color.B);
                rectFillColor.Fill = new SolidColorBrush(colorFill);
            }
        }

        private void canvasField_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pointsList.Add(e.GetPosition(canvasField));
            if (pointsList.Count == CountPoints)
            {
                Type shapeType = listShapesTypes[cmbShapes.SelectedIndex];
                ConstructorInfo constructorInfo = shapeType.GetConstructor(new Type[] { typeof(double), typeof(Color),
                                                                                            typeof(Color), typeof(PointCollection) });
                object objShape = constructorInfo.Invoke(new object[] { widthStroke, colorStroke, colorFill, pointsList });
                MethodInfo methodInfo = shapeType.GetMethod("Draw");
                methodInfo.Invoke(objShape, new object[] { canvasField });
                listShapes.Shapes.Add((NewShape)objShape);
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

        private void itemSave_Click(object sender, RoutedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(NewShapeList), listShapesTypes.ToArray());
                using (FileStream file = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    xmlFormatter.Serialize(file, listShapes);
                }
            }
        }

        private void itemOpen_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(NewShapeList), listShapesTypes.ToArray());
                using (FileStream file = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    try
                    {
                        NewShapeList tempShapes = xmlFormatter.Deserialize(file) as NewShapeList;
                        tempShapes.Draw(canvasField);
                        listShapes.Shapes = listShapes.Shapes.Concat(tempShapes.Shapes).ToList();
                    }
                    catch (InvalidOperationException)
                    {
                        System.Windows.MessageBox.Show("Не удалось десериализовать объект", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}