using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 0, Y: 0, Width: 15, Height: 8)]
    [Sprite(ContentName: "revolver2", Layer: 0.05f, SpriteX: 0, SpriteY: 0, SpriteWidth: 266, SpriteHeight: 168)]
    [ParticleSystem(Texture: "Sprites/Pixel", ParticleCount: 25, SpawnPerFrame: 1, Time: .15f,
                    EmmiterX: 1f, EmmiterY: .1f, DirectionX: 1, DirectionY: .33f, Range: 30, Life: .1f,
                    Velocity: 300f, Scale: 1.5f)]
    public class Gun : Entity
    {
        public float BulletSpeed = 10f;
        public float FireRate = .5f;

        private Transform transform;
        private ParticleSystem particleSystem;
        private double elapsedTime;

        public override void Initialize()
        {
            transform = GetComponent<Transform>();
            particleSystem = GetComponent<ParticleSystem>();
            particleSystem.Color = Color.Gray;
            elapsedTime = 0;
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (InputManager.LeftMousePressed() && elapsedTime > FireRate)
            {
                fire();
                elapsedTime = 0;
            }
        }

        private void fire()
        {
            Bullet bullet = SceneManager.GetCurrentScene().CreateEntity<Bullet>();
            bullet.SetPosition(transform.Position.X + transform.Scale.X, transform.Position.Y);
            bullet.SetVelocity((float)Math.Cos(transform.Rotation) * BulletSpeed, (float)Math.Sin(transform.Rotation) * BulletSpeed);
            particleSystem.Play();
        }

        public void SetPos(Vector vector)
        {
            transform.Position = new Vector(vector.X, vector.Y);
        }
    }

    [Transform(X: 0, Y: 0, Width: 5, Height: 2)]
    [Sprite(ContentName: "Pixel", Layer: 0f)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 0, Width: 1, Height: 1, TriggerOnly: true)]
    public class Bullet : Entity
    {
        Physics physics;
        Transform transform;
        BoxCollision collision;
        Sprite sprite;

        public override void Initialize()
        {
            physics = GetComponent<Physics>();
            transform = GetComponent<Transform>();
            collision = GetComponent<BoxCollision>();
            sprite = GetComponent<Sprite>();

            sprite.Color = Color.Gray;
            collision.HandleCollision = handleCollision;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        private void handleCollision(Entity collider, string side)
        {
            if (collider.Name == "Enemy")
            {
                Enemy enemy = (Enemy)collider;
                enemy.Explode();
            }
            GetComponent<BoxCollision>().Enabled = false;
            GetComponent<Sprite>().Enabled = false;
            physics.Velocity.X = 0;
            //SceneManager.GetCurrentScene().RemoveEntity(this);
        }

        public void SetVelocity(float x, float y)
        {
            physics.Velocity = new Vector(x, y);
        }

        public void SetPosition(float x, float y)
        {
            transform.Position = new Vector(x, y);
        }
    }
}
