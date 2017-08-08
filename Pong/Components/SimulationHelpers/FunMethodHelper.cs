using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pong.Livings;

namespace Pong.Components.SimulationHelpers
{
    static class FunMethodHelper
    {
        private const float ACCELERATION_FOR_FUN = 0.0005f;

        public static float autoPlayer1Dir = 0.01f;
        public static float autoPlayer2Dir = -0.01f;

        public static void AccelerateForFun(Ball ball)
        {
            ball.Direction.X += ball.Direction.X > 0 ? ACCELERATION_FOR_FUN : -ACCELERATION_FOR_FUN;
            ball.Direction.Y += ball.Direction.Y > 0 ? ACCELERATION_FOR_FUN : -ACCELERATION_FOR_FUN;
        }

        public static void HandleAutoMovement(Player playerOne,Player playerTwo)
        {
            playerOne.Position.Y += autoPlayer1Dir;
            playerTwo.Position.Y += autoPlayer2Dir;
        }
    }
}
