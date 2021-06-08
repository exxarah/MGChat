﻿using System;
using Microsoft.Xna.Framework;

namespace MGChat.Util
{
    public static class Conversion
    {
        public static string VectorToDirection(Vector2 direction)
        {
            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                // XDominant
                return direction.X < 0 ? "Left" : "Right";
            }
            else
            {
                // YDominant
                return direction.Y < 0 ? "Up" : "Down";
            }
        }
    }
}