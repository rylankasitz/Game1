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
    [Transform(X: 1060, Y: 200, Width: 1, Height: 1)]
    [TextDraw(Text: "")]
    public class WinScreen : Entity
    {
        private TextDraw text;

        public override void Initialize()
        {
            text = GetComponent<TextDraw>();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void Show(string message)
        {
            text.Text = message;
        }
    }
}
