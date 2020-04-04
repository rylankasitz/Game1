﻿using Microsoft.Xna.Framework;
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
        public Sprite(string ContentName, int SpriteX = 0, int SpriteY = 0, int SpriteWidth = 0, int SpriteHeight = 0, float Layer = 1f, 
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

    public class Parallax : Component
    {
        public int Layer { get; set; }
        public float LayerNum { get; set; }
        public bool Repeat { get; set; }
        public float Speed { get; set; }
        public float ElapsedTime { get; set; }
        public Vector RepeatCount { get; set; } = new Vector(0, 1);
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(-ElapsedTime * Speed, 0, 0);
            }
        }
        public Parallax() { }
        public Parallax(int Layer = 0, bool Repeat = true, float Speed = 100f)
        {
            this.Layer = Layer;
            this.Repeat = Repeat;
            this.Speed = Speed;
        }
    }
}
