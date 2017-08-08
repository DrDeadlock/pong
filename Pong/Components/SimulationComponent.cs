using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Pong.Components.SimulationHelpers;
using Pong.Livings;

namespace Pong.Components
{
    internal class SimulationComponent : GameComponent
    {
        private const float INIT_VELOCITY = 0.015f;
        private const float INIT_PLAYER_WIDTH = 0.02f;
        private const float INIT_PLAYER_HEIGHT = 0.2f;
        
        private Game1 Game;

        public Player PlayerOne;
        public Player PlayerTwo;
        public Ball Ball;

        private Rectangle PlayerOneRect;
        private Rectangle PlayerTwoRect;
        private Rectangle BallRect;

        public SimulationComponent(Game1 game) : base(game)
        {
            this.Game = game;
            this.Game.TargetElapsedTime = TimeSpan.FromSeconds(1/60f);
            
            //How to initialize them exactly in the center of the battlefield?
            PlayerOne = new Player(new Vector2(1 / 6f, 0.49f),INIT_VELOCITY,INIT_PLAYER_WIDTH,INIT_PLAYER_HEIGHT);
            PlayerTwo = new Player(new Vector2(5 / 6f, 0.49f),INIT_VELOCITY,INIT_PLAYER_WIDTH,INIT_PLAYER_HEIGHT);
            Ball = new Ball(new Vector2(1 / 2f, 1 / 2f), new Vector2(0.005f, 0.001f),0.016f,0.022f);
        }
        
        public override void Update(GameTime gameTime)
        {
            //gameTime.ElapsedGameTime multiplizieren mit Velocity um das Spiel framerate frei zu machen.

            Ball.Position.X += Ball.Direction.X;
            Ball.Position.Y += Ball.Direction.Y;

            PlayerOneRect = new Rectangle(
                (int)(PlayerOne.Position.X*100),(int)(PlayerOne.Position.Y*100),
                (int)(PlayerOne.Width*100),(int)(PlayerOne.Height*100));
            PlayerTwoRect = new Rectangle(
                (int)(PlayerTwo.Position.X * 100), (int)(PlayerTwo.Position.Y * 100), 
                (int)(PlayerTwo.Width * 100), (int)(PlayerTwo.Height * 100));
            BallRect = new Rectangle(
                (int)(Ball.Position.X * 100), (int)(Ball.Position.Y * 100), 
                (int)(Ball.Height * 100), (int)(Ball.Height * 100));

            //HandleAutoMovement();

            CollisionHelper.HandleBallWallCollision(Ball, PlayerOne, PlayerTwo);
            CollisionHelper.HandleBallPlayerCollision(Ball,PlayerOne, BallRect,PlayerOneRect);
            CollisionHelper.HandleBallPlayerCollision(Ball,PlayerTwo, BallRect,PlayerTwoRect);
            CollisionHelper.HandlePlayerWallCollision(PlayerOne,PlayerTwo);

            base.Update(gameTime);
        }
    }
}
