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
        private float enemySpawnTime = 1.5f;
        private int spawnBuffer = 40;
        private int wallWidth = 50;

        private float currentEnemySpawnTime;
        private Random r = new Random();

        public MainScene() { Name = "testScene"; }

        public override void Initialize()
        {
            GameManager.IsMouseVisible = true;          

            currentEnemySpawnTime = enemySpawnTime;

            AddEntity(new Player());
            AddEntity(new HUD());

            // Map (temporary will use map editor later)
            AddEntity(new StaticObject(GameManager.WindowWidth - wallWidth, 0, wallWidth, GameManager.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, 0, wallWidth, GameManager.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, GameManager.WindowHeight - wallWidth, GameManager.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, 0, GameManager.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(GameManager.WindowWidth / 2 - 50, GameManager.WindowHeight / 2 - 50, 100, 100, "PixelWhite", new Rectangle(0, 0, 1, 1)));
        }

        public override void Update(GameTime gameTime)
        {
            currentEnemySpawnTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (currentEnemySpawnTime > enemySpawnTime)
            {
                AddEntity(new Enemy(r.Next(wallWidth + spawnBuffer, GameManager.WindowWidth - wallWidth - spawnBuffer),
                                       r.Next(wallWidth + spawnBuffer, GameManager.WindowHeight - wallWidth - spawnBuffer),
                                       r.Next(1, 4)));
                currentEnemySpawnTime = 0;
            }
        }
    }
}
