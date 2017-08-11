using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Components;
using Pong.Managers;
using Pong.Models;

namespace Pong.Sprites
{
    class Sprite
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public float Speed = 1f;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture,Position,Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else 
                throw new Exception();
        }

        public virtual void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);
        }


    }
}
