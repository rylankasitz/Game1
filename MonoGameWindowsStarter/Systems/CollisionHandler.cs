using Microsoft.Xna.Framework;
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
        private Vector p1, p2, s1, s2;

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
            p1 = collider1.Position + transform1.Position;
            p2 = collider2.Position + transform2.Position;
            s1 = collider1.Scale + transform1.Scale;
            s2 = collider2.Scale + transform2.Scale;

            return (p1.X < p2.X + s2.X && p1.X + s1.X > p2.X && p1.Y < p2.Y + s2.Y && p1.Y + s1.Y > p2.Y);
        }

        private void handlePhysics(Entity entity, Transform transform, Vector p, Vector s)
        {
            if (entity.HasComponent<Physics>())
            {
                Physics physics = entity.GetComponent<Physics>();

                if (transform.Position.Y + transform.Scale.Y - physics.Velocity.Y > p.Y && 
                    transform.Position.Y - physics.Velocity.Y < p.Y + s.Y) {

                    if (physics.Velocity.X > 0)
                    {
                        transform.Position.X = p.X - transform.Scale.Y;
                    }
                    else if (physics.Velocity.X < 0)
                    {
                        transform.Position.X = p.X + s.X;
                    }
                }

                if (transform.Position.X + transform.Scale.Y - physics.Velocity.X > p.X && 
                    transform.Position.X - physics.Velocity.X < p.X + s.X)
                {

                    if (physics.Velocity.Y > 0)
                    {
                        transform.Position.Y = p.Y - transform.Scale.Y;
                    }
                    else if (physics.Velocity.Y < 0)
                    {
                        transform.Position.Y = p.Y + s.Y;
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
                            handlePhysics(Entities[i], transform1, p2, s2);
                            handlePhysics(Entities[j], transform2, p1, s1);
                        }
                    }
                }
            }
        }
    }
}
