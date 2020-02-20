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
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.PlatformerGame.Scenes;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 260, Y: 340, Width: 20, Height: 20)]
    [Sprite(ContentName: "spritesheet", Layer: 0f)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 0, Width: 1, Height: 1)]
    [Animation(CurrentAnimation: "PlayerWalk")]
    public class Player : Entity
    {
        public float Speed = 2.5f;
        public float Gravity = .15f;
        public float JumpForce = 4f;

        private Physics physics;
        private Transform transform;
        private BoxCollision boxCollision;
        private Animation animation;
        private Sprite sprite;

        private bool isGrounded;

        public override void Initialize()
        {
            physics = GetComponent<Physics>();
            transform = GetComponent<Transform>();
            boxCollision = GetComponent<BoxCollision>();
            animation = GetComponent<Animation>();
            sprite = GetComponent<Sprite>();

            boxCollision.HandleCollision = handleCollision;
            isGrounded = false;

            Camera.Zoom = 3;
        }

        public override void Update(GameTime gameTime)
        {
            physics.Velocity.X = 0;
            animation.CurrentAnimation = "PlayerIdle";

            if (InputManager.KeyPressed(Keys.A))
            {
                physics.Velocity.X = -Speed;
                sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
                if (isGrounded)
                    animation.CurrentAnimation = "PlayerWalk";
            }
            if (InputManager.KeyPressed(Keys.D))
            {
                physics.Velocity.X = Speed;
                sprite.SpriteEffects = SpriteEffects.None;
                if (isGrounded)
                    animation.CurrentAnimation = "PlayerWalk";
            }
            if (InputManager.KeyDown(Keys.Space) && isGrounded)
            {
                physics.Velocity.Y = -JumpForce;
            }
            
            if (!isGrounded)
                physics.Velocity.Y += Gravity;

            Camera.Position.X = (int) transform.Position.X;
            Camera.Position.Y = 300;

            isGrounded = false;
        }

        private void handleCollision(Entity collider, string side)
        {
            if (side == "Top")
                isGrounded = true;

            if (collider.Name == "Lava" || collider.Name == "Water" || collider.Name == "Enemy")
            {
                respawn();
            }

            if (collider.Name == "Flag")
            {
                Win();
            }
        }

        private void Win()
        {
            ((MainScene)SceneManager.GetCurrentScene()).WinScreen.Show("Level Complete");
        }

        private void respawn()
        {
            transform.Position = new Vector(260, 340);
        }
    }
}