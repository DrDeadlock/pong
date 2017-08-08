using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pong.Livings
{
    public class Player
    {
        public enum MoveDirection { Up, Down};
        public Vector2 Position;

        public float Width;
        public float Height;
        public float Velocity = 0.015f;

        public int Score;

        public Player(Vector2 initPosition, float initVelocity, float initWidth, float initHeight)
        {
            Position = initPosition;
            Velocity = initVelocity;
            Width = initWidth;
            Height = initHeight;

            Score = 0;
        }

        public void Move(MoveDirection direction)
        {
            switch (direction)
            {
                    case MoveDirection.Up:
                        Position.Y -= Velocity;
                    break;

                    case MoveDirection.Down:
                        Position.Y += Velocity;
                    break;
            }
        }
    }
}
