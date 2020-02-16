using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Componets
{
    public class RenderComponents
    {
        public class Sprite : Component
        {
            public string ContentName { get; set; } = "test";
            public Rectangle SpriteLocation { get; set; }
            public Color Color { get; set; } = Color.White;
            public Sprite() { }
            public Sprite(string ContentName, int SpriteX, int SpriteY, int SpriteWidth, int SpriteHeight)
            {
                this.ContentName = ContentName;
                SpriteLocation = new Rectangle(SpriteX, SpriteY, SpriteWidth, SpriteHeight);
            }
        }

        public class Animation : Component 
        {
            public string CurrentAnimation { get; set; } = string.Empty;
            public string AnimationFile { get; set; } = string.Empty;
        }

        public class TextDraw : Component
        {
            public string Text { get; set; } = "";
            public Color Color { get; set; } = Color.White;
        }
    }
}
