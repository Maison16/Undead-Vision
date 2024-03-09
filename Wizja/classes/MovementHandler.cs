using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wizja.classes
{
    public static class MovementHandler
    {
        static Player Player;

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
    }
}
