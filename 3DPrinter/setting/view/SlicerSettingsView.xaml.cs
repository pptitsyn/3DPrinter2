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
using _3DPrinter.setting.model;

namespace _3DPrinter.setting.view
{
    /// <summary>
    /// Interaction logic for SlicerSettingsView.xaml
    /// </summary>
    public partial class SlicerSettingsView : UserControl
    {
        public SlicerSettingsView()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            string property = "";
            
            switch (combo.SelectedIndex)
            {
                case 0 :
                    property = "startCode";
                    break;
                case 1 :
                    property = "endCode";
                    break;
                case 2 :
                    property = "preSwitchExtruderCode";
                    break;
                case 3 :
                    property = "postSwitchExtruderCode";
                    break;
            }

            var binding = new Binding(property);
            binding.Source = SettingsProvider.Instance.SelectedSlicerSettings;
            binding.Mode = BindingMode.TwoWay;
            var bound = codeBox.SetBinding(TextBox.TextProperty, binding);


        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsProvider.Instance.DeleteSelectedSlicerSetting((SlicerSettingsModel)printerComboBox.SelectedItem);
        }

        private void PrinterComboBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!SettingsProvider.Instance.InputSlicerSettings((sender as ComboBox).Text))
                {
                    (sender as ComboBox).Text = (string)(sender as ComboBox).SelectedValue;
                }
            }            

        }
    }


    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToDouble((int) value)/1000.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d = Double.Parse((string)value,culture.NumberFormat)*1000.0;
            return System.Convert.ToInt32(d);
        }
    }
}
