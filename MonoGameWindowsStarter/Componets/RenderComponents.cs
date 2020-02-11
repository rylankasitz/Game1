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
            public Texture2D Texture { get; set; }
            public Rectangle SpriteLocation { get; set; }
            public Color Color { get; set; } = Color.White;
        }

        public class Animation : Component 
        {
            public string CurrentAnimation { get; set; } = string.Empty;
            public string AnimationFile { get; set; } = string.Empty;

        }
    }
}
