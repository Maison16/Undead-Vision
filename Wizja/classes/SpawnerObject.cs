using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wizja.classes
{
    public class SpawnerObject
    {
        public Rectangle place;
        public SpawnerObject(Canvas gameCanvas, Point placeToSpawn)
        {
            place = new Rectangle()
            {
                Width = 32,
                Height = 32,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(place, placeToSpawn.X);
            Canvas.SetTop(place, placeToSpawn.Y);
            gameCanvas.Children.Add(place);
        }
    }
}
