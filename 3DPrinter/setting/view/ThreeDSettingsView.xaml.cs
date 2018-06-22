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

namespace _3DPrinter.setting.view
{
    /// <summary>
    /// Interaction logic for ThreeDSettingsView.xaml
    /// </summary>
    public partial class ThreeDSettingsView : UserControl
    {
        public ThreeDSettingsView()
        {
            InitializeComponent();
        }
    }


    [ValueConversion(typeof(System.Windows.Media.Color), typeof(System.Drawing.Color))]
    public class MColorToDColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            System.Drawing.Color color = (System.Drawing.Color)value;
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Color mediacolor = (System.Windows.Media.Color)value;
            return System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        }
    }
}
