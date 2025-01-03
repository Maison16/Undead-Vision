﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Numerics;

namespace Wizja.classes
{
    public static class MovementHandler
    {
        static Player Player;
        private static Label pathLabel;

        public static void initialize(Player player, Label PathLabel)
        {
            Player = player;
            pathLabel=PathLabel;
        }

        public static void Step(bool[] direction, List<Rectangle> movableEntities1, List<Rectangle> movableEntities2, List<Rectangle> movableEntities3, List<Rectangle> staticobjects)
        {

            if (direction[0])
            {
                if (TryUp(staticobjects))
                {
                    Canvas.SetTop(pathLabel, Canvas.GetTop(pathLabel) + Player.movingSpeed);
                    GoUp(movableEntities1);
                    GoUp(movableEntities2);
                    GoUp(movableEntities3);
                }
            }
            if (direction[1])
                if (TryLeft(staticobjects))
                {
                    Canvas.SetLeft(pathLabel, Canvas.GetLeft(pathLabel) + Player.movingSpeed);
                    GoLeft(movableEntities1);
                    GoLeft(movableEntities2);
                    GoLeft(movableEntities3);
                }

            if (direction[2])
                if (TryDown(staticobjects))
                {
                    Canvas.SetTop(pathLabel, Canvas.GetTop(pathLabel) - Player.movingSpeed);
                    GoDown(movableEntities1);
                    GoDown(movableEntities2);
                    GoDown(movableEntities3);
                }
            if (direction[3])
                if (TryRight(staticobjects))
                {
                    Canvas.SetLeft(pathLabel, Canvas.GetLeft(pathLabel) - Player.movingSpeed);
                    GoRight(movableEntities1);
                    GoRight(movableEntities2);
                    GoRight(movableEntities3);
                }

        }
        public static void GoUp(List<Rectangle> movableEntities)
        {
            foreach (Rectangle entity in movableEntities)
            {
                Canvas.SetTop(entity, Canvas.GetTop(entity) + Player.movingSpeed);
            }
        }
        public static void GoLeft(List<Rectangle> movableEntities)
        {
            foreach (Rectangle entity in movableEntities)
            {
                Canvas.SetLeft(entity, Canvas.GetLeft(entity) + Player.movingSpeed);
            }
        }
        public static void GoDown(List<Rectangle> movableEntities)
        {
            foreach (Rectangle entity in movableEntities)
            {
                Canvas.SetTop(entity, Canvas.GetTop(entity) - Player.movingSpeed);
            }
        }
        public static void GoRight(List<Rectangle> movableEntities)
        {
            foreach (Rectangle entity in movableEntities)
            {
                Canvas.SetLeft(entity, Canvas.GetLeft(entity) - Player.movingSpeed);
            }
        }
        private static bool TryUp(List<Rectangle> staticobjects)
        {
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + Player.playerImage.Width, Canvas.GetTop(Player.playerImage) + Player.playerImage.Height));
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
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + Player.playerImage.Width, Canvas.GetTop(Player.playerImage) + Player.playerImage.Height));
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

            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + Player.playerImage.Width, Canvas.GetTop(Player.playerImage) + Player.playerImage.Height));
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
            Rect playerHitbox = new Rect(new Point(Canvas.GetLeft(Player.playerImage), Canvas.GetTop(Player.playerImage)), new Point(Canvas.GetLeft(Player.playerImage) + Player.playerImage.Width, Canvas.GetTop(Player.playerImage) + Player.playerImage.Height));
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
        
