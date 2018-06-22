using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _3DPrinter.view.menu
{
    public class ButtonExt: Button
    {

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
            typeof(string), typeof(ButtonExt), new PropertyMetadata(string.Empty));

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }




    }
}
