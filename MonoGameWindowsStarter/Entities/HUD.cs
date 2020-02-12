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
        public TextEntity Score { get; set; }

        private Scene scene;
        public override void Initialize()
        {
            scene = SceneManager.GetCurrentScene();

            Score = new TextEntity();

            TextDraw scoreText = Score.GetComponent<TextDraw>();
            Transform scorePos = Score.GetComponent<Transform>();

            Score.GetComponent<TextDraw>().Text = "10000";
            scoreText.Color = Color.Black;
            scorePos.Position = new Vector(scene.GameManager.WindowWidth - 70, 5);
            scorePos.Scale = new Vector(1, 1);

            scene.AddEntity(Score);
        }

        public override void Update(GameTime gameTime)
        {

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
        public override void Initialize()
        {
            AddComponent<Sprite>();
            AddComponent<Transform>();
        }

        public override void Update(GameTime gameTime) { }
    }
}
