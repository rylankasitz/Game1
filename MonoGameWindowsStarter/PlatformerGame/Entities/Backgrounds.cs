using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlatformerGame.Entities
{
    [Transform(X: 0, Y: 0, Width: 384, Height: 500)]
    [Sprite(ContentName: "sky")]
    [Parallax(Layer: 0, Speed: 0)]
    public class Background1 : Entity
    {
        public override void Initialize() { }

        public override void Update(GameTime gameTime) { }
    }

    [Transform(X: 0, Y: 250, Width: 384, Height: 216)]
    [Sprite(ContentName: "clouds_bg")]
    [Parallax(Layer: 1, Speed: 10)]
    public class Background2 : Entity
    {
        public override void Initialize() { }

        public override void Update(GameTime gameTime) { }
    }

    [Transform(X: 0, Y: 250, Width: 384, Height: 216)]
    [Sprite(ContentName: "glacial_mountains")]
    [Parallax(Layer: 2, Speed: 0)]
    public class Background3 : Entity
    {
        public override void Initialize() { }

        public override void Update(GameTime gameTime) { }
    }

    [Transform(X: 0, Y: 237, Width: 384, Height: 216)]
    [Sprite(ContentName: "clouds_mg_1")]
    [Parallax(Layer: 3, Speed: 40)]
    public class Background4 : Entity
    {
        public override void Initialize() { }

        public override void Update(GameTime gameTime) { }
    }
}
