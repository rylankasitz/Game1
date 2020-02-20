using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
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
        public WinScreen WinScreen;

        public MainScene() { Name = "testScene";  }
        public override void Initialize()
        {
            MapManager.LoadMap(GameManager.Content, "level1", this);

            CreateEntity<Player>();
            WinScreen = CreateEntity<WinScreen>();

            addEnemy(400, 300, 0);
            addEnemy(600, 300, .5);
            addEnemy(800, 300, .2);
            addEnemy(1000, 300, .7);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        private void addEnemy(float x, float y, double startOffset)
        {
            Enemy enemy = CreateEntity<Enemy>();
            enemy.GetComponent<Transform>().Position = new Vector(x, y);
            enemy.ElapsedTime = startOffset;
        }
    }
}
