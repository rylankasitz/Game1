using Microsoft.Xna.Framework;
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
    public class Enemy : Entity
    {
        private int size = 40;
        private float flashSpeed = .1f;
        public float Speed = 5f;

        private int health;
        private int x, y;
        private float currentFlashSpeed;

        private Sprite sprite;
        private Transform transform;
        private Scene currentScene;
        private Player player;
        private HUD hud;

        /*public Enemy(int x, int y, int health)
        {
            this.x = x;
            this.y = y;
            this.health = health;
        }*/

        public override void Initialize()
        {
            Name = "Enemy";

            sprite = AddComponent<Sprite>();
            transform = AddComponent<Transform>();
            BoxCollision boxCollision = AddComponent<BoxCollision>();
            Physics physics = AddComponent<Physics>();
       
            transform.Position = new Vector(x, y);
            transform.Scale = new Vector(size, size) *health;

            sprite.ContentName = "PixelWhite";
            sprite.Color = Color.Blue;
            sprite.SpriteLocation = new Rectangle(0,0,1,1);

            boxCollision.HandleCollision = handleCollision;
            boxCollision.TriggerOnly = true;

            currentFlashSpeed = flashSpeed;

            currentScene = SceneManager.GetCurrentScene();
            player = currentScene.GetEntity<Player>("Player");
            hud = currentScene.GetEntity<HUD>("HUD");

            if (transform.Position.Y == 0)
            {
                physics.Velocity = new Vector(0, Speed);
            }
            else
            {
                physics.Velocity = new Vector(0, -Speed);
            }
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

        private void handleCollision(Entity collider)
        {
            if (collider.Name == "Bullet")
            {    
                currentFlashSpeed = 0f;
                health--;
                transform.Scale -= new Vector(size, size);
                transform.Position += new Vector(size, size)/2;

                if (health <= 0)
                {
                    currentScene.RemoveEntity(this);
                    player.Score += 100;
                    hud.Score.GetComponent<TextDraw>().Text = player.Score.ToString();
                }
            }

            if (health <= 0)
            {
                currentScene.RemoveEntity(this);
            }
        }
    }
}
