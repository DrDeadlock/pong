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
        private readonly Game1 _game;
        private KeyboardState KeyState;

        public InputComponent(Game1 game) : base(game)
        {
            this._game = game;
        }

        private void MovePlayerOne(KeyboardState pKeyboardState)
        {
            if (pKeyboardState.IsKeyDown(Keys.W))
                _game.Simulation.PlayerOne.Move(Player.MoveDirection.Up);

            if (pKeyboardState.IsKeyDown(Keys.S))
                _game.Simulation.PlayerOne.Move(Player.MoveDirection.Down);


        }

        private void MovePlayerTwo(KeyboardState pKeyboardState)
        {
            if (pKeyboardState.IsKeyDown(Keys.Up))
                _game.Simulation.PlayerTwo.Move(Player.MoveDirection.Up);

            if (pKeyboardState.IsKeyDown(Keys.Down))
                _game.Simulation.PlayerTwo.Move(Player.MoveDirection.Down);
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
