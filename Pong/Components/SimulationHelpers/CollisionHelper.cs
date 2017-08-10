using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pong.Livings;
using Pong.Constants;

namespace Pong.Components.SimulationHelpers
{
    static class CollisionHelper
    {
        //TODO: Find out why this Constants.Constants. ... is necessary .___O
        private const float REACHTIME = SysConstants.REACHTIME;

        private static double CalculateRadAngle(Ball ball, Player player)
        {
            //Treffer mit unterer Kante liefert relation = 0.10 --> 51°
            double relation = ball.Position.Y - player.Position.Y;
            if (relation < 0 || relation > 0.5f)
                return 2 * Math.PI * 80 / 360;
            //TODO: adjust function to detect angle or change into switch-case...
            double angleDEG = 320 * Math.Pow(relation, 2) / Math.Pow(player.Height, 2) - 320 * relation / player.Height + 80;
            return 2 * Math.PI * angleDEG / 360;
        }

        #region First (honestly... second) try for collision.
        //private static Vector2 CalculateUnweightResultVector(double angleRAD, Ball ball, Player player)
        //{
        //    var newDirX = (float)Math.Sqrt(Math.Pow(ball.Velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));
        //    var newDirY = (float)Math.Sqrt(Math.Pow(ball.Velocity, 2) - Math.Pow(ball.Velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));

        //    if (ball.Direction.X > 0)
        //        newDirX *= -1;

        //    if (ball.Position.Y - player.Position.Y < player.Height / 2)
        //        newDirY *= -1;

        //    return new Vector2(newDirX,newDirY);
        //}

        //private static Vector2 CalculateWeightedResultVector(double reachTime,Player player, Vector2 unweightResultVector)
        //{
        //    double activeFieldLength;
        //    if (player.Position.X < 0.5f)
        //        activeFieldLength = 1 - 2 * player.Position.X;
        //    else
        //        activeFieldLength = 1 - (1 - player.Position.X) * 2;

        //    var einheitsVektor = unweightResultVector / unweightResultVector.Length();
        //    var framerate = reachTime * 60;
        //    return new Vector2((float)(einheitsVektor.X * activeFieldLength / framerate), (float)(einheitsVektor.Y * activeFieldLength / framerate));
        //}
        #endregion

        private static Vector2 CalculateNewDirection(double angleRAD, Ball ball, Player player)
        {
            var s = EntityConstants.FIELDLENGTH / Math.Cos(angleRAD);
            var frames = SysConstants.FRAMERATE * s / EntityConstants.FIELDLENGTH;
            var y = (float) (s * Math.Sin(angleRAD));  // frames * REACHTIME);
            var x = (float) (Math.Sqrt(Math.Pow(s, 2) - Math.Pow(y, 2))); // frames * REACHTIME);

            //Switch direction if player two hit the ball
            if (ball.Direction.X > 0)
                x *= -1;

            //upper half of paddle throws ball to the top, lower half throws to the bottom
            if (ball.Position.Y - player.Position.Y < player.Height / 2)
                y *= -1;

            return new Vector2((float)(x / (frames * REACHTIME)),(float)(y / (frames * REACHTIME)));
        }

        private static void HandleBallPlayerCollision(Ball ball, Player player)
        {
            //TODO: Adjust angleDEG Detection. (Die Parabel ist zu flach)
            var angleRAD = CalculateRadAngle(ball, player);
            ball.Direction = CalculateNewDirection(angleRAD, ball, player);
        }

        public static void CheckBallPlayerCollision(Ball ball, Player player, Rectangle ballRect, Rectangle playerRectangle)
        {
            if (playerRectangle.Intersects(ballRect))
            {
                //Fixes ball-in-paddle-prisoned error caused by intersection method
                if (player.Position.X < 0.5f)
                    ball.Position.X = player.Position.X + player.Width;
                else
                    ball.Position.X = player.Position.X - 0.015f;
                
                HandleBallPlayerCollision(ball,player);
            }
        }

        public static void HandleBallWallCollision(Ball ball, Player playerOne, Player playerTwo)
        {
            //TODO: change reflection of side walls to start of a new round
            if (ball.Position.X < 0 || ball.Position.X > 0.98f)
            {
                ball.Direction *= new Vector2(-1, 1);
                if (ball.Position.X < 0)
                    playerTwo.Score ++;
                else
                    playerOne.Score ++;
            }

            if (ball.Position.Y < 0 || ball.Position.Y > 0.98f)
                ball.Direction *= new Vector2(1, -1);
        }

        public static void HandlePlayerWallCollision(Player playerOne, Player playerTwo)
        {
            if (playerOne.Position.Y < 0)
            {
                playerOne.Position.Y = 0;
                FunMethodHelper.autoPlayer1Dir *= -1;
            }

            if (playerOne.Position.Y > 1 - playerOne.Height)
            {
                playerOne.Position.Y = (float)(1 - playerOne.Height);
                FunMethodHelper.autoPlayer1Dir *= -1;
            }

            if (playerTwo.Position.Y < 0)
            {
                playerTwo.Position.Y = 0;
                FunMethodHelper.autoPlayer2Dir *= -1;
            }

            if (playerTwo.Position.Y > 1 - playerTwo.Height)
            {
                playerTwo.Position.Y = (float)(1 - playerTwo.Height);
                FunMethodHelper.autoPlayer2Dir *= -1;
            }

        }
    }
}
