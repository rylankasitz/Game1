﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using Microsoft.Xna.Framework.Input;
using static MonoGameWindowsStarter.Componets.RenderComponents;

namespace MonoGameWindowsStarter.Systems
{
    public class Renderer : ECSCore.System 
    {
        private ContentManager contentManager;

        public override bool SetSystemRequirments(Entity entity)
        {
            return entity.HasComponent<Sprite>() &&
                   entity.HasComponent<Transform>();
        }

        public override void Initialize() { }

        public override void InitializeEntity(Entity entity)
        {
            Sprite sprite = entity.GetComponent<Sprite>();
            sprite.Texture = contentManager.Load<Texture2D>(sprite.ContentName);
        }

        public void LoadContent(ContentManager content)
        {
            contentManager = content;

            Texture2D tex = content.Load<Texture2D>("MouseTarget");
            Mouse.SetCursor(MouseCursor.FromTexture2D(tex, -12, -12));

            foreach (Entity entity in Entities)
            {
                InitializeEntity(entity);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in Entities)
            {
                Transform transform = entity.GetComponent<Transform>();
                Sprite sprite = entity.GetComponent<Sprite>();

                spriteBatch.Draw(sprite.Texture, 
                    new Rectangle((int) transform.Position.X, (int) transform.Position.Y, (int) transform.Scale.X, (int) transform.Scale.Y),
                    sprite.SpriteLocation, sprite.Color, transform.Rotation, new Vector2(0, 0),
                    SpriteEffects.None, 1f);  
            }
        }
    }
}
