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

            transform.Width = 18;
            transform.Height = 9;

            boxCollision.HandleCollision = handleCollision;
            boxCollision.TriggerOnly = true;
        }

        public override void Update(GameTime gameTime)
        {
        }

        private void handleCollision(BoxCollision collider)
        {
            if (collider.Transform.Name == "StaticObject")
            {
                SceneManager.GetCurrentScene().RemoveEntity(this);
            }
        }
    }
}
