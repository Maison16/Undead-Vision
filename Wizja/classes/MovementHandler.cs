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
        static Rectangle Player;

        public static void initialize(Rectangle player)
        {
            Player = player;
        }

        public static void Step(int direction, List<Rectangle> movableEntities)
        {

            if (direction == 0x0000)
                return;

            foreach (Rectangle entity in movableEntities)
            {
                if ((direction & 0x1000) == 1)
                    Canvas.SetTop(entity, Canvas.GetTop(entity) - 30);

                if ((direction & 0x0100) == 1)
                    Canvas.SetLeft(entity, Canvas.GetLeft(entity) - 30);

                if ((direction & 0x0010) == 1)
                    Canvas.SetLeft(entity, Canvas.GetTop(entity) + 30);

                if ((direction & 0x0001) == 1)
                    Canvas.SetTop(entity, Canvas.GetTop(entity) + 30);
            }
        }
    }
}
