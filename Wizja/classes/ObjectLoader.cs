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
        static private List<Rectangle> hitObjects = new List<Rectangle>();
        static private List<Rectangle> movingObjects = new List<Rectangle>();
        static private Canvas gameCanvas;
        private ImageBrush sanctuaryImage = new ImageBrush();
        private ImageBrush tombstoneImage = new ImageBrush();
        private ImageBrush treeImage = new ImageBrush();
        private ImageBrush grandCrossImage = new ImageBrush();
        private ImageBrush metalFenceImage = new ImageBrush();
        private ImageBrush backgroundImage = new ImageBrush();

        public ObjectLoader(Canvas GameCanvas)
        {
            sanctuaryImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            tombstoneImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            treeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            grandCrossImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            metalFenceImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fence.png"));
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/ground.png"));
            gameCanvas = GameCanvas;
            LoadMap();
        }
        public void LoadMap()
        {
            BuildMap(backgroundImage);
            BuildConstrution(0, 0, metalFenceImage, 6000, 10);
            BuildConstrution(6000, 0, metalFenceImage, 10, 4000);
            BuildConstrution(6000, 4000, metalFenceImage, 6000, 10);
            BuildConstrution(0, 4000, metalFenceImage, 10, 4000);
            BuildConstrution(2800, 1750, grandCrossImage, 80, 100);
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

        public void BuildConstrution(int leftPossition, int topPossition, ImageBrush imageBrush,int width, int height)
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
            hitObjects.Add(build);
            gameCanvas.Children.Add(build);
        }

        public List<Rectangle> GetListMapObjects()
        {
            return hitObjects;
        }
        public List<Rectangle> GetListMovingObjects()
        {
            return movingObjects;
        }
    }
}
