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
        public MainScene()
        {
            Name = "testScene";

            Entities.Add(new Player());

            for (int i = 0; i < 100; i++)
                Entities.Add(new StaticObject());
        }
    }
}
