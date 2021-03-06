﻿using System;
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
        //Property Game überschrieben und auf Game1 "gecastet" und hat in Kurzschreibweise eine GetMethode
        public new Game1 Game => (Game1)base.Game;
        private SpriteBatch SpriteBatch;

        private Texture2D PlayerOne;
        private Texture2D PlayerTwo;
        private Texture2D Ball;

        private SpriteFont Schrift;

        private int Width;
        private int Height;

        //TODO: Optimize ScoreTextAdjustement
        private double Score1;
        private double Score2;
        private int DigitCount;
        private int DigitCountScore1;
        private int DigitCountScore2;
        private float Score1Relative = 0.47f;
        private float Score2Relative = 0.53f;

        public SceneComponent(Game1 game) : base(game)
        {
            //TODO --> change behaviour after 'restart' technology was implemented.
            DigitCountScore1 = 1;
            DigitCountScore2 = 1;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            PlayerOne = PlayerTwo = Ball = Game.Content.Load<Texture2D>("Pixel1x1");

            Schrift = Game.Content.Load<SpriteFont>("File");

            base.LoadContent();
        }

        private void CheckForStringMove()
        {
            Score1 = Game.Simulation.PlayerOne.Score;
            DigitCount = Score1 > 0 ? (int)Math.Floor(Math.Log10(Score1) + 1) : 1;

            if (DigitCount > DigitCountScore1)
            {
                Score1Relative -= 0.01f;
                DigitCountScore1++;
            }

            Score2 = Game.Simulation.PlayerTwo.Score;
            DigitCount = Score2 > 0 ? (int)Math.Floor(Math.Log10(Score2)) : 1;

            if (DigitCount > DigitCountScore2)
            {
                Score2Relative += 0.01f;
                DigitCountScore2++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            CheckForStringMove();

            base.Update(gameTime);
        }

        private void DrawPlayers()
        {
            SpriteBatch.Draw(
                PlayerOne,
                new Rectangle((int)(Game.Simulation.PlayerOne.Position.X * Width), (int)(Game.Simulation.PlayerOne.Position.Y * Height),
                    (int)(Game.Simulation.PlayerOne.Width * Width), (int)(Game.Simulation.PlayerOne.Height * Height)),
                Color.White);
            SpriteBatch.Draw(
                PlayerTwo,
                new Rectangle((int)(Game.Simulation.PlayerTwo.Position.X * Width), (int)(Game.Simulation.PlayerTwo.Position.Y * Height),
                    (int)(Game.Simulation.PlayerTwo.Width * Width), (int)(Game.Simulation.PlayerTwo.Height * Height)),
                Color.White);
            SpriteBatch.Draw(Ball,
                new Rectangle((int)(Game.Simulation.Ball.Position.X * Width), (int)(Game.Simulation.Ball.Position.Y * Height),
                    (int)(Game.Simulation.Ball.Width * Width), (int)(Game.Simulation.Ball.Height * Height)),
                Color.White);
        }

        private void DrawScores()
        {
            SpriteBatch.DrawString(Schrift, Game.Simulation.PlayerOne.Score.ToString(), new Vector2(Score1Relative * Width, 0.1f * Height), Color.Black);
            SpriteBatch.DrawString(Schrift, Game.Simulation.PlayerTwo.Score.ToString(), new Vector2(Score2Relative * Width, 0.1f * Height), Color.Black);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            Width = GraphicsDevice.Viewport.Width;
            Height = GraphicsDevice.Viewport.Height;
            
            SpriteBatch.Begin();

            DrawPlayers();
            DrawScores();

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
