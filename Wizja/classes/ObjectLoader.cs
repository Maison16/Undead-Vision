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
        private ImageBrush woodenFenceHorizontal = new ImageBrush();
        private ImageBrush woodenFenceVertical = new ImageBrush();

        public ObjectLoader(Canvas GameCanvas)
        {
            sanctuaryImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            tombstoneImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            treeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            grandCrossImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/background1.png"));
            woodenFenceHorizontal.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fenceHorizontal.png"));
            woodenFenceVertical.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fenceVertical.png"));
            gameCanvas = GameCanvas;
            LoadMap();
        }
        public void LoadMap()
        {
            BuildMap(backgroundImage);
            //BuildConstrution(0, 0, metalFenceImage, 6000, 20);
            //BuildConstrution(5980, 0, metalFenceImage, 20, 4000);
            //BuildConstrution(0, 0, metalFenceImage, 20, 4000);
            //BuildConstrution(0, 3980, metalFenceImage, 6000, 20);
            BuildFences();
            BuildConstrution(2800, 1750, grandCrossImage, 80, 100); //6000:4000
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

        private void BuildFences()
        {
            for (int i = 0; i <= 5744; i += 256)
            {
                BuildConstrution(i, -80, woodenFenceHorizontal, 312, 120);
                BuildConstrution(i, 3880, woodenFenceHorizontal, 312, 120);
            }


            for (int i = 0; i <= 3744; i += 256)
            {
                BuildConstrution(0, i-78, woodenFenceVertical, 56, 376);
                BuildConstrution(5944, i-78, woodenFenceVertical, 56, 376);
            }
            BuildConstrution(5944, 3640, woodenFenceVertical, 56, 376);
        }
        public Rectangle BuildShop(int leftPossition, int topPossition, ImageBrush imageBrush, int width, int height)
        {
            Rectangle build = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = imageBrush
            };
            Canvas.SetLeft(build, leftPossition);
            Canvas.SetTop(build, topPossition);
            return build;
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
        public void SetListMapObjects(List<Rectangle> rectangles)
        {
            foreach (Rectangle temp in rectangles)
            {
                hitObjects.Add(temp);
            }
        }

        public void SetListMovingObjects(List<Rectangle> rectangles)
        {
            foreach (Rectangle temp in rectangles)
            {
                movingObjects.Add(temp);
            }
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
