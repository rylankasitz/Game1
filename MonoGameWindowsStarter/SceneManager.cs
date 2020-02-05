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
    public static class SceneManager
    {
        public static List<ECSCore.System> systems { get; set; }

        private static Scene currentScene;
        private static List<Scene> scenes = new List<Scene>();  

        public static void Initialize(GameManager game)
        {
            // Add all scenes here
            scenes.Add(new MainScene(game));
        }

        public static Scene GetCurrentScene()
        {
            return currentScene;
        }

        public static void SetScene(string name)
        {
            foreach(Scene scene in scenes)
            {
                if (scene.Name == name)
                    currentScene = scene;
            }
        }

        public static void LoadScene()
        {
            currentScene.LoadScene(systems);
        }

        public static void UpdateScene(GameTime gameTime)
        {
            currentScene.UpdateScene(gameTime);
            currentScene.Update(gameTime);
        }
    }
}
