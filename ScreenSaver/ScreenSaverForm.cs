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

        private List<Bitmap> _pictures;
        private Bitmap _darkPicture;

        public ScreenSaverForm(Rectangle Bounds, List<Bitmap> pictures)
        {
            InitializeComponent();
            this.Bounds = Bounds;
            this._pictures = pictures;

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

        private void SetImageOpacity(Image image, double opacity)
        {
            _darkPicture = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(_darkPicture))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = (float)opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, _darkPicture.Width, _darkPicture.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                g.Dispose();
            }
        }

        private void DisplayRandomImage()
        {
            if (_pictures.Count == 0) return;

            Random rng = new Random();

            int pictureRng = rng.Next(0, _pictures.Count);
            
            int imageWidth = _pictures.ElementAt(pictureRng).Width;
            int imageHeight = _pictures.ElementAt(pictureRng).Height;
            double imageAspectRatio = (double)imageWidth / (double)imageHeight;

            int screenCenterX = Bounds.Width / 2;
            int screenCenterY = Bounds.Height / 2;

            if (_darkPicture != null) _darkPicture.Dispose();

            SetImageOpacity(_pictures.ElementAt(pictureRng), BACKGROUND_OPACITY);
            this.BackgroundImage = _darkPicture;
            //this.BackgroundImage = _pictures.ElementAt(pictureRng);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            int pictureboxHeight = (int)(Bounds.Height * SCREEN_COVERAGE);
            int pictureboxWidth = (int)(pictureboxHeight * imageAspectRatio);

            pictureBox.Top = screenCenterY - (pictureboxHeight / 2);
            pictureBox.Left = screenCenterX - (pictureboxWidth / 2);

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox.ClientSize = new Size(pictureboxWidth, pictureboxHeight);
            pictureBox.Image = (Image)_pictures.ElementAt(pictureRng);

            pictureBox.Padding = new Padding(BORDER_THICKNESS);
            pictureBox.BackColor = BORDER_COLOR;
            Logger.log($"Display image {pictureRng}");
        }
    }
}
