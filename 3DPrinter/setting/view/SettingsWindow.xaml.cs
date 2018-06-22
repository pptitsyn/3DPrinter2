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
using System.Windows.Shapes;

namespace _3DPrinter.setting.view
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void CategoryListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;
            i++;
        }
    }

    public class ListBoxItemExt : ListBoxItem
    {

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
            typeof(string), typeof(ListBoxItemExt), new PropertyMetadata(string.Empty));

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

    }
}
