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
    /// Interaction logic for MaterialSettingsView.xaml
    /// </summary>
    public partial class MaterialSettingsView : UserControl
    {
        public MaterialSettingsView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SettingsProvider.Instance.DeleteSelectedMaterialSetting((MaterialSettingsModel)materialsComboBox.SelectedItem);
        }

        private void MaterialsComboBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!SettingsProvider.Instance.InputMaterialSettings((sender as ComboBox).Text,(sender as ComboBox)))
                {
                    (sender as ComboBox).Text = (string)(sender as ComboBox).SelectedValue;
                }
            }             
        }
    }
}
