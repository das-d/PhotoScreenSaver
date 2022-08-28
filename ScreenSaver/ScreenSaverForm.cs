using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        private const int INTERVAL = 30000;
        private const double SCREEN_COVERAGE = 0.85;
        private const int BORDER_THICKNESS = 2;
        private Color BORDER_COLOR = Color.DarkGray;
        private const double BACKGROUND_OPACITY = 0.20;


        private string[] _pictures;

        public ScreenSaverForm(Rectangle Bounds, string[] picturePaths)
        {
            InitializeComponent();
            this.Bounds = Bounds;
            this._pictures = picturePaths;

            DisplayRandomImage();
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            this.TopMost = true;

            moveTimer.Interval = INTERVAL;
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
            moveTimer.Start();
        }

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {
            DisplayRandomImage();
        }

        private Image SetImageOpacity(Image image, double opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = (float)opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        private void DisplayRandomImage()
        {
            Random rng = new Random();

            int pictureRng = rng.Next(0, _pictures.Length - 1);

            Bitmap image = new Bitmap(_pictures[pictureRng]);
            int imageWidth = image.Width;
            int imageHeight = image.Height;
            double imageAspectRatio = (double)imageWidth / (double)imageHeight;

            int screenCenterX = Bounds.Width / 2;
            int screenCenterY = Bounds.Height / 2;

            this.BackgroundImage = SetImageOpacity(image, BACKGROUND_OPACITY);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            int pictureboxHeight = (int)(Bounds.Height * SCREEN_COVERAGE);
            int pictureboxWidth = (int)(pictureboxHeight * imageAspectRatio);

            pictureBox.Top = screenCenterY - (pictureboxHeight / 2);
            pictureBox.Left = screenCenterX - (pictureboxWidth / 2);

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox.ClientSize = new Size(pictureboxWidth, pictureboxHeight);
            pictureBox.Image = (Image)image;

            pictureBox.Padding = new Padding(BORDER_THICKNESS);
            pictureBox.BackColor = BORDER_COLOR;
        }
    }
}
