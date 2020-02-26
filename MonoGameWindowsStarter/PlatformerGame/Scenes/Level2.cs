using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.PlatformerGame.Entities;
using MonoGameWindowsStarter.Systems.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlatformerGame.Scenes
{
    public class Level2 : Scene
    {
        public WinScreen WinScreen;

        public Level2() { Name = "Level2"; }
        public override void Initialize()
        {
            MapManager.LoadMap(GameManager.Content, "level2", this);

            CreateEntity<Player>();
            WinScreen = CreateEntity<WinScreen>();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
