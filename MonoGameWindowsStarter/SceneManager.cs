using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.ECSCore;
using MonoGameWindowsStarter.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public class SceneManager
    {
        private List<Scene> scenes = new List<Scene>();
        private List<ECSCore.System> systems;

        private Scene currentScene;

        public SceneManager(List<ECSCore.System> systems)
        {
            this.systems = systems;

            // Add all scenes here
            scenes.Add(new MainScene());
        }

        public void SetScene(string name)
        {
            foreach(Scene scene in scenes)
            {
                if (scene.Name == name)
                    currentScene = scene;
            }
        }

        public void LoadScene()
        {
            currentScene.LoadScene(systems);
        }

        public void UpdateScene(GameTime gameTime)
        {
            currentScene.UpdateScene(gameTime);
        }
    }
}
