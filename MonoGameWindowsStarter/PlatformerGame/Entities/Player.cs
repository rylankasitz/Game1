using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.Systems;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Systems.Global;
using System.Diagnostics;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 40, Y: 200, Width: 20, Height: 20)]
    [Sprite(ContentName: "spritesheet", layer: 0f, SpriteX: 440, SpriteY: 6, SpriteWidth: 20, SpriteHeight: 20)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 0, Width: 1, Height: 1)]
    [Animation(AnimationFile: "Player")]
    public class Player : Entity
    {
        public float Speed = 4;
        public float Gravity = .15f;
        public float JumpForce = 5;

        private Physics physics;
        private Transform transform;
        private BoxCollision boxCollision;

        private bool isGrounded;

        public override void Initialize()
        {
            physics = GetComponent<Physics>();
            transform = GetComponent<Transform>();
            boxCollision = GetComponent<BoxCollision>();

            boxCollision.HandleCollision = handleCollision;
            isGrounded = false;

            Camera.Zoom = 3;
        }

        public override void Update(GameTime gameTime)
        {
            physics.Velocity.X = 0;

            if (InputManager.KeyPressed(Keys.A))
            {
                physics.Velocity.X = -Speed;
            }
            if (InputManager.KeyPressed(Keys.D))
            {
                physics.Velocity.X = Speed;
            }
            if (InputManager.KeyDown(Keys.Space) && isGrounded)
            {
                physics.Velocity.Y = -JumpForce;
                isGrounded = false;
            }
            
            if (!isGrounded)
                physics.Velocity.Y += Gravity;

            Camera.Position.X = (int) transform.Position.X;
            Camera.Position.Y = 290;

            Debug.WriteLine(isGrounded);
            isGrounded = false;
        }

        private void handleCollision(Entity collider)
        {
            isGrounded = true;
        }
    }
}
