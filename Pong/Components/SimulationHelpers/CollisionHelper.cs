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
            double angleDEG = 320 * Math.Pow(relation, 2) / Math.Pow(player.Height, 2) - 320 * relation / player.Height + 80;
            return 2 * Math.PI * angleDEG / 360;
        }

        private static Vector2 calculateUnweightResultVector(double angleRAD, Ball ball, Player player)
        {
            var newDirX = (float)Math.Sqrt(Math.Pow(ball.Velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));
            var newDirY = (float)Math.Sqrt(Math.Pow(ball.Velocity, 2) - Math.Pow(ball.Velocity, 2) / (Math.Pow(Math.Tan(angleRAD), 2) + 1));

            if (ball.Direction.X > 0)
                newDirX *= -1;

            if (ball.Position.Y - player.Position.Y < player.Height / 2)
                newDirY *= -1;

            return new Vector2(newDirX,newDirY);
        }

        private static Vector2 calculateWeightedResultVector(double reachTime,Player player, Vector2 unweightResultVector)
        {
            double activeFieldLength;
            if (player.Position.X < 0.5f)
                activeFieldLength = 1 - 2 * player.Position.X;
            else
                activeFieldLength = 1 - (1 - player.Position.X) * 2;

            var einheitsVektor = unweightResultVector / unweightResultVector.Length();
            var framerate = reachTime * 60;
            return new Vector2((float)(einheitsVektor.X * activeFieldLength / framerate), (float)(einheitsVektor.Y * activeFieldLength / framerate));
        }

        

        private static void HandleBallPlayerCollision(Ball ball, Player player)
        {
            double angleRAD = calculateRADAngle(ball, player);
            var newV = calculateUnweightResultVector(angleRAD, ball, player);
            ball.Direction = calculateWeightedResultVector(1, player, newV);
        }

        public static void CheckBallPlayerCollision(Ball ball, Player player, Rectangle ballRect, Rectangle playerRectangle)
        {
            //Sieht noch nicht besonders schön aus.
            if (playerRectangle.Intersects(ballRect))
            {
                if (player.Position.X < 0.5f)
                    ball.Position.X = player.Position.X + player.Width;
                //else
                    //ball.Position.X = player.Position.X -0.001f;

                HandleBallPlayerCollision(ball,player);
            }
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
