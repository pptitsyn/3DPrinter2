using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace _3DPrinter.setting.view
{
    /// <summary>
    /// Interaction logic for PrinterSettingsView.xaml
    /// </summary>
    public partial class PrinterSettingsView : UserControl
    {

        public List<int> Rates { get; set; }

        public PrinterSettingsView()
        {
            Rates = new List<int>()
        {
                        9600,
                        14400,
                        19200,
                        28800,
                        38400,
                        56000,
                        57600,
                        76800,
                        111112,
                        115200,
                        128000,
                        230400,
                        250000,
                        256000,
                        460800,
                        500000,
                        921600,
                        1000000,
                        1500000
        };


            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();

            string selected =
                projectManager.ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.ComPortName;
            if (ports.FirstOrDefault(x => x.Equals(selected)) == null)
                projectManager.ProjectManager.Instance.CurrentProject.projectSettings.PrinterSettings.ComPortName = ports[0];
            comboBoxListCOMPorts.ItemsSource = ports;
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!SettingsProvider.Instance.InputPrinterSettings((sender as ComboBox).Text))
                {
                    (sender as ComboBox).Text = (string)(sender as ComboBox).SelectedValue;
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsProvider.Instance.DeleteSelectedPrinterSettingQuestion();
        }
    }

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ComboBoxItem cbi = value as ComboBoxItem;
            return Int32.Parse(cbi.Content.ToString());
        }
    }
}
