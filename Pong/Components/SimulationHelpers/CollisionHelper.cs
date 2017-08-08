using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pong.Livings;

namespace Pong.Components.SimulationHelpers
{
    static class CollisionHelper
    {
        //private static double CalculateBallPaddleHitRelation(Ball ball, Player player)
        //{
        //    /*
        //     * BallPaddleHitRelation ist definiert im Bereich 0 .. 1/2 (scaleFactor).
        //     * Trifft der Ball den Schläger an der unteren Hälfte, wird relation > 1/2 und muss daher nachskaliert werden (if ...)
        //     */
        //    var relation = ball.Position.Y - player.Position.Y;
        //    if (relation > player.Height / 2f)
        //    {
        //        relation -= player.Height / 2;
        //        relation *= -1;
        //    }

        //    return relation / player.Height;
        //}

        //private static Vector2 CalculateReflectionDirection(double ballPaddleHitRelation, double ballVelocity, Ball ball)
        //{
        //    var angleDEG = CalculateReflectionAngle(ballPaddleHitRelation);
        //    var angleRAD = 2 * Math.PI * angleDEG / 360;

        //    var newDirX = (float)Math.Sqrt(Math.Pow(ballVelocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));
        //    var newDirY = (float)Math.Sqrt(Math.Pow(ballVelocity, 2) - Math.Pow(ballVelocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));

        //    if (ball.Direction.X > 0)
        //        newDirX *= -1;
        //    if (ballPaddleHitRelation < 0)
        //        newDirY *= -1;

        //    return new Vector2(newDirX,newDirY);
        //}

        //private static double CalculateReflectionAngle(double x)
        //{
        //    /*
        //     * 80° bei Vektorlänge 1 => n = arctan (0.909 / 0.1731)
        //     * Anstieg ergibt sich durch die zweite Tangentengleichung mit y = 0 und x = 1/2
        //     */
        //    return Math.Pow(-158.364f * x + 79.182f,2);
        //}

        private static double calculateRADAngle(Ball ball, Player player)
        {
            double relation = ball.Position.Y - player.Position.Y;
            double angleDEG = 160 * Math.Pow(relation, 2) / player.Height - 80 * relation / player.Height;
            return 2 * Math.PI * angleDEG / 360;
        }

        private static Vector2 calculateUnweightResultVector(double angleRAD, Ball ball)
        {
            var velocity = 1;
            var newDirX = (float)Math.Sqrt(Math.Pow(velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));
            var newDirY = (float)Math.Sqrt(Math.Pow(velocity, 2) - Math.Pow(velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));

            return new Vector2(newDirX,newDirY);
        }

        public static void HandleBallWallCollision(Ball ball, Player playerOne, Player playerTwo)
        {
            if (ball.Position.X < 0 || ball.Position.X > 0.98f)
            {
                ball.Direction *= new Vector2(-1, 1);
                if (ball.Position.X < 0)
                    playerTwo.Score++;
                else
                    playerOne.Score++;
            }

            if (ball.Position.Y < 0 || ball.Position.Y > 0.98f)
                ball.Direction *= new Vector2(1, -1);
        }

        public static void HandleBallPlayerCollision(Ball ball, Player player, Rectangle BallRect, Rectangle playerRectangle)
        {
            //Sieht noch nicht besonders schön aus.
            if (playerRectangle.Intersects(BallRect))
            {
                double angleRAD = calculateRADAngle(ball, player);
                var newV = calculateUnweightResultVector(angleRAD, ball);
                // Resultatvektor auf Geschwindigkeit wichten
            }
        }

        public static void HandlePlayerWallCollision(Player PlayerOne, Player PlayerTwo)
        {
            if (PlayerOne.Position.Y < 0)
            {
                PlayerOne.Position.Y = 0;
                FunMethodHelper.autoPlayer1Dir *= -1;
            }

            if (PlayerOne.Position.Y > 1 - PlayerOne.Height)
            {
                PlayerOne.Position.Y = (float)(1 - PlayerOne.Height);
                FunMethodHelper.autoPlayer1Dir *= -1;
            }

            if (PlayerTwo.Position.Y < 0)
            {
                PlayerTwo.Position.Y = 0;
                FunMethodHelper.autoPlayer2Dir *= -1;
            }

            if (PlayerTwo.Position.Y > 1 - PlayerTwo.Height)
            {
                PlayerTwo.Position.Y = (float)(1 - PlayerTwo.Height);
                FunMethodHelper.autoPlayer2Dir *= -1;
            }

        }
    }
}
