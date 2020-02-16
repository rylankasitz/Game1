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
    public delegate void HandleCollision(Entity collider);

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

    public class Transform : Component
    {
        public Vector Position { get; set; } = new Vector(0, 0);
        public Vector Scale { get; set; } = new Vector(100, 100);
        public float Rotation { get; set; } = 0;
        public Transform() { }
        public Transform(int X, int Y, int Width, int Height)
        {
            Position.X = X;
            Position.Y = Y;
            Scale.X = Width;
            Scale.Y = Height;
        }
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
