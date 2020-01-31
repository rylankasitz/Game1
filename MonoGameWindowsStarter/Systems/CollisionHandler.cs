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
                BoxCollision collider = entity.GetComponent<BoxCollision>();
                Transform transform = entity.GetComponent<Transform>();
                collider.Transform = transform;
            }
        }

        private bool checkCollision(BoxCollision collider1, BoxCollision collider2, 
            Transform transform1, Transform transform2)
        {
            int x1 = collider1.X + transform1.X;
            int x2 = collider2.X + transform2.X;
            int y1 = collider1.Y + transform1.Y;
            int y2 = collider2.Y + transform2.Y;
            int w1 = collider1.Width * transform1.Width;
            int w2 = collider2.Width * transform2.Width;
            int h1 = collider1.Height * transform1.Height;
            int h2 = collider2.Height * transform2.Height;

            return (x1 < x2 + w2 && x1 + w1 > x2 && y1 < y2 + h2 && y1 + h1 > y2);
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
                        if (collider1.Static && !collider2.Static)
                        {
                            collider2.HandleCollision(collider1);
                            // push back collider2
                        }
                        else if(!collider1.Static && collider2.Static)
                        {
                            collider1.HandleCollision(collider2);
                            // push back collider1
                        }
                        else if (!collider1.Static && !collider2.Static)
                        {
                            collider1.HandleCollision(collider2);
                            collider2.HandleCollision(collider1);
                        }
                    }
                }
            }
        }
    }
}
