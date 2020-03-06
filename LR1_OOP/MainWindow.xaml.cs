using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LR1_OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color defaultColor;
        double defaultStrWidth;
        NShapeList list;

        public MainWindow()
        {
            InitializeComponent();
            ObservableCollection<string> comboItems = new ObservableCollection<string>();
            cmbShapes.ItemsSource = comboItems;
            comboItems.Add("Линия");
            comboItems.Add("Прямоугольник");
            comboItems.Add("Эллипс");
            defaultColor = Colors.Black;
            defaultStrWidth = 7;
            tbStrokeWidth.Text = defaultStrWidth.ToString();
            list = new NShapeList();
            list.Shapes.Add(new NLine(defaultStrWidth, Colors.Aquamarine, Colors.Transparent, new Point(56, 345), new Point(467, 475)));
            list.Shapes.Add(new NRectangle(defaultStrWidth, Colors.Coral, Colors.DarkSlateGray, new Point(10, 10), new Point(200, 200)));
            list.Shapes.Add(new NEllipse(defaultStrWidth, Colors.Red, Colors.Red, new Point(400, 400), new Point(600, 600)));
        }

        private void btnStrokeColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                list.Shapes[cmbShapes.SelectedIndex].StrokeBrush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));      
            }
        }

        private void btnFillColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color color = colorPicker.Color;
                list.Shapes[cmbShapes.SelectedIndex].FillBrush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            list.Shapes[cmbShapes.SelectedIndex].Draw(cnvField);
        }

        private void tbStrokeWidth_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var matches = Regex.IsMatch(e.Text, @"[,\.0-9]", RegexOptions.IgnoreCase);
            
            if(!matches)
            {
                e.Handled = true;
            }
        }

        private void tbStrokeWidth_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var text = tbStrokeWidth.Text;
                text = text.Replace(".", ",");
                tbStrokeWidth.Text = text;
                tbStrokeWidth.SelectionStart = tbStrokeWidth.Text.Length;
                var matches = Regex.IsMatch(text, @"^([0-9])+((\,)([0-9])+)?$", RegexOptions.IgnoreCase);

                if (matches)
                {
                    list.Shapes[cmbShapes.SelectedIndex].StrokeWidth = double.Parse(text);
                }
                else
                {
                    System.Windows.MessageBox.Show("Неверный ввод", "Ошибка при вводе значения", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbStrokeWidth.Text = defaultStrWidth.ToString();
                }
            }
        }
    }

}
