using PhotoScreenSaver.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PhotoScreenSaver
{
    public class PictureHandler
    {
        private HashSet<string> _picturePaths = new HashSet<string>();
        private HashSet<PictureWindow> _pictureWindows = new HashSet<PictureWindow>();
        private DispatcherTimer _timer;
        private DispatcherTimer _clockTimer;
        private int _rng = 1337;
        private Random _random = new Random();


        public PictureHandler()
        {
            GetPictures();

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(TimerTicked);
            _timer.Interval = TimeSpan.FromSeconds(Settings.Interval);
            _timer.Start();

            _clockTimer = new DispatcherTimer();
            _clockTimer.Tick += new EventHandler(ClockTimerTicked);
            _clockTimer.Interval = TimeSpan.FromSeconds(5);
            _clockTimer.Start();
        }

        public void AddWindow(PictureWindow window)
        {
            _pictureWindows.Add(window);
        }

        public void FadePictures()
        {
            double animationTime = Settings.SmoothTransition;

            foreach (PictureWindow wnd in _pictureWindows)
            {
                if (wnd.ImageOddControl.Opacity == 1.0)
                {
                    var anim = new DoubleAnimation(0.0, (Duration)TimeSpan.FromSeconds(animationTime));
                    anim.Completed += (s, _) => wnd.ImageOdd = GetRandomPicture();
                    wnd.ImageOddControl.BeginAnimation(UIElement.OpacityProperty, anim);
                }
                else
                {
                    var anim = new DoubleAnimation(1.0, (Duration)TimeSpan.FromSeconds(animationTime));
                    anim.Completed += (s, _) => wnd.ImageEven = GetRandomPicture();
                    wnd.ImageOddControl.BeginAnimation(UIElement.OpacityProperty, anim);
                }
            }
        }

        public void AssignPictures()
        {
            foreach (PictureWindow wnd in _pictureWindows)
            {
                wnd.ImageOdd = GetRandomPicture();
                wnd.ImageEven = GetRandomPicture();
            }
        }

        private void GetPictures()
        {
            try
            {
                _picturePaths = Directory.GetFiles(Settings.PicturePath, "*.*", SearchOption.TopDirectoryOnly).Where(f => f.EndsWith(".jpeg") || f.EndsWith(".jpg") || f.EndsWith(".bmp") || f.EndsWith(".png")).ToHashSet();
            }
            catch (Exception)
            {

            }
        }

        private BitmapImage GetRandomPicture()
        {
            if (_picturePaths.Count() == 0) return null;

            _rng = _random.Next(0, _picturePaths.Count());

            return new BitmapImage(new Uri(_picturePaths.ElementAt(_rng)));
        }

        private void TimerTicked(object sender, EventArgs e)
        {
            FadePictures();
        }

        private void ClockTimerTicked(object sender, EventArgs e)
        {
            foreach (PictureWindow wnd in _pictureWindows)
            {
                wnd.Info = $"{DateTime.Now.ToString("HH:mm")}";
            }
        }
    }
}
