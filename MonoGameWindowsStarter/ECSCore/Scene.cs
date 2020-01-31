using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public class Scene
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public string Name { get; set; } = "Unamed Scene";

        public void LoadScene(List<System> systems)
        {
            Matcher matcher = new Matcher(Entities);

            // Initialize Entities
            foreach (Entity entity in Entities)
            {
                entity.Initialize();
            }

            // Initialize Systems
            foreach (System system in systems)
            {
                system.LoadEntities(matcher);
                system.Initialize();
            }
        }

        public void UpdateScene(GameTime gameTime)
        {
            foreach (Entity entity in Entities)
            {
                entity.Update(gameTime);
            }
        }
    }
}
