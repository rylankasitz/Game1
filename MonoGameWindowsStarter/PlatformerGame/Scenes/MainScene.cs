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
    public class MainScene : Scene
    {
        public MainScene() { Name = "testScene";  }
        public override void Initialize()
        {
            CreateEntity<Player>();

            MapManager.LoadMap(GameManager.Content, "level1", this);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
