using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.ECSCore
{
    public abstract class Entity
    {
        public Dictionary<string, Component> Components { set;  get; } = new Dictionary<string, Component>();
        public string Name { get; set; } = "Unamed";

        public T AddComponent<T>() where T : Component, new()
        {
            T c = new T();
            Components[typeof(T).Name] = c;
            return c;
        }

        public T GetComponent<T>() where T : Component, new()
        {
            return (T) Components[typeof(T).Name];
        }

        public bool HasComponent<T>() where T : Component, new()
        {
            return Components.Keys.Contains(typeof(T).Name);
        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
    }
}
