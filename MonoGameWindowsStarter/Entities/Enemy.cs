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
    public class Enemy : Entity
    {
        private float flashSpeed = .1f;
        private int heath = 100;

        private int x, y, w, h;
        private Sprite sprite;
        private float currentFlashSpeed;

        public Enemy(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.h = h;
            this.w = w;
        }

        public override void Initialize()
        {
            sprite = AddComponent<Sprite>();
            Transform transform = AddComponent<Transform>();
            BoxCollision boxCollision = AddComponent<BoxCollision>();

            transform.Name = "Enemy";
            transform.X = x;
            transform.Y = y;
            transform.Height = h;
            transform.Width = w;

            sprite.ContentName = "PixelWhite";
            sprite.Color = Color.Blue;
            sprite.SpriteLocation = new Rectangle(0,0,1,1);

            boxCollision.HandleCollision = handleCollision;

            currentFlashSpeed = flashSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            currentFlashSpeed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            if (flashSpeed > currentFlashSpeed)
            {
                sprite.Color = Color.Red;
            }
            else
            {
                sprite.Color = Color.Blue;
            }
        }

        private void handleCollision(BoxCollision collider)
        {
            if (collider.Transform.Name == "Bullet")
            {    
                currentFlashSpeed = 0f;
                heath -= 20;
            }

            if (heath <= 0)
            {
                SceneManager.GetCurrentScene().RemoveEntity(this);
            }
        }
    }
}
