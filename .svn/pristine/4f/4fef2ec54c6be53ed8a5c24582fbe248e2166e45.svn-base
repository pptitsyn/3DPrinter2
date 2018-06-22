using System;
using System.Collections;
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

namespace _3DPrinter.view.menu
{
    /// <summary>
    /// Interaction logic for MenuApp.xaml
    /// </summary>
    public partial class MenuApp : ListView
    {
        public MenuApp()
        {
            InitializeComponent();
        }
    }


    public class MyStyleSelector : StyleSelector
    {
        public Style Style1 { get; set; }
        public Style Style2 { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            //  if (s.Name == "Gopi")
            return Style1;
            //  else
            //      return Style2;
        }
    }



}
