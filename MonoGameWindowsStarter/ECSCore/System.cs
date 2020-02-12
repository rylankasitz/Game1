using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public delegate bool ComponentsFunc(Entity entity);

    public abstract class System
    {
        public List<Entity> Entities { get; set; } = new List<Entity>();
        public bool Loaded = false;

        private ComponentsFunc Components;

        public void LoadEntities(Matcher matcher)
        {
            Components = SetSystemRequirments;
            Entities = matcher.Match(Components);
            Loaded = true;
        }

        public void AddEntity(Entity entity)
        {
            if (Components(entity))
            {
                InitializeEntity(entity);
                Entities.Add(entity);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public abstract void InitializeEntity(Entity entity);
        public abstract void Initialize();
        public abstract bool SetSystemRequirments(Entity entity);
    }
}
