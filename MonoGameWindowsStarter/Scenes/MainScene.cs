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

        private float enemySpawnTime = 2f;
        private int spawnBuffer = 20;
        private int wallWidth = 50;

        private float currentEnemySpawnTime;
        private Random r = new Random();
        private GameManager game;

        public MainScene(GameManager game)
        {
            this.game = game;

            game.IsMouseVisible = true;          

            Name = "testScene";

            currentEnemySpawnTime = enemySpawnTime;

            Entities.Add(new Player());    

            // Map (temporary will use map editor later)
            Entities.Add(new StaticObject(game.WindowWidth - wallWidth, 0, wallWidth, game.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, 0, wallWidth, game.WindowHeight, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, game.WindowHeight - wallWidth, game.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
            Entities.Add(new StaticObject(0, 0, game.WindowWidth, wallWidth, "PixelWhite", new Rectangle(0, 0, 1, 1)));
        }

        public override void Update(GameTime gameTime)
        {
            currentEnemySpawnTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (currentEnemySpawnTime > enemySpawnTime)
            {
                AddEntity(new Enemy(r.Next(wallWidth + spawnBuffer, game.WindowWidth - wallWidth - spawnBuffer),
                                       r.Next(wallWidth + spawnBuffer, game.WindowHeight - wallWidth - spawnBuffer),
                                       20, 20));
                currentEnemySpawnTime = 0;
            }
        }
    }
}
