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
    [Animation(CurrentAnimation: "EnemyFly")]
    public class Enemy : Entity
    {
        public static float Speed = 1.5f;
        public static float SwitchTime = 1.5f;
        public double ElapsedTime = 0;

        private Physics physics;
        private Sprite sprite;

        public override void Initialize()
        {
            Name = "Enemy";

            physics = GetComponent<Physics>();
            sprite = GetComponent<Sprite>();

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

        private void switchDirection()
        {
            physics.Velocity.X *= -1;
            sprite.SpriteEffects = SpriteEffects.FlipHorizontally;

            if (physics.Velocity.X < 0)
                sprite.SpriteEffects = SpriteEffects.None;
        }
    }
}
