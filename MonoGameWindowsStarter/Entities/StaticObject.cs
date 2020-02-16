﻿using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonoGameWindowsStarter.Componets.RenderComponents;

namespace MonoGameWindowsStarter.Entities
{
    public class StaticObject : Entity
    {
        private int x, y, w, h;
        private string contentName;
        private Rectangle spriteLocation;

        /*public StaticObject(int x, int y, int w, int h, string contentName, Rectangle spriteLocation)
        {
            this.x = x;
            this.y = y;
            this.h = h;
            this.w = w;

            this.contentName = contentName;
            this.spriteLocation = spriteLocation;
        }*/

        public override void Initialize()
        {
            Name = "StaticObject";

            Sprite sprite = AddComponent<Sprite>();
            Transform transform = AddComponent<Transform>();
            BoxCollision boxCollision = AddComponent<BoxCollision>();

            transform.Position = new Vector(x, y);
            transform.Scale = new Vector(w, h);

            sprite.ContentName = contentName;
            sprite.SpriteLocation = spriteLocation;
        }

        public override void Update(GameTime gameTime) { }
    }
}
