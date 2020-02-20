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
    public class Sprite : Component
    {
        public string ContentName { get; set; } = "test";
        public Rectangle SpriteLocation { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Layer { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Sprite() { }
        public Sprite(string ContentName, int SpriteX = 0, int SpriteY = 0, int SpriteWidth = 1, int SpriteHeight = 1, float Layer = 1f, 
            SpriteEffects SpriteEffects = SpriteEffects.None)
        {
            this.ContentName = ContentName;
            this.Layer = Layer;
            this.SpriteEffects = SpriteEffects;
            SpriteLocation = new Rectangle(SpriteX, SpriteY, SpriteWidth, SpriteHeight);
        }
    }

    public class Animation : Component 
    {
        public string CurrentAnimation { get; set; } = string.Empty;
        public Dictionary<string, AnimationTracker> AnimationTracker { get; set; } = new Dictionary<string, AnimationTracker>();
        public Animation() { }
        public Animation(string CurrentAnimation)
        {
            this.CurrentAnimation = CurrentAnimation;
        }
    }

    public class TextDraw : Component
    {
        public string Text { get; set; } = "";
        public Color Color { get; set; } = Color.Black;

        public TextDraw() { }
        public TextDraw(string Text)
        {
            this.Text = Text;
        }
    }
}
