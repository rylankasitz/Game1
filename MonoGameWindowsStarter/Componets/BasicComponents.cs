using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Componets
{
    public delegate void HandleCollision(BoxCollision collider);

    public class Sprite : Component
    {
        public string ContentName { get; set; } = "test";
        public Texture2D Texture { get; set; }
    }

    public class Transform : Component
    {
        public string Name { get; set; } = "Unnamed";
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;
    }

    public class BoxCollision : Component
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public bool Static { get; set; } = true;
        public HandleCollision HandleCollision { get; set; }
        public Transform Transform { get; set; }
    }
}
