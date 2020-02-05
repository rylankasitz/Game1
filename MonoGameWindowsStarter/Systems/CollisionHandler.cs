using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Systems
{
    public class CollisionHandler : ECSCore.System
    {
        private int x1, x2, y1, y2, w1, w2, h1, h2;

        public override List<Type> Components {
            get => new List<Type>()
            {
                typeof(BoxCollision),
                typeof(Transform)
            };
        }

        public override void Initialize()
        {
            foreach (Entity entity in Entities)
            {
                InitializeEntity(entity);
            }
        }

        public override void InitializeEntity(Entity entity)
        {
            BoxCollision collider = entity.GetComponent<BoxCollision>();
            Transform transform = entity.GetComponent<Transform>();
            collider.Transform = transform;
        }

        private bool checkCollision(BoxCollision collider1, BoxCollision collider2, 
            Transform transform1, Transform transform2)
        {
            x1 = collider1.X + transform1.X;
            x2 = collider2.X + transform2.X;
            y1 = collider1.Y + transform1.Y;
            y2 = collider2.Y + transform2.Y;
            w1 = collider1.Width * transform1.Width;
            w2 = collider2.Width * transform2.Width;
            h1 = collider1.Height * transform1.Height;
            h2 = collider2.Height * transform2.Height;

            return (x1 < x2 + w2 && x1 + w1 > x2 && y1 < y2 + h2 && y1 + h1 > y2);
        }

        private void handlePhysics(Entity entity, Transform transform, int x, int y, int w, int h)
        {
            if (entity.HasComponent<Physics>())
            {
                Physics physics = entity.GetComponent<Physics>();

                if (transform.Y + transform.Height - physics.Velocity.Y > y && 
                    transform.Y - physics.Velocity.Y < y + h) {

                    if (physics.Velocity.X > 0)
                    {
                        transform.X = x - transform.Width;
                    }
                    else if (physics.Velocity.X < 0)
                    {
                        transform.X = x + w;
                    }
                }

                if (transform.X + transform.Width - physics.Velocity.X > x && 
                    transform.X - physics.Velocity.X < x + w)
                {

                    if (physics.Velocity.Y > 0)
                    {
                        transform.Y = y - transform.Height;
                    }
                    else if (physics.Velocity.Y < 0)
                    {
                        transform.Y = y + h;
                    }
                }
            }
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                for (int j = i + 1; j < Entities.Count; j++)
                {
                    BoxCollision collider1 = Entities[i].GetComponent<BoxCollision>();
                    BoxCollision collider2 = Entities[j].GetComponent<BoxCollision>();

                    Transform transform1 = Entities[i].GetComponent<Transform>();
                    Transform transform2 = Entities[j].GetComponent<Transform>();

                    if (checkCollision(collider1, collider2, transform1, transform2))
                    {
                        collider1.HandleCollision?.Invoke(collider2);
                        collider2.HandleCollision?.Invoke(collider1);

                        if (!collider1.TriggerOnly && !collider2.TriggerOnly)
                        {
                            handlePhysics(Entities[i], transform1, x2, y2, w2, h2);
                            handlePhysics(Entities[j], transform2, x1, y1, w1, h1);
                        }
                    }
                }
            }
        }
    }
}
