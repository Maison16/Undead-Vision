using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Wizja.classes
{
    public static class MovementHandler
    {
        static Player Player;
        static public Rect playerHitbox;

        public static void initialize(Player player)
        {
            Player = player;
        }

        public static void Step(bool[] direction, List<Rectangle> movableEntities)
        {
            foreach (Rectangle entity in movableEntities)
            {
                if (direction[0])
                    Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);

                if (direction[1])
                    Canvas.SetLeft(entity, Canvas.GetLeft(entity) + Player.movingSpeed);

                if (direction[2])
                    Canvas.SetTop(entity, Canvas.GetTop(entity) - Player.movingSpeed);

                if (direction[3])
                    Canvas.SetLeft(entity, Canvas.GetLeft(entity) - Player.movingSpeed);
            }
        }

        public static void Step2(bool[] direction, List<Rectangle> movableEntities, List<Rectangle> staticobjects)
        {
              if (!direction[0] && !direction[1] && !direction[2] && !direction[3])
                 return;
            playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage)+47, Canvas.GetTop(Player.playerImage)+75));
            foreach (Rectangle entity in movableEntities)
            {
                
                if (direction[0])
                {
                    if (IsColision(staticobjects)) 
                    {
                        Step(new bool[4] {false, false, true, false }, movableEntities);
                    } 
                }


                if (direction[1])
                {
                    if (IsColision(staticobjects))
                    {
                        Step(new bool[4] { false, false, false, true }, movableEntities);
                    }
                }


                if (direction[2])
                {
                    if (IsColision(staticobjects))
                    {
                        Step(new bool[4] { true, false, false, false }, movableEntities);
                    }
                }


                if (direction[3])
                {

                    if (IsColision(staticobjects))
                    {
                        Step(new bool[4] { false, true, false, false }, movableEntities);
                    }
                }

            }
        }
        private static bool TryUp(List<Rectangle> staticobjects)
        {
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
            bool val = false;
            foreach (Rectangle entity in staticobjects)
            {
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity) + Player.movingSpeed, Canvas.GetTop(entity)), new Point(Canvas.GetLeft(entity) + Player.movingSpeed + entity.Width, Canvas.GetTop(entity) + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    val = true;
                }
                else 
                {
                    entityHitbox = new Rect(new Point(Canvas.GetLeft(entity) + Player.movingSpeed, Canvas.GetTop(entity)), new Point(Canvas.GetLeft(entity) + Player.movingSpeed + entity.Width, Canvas.GetTop(entity) + entity.Height));
                }
            }
            return val;
        }
        private static bool TryDown(List<Rectangle> staticobjects)
        {


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
        private static bool IsColision(List<Rectangle> staticobjects) 
        {
            foreach (Rectangle entity in staticobjects)
            {
                Rect entityHitbox = new Rect(new Point(Canvas.GetLeft(entity), Canvas.GetTop(entity) + Player.movingSpeed), new Point(Canvas.GetLeft(entity) + entity.Width, Canvas.GetTop(entity) + Player.movingSpeed + entity.Height));
                if (playerHitbox.IntersectsWith(entityHitbox))
                {
                    //Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
                    return true;
                }
            }
            return false;
        }
    }
}
