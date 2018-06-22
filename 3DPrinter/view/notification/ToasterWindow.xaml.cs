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
using System.Windows.Threading;

namespace _3DPrinter.view.notification
{
    /// <summary>
    /// Логика взаимодействия для ToasterWindow.xaml
    /// </summary>
    public partial class ToasterWindow : Window
    {
        public ToasterWindow(FrameworkElement owner, string title, string message,
            ToasterPosition position, ToasterAnimation animation, double margin)
        {
            InitializeComponent();
            ToasterTitle = title;
            Message = message;
            Position = position;
            Animation = animation;
            Margins = margin;

            var story = ToastSupport.GetAnimation(Animation, Notification);
            story.Completed += (sender, args) => { Close(); };
            story.Begin(Notification);

            Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                var topLeftDict = ToastSupport.GetTopandLeft(Position, this, Margins);
                Top = topLeftDict["Top"];
                Left = topLeftDict["Left"];
            }));

            if (owner != null)
                owner.Unloaded += Owner_Unloaded;
        }
        public static readonly DependencyProperty ToasterTitleProperty = DependencyProperty.Register("Title",
          typeof(string), typeof(ToasterControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string), typeof(ToasterControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Image",
            typeof(string), typeof(ToasterControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register("Animation",
            typeof(ToasterAnimation), typeof(ToasterControl), new PropertyMetadata(ToasterAnimation.FadeIn));

        public static readonly DependencyProperty MarginsProperty = DependencyProperty.Register("Margins",
            typeof(double), typeof(ToasterControl), new PropertyMetadata(0D));


        public string ToasterTitle
        {
            get { return (string)GetValue(ToasterTitleProperty); }
            set { SetValue(ToasterTitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public ToasterPosition Position
        {
            get { return (ToasterPosition)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public ToasterAnimation Animation
        {
            get { return (ToasterAnimation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public double Margins
        {
            get { return (double)GetValue(MarginsProperty); }
            set { SetValue(MarginsProperty, value); }
        }

        protected void Owner_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
