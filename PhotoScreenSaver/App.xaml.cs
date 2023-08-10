using PhotoScreenSaver.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace PhotoScreenSaver
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public PictureHandler _pictureHandler;

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Count() < 1) return;

            string argPart = e.Args[0].Substring(0, 2);

            switch (argPart)
            {
                case "/s":
                    //normal start
                    Settings.LoadSettings();
                    _pictureHandler = new PictureHandler();

                    foreach (Screen screen in Screen.AllScreens)
                    {
                        if (screen != Screen.PrimaryScreen || true)
                        {
                            PictureWindow screenPrim = new PictureWindow();
                            System.Drawing.Rectangle bounds = screen.Bounds;
                            screenPrim.AllowsTransparency = false;
                            screenPrim.Opacity = 1.0;
                            screenPrim.Top = bounds.Y;
                            screenPrim.Left = bounds.X;
                            screenPrim.Height = bounds.Height;
                            screenPrim.Width = bounds.Width;
                            _pictureHandler.AddWindow(screenPrim);
                            screenPrim.Show();
                        }
                    }

                    _pictureHandler.AssignPictures();
                    break;
                case "/c":
                    //configuration mode
                    Settings.LoadSettings();
                    SettingsWindow settingsWind = new SettingsWindow();
                    settingsWind.Show();
                    break;
                case "/p":
                    //preview mode
                    this.Shutdown();
                    break;
                default:
                    this.Shutdown();
                    break;
            }
        }
    }
}
