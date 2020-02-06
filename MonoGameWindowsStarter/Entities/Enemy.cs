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
        private int size = 20;

        private int health;
        private int x, y;
        private Sprite sprite;
        private float currentFlashSpeed;
        private Transform transform;

        public Enemy(int x, int y, int health)
        {
            this.x = x;
            this.y = y;
            this.health = health;
        }

        public override void Initialize()
        {
            sprite = AddComponent<Sprite>();
            transform = AddComponent<Transform>();
            BoxCollision boxCollision = AddComponent<BoxCollision>();

            transform.Name = "Enemy";
            transform.Position = new Vector(x, y);
            transform.Scale = new Vector(size, size) *health;

            sprite.ContentName = "PixelWhite";
            sprite.Color = Color.Blue;
            sprite.SpriteLocation = new Rectangle(0,0,1,1);

            boxCollision.HandleCollision = handleCollision;
            boxCollision.TriggerOnly = true;

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
                health--;
                transform.Scale -= new Vector(size, size);
                transform.Position += new Vector(size, size)/2;
            }

            if (collider.Transform.Name == "StaticObject")
            {
                SceneManager.GetCurrentScene().RemoveEntity(this);
            }

            if (health <= 0)
            {
                SceneManager.GetCurrentScene().RemoveEntity(this);
            }
        }
    }
}
