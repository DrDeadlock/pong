using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Livings;

namespace Pong.Components
{
    internal class InputComponent : GameComponent
    {
        public new Game1 Game => (Game1) base.Game;
        private KeyboardState KeyState;

        public InputComponent(Game1 game) : base(game)
        {
            
        }

        private void MovePlayerOne(KeyboardState pKeyboardState)
        {
            if (pKeyboardState.IsKeyDown(Keys.W))
                Game.Simulation.PlayerOne.Move(Player.MoveDirection.Up);

            if (pKeyboardState.IsKeyDown(Keys.S))
                Game.Simulation.PlayerOne.Move(Player.MoveDirection.Down);


        }

        private void MovePlayerTwo(KeyboardState pKeyboardState)
        {
            if (pKeyboardState.IsKeyDown(Keys.Up))
                Game.Simulation.PlayerTwo.Move(Player.MoveDirection.Up);

            if (pKeyboardState.IsKeyDown(Keys.Down))
                Game.Simulation.PlayerTwo.Move(Player.MoveDirection.Down);
        }

        public override void Update(GameTime gameTime)
        {
            KeyState = Keyboard.GetState();
            MovePlayerOne(KeyState);
            MovePlayerTwo(KeyState);


            base.Update(gameTime);
        }
    }
}
