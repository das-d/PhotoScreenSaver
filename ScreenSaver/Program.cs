using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScreenSaver
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 

        private static string _picturePath = "";
        private static FolderBrowserDialog _folderBrowserDialog;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                // Handle cases where arguments are separated by colon.
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                    secondArgument = args[1];

                if (firstArgument == "/c")           // Configuration mode
                {
                    _folderBrowserDialog = new FolderBrowserDialog();
                    _folderBrowserDialog.Description = "Select dir with pictures to show";
                    DialogResult result = _folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _picturePath = _folderBrowserDialog.SelectedPath;
                        SaveSettingsToRegistry();
                    }
                }
                else if (firstArgument == "/p")      // Preview mode
                {
                    // TODO
                }
                else if (firstArgument == "/s")      // Full-screen mode
                {
                    LoadSettings();
                    ShowScreenSaver();
                    Application.Run();
                }
                else    // Undefined argument
                {
                    MessageBox.Show("Sorry, but the command line argument \"" + firstArgument +
                        "\" is not valid.", "ScreenSaver",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else    // No arguments - treat like /c
            {
                MessageBox.Show("No args found");
                // TODO
            }

            //Application.Run(new ScreenSaverForm());
        }

        static void ShowScreenSaver()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                ScreenSaverForm screensaver = new ScreenSaverForm(screen.Bounds);
                screensaver.Show();
            }
        }

        private static void SaveSettingsToRegistry()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\PictureScreenSaver");
            key.SetValue("picturePath", _picturePath);
        }

        private static void LoadSettings()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\PictureScreenSaver");
                _picturePath = (string)key.GetValue("picturePath");
            }
            catch (Exception)
            {
                MessageBox.Show("Couln't read RegKey");
                throw;
            }
        }
    }
}
