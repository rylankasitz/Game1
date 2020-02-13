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
using MonoGameWindowsStarter.Systems.Global;
using static MonoGameWindowsStarter.Componets.RenderComponents;

namespace MonoGameWindowsStarter.Entities
{
    class Player : Entity
    {
        public int Speed = 5;
        public int Health = 5;
        public float FireRate = .25f;
        public float BulletSpeed = 25;
        public float Score = 0;
        public bool GameOver = false;

        private Sprite sprite;
        private Transform transform;
        private BoxCollision boxCollision;
        private Physics physics;
        private Animation animations;

        private float timeSinceShot;

        public override void Initialize()
        {
            Name = "Player";

            sprite = AddComponent<Sprite>();
            transform = AddComponent<Transform>();
            boxCollision = AddComponent<BoxCollision>();
            physics = AddComponent<Physics>();
            animations = AddComponent<Animation>();

            animations.AnimationFile = "Player";
            animations.CurrentAnimation = "WalkUp";

            sprite.ContentName = "CharacterSpriteSheet";
            sprite.SpriteLocation = new Rectangle(0, 0, 30, 52);

            transform.Position = new Vector(100, 100);
            transform.Scale = new Vector(37, 54);

            boxCollision.HandleCollision = handleCollision;

            timeSinceShot = FireRate;       
        }

        public override void Update(GameTime gameTime)
        {
            move();

            timeSinceShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputManager.LeftMousePressed() && FireRate < timeSinceShot)
            {
                fire();
                timeSinceShot = 0;
            }
        }

        private void handleCollision(Entity entity)
        {
            if (entity.Name == "Enemy" && Health > 0)
            {
                Health--;

                AudioManager.Play("Hit 2");
                SceneManager.GetCurrentScene().RemoveEntity(entity);

                HUD hud = SceneManager.GetCurrentScene().GetEntity<HUD>("HUD");
                hud.Health[Health].GetComponent<Sprite>().Color = Color.Red;

                if (Health <= 0)
                {
                    hud.GameOver.GetComponent<TextDraw>().Text = "GAME OVER\nPress R TO RESTART";
                    GameOver = true;
                }
            }
        }

        private void move()
        {
            physics.Velocity.X = 0;
            physics.Velocity.Y = 0;

            if (InputManager.KeyPressed(Keys.W))
            {
                physics.Velocity.Y = -Speed;
                animations.CurrentAnimation = "WalkUp";
            }
            if (InputManager.KeyPressed(Keys.S))
            {
                physics.Velocity.Y = Speed;
                animations.CurrentAnimation = "WalkDown";
            }
            if (InputManager.KeyPressed(Keys.A))
            {
                physics.Velocity.X = -Speed;
                animations.CurrentAnimation = "WalkLeft";
            }
            if (InputManager.KeyPressed(Keys.D))
            {
                physics.Velocity.X = Speed;
                animations.CurrentAnimation = "WalkRight";
            }

            // Change
            if (physics.Velocity.X != 0 && physics.Velocity.Y != 0)
            {
                physics.Velocity = new Vector(physics.Velocity.X / Speed, physics.Velocity.Y / Speed) * Speed;
            }

            if (physics.Velocity.X == 0 && physics.Velocity.Y == 0)
            {
                animations.CurrentAnimation = (animations.CurrentAnimation.Replace("Walk", "Idle"));
            }
        }

        private void fire()
        {      
            Bullet bullet = new Bullet();
            SceneManager.GetCurrentScene().AddEntity(bullet);

            Vector2 mouseVector = -Vector2.Normalize(transform.Position - InputManager.GetMousePosition());
            
            float rotation = (float) Math.Atan2(mouseVector.Y, mouseVector.X);

            Transform bulletPos = bullet.GetComponent<Transform>();
            Physics bulletPhys = bullet.GetComponent<Physics>();
            Vector2 velocity = mouseVector * BulletSpeed;

            bulletPos.Position = transform.Position + transform.Scale/2;

            bulletPos.Rotation = rotation;

            bulletPhys.Velocity.X = velocity.X;
            bulletPhys.Velocity.Y = velocity.Y;

            AudioManager.Play("Shoot 3");
        }
    }
}
