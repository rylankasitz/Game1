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
    public delegate void HandleCollision(Entity collider, string side);

    public class Vector
    {
        public Vector(float x, float y) { X = x; Y = y; }
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public static implicit operator Vector2(Vector v) => new Vector2(v.X, v.Y);

        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector a, Vector b) => new Vector(a.X * b.X, a.Y * b.Y);

        public static Vector operator *(Vector v, float n) => new Vector(v.X * n, v.Y * n);
        public static Vector operator /(Vector v, float n) => new Vector(v.X/n, v.Y/n);
    }

    public class Transform : Component
    {
        public Vector Position { get; set; }
        public Vector Scale { get; set; }
        public float Rotation { get; set; }
        public Transform() { }
        public Transform(int X, int Y, int Width, int Height)
        {
            Position = new Vector(X, Y);
            Scale = new Vector(Width, Height);
        }
    }

    public class Physics : Component
    {
        public Vector Velocity { get; set; }
        public Physics() { }
        public Physics(float VelocityX, float VelocityY)
        {
            Velocity = new Vector(VelocityX, VelocityY);
        }
    }

    public class BoxCollision : Component
    {
        public Vector Position { get; set; }
        public Vector Scale { get; set; }
        public HandleCollision HandleCollision { get; set; }
        public bool TriggerOnly { get; set; }
        public string Layer { get; set; } = "All";
        public BoxCollision() { }
        public BoxCollision(int X, int Y, float Width, float Height, bool TriggerOnly = false)
        {
            Position = new Vector(X, Y);
            Scale = new Vector(Width, Height);
            this.TriggerOnly = TriggerOnly;
        }
    }
}
