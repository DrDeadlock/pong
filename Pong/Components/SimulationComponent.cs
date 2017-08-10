using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pong.Components.SimulationHelpers;
using Pong.Constants;
using Pong.Livings;

namespace Pong.Components
{
    internal class SimulationComponent : GameComponent
    {
        private const float INIT_VELOCITY = 0.015f;
        private const float INIT_PLAYER_WIDTH = 0.02f;
        private const float INIT_PLAYER_HEIGHT = 0.16f;
        
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Ball Ball { get; set; }
        
        private Rectangle PlayerOneRect { get; set; }
        private Rectangle PlayerTwoRect { get; set; }
        private Rectangle BallRect { get; set; }

        public SimulationComponent(Game1 game) : base(game)
        {
            this.Game.TargetElapsedTime = TimeSpan.FromSeconds(1/60f);

            PlayerOne = new Player(new Vector2(1 / 6f, 0.5f - INIT_PLAYER_HEIGHT / 2), INIT_VELOCITY, INIT_PLAYER_WIDTH, INIT_PLAYER_HEIGHT);
            PlayerTwo = new Player(new Vector2(5 / 6f, 0.5f - INIT_PLAYER_HEIGHT / 2),INIT_VELOCITY,INIT_PLAYER_WIDTH,INIT_PLAYER_HEIGHT);
            //Initial way starts at half. That's why only the half of fieldLength is regarded for the first directionVector.
            Ball = new Ball(new Vector2(1 / 2f, 1 / 2f), new Vector2(EntityConstants.FIELDLENGTH * SysConstants.REACHTIME / SysConstants.FRAMERATE, 0.00f), 0.016f, 0.022f);

            //For debugging - Ball has no obstacles
            //Ball = new Ball(new Vector2(1 / 6f, 3 / 4f), new Vector2(0.8f * Constants.Constants.REACHTIME / Constants.Constants.FRAMERATE, 0.00f), 0.016f, 0.022f);

        }

        public override void Update(GameTime gameTime)
        {
            //gameTime.ElapsedGameTime multiplizieren mit Velocity um das Spiel framerate frei zu machen.

            Ball.Position.X += Ball.Direction.X;
            Ball.Position.Y += Ball.Direction.Y;

            Ball.UpdateBallEssentials();

            PlayerOneRect = new Rectangle(
                (int)(PlayerOne.Position.X*100),(int)(PlayerOne.Position.Y*100),
                (int)(PlayerOne.Width*100),(int)(PlayerOne.Height*100));
            PlayerTwoRect = new Rectangle(
                (int)(PlayerTwo.Position.X * 100), (int)(PlayerTwo.Position.Y * 100), 
                (int)(PlayerTwo.Width * 100), (int)(PlayerTwo.Height * 100));
            BallRect = new Rectangle(
                (int)(Ball.Position.X * 100), (int)(Ball.Position.Y * 100), 
                (int)(Ball.Height * 100), (int)(Ball.Height * 100));

            //Call of FunMethods. AccelerateForFun is deprecated, but calling it with automove shows funny behaviour
            //FunMethodHelper.HandleAutoMovement(PlayerOne, PlayerTwo);
            //FunMethodHelper.AccelerateForFun(Ball);


            CollisionHelper.HandleBallWallCollision(Ball, PlayerOne, PlayerTwo);
            CollisionHelper.CheckBallPlayerCollision(Ball,PlayerOne, BallRect,PlayerOneRect);
            CollisionHelper.CheckBallPlayerCollision(Ball,PlayerTwo, BallRect,PlayerTwoRect);
            CollisionHelper.HandlePlayerWallCollision(PlayerOne,PlayerTwo);

            base.Update(gameTime);
        }
    }
}
