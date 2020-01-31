using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Entities
{
    public class StaticObject : Entity
    {
        public Sprite Sprite { get; set; } = new Sprite();
        public Transform Transform { get; set; } = new Transform();
        public BoxCollision BoxCollision { get; set; } = new BoxCollision();

        public override void Initialize()
        {
            Sprite = AddComponent<Sprite>();
            Transform = AddComponent<Transform>();
            BoxCollision = AddComponent<BoxCollision>();

            Transform.X = 300;
            Transform.Y = 300;
            Transform.Width = 100;
            Transform.Height = 200;

            BoxCollision.Static = true;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
