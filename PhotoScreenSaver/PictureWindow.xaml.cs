using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PhotoScreenSaver
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class PictureWindow : Window, INotifyPropertyChanged
    {
        private BitmapImage _imageEven;
        private BitmapImage _imageOdd;
        private double _backgroundOpacity;
        private double _screenCoverage;
        private double _borderSize;
        private double _blurRadius;
        private Point _mouseLocation = new Point(0, 0);
        private string _info = $"{DateTime.Now.ToString("HH:mm")}";

        public BitmapImage ImageEven { get => _imageEven; set { _imageEven = value; OnPropertyChanged(); } }
        public BitmapImage ImageOdd { get => _imageOdd; set { _imageOdd = value; OnPropertyChanged(); } }
        public double BackgroundOpacity { get => _backgroundOpacity; set { _backgroundOpacity = value; OnPropertyChanged(); } }
        public double ScreenCoverage { get => _screenCoverage; set { _screenCoverage = value; OnPropertyChanged(); } }
        public double BorderSize { get => _borderSize; set { _borderSize = value; OnPropertyChanged(); } }
        public double BlurRadius { get => _blurRadius; set { _blurRadius = value; OnPropertyChanged(); } }
        public string Info { get => _info; set { _info = value; OnPropertyChanged(); } }


        public PictureWindow()
        {
            InitializeComponent();

            BackgroundOpacity = Settings.BackgroundOpacity;
            ScreenCoverage = Settings.ScreenCoverage;
            BorderSize = Settings.BorderThickness;
            BlurRadius = Settings.BlurRadius;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.None;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseLocation.X != 0 && _mouseLocation.Y != 0)
            {
                Point currentMousePos = this.PointToScreen(Mouse.GetPosition(this));

                double mouseMovementX = Math.Abs(_mouseLocation.X - currentMousePos.X);
                double mouseMovementY = Math.Abs(_mouseLocation.Y - currentMousePos.Y);

                if (mouseMovementX > 10 || mouseMovementY > 10)
                {
                    Application.Current.Shutdown();
                }
            }

            _mouseLocation = this.PointToScreen(Mouse.GetPosition(this));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
