using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 260, Y: 350, Width: 20, Height: 20)]
    [Sprite(ContentName: "spritesheet", Layer: .05f)]
    [Physics(VelocityX: 0, VelocityY: 0)]
    [BoxCollision(X: 0, Y: 6, Width: 1, Height: .5f, TriggerOnly: true)]
    [ParticleSystem(Texture: "Sprites/Pixel", ParticleCount: 100, SpawnPerFrame: 1, Time: .5f,
                    EmmiterX: .5f, EmmiterY: 1, DirectionX: -1, DirectionY: 0, Range: 360, Scale: 2f)]
    [Animation(CurrentAnimation: "EnemyFly")]
    public class Enemy : Entity
    {
        public static float Speed = 1.5f;
        public static float SwitchTime = 1.5f;
        public double ElapsedTime = 0;

        private Physics physics;
        private Sprite sprite;
        private ParticleSystem particleSystem;

        public override void Initialize()
        {
            Name = "Enemy";

            physics = GetComponent<Physics>();
            sprite = GetComponent<Sprite>();
            particleSystem = GetComponent<ParticleSystem>();

            particleSystem.Color = Color.Black;
            physics.Velocity.X = -Speed;
        }

        public override void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (ElapsedTime > SwitchTime)
            {
                switchDirection();
                ElapsedTime = 0;
            }
        }

        public void Explode()
        {
            particleSystem.Play();
            physics.Velocity.X = 0;
            sprite.Enabled = false;
            GetComponent<BoxCollision>().Enabled = false;
        }

        private void switchDirection()
        {
            physics.Velocity.X *= -1;
            sprite.SpriteEffects = SpriteEffects.FlipHorizontally;

            if (physics.Velocity.X < 0)
                sprite.SpriteEffects = SpriteEffects.None;
        }
    }
}
