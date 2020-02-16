using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.Componets;
using MonoGameWindowsStarter.ECSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonoGameWindowsStarter.Componets.RenderComponents;

namespace MonoGameWindowsStarter.Entities
{
    public class HUD : Entity
    {
        public TextEntity Score;
        public List<SpriteEntity> Health;
        public TextEntity GameOver;

        private Scene scene;
        public override void Initialize()
        {
            Name = "HUD";

            scene = SceneManager.GetCurrentScene();

            CreateScore();
            CreateHealth(scene.GetEntity<Player>("Player").Health);
            CreateGameOver();

            Score = scene.CreateEntity<TextEntity>();
        }

        public override void Update(GameTime gameTime) { }

        public void Reset()
        {
            Score.GetComponent<TextDraw>().Text = "0";
            foreach (SpriteEntity spriteEntity in Health)
            {
                spriteEntity.GetComponent<Sprite>().Color = Color.Green;
            }
            GameOver.GetComponent<TextDraw>().Text = "";
        }

        private void CreateScore()
        {
            Score = new TextEntity();

            TextDraw scoreText = Score.GetComponent<TextDraw>();
            Transform scorePos = Score.GetComponent<Transform>();

            scoreText.Text = "0";
            scoreText.Color = Color.Black;
            scorePos.Position = new Vector(scene.GameManager.WindowWidth - 70, 5);
            scorePos.Scale = new Vector(1, 1);
        }

        private void CreateHealth(int bars)
        {
            Health = new List<SpriteEntity>();

            for (int i = 0; i < bars; i++)
            {
                SpriteEntity bar = scene.CreateEntity<SpriteEntity>();
                Sprite sprite = bar.GetComponent<Sprite>();
                Transform trans = bar.GetComponent<Transform>();

                sprite.ContentName = "PixelWhite";
                sprite.Color = Color.Green;

                trans.Position = new Vector(10 + 20 * i, 5);
                trans.Scale = new Vector(10, 30);
                
                Health.Add(bar);
            }
        }
    
        private void CreateGameOver()
        {
            GameOver = scene.CreateEntity<TextEntity>();

            TextDraw text = GameOver.GetComponent<TextDraw>();
            Transform trans = GameOver.GetComponent<Transform>();

            text.Text = "";
            text.Color = Color.Black;

            trans.Position = new Vector(scene.GameManager.WindowWidth / 2 - 50 , scene.GameManager.WindowHeight / 2 - 14);
            trans.Scale = new Vector(1, 1);
        }
    }

    public class TextEntity : Entity
    {
        public TextEntity()
        {
            AddComponent<TextDraw>();
            AddComponent<Transform>();
        }

        public override void Initialize() { }
        public override void Update(GameTime gameTime) { }
    }

    public class SpriteEntity : Entity
    {
        public SpriteEntity()
        {
            AddComponent<Sprite>();
            AddComponent<Transform>();
        }

        public override void Initialize() { }

        public override void Update(GameTime gameTime) { }
    }
}
