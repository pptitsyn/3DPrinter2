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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Localization = _3DPrinter.setting.Localization;
using StoryBoard = System.Windows.Media.Animation.Storyboard;

namespace _3DPrinter.view.notification
{
    /// <summary>
    /// Логика взаимодействия для ToasterControl.xaml
    /// </summary>
    public partial class ToasterControl : UserControl
    {
        public ToasterControl()
        {
            InitializeComponent();

//            var story = ToastSupport.GetAnimation(ToasterAnimation.SlideInFromRight, Notification);
//            story.Completed += (sender, args) => { Visibility = Visibility.Hidden; };

            var story = GetShowAnimation(Notification);
            story.Begin(Notification);

        }

        private Storyboard GetShowAnimation(UIElement toaster)
        {
            var story = new StoryBoard();
            toaster.RenderTransformOrigin = new Point(1, 0);
            toaster.RenderTransform = new TranslateTransform(360.0, 0);

            var slideinFromRightAnimation = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                KeyFrames = new DoubleKeyFrameCollection
                        {
                            new EasingDoubleKeyFrame(360.0, KeyTime.FromPercent(0)),
                            new EasingDoubleKeyFrame(0.0, KeyTime.FromPercent(0.5), new ExponentialEase
                            {
                                EasingMode = EasingMode.EaseInOut
                            })
                            /*,
                            new EasingDoubleKeyFrame(0.0, KeyTime.FromPercent(0.8)),
                            new EasingDoubleKeyFrame(360.0, KeyTime.FromPercent(0.9), new ExponentialEase
                            {
                                EasingMode = EasingMode.EaseOut
                            })
                             */
                        }
            };

            Storyboard.SetTargetProperty(slideinFromRightAnimation,
                new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            story.Children.Add(slideinFromRightAnimation);

            return story;

        }

        public void CloseToaster()
        {
            ProgressBar1.Visibility = Visibility.Collapsed;
            var story = GetHideAnimation(Notification);
            story.Completed += (sender, args) => { Visibility = Visibility.Collapsed; };
            story.Begin(Notification);
        }

        public void ShowToaster(string title, string message)
        {
            ToasterTitle = title;
            Message = message;
            Visibility = Visibility.Visible;
            var story = GetShowAnimation(Notification);
            story.Begin(Notification);
        }

        List<string> history = new List<string>();
        public void SetProgressBar(int max, int value,string state)
        {
            history.Add(max+ " : "+value);
            ProgressBar1.Minimum = 0;
            ProgressBar1.Maximum = max;
            ProgressBar1.Value = value;
            Message = Localization.Instance.CurrentLanguage.Stage + state;
            ProgressBar1.Visibility = Visibility.Visible;
        }


        private Storyboard GetHideAnimation(UIElement toaster)
        {
            var story = new StoryBoard();

            var slideinFromRightAnimation = new DoubleAnimationUsingKeyFrames
            {
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                KeyFrames = new DoubleKeyFrameCollection
                        {
/*
                            new EasingDoubleKeyFrame(360.0, KeyTime.FromPercent(0)),
                            new EasingDoubleKeyFrame(0.0, KeyTime.FromPercent(0.1), new ExponentialEase
                            {
                                EasingMode = EasingMode.EaseInOut
                            })
 */ 
                            new EasingDoubleKeyFrame(0.0, KeyTime.FromPercent(0)),
                            new EasingDoubleKeyFrame(360.0, KeyTime.FromPercent(1), new ExponentialEase
                            {
                                EasingMode = EasingMode.EaseOut
                            })
                             
                        }
            };

            Storyboard.SetTargetProperty(slideinFromRightAnimation,
                new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            story.Children.Add(slideinFromRightAnimation);


            return story;

        }

        public static readonly DependencyProperty ToasterTitleProperty = DependencyProperty.Register("Title",
  typeof(string), typeof(ToasterControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string), typeof(ToasterControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
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

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
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

        private void ToasterControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (CancelEvent != null) 
                CancelEvent(this,null);

        }

        public event EventHandler CancelEvent;
    }


}
