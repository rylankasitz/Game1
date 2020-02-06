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

    public class Vector
    {
        public Vector(float x, float y) { X = x; Y = y; }
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public static implicit operator Vector2(Vector v) => new Vector2(v.X, v.Y);

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);

        public static Vector operator *(Vector v, float n) => new Vector(v.X * n, v.Y * n);
        public static Vector operator /(Vector v, float n) => new Vector(v.X/n, v.Y/n);
    }

    public class Sprite : Component
    {
        public string ContentName { get; set; } = "test";
        public Texture2D Texture { get; set; }
        public Rectangle SpriteLocation { get; set; }
        public Color Color { get; set; } = Color.White;
    }

    public class Transform : Component
    {
        public string Name { get; set; } = "Unnamed";
        public Vector Position { get; set; } = new Vector(0, 0);
        public Vector Scale { get; set; } = new Vector(100, 100);
        public float Rotation { get; set; } = 0;
    }

    public class Physics : Component
    {
        public Vector Velocity { get; set; } = new Vector(0, 0);
    }

    public class BoxCollision : Component
    {
        public Vector Position { get; set; } = new Vector(0, 0);
        public Vector Scale { get; set; } = new Vector(1, 1);
        public HandleCollision HandleCollision { get; set; }
        public Transform Transform { get; set; }
        public bool TriggerOnly { get; set; } = false;
    }
}
