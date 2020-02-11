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
    public class Bullet : Entity
    {
        private Sprite sprite;
        private Transform transform;
        private BoxCollision boxCollision;
        private Physics physics;

        public override void Initialize()
        {
            sprite = AddComponent<Sprite>();
            transform = AddComponent<Transform>();
            boxCollision = AddComponent<BoxCollision>();
            physics = AddComponent<Physics>();

            sprite.ContentName = "BulletsSpriteSheet";
            sprite.SpriteLocation = new Rectangle(249, 9, 18, 9);

            transform.Name = "Bullet";
            transform.Scale = new Vector(18, 9);

            boxCollision.HandleCollision = handleCollision;
            boxCollision.TriggerOnly = true;
        }

        public override void Update(GameTime gameTime)
        {
        }

        private void handleCollision(Entity collider)
        {
            if (collider.GetComponent<Transform>().Name == "StaticObject" || collider.GetComponent<Transform>().Name == "Enemy")
            {
                SceneManager.GetCurrentScene().RemoveEntity(this);
            }
        }
    }
}
