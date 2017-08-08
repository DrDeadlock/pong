using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Components;

namespace Pong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        internal InputComponent Input { get; set; }
        internal SimulationComponent Simulation { get; set; }
        internal SceneComponent Scene { get; set; }

        public int ScreenWidth;
        public int ScreenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            Input = new InputComponent(this) {UpdateOrder = 0};
            Components.Add(Input);

            Simulation = new SimulationComponent(this) {UpdateOrder = 1};
            Components.Add(Simulation);

            Scene = new SceneComponent(this) {UpdateOrder = 2, DrawOrder = 0};
            Components.Add(Scene);

            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

    }
}
