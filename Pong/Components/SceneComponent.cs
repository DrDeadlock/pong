using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Components
{
    internal class SceneComponent : DrawableGameComponent
    {
        private Game1 Game;
        private SpriteBatch spriteBatch;

        private Texture2D PlayerOne;
        private Texture2D PlayerTwo;
        private Texture2D Ball;

        private SpriteFont schrift;

        private int width;
        private int height;

        //TODO: Find a better way to avoid those redundancies of variables
        private double score1;
        private double score2;
        private int digitCount;
        private int digitCountScore1;
        private int digitCountScore2;
        private float score1Relative = 0.47f;
        private float score2Relative = 0.53f;

        public SceneComponent(Game1 game) : base(game)
        {
            this.Game = game;

            //TODO --> change behaviour after 'restart' technology was implemented.
            digitCountScore1 = 1;
            digitCountScore2 = 1;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerOne = Game.Content.Load<Texture2D>("Pixel1x1");
            PlayerTwo = Game.Content.Load<Texture2D>("Pixel1x1");
            Ball = Game.Content.Load<Texture2D>("Pixel1x1");

            schrift = Game.Content.Load<SpriteFont>("File");

            base.LoadContent();
        }

        private void CheckForStringMove()
        {
            score1 = Game.Simulation.PlayerOne.Score;
            digitCount = score1 > 0 ? (int)Math.Floor(Math.Log10(score1) + 1) : 1;

            if (digitCount > digitCountScore1)
            {
                score1Relative -= 0.01f;
                digitCountScore1++;
            }

            score2 = Game.Simulation.PlayerTwo.Score;
            digitCount = score2 > 0 ? (int)Math.Floor(Math.Log10(score2)) : 1;

            if (digitCount > digitCountScore2)
            {
                score2Relative += 0.01f;
                digitCountScore2++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckForStringMove();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

            //TODO: Put several DrawBehaviours into seperate functions
            spriteBatch.Begin();
            spriteBatch.Draw(
                PlayerOne,
                new Rectangle((int)(Game.Simulation.PlayerOne.Position.X * width), (int)(Game.Simulation.PlayerOne.Position.Y * height), 
                            (int)(Game.Simulation.PlayerOne.Width * width), (int)(Game.Simulation.PlayerOne.Height * height)),
                Color.White);
            spriteBatch.Draw(
                PlayerTwo,
                new Rectangle((int)(Game.Simulation.PlayerTwo.Position.X * width), (int)(Game.Simulation.PlayerTwo.Position.Y * height),
                    (int)(Game.Simulation.PlayerTwo.Width * width), (int)(Game.Simulation.PlayerTwo.Height * height)),
                Color.White);
            spriteBatch.Draw(Ball, 
                new Rectangle((int)(Game.Simulation.Ball.Position.X * width),(int)(Game.Simulation.Ball.Position.Y * height), 
                            (int)(Game.Simulation.Ball.Width * width),(int)(Game.Simulation.Ball.Height * height)),
                Color.White);

            spriteBatch.DrawString(schrift, Game.Simulation.PlayerOne.Score.ToString(),new Vector2(score1Relative*width,0.1f*height),Color.Black );
            spriteBatch.DrawString(schrift,Game.Simulation.PlayerTwo.Score.ToString(), new Vector2(score2Relative * width, 0.1f * height),Color.Black);


            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
