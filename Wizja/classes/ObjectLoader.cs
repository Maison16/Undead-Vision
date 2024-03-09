using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Wizja.classes
{
    public class ObjectLoader
    {
        static public List<Rectangle> mapObjects = new List<Rectangle>();
        static private Canvas gameCanvas;
        private ImageBrush sanctuary = new ImageBrush();
        private ImageBrush tombstone = new ImageBrush();
        private ImageBrush tree = new ImageBrush();
        private ImageBrush grandCross = new ImageBrush();
        private ImageBrush metalFence = new ImageBrush();
        private ImageBrush backgroundImage = new ImageBrush();

        public ObjectLoader(Canvas GameCanvas)
        {
            sanctuary.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            tombstone.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            tree.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            grandCross.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            metalFence.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/background1.png"));
            gameCanvas = GameCanvas;
            LoadMap();
        }
        public void LoadMap()
        {
            BuildConstrution(0, 0, backgroundImage, 6000, 4000);
            BuildConstrution(3000, 1700, grandCross, 80, 100);
        }
        static private void BuildConstrution(int leftDistance, int upDistance, ImageBrush imageBrush, int width, int height)
        {
            Rectangle build = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = imageBrush
            };
            Canvas.SetLeft(build, leftDistance);
            Canvas.SetTop(build, upDistance);
            mapObjects.Add(build);
            gameCanvas.Children.Add(build);
        }
    }
}
