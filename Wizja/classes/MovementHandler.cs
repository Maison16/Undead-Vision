using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Wizja.classes
{
    public static class MovementHandler
    {
        static Player Player;

        public static void initialize(Player player)
        {
            Player = player;
        }

        public static void Step(bool[] direction, List<Rectangle> movableEntities, List<Rectangle> staticobjects)
        {
            if (!direction[0] && !direction[1] && !direction[2] && !direction[3])
                return;

            foreach (Rectangle entity in movableEntities)
            {
                
                if (direction[0]) 
                {
                    if(TryUp(staticobjects))
                        Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                }
                    

                if (direction[1])
                {
                    if(TryLeft(staticobjects))
                        Canvas.SetLeft(entity, Canvas.GetLeft(entity) + Player.movingSpeed);
                }
                

                if (direction[2])
                {
                    
                    if(TryDown(staticobjects))
                        Canvas.SetTop(entity, Canvas.GetTop(entity) - Player.movingSpeed);
                }
                

                if (direction[3])
                {

                    if (TryRight(staticobjects))
                        Canvas.SetLeft(entity, Canvas.GetLeft(entity) - Player.movingSpeed);
                }
                
            }
        }
        private static bool TryUp(List<Rectangle> staticobjects)
        {
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage) , Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage)+47, Canvas.GetTop(Player.playerImage)+75));
            foreach (Rectangle entity in staticobjects)
            {           
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity), Canvas.GetTop(entity) + Player.movingSpeed), new Point(Canvas.GetLeft(entity) + entity.Width, Canvas.GetTop(entity) + Player.movingSpeed + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    return false;
                }
            }
            return true;
        }
        private static bool TryLeft(List<Rectangle> staticobjects)
        {
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + 47, Canvas.GetTop(Player.playerImage) + 75));
            foreach (Rectangle entity in staticobjects)
            {
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity) + Player.movingSpeed, Canvas.GetTop(entity)), new Point(Canvas.GetLeft(entity) + Player.movingSpeed + entity.Width, Canvas.GetTop(entity) + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    return false;
                }
            }
            return true;
        }
        private static bool TryDown(List<Rectangle> staticobjects)
        {
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + 47, Canvas.GetTop(Player.playerImage) + 75));
            foreach (Rectangle entity in staticobjects)
            {
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity), Canvas.GetTop(entity) - Player.movingSpeed), new Point(Canvas.GetLeft(entity) + entity.Width, Canvas.GetTop(entity) - Player.movingSpeed + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    return false;
                }
            }
            return true;
        }
        private static bool TryRight(List<Rectangle> staticobjects)
        {
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + 47, Canvas.GetTop(Player.playerImage) + 75));
            foreach (Rectangle entity in staticobjects)
            {
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity) - Player.movingSpeed, Canvas.GetTop(entity)), new Point(Canvas.GetLeft(entity) - Player.movingSpeed + entity.Width, Canvas.GetTop(entity) + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    return false;
                }
            }
            return true;
        }
    }
}
