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

        public SceneComponent(Game1 game) : base(game)
        {
            this.Game = game;
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

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;

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

            spriteBatch.DrawString(schrift, Game.Simulation.PlayerOne.Score.ToString(),new Vector2(0.47f*width,0.1f*height),Color.Black );
            spriteBatch.DrawString(schrift,Game.Simulation.PlayerTwo.Score.ToString(), new Vector2(0.53f * width, 0.1f * height),Color.Black);


            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
