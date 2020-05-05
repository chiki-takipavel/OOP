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
        private const string defaultXmlFileName = "Shapes";
        private const string defaultXmlExtension = ".xml";
        private const string fileXmlFilter = "XML-файл (.xml)|*.xml";
        private const string defaultDllExtension = ".dll";
        private const string fileDllFilter = "DLL-файл (.dll)|*.dll";

        private Color colorStroke;
        private Color colorFill;
        private double widthStroke;
        private readonly PointCollection pointsList;
        private int pointsCount;

        private readonly NewShapeList listShapes;
        private readonly ObservableCollection<NewShapeFactory> listFactory;
        private readonly List<Type> listShapesTypes;
        private readonly List<Type> listFactoryTypes;

        private readonly Microsoft.Win32.OpenFileDialog openXmlFileDialog;
        private readonly Microsoft.Win32.OpenFileDialog openClassFileDialog;
        private readonly Microsoft.Win32.SaveFileDialog saveXmlFileDialog;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PointsCount
        {
            get { return pointsCount; }
            set
            {
                if (value != pointsCount)
                {
                    pointsCount = value;
                    OnPropertyChanged("PointsCount");
                }
            }
        }

        /// <summary>
        /// Инициализация окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            colorStroke = Colors.Black;
            colorFill = Colors.White;
            widthStroke = 7;
            txtPointsCount.DataContext = this;
            pointsList = new PointCollection();

            listShapes = new NewShapeList();
            listShapesTypes = new List<Type>(Assembling.ReflectiveEnumerator.GetEnumerableOfType<NewShape>(Assembly.GetExecutingAssembly()));

            listFactoryTypes = new List<Type>(Assembling.ReflectiveEnumerator.GetEnumerableOfType<NewShapeFactory>(Assembly.GetExecutingAssembly()));
            listFactory = new ObservableCollection<NewShapeFactory>();
            foreach (Type type in listFactoryTypes)
            {
                NewShapeFactory shape = (NewShapeFactory)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                listFactory.Add(shape);
            }
            cmbShapes.ItemsSource = listFactory;
            cmbShapes.DisplayMemberPath = "ShapeName";

            slidStrWidth.Value = widthStroke;
            rectStrokeColor.Fill = new SolidColorBrush(colorStroke);
            rectFillColor.Fill = new SolidColorBrush(colorFill);

            openXmlFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = defaultXmlExtension,
                Filter = fileXmlFilter
            };
            saveXmlFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = defaultXmlFileName,
                DefaultExt = defaultXmlExtension,
                Filter = fileXmlFilter
            };
            openClassFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = defaultDllExtension,
                Filter = fileDllFilter
            };
        }

        /// <summary>
        /// Нажатие на кнопку выбора цвета контура
        /// </summary>
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

        /// <summary>
        /// Нажатие на кнопку выбора цвета заливки
        /// </summary>
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

        /// <summary>
        /// Нажатие левой кнопки мыши по канвасу для отрисовки фигур
        /// </summary>
        private void canvasField_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pointsList.Add(e.GetPosition(canvasField));
            if (pointsList.Count == PointsCount)
            {
                NewShapeFactory currentShape = (NewShapeFactory)cmbShapes.SelectedValue;
                NewShape shape = currentShape.Create(widthStroke, colorStroke, colorFill, pointsList);
                shape.Draw(canvasField);
                listShapes.Shapes.Add(shape);
                pointsList.Clear();
            }
        }

        /// <summary>
        /// Изменение значения толщины контура
        /// </summary>
        private void slidStrWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            widthStroke = slidStrWidth.Value;
        }

        /// <summary>
        /// Выбор фигуры в ComboBox
        /// </summary>
        private void cmbShapes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            pointsList.Clear();
            NewShapeFactory currentShape = (NewShapeFactory)cmbShapes.SelectedValue;
            PointsCount = currentShape.PointsCount;
            if (currentShape.IsChangeablePoints)
            {
                panelCountPoints.Visibility = Visibility.Visible;
            }
            else
            {
                panelCountPoints.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Изменение значения в поле количества точек
        /// </summary>
        private void txtPointsCount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            pointsList.Clear();
        }

        /// <summary>
        /// Удаление последней нарисованной фигуры при нажатии Ctrl+Z
        /// </summary>
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

        /// <summary>
        /// Открытие файла с сериализованными фигурами
        /// </summary>
        private void itemOpen_Click(object sender, RoutedEventArgs e)
        {
            if (openXmlFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(NewShapeList), listShapesTypes.ToArray());
                using FileStream file = new FileStream(openXmlFileDialog.FileName, FileMode.Open);
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

        /// <summary>
        /// Сохранение сериализованных фигур
        /// </summary>
        private void itemSave_Click(object sender, RoutedEventArgs e)
        {
            if (saveXmlFileDialog.ShowDialog() == true)
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(NewShapeList), listShapesTypes.ToArray());
                using FileStream file = new FileStream(saveXmlFileDialog.FileName, FileMode.Create);
                xmlFormatter.Serialize(file, listShapes);
            }
        }

        /// <summary>
        /// Добавление новых фигур
        /// </summary>
        private void itemAddShapes_Click(object sender, RoutedEventArgs e)
        {
            if (openClassFileDialog.ShowDialog() == true)
            {
                string filename = openClassFileDialog.FileName;
                try
                {
                    List<Type> list = Assembling.ReflectiveEnumerator.GetEnumerableOfType<NewShapeFactory>(Assembly.LoadFile(filename));
                    bool isAdded = false;
                    foreach (Type type in list)
                    {
                        if (!listFactoryTypes.Contains(type))
                        {
                            NewShapeFactory shape = (NewShapeFactory)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                            listFactory.Add(shape);
                            listFactoryTypes.Add(type);
                            isAdded = true;
                        }
                    }
                    if (isAdded)
                    {
                        cmbShapes.SelectedIndex = cmbShapes.Items.Count - 1;
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("Ошибка загрузки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        private void itemExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}