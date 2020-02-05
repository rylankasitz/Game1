using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Scenes
{
    public class MainScene : Scene
    {
        public MainScene(GameManager game)
        {
            game.IsMouseVisible = true;          

            Name = "testScene";

            Entities.Add(new Player());

            // Map (temporary will use map editor later)
            int wallWidth = 50;
            Entities.Add(new StaticObject(game.WindowWidth - wallWidth, 0, wallWidth, game.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, 0, wallWidth, game.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, game.WindowHeight - wallWidth, game.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, 0, game.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
        }
    }
}
