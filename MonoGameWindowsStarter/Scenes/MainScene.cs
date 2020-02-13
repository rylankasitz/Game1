using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Entities;
using MonoGameWindowsStarter.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Scenes
{
    public class MainScene : Scene
    {
        private float enemySpawnTime = 1.4f;
        private float increaseRate = .06f;
        private int spawnBuffer = 40;
        private int wallWidth = 50;

        private Player player;
        private HUD hud;

        private float currentEnemySpawnTime;
        private Random r = new Random();

        public MainScene() { Name = "testScene"; }

        public override void Initialize()
        {
            GameManager.IsMouseVisible = true;          

            currentEnemySpawnTime = enemySpawnTime;

            AddEntity(player = new Player());
            AddEntity(hud = new HUD());

            // Map (temporary will use map editor later)
            AddEntity(new StaticObject(GameManager.WindowWidth - wallWidth, 0, wallWidth, GameManager.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, 0, wallWidth, GameManager.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, GameManager.WindowHeight - wallWidth, GameManager.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            AddEntity(new StaticObject(0, 0, GameManager.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
        }

        public override void Update(GameTime gameTime)
        {
            if (!player.GameOver)
            {
                currentEnemySpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentEnemySpawnTime > enemySpawnTime)
                {
                    AddEntity(new Enemy(r.Next(wallWidth + spawnBuffer, GameManager.WindowWidth - wallWidth - spawnBuffer),
                                        r.Next(0, 2) * GameManager.WindowHeight,
                                        r.Next(1, 2)));
                    currentEnemySpawnTime = 0;

                    Console.WriteLine(enemySpawnTime);
                }

                if (enemySpawnTime > .25f)
                    enemySpawnTime -= (float)gameTime.ElapsedGameTime.TotalSeconds * increaseRate;
            }
            else if (InputManager.KeyPressed(Keys.R))
            {
                player.GameOver = false;
                player.Health = 5;
                player.Score = 0;
                hud.Reset();
                enemySpawnTime = 1.4f;
            }
        }
    }
}
