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
        private ImageBrush backgroundImage = new ImageBrush();
        private ImageBrush woodenFenceHorizontal = new ImageBrush();
        private ImageBrush woodenFenceVertical = new ImageBrush();

        public ObjectLoader(Canvas GameCanvas)
        {
            sanctuaryImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/chapel.png"));
            tombstoneImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/tombstone.png"));
            treeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/tree.png"));
            grandCrossImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/grandcross.png"));
            backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/ground.png"));
            woodenFenceHorizontal.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fenceHorizontal.png"));
            woodenFenceVertical.ImageSource = new BitmapImage(new Uri("pack://application:,,,/res/fenceVertical.png"));
            gameCanvas = GameCanvas;
            LoadMap();
        }
        public void LoadMap()
        {
            BuildMap(backgroundImage);
            BuildFences();
            BuildConstrution(400, 800, sanctuaryImage, 300, 400);

            BuildConstrution(3800, 900, treeImage, 200, 300);
            BuildConstrution(1000, 1200, treeImage, 200, 300);
            BuildConstrution(2300, 300, treeImage, 200, 300);
            BuildConstrution(5300, 3000, treeImage, 200, 300);
            BuildConstrution(1800, 3200, treeImage, 200, 300);
            BuildConstrution(1400, 400, treeImage, 200, 300);
            BuildConstrution(5400, 200, treeImage, 200, 300);
            BuildConstrution(4600, 300, treeImage, 200, 300);
            BuildConstrution(3600, 600, tombstoneImage, 50, 50);
            BuildConstrution(3600, 400, tombstoneImage, 50, 50);
            BuildConstrution(3800, 600, tombstoneImage, 50, 50);
            BuildConstrution(3800, 400, tombstoneImage, 50, 50);
            BuildConstrution(4600, 100, tombstoneImage, 50, 50);
            BuildConstrution(2700, 2000, grandCrossImage, 66, 100);
            BuildConstrution(2500, 2000, grandCrossImage, 66, 100);
            BuildConstrution(1100, 2000, treeImage, 200, 250);
            BuildConstrution(5000, 1600, treeImage, 200, 250);

            BuildConstrution(3400, 3200, sanctuaryImage, 300, 400);
            BuildConstrution(3800, 3200, treeImage, 200, 300);
            BuildConstrution(3100, 3200, treeImage, 200, 300);

            BuildConstrution(4600, 1900, sanctuaryImage, 300, 400);
            BuildConstrution(5000, 2310, tombstoneImage, 50, 50);
            BuildConstrution(5000, 2510, tombstoneImage, 50, 50);
            BuildConstrution(5000, 2710, tombstoneImage, 50, 50);
            BuildConstrution(4800, 2310, tombstoneImage, 50, 50);
            BuildConstrution(4800, 2510, tombstoneImage, 50, 50);
            BuildConstrution(4800, 2710, tombstoneImage, 50, 50);
            BuildConstrution(4600, 2310, tombstoneImage, 50, 50);
            BuildConstrution(4600, 2510, tombstoneImage, 50, 50);
            BuildConstrution(4600, 2710, tombstoneImage, 50, 50);
            BuildConstrution(4400, 2310, tombstoneImage, 50, 50);
            BuildConstrution(4400, 2510, tombstoneImage, 50, 50);
            BuildConstrution(4400, 2710, tombstoneImage, 50, 50);

            BuildConstrution(100, 550, grandCrossImage, 66, 100);
            BuildConstrution(300, 350, grandCrossImage, 100, 150);
            BuildConstrution(500, 150, grandCrossImage, 140, 200);
            BuildConstrution(700, 350, grandCrossImage, 100, 150);
            BuildConstrution(900, 550, grandCrossImage, 66, 100);

            BuildConstrution(200, 3500, tombstoneImage, 50, 50);
            BuildConstrution(400, 3500, tombstoneImage, 50, 50);
            BuildConstrution(600, 3500, tombstoneImage, 50, 50);
            BuildConstrution(800, 3500, tombstoneImage, 50, 50);
            BuildConstrution(200, 3700, tombstoneImage, 50, 50);
            BuildConstrution(400, 3700, tombstoneImage, 50, 50);
            BuildConstrution(600, 3700, tombstoneImage, 50, 50);
            BuildConstrution(800, 3700, tombstoneImage, 50, 50);



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
