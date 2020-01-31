using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Systems;

namespace MonoGameWindowsStarter.Entities
{
    class Player : Entity
    {
        Sprite sprite;
        Transform transform;
        BoxCollision boxCollision;

        private int speed = 5;

        public override void Initialize()
        {
            sprite = AddComponent<Sprite>();
            transform = AddComponent<Transform>();
            boxCollision = AddComponent<BoxCollision>();

            sprite.ContentName = "test";

            transform.X = 0;
            transform.Y = 0;
            transform.Width = 100;
            transform.Height = 100;

            boxCollision.Static = false;
            boxCollision.HandleCollision = handleCollision;
        }

        public override void Update(GameTime gameTime)
        {
            move();     
        }

        private void handleCollision(BoxCollision collider)
        {
            Console.WriteLine(collider.Transform.Name);
        }

        private void move()
        {
            if (InputManager.KeyPressed(Keys.W))
            {
                transform.Y -= speed;
            }
            if (InputManager.KeyPressed(Keys.S))
            {
                transform.Y += speed;
            }
            if (InputManager.KeyPressed(Keys.A))
            {
                transform.X -= speed;
            }
            if (InputManager.KeyPressed(Keys.D))
            {
                transform.X += speed;
            }
        }
    }
}
