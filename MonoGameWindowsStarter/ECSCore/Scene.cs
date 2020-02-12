using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public abstract class Scene
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();

        public string Name { get; set; } = "Unamed Scene";

        private Matcher matcher;
        private List<System> systems;

        public abstract void Update(GameTime gameTime);

        public void AddEntity(Entity entity)
        {
            entity.Initialize();

            foreach (System system in systems)
            {
                system.AddEntity(entity);
            }

            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            foreach (System system in systems)
            {
                system.RemoveEntity(entity);
            }

            Entities.Remove(entity);
        }

        public void LoadScene(List<System> systems)
        {
            this.systems = systems;

            matcher = new Matcher(Entities);

            foreach (Entity entity in Entities)
            {
                entity.Initialize();
            }

            foreach (System system in systems)
            {
                system.LoadEntities(matcher);
                system.Initialize();
            }
        }

        public void UpdateScene(GameTime gameTime)
        {
            for (int i = 0;  i < Entities.Count; i++)
            {
                Entities[i].Update(gameTime);
            }
        }
    }
}
