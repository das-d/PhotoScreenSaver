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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoScreenSaver
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class ShowPictureControl : UserControl
    {
        public static readonly DependencyProperty ImageProperty =
         DependencyProperty.Register(
             "Image", 
             typeof(BitmapImage), 
             typeof(ShowPictureControl), 
             new PropertyMetadata(null, new PropertyChangedCallback(OnImageChanged))
             );

        public static readonly DependencyProperty BackgroundOpacityProperty =
         DependencyProperty.Register(
             "BackgroundOpacity",
             typeof(double),
             typeof(ShowPictureControl),
             new PropertyMetadata(0.0, new PropertyChangedCallback(OnBackgroundOpacityChanged))
             );

        public static readonly DependencyProperty BackgroundBlurRadiusProperty =
         DependencyProperty.Register(
             "BackgroundBlurRadius",
             typeof(double),
             typeof(ShowPictureControl),
             new PropertyMetadata(0.0, new PropertyChangedCallback(OnBackgroundBlurRadiusChanged))
             );

        public static readonly DependencyProperty ScreenCoverageProperty =
         DependencyProperty.Register(
             "ScreenCoverage",
             typeof(double),
             typeof(ShowPictureControl),
             new PropertyMetadata(0.0, new PropertyChangedCallback(OnScreenCoverageChanged))
             );

        public static readonly DependencyProperty BorderSizeProperty =
         DependencyProperty.Register(
             "BorderSize",
             typeof(double),
             typeof(ShowPictureControl),
             new PropertyMetadata(0.0, new PropertyChangedCallback(OnBorderSizeChanged))
             );

        public static readonly DependencyProperty InfoProperty =
         DependencyProperty.Register(
             "Info",
             typeof(string),
             typeof(ShowPictureControl),
             new PropertyMetadata("", new PropertyChangedCallback(OnInfoChanged))
             );

        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        public double BackgroundBlurRadius
        {
            get { return (double)GetValue(BackgroundBlurRadiusProperty); }
            set { SetValue(BackgroundBlurRadiusProperty, value); }
        }

        public double ScreenCoverage
        {
            get { return (double)GetValue(ScreenCoverageProperty); }
            set { SetValue(ScreenCoverageProperty, value); }
        }

        public double BorderSize
        {
            get { return (double)GetValue(BorderSizeProperty); }
            set { SetValue(BorderSizeProperty, value); }
        }

        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        public ShowPictureControl()
        {
            InitializeComponent();
        }

        private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnImageChanged(e);
        }

        private void OnImageChanged(DependencyPropertyChangedEventArgs e)
        {
            foregroundImage.Source = e.NewValue as BitmapImage;
            backgroundImage.Source = e.NewValue as BitmapImage;
        }

        private static void OnBackgroundOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnBackgroundOpacityChanged(e);
        }

        private void OnBackgroundOpacityChanged(DependencyPropertyChangedEventArgs e)
        {
            backgroundImage.Opacity = e.NewValue == null ? 0.0 : (double)e.NewValue;
        }

        private static void OnScreenCoverageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnScreenCoverageChanged(e);
        }

        private void OnScreenCoverageChanged(DependencyPropertyChangedEventArgs e)
        {
            Grid gr = this.Parent as Grid;
            Window wnd = gr.Parent as Window;

            double paddingPixels = wnd.Height - (wnd.Height * (e.NewValue == null ? 0.0 : (double)e.NewValue));

            imageCanvas.Margin = new Thickness(paddingPixels/2);
        }

        private static void OnBorderSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnBorderSizeChanged(e);
        }

        private void OnBorderSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            foregroundImageBorder.BorderThickness = new Thickness(e.NewValue == null ? 0.0 : (double)e.NewValue);
        }

        private static void OnBackgroundBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnBackgroundBlurRadiusChanged(e);
        }

        private void OnBackgroundBlurRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            blurEffect.Radius = e.NewValue == null ? 0.0 : (double)e.NewValue;
        }

        private static void OnInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShowPictureControl control = d as ShowPictureControl;
            control.OnInfoChanged(e);
        }

        private void OnInfoChanged(DependencyPropertyChangedEventArgs e)
        {
            infoText.Text = e.NewValue.ToString();
        }
    }
}
