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
        public Dictionary<string, Component> Components { get; } = new Dictionary<string, Component>();

        public T AddComponent<T>() where T : Component, new()
        {
            T c = new T();
            Components[typeof(T).ToString()] = c;
            return c;
        }

        public T GetComponent<T>() where T : Component, new()
        {
            return (T) Components[typeof(T).ToString()];
        }

        public bool HasComponent<T>() where T : Component, new()
        {
            return Components.Keys.Contains(typeof(T).ToString());
        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
    }
}
