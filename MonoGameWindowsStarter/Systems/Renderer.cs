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

namespace MonoGameWindowsStarter.Systems
{
    public class Renderer : ECSCore.System 
    {
        public override List<Type> Components
        {
            get => new List<Type>()
            {
                typeof(Sprite),
                typeof(Transform)
            };
        }

        public override void Initialize() { }

        public void LoadContent(ContentManager content)
        {
            foreach (Entity entity in Entities)
            {
                Sprite sprite = entity.GetComponent<Sprite>();
                sprite.Texture = content.Load<Texture2D>(sprite.ContentName);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in Entities)
            {
                Transform transform = entity.GetComponent<Transform>();
                spriteBatch.Draw(entity.GetComponent<Sprite>().Texture, 
                    new Rectangle(transform.X, transform.Y, transform.Width, transform.Height), Color.White);
            }
        }
    }
}
