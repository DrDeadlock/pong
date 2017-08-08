using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pong.Livings
{
    public class Ball
    {
        public Vector2 Position;
        public Vector2 Direction;

        public float Width;
        public float Height;
        public double Velocity;

        public Ball(Vector2 initPosition, Vector2 initDirection, float initWidth, float initHeight)
        {
            Position = initPosition;
            Direction = initDirection;
            Width = initWidth;
            Height = initHeight;
            
            Velocity = Math.Sqrt((double)(Direction.X * Direction.X + Direction.Y * Direction.Y))*2;
        }
    }
}
