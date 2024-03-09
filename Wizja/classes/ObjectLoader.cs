using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        static private List<Rectangle> mapObjects = new List<Rectangle>();
        static private List<Rectangle> movingObjects = new List<Rectangle>();
        static private Canvas gameCanvas;
        private ImageBrush sanctuary = new ImageBrush();
        private ImageBrush shop = new ImageBrush();
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
            metalFence.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fence.png"));
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/background1.png"));
            shop.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/shopOpen.png"));
            gameCanvas = GameCanvas;
            LoadMap();
        }
        public void LoadMap()
        {
            BuildMap(backgroundImage);
            BuildConstrution(0, 0, metalFence, 6000, 10);
            BuildConstrution(6000, 0, metalFence, 10, 4000);
            BuildConstrution(6000, 4000, metalFence, 6000, 10);
            BuildConstrution(0, 4000, metalFence, 10, 4000);
            BuildConstrution(3000, 1700, shop, 300, 200);
        }

        private void BuildMap(ImageBrush imageBrush)
        {
            Rectangle build = new Rectangle
            {
                Width = gameCanvas.Width,
                Height = gameCanvas.Height,
                Fill = imageBrush
            };
            Canvas.SetLeft(build, 0);
            Canvas.SetTop(build, 0);
            movingObjects.Add(build);
            gameCanvas.Children.Add(build);
        }

        static private void BuildConstrution(int leftPossition, int topPossition, ImageBrush imageBrush,int width, int height)
        {
            Rectangle build = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = imageBrush
            };
            Canvas.SetLeft(build, leftPossition);
            Canvas.SetTop(build, topPossition);
            movingObjects.Add(build);
            mapObjects.Add(build);
            gameCanvas.Children.Add(build);
        }

        public List<Rectangle> GetListMapObjects()
        {
            return mapObjects;
        }
        public List<Rectangle> GetListMovingObjects()
        {
            return movingObjects;
        }
    }
}
