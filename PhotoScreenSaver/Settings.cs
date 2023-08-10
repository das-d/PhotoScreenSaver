using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoScreenSaver
{
    public static class Settings
    {
        private static string picturePath = "C:\\";
        private static double borderThickness = 4;
        private static int blurRadius = 40;
        private static double backgroundOpacity = 0.50;
        private static double screenCoverage = 0.85;
        private static int interval = 30;
        private static double smoothTransition = 0.00;

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        public static int Interval { get => interval; set { interval = value; OnStaticPropertyChanged("Interval"); } }
        public static double SmoothTransition { get => smoothTransition; set { smoothTransition = value; OnStaticPropertyChanged("SmoothTransition"); } }
        public static double ScreenCoverage { get => screenCoverage; set { screenCoverage = value; OnStaticPropertyChanged("ScreenCoverage"); } }
        public static double BackgroundOpacity { get => backgroundOpacity; set { backgroundOpacity = value; OnStaticPropertyChanged("BackgroundOpacity"); } }
        public static int BlurRadius { get => blurRadius; set { blurRadius = value; OnStaticPropertyChanged("BlurRadius"); } }
        public static double BorderThickness { get => borderThickness; set { borderThickness = value; OnStaticPropertyChanged("BorderThickness"); } }
        public static string PicturePath { get => picturePath; set { picturePath = value; OnStaticPropertyChanged("PicturePath"); } }


        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static void LoadSettings()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\PictureScreenSaver");
                Settings.PicturePath = key.GetValue("picturePath") == null ? Settings.PicturePath : (string)key.GetValue("picturePath");
                Settings.Interval = key.GetValue("interval") == null ? Settings.Interval : Convert.ToInt32(key.GetValue("interval"));
                Settings.SmoothTransition = key.GetValue("smoothTransition") == null ? Settings.SmoothTransition : Convert.ToDouble(key.GetValue("smoothTransition"));
                Settings.ScreenCoverage = key.GetValue("screenCoverage") == null ? Settings.ScreenCoverage : Convert.ToDouble(key.GetValue("screenCoverage"));
                Settings.BackgroundOpacity = key.GetValue("backgroundOpacity") == null ? Settings.BackgroundOpacity : Convert.ToDouble(key.GetValue("backgroundOpacity"));
                Settings.BlurRadius = key.GetValue("blurRadius") == null ? Settings.BlurRadius : Convert.ToInt32(key.GetValue("blurRadius"));
                Settings.BorderThickness = key.GetValue("borderThickness") == null ? Settings.BorderThickness : Convert.ToDouble(key.GetValue("borderThickness"));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"{ex.Message} - Return to default", "Ack");
                SaveSettings();
                //this.Shutdown();
            }
        }

        public static void SaveSettings()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\PictureScreenSaver");
                key.SetValue("picturePath", Settings.PicturePath);
                key.SetValue("interval", Settings.Interval);
                key.SetValue("smoothTransition", Settings.SmoothTransition);
                key.SetValue("screenCoverage", Settings.ScreenCoverage);
                key.SetValue("backgroundOpacity", Settings.BackgroundOpacity);
                key.SetValue("blurRadius", Settings.BlurRadius);
                key.SetValue("borderThickness", Settings.BorderThickness);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Settings couldn't be saved");
            }
            finally
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
