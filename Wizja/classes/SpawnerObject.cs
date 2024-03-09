using System.Net;
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
            };
            Canvas.SetLeft(place, placeToSpawn.X);
            Canvas.SetTop(place, placeToSpawn.Y);
            gameCanvas.Children.Add(place);
        }
        public double DistanceToPlayer(Rectangle player)
        {
            double x = Canvas.GetLeft(place);
            double y = Canvas.GetTop(place);
            double dx = Canvas.GetLeft(player) - x;
            double dy = Canvas.GetTop(player) - y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }
    }
 
}
