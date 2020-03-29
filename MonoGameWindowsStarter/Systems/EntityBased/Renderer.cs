using Microsoft.Xna.Framework.Graphics;
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
using System.IO;

namespace MonoGameWindowsStarter.Systems
{
    public class Renderer : ECSCore.System 
    {
        private ContentManager contentManager;
        private SpriteFont font;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public override bool SetSystemRequirments(Entity entity)
        {
            return (entity.HasComponent<Sprite>() || entity.HasComponent<TextDraw>()) &&
                   entity.HasComponent<Transform>();
        }

        public override void Initialize() { }

        public override void InitializeEntity(Entity entity) { }

        public override void RemoveFromSystem(Entity entity) { }

        public void LoadContent(ContentManager content)
        {
            contentManager = content;
            font = contentManager.Load<SpriteFont>("Fonts/BasicFont");

            string[] files = Directory.GetFiles(content.RootDirectory + "\\Sprites", "*.xnb", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                textures[filename] = contentManager.Load<Texture2D>("Sprites\\" + filename);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            foreach (Entity entity in Entities)
            {
                Transform transform = entity.GetComponent<Transform>();

                if (entity.HasComponent<Sprite>() && entity.GetComponent<Sprite>().Enabled)
                {
                    Sprite sprite = entity.GetComponent<Sprite>();
                    spriteBatch.Draw(textures[sprite.ContentName],
                        new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.Scale.X, (int)transform.Scale.Y),
                        sprite.SpriteLocation, sprite.Color, transform.Rotation, new Vector2(0, 0),
                        sprite.SpriteEffects, sprite.Layer);
                }

                if (entity.HasComponent<TextDraw>())
                {
                    TextDraw text = entity.GetComponent<TextDraw>();

                    spriteBatch.DrawString(font, text.Text, transform.Position, text.Color, transform.Rotation, new Vector2(0,0), transform.Scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
