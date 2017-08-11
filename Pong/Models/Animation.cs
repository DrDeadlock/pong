using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Models
{
    class Animation
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; set; }

        public int FrameWidth => Texture.Width / FrameCount;

        public int FrameHeight => Texture.Height;

        public float FrameSpeed { get; set; }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; set; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.3f;
        }
    }
}
