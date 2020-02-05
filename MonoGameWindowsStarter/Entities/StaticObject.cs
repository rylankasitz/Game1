using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Entities
{
    public class StaticObject : Entity
    {
        private int x, y, w, h;
        private string contentName;
        private Rectangle spriteLocation;

        public StaticObject(int x, int y, int w, int h, string contentName, Rectangle spriteLocation)
        {
            this.x = x;
            this.y = y;
            this.h = h;
            this.w = w;

            this.contentName = contentName;
            this.spriteLocation = spriteLocation;
        }

        public override void Initialize()
        {
            Sprite sprite = AddComponent<Sprite>();
            Transform transform = AddComponent<Transform>();
            BoxCollision boxCollision = AddComponent<BoxCollision>();

            transform.Name = "StaticObject";
            transform.X = x;
            transform.Y = y;
            transform.Height = h;
            transform.Width = w;

            sprite.ContentName = contentName;
            sprite.SpriteLocation = spriteLocation;
        }

        public override void Update(GameTime gameTime) { }
    }
}
